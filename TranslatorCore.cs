// #define DEBUG_OUTPUT_DIALOG

using System;
using System.Web;
using System.Collections.Generic;
using WikiFunctions;
using WikiFunctions.Plugin;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WikiFunctions.AWBSettings;
using WikiFunctions.Parse;
using WikiFunctions.API;
using WikiFunctions.Lists;
using WikiFunctions.Lists.Providers;

/*
 * TODO:
 *  - ignore blue links and translate only red links (as an option)
 *  - follow redirects on source wiki
  */

namespace AutoWikiBrowser.Plugins.Translator
{
    public class TranslatorAWBPlugin : IAWBPlugin
    {

        internal IAutoWikiBrowser AWBForm;
        internal TabPage TranslatorPluginTabPage = new TabPage("Translator");

        private readonly ToolStripMenuItem pluginenabledMenuItem = new ToolStripMenuItem("Translator plugin");
        private readonly ToolStripMenuItem pluginconfigMenuItem = new ToolStripMenuItem("Configuration");

        internal static string SourceLanguage = "en";
        internal static string username;
        internal static string password;
        private static bool Enabled = false;

#if DEBUG_OUTPUT_DIALOG
        internal static DebugOutput dlg;
#endif

        #region IAWBPlugin Members

        public void Initialise(IAutoWikiBrowser sender)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");

            AWBForm = sender;

            // Menuitem should be checked when plugin is active and unchecked when not, and default to not!
            pluginenabledMenuItem.CheckOnClick = true;

            pluginconfigMenuItem.Click += ShowSettings;
            pluginenabledMenuItem.CheckedChanged += PluginEnabledCheckedChange;
            //aboutMenuItem.Click += AboutMenuItemClicked;
            pluginenabledMenuItem.DropDownItems.Add(pluginconfigMenuItem);

            sender.PluginsToolStripMenuItem.DropDownItems.Add(pluginenabledMenuItem);
            //sender.HelpToolStripMenuItem.DropDownItems.Add(aboutMenuItem);

            Reset();
        }

        void ShowSettings(object sender, EventArgs e)
        {
            new TranslatorSettingsForm().ShowDialog(AWBForm.Form);
        }

        void PluginEnabledCheckedChange(object sender, EventArgs e)
        {
            Enabled = pluginenabledMenuItem.Checked;
        }

        public string Name
        {
            get { return "Translator"; }
        }

        public string WikiName
        {
            get { return "Translator Plugin"; }
        }

        public void disablePlugin()
        {
            Enabled = false;
            pluginenabledMenuItem.Checked = false;
        }

        public void enablePlugin()
        {
            Enabled = true;
            pluginenabledMenuItem.Checked = true;
        }

        public string ProcessArticle(IAutoWikiBrowser sender, IProcessArticleEventArgs eventargs)
        {

            if (!Enabled) return eventargs.ArticleText;

            if (username == "" || password == "")
            {
                MessageBox.Show("Source wiki username and/or password not set. Turning off the plugin.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                disablePlugin();
                return eventargs.ArticleText;
            }
            
            String linkTitle;
            String linkBody;
            String sourceText = eventargs.ArticleText;
            String url = String.Format("http://{0}.wikipedia.org/w/", SourceLanguage);
            ApiEdit sourceWiki = new ApiEdit(url);

            try
            {
                sourceWiki.Login(username, password);
            }
            catch (LoginException e)
            {
                MessageBox.Show("Source wiki username and/or password incorrect. Turning off the plugin.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                disablePlugin();
                return eventargs.ArticleText;
            }

#if DEBUG_OUTPUT_DIALOG
            DebugOutput dlg = new DebugOutput();
            dlg.Show(sender.Form);
#endif

            String langCode = Variables.LangCode.ToString();
            Regex r = new Regex(String.Format(@"\[\[{0}:([^\]]+)\]\]", langCode));
            Match m;
            String replaceReg;
            String replaceTo;

            string[] arr = { eventargs.ArticleTitle };
            LinksOnPageListProvider linksProvider = new LinksOnPageListProvider();
            List<Article> links = linksProvider.MakeList(arr);

            // Message
            AWBForm.StartProgressBar();
            AWBForm.StatusLabelText = String.Format("Translating {0} links...", links.Count);

#if DEBUG_OUTPUT_DIALOG
            dlg.StartSection(string.Format("Article {0}", eventargs.ArticleTitle));
#endif

            foreach (Article link in links)
            {
#if DEBUG_OUTPUT_DIALOG
                dlg.StartSection(string.Format("Link: {0}", link.Name));
#endif
                linkTitle = link.Name;
                linkBody = sourceWiki.Open(linkTitle);
                m = r.Match(linkBody);
                if (m.Success)
                {
#if DEBUG_OUTPUT_DIALOG
                    dlg.AddSection("From: ", linkTitle);
                    dlg.AddSection("To: ", m.Groups[1].ToString());
#endif
                    replaceReg = String.Format(@"\[\[{0}\]\]", linkTitle);
                    replaceTo = String.Format("[[{0}|{1}]]", m.Groups[1], linkTitle);
                    sourceText = Regex.Replace(sourceText, replaceReg, replaceTo, RegexOptions.IgnoreCase);

                    replaceReg = String.Format(@"\[\[{0}\|([^\]]+)\]\]", linkTitle);
                    replaceTo = String.Format("[[{0}|$1]]", m.Groups[1], linkTitle);
                    sourceText = Regex.Replace(sourceText, replaceReg, replaceTo, RegexOptions.IgnoreCase);
                }
#if DEBUG_OUTPUT_DIALOG
                else {
                    dlg.AddSection("No match", "");
                }
#endif
#if DEBUG_OUTPUT_DIALOG
                dlg.EndSection();
#endif
                Application.DoEvents();
            }
#if DEBUG_OUTPUT_DIALOG
            dlg.EndSection();
#endif
            AWBForm.StopProgressBar();
            AWBForm.StatusLabelText = "";
            eventargs.EditSummary = "Automatic translation";
            return sourceText;
        }

        public void LoadSettings(object[] prefs)
        {
            Reset();
            if (prefs == null) return;

            foreach (object o in prefs)
            {
                PrefsKeyPair p = o as PrefsKeyPair;
                if (p == null) continue;

                switch (p.Name.ToLower())
                {
                    case "enabled":
                        Enabled = (bool)p.Setting;
                        if (Enabled) enablePlugin();
                        break;
                    case "username":
                        username = (string)p.Setting;
                        break;
                    case "password":
                        password = (string)p.Setting;
                        break;
                    case "sourcelanguage":
                        SourceLanguage = (string)p.Setting;
                        break;
                }
            }
        }

        public object[] SaveSettings()
        {
            return new object[]
            {
                new PrefsKeyPair("SourceLanguage", SourceLanguage),
                new PrefsKeyPair("Enabled", Enabled),
                new PrefsKeyPair("Username", username),
                new PrefsKeyPair("Password", password)
            };
        }

        public void Reset()
        {
            SourceLanguage = "en";
            Enabled = false;
            username = "";
            password = "";
        }

        public void Nudge(out bool cancel)
        {
            cancel = false;
        }

        public void Nudged(int nudges)
        {   
        }

        #endregion
    }
}
