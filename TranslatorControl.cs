using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WikiFunctions;
using WikiFunctions.Plugin;
using WikiFunctions.Parse;

namespace AutoWikiBrowser.Plugins.Translator
{
    public partial class TranslatorControl : UserControl
    {
        public TranslatorControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            cboLanguages.DataSource = SiteMatrix.WikipediaLanguages; 
            base.OnLoad(e);
        }

        internal String Article
        {
            get { return txtArticle.Text; }
        }

        internal String langCode
        {
            get { return (String) cboLanguages.SelectedItem; }
        }
    }
}
