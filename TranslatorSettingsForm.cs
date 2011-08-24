using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WikiFunctions.Parse;

namespace AutoWikiBrowser.Plugins.Translator
{
    public partial class TranslatorSettingsForm : Form
    {
        public TranslatorSettingsForm()
        {
            InitializeComponent();
            Icon = WikiFunctions.Properties.Resources.AWBIcon;
        }

        private void TranslatorSettingsForm_Load(object sender, EventArgs e)
        {
            // TODO: setting previously selected source language
            cboLanguages.DataSource = SiteMatrix.WikipediaLanguages;
            cboLanguages.SelectedIndex = cboLanguages.FindStringExact(TranslatorAWBPlugin.SourceLanguage);
            txtUsername.Text = TranslatorAWBPlugin.username;
            txtPassword.Text = TranslatorAWBPlugin.password;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TranslatorAWBPlugin.SourceLanguage = (String)cboLanguages.SelectedItem;
            TranslatorAWBPlugin.username = txtUsername.Text;
            TranslatorAWBPlugin.password = txtPassword.Text;
        }
    }
}
