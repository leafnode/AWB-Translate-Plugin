namespace AutoWikiBrowser.Plugins.Translator
{
    partial class TranslatorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtArticle = new System.Windows.Forms.TextBox();
            this.cboLanguages = new System.Windows.Forms.ComboBox();
            this.lblArticle = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtArticle
            // 
            this.txtArticle.Location = new System.Drawing.Point(128, 21);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.Size = new System.Drawing.Size(121, 20);
            this.txtArticle.TabIndex = 0;
            // 
            // cboLanguages
            // 
            this.cboLanguages.FormattingEnabled = true;
            this.cboLanguages.Location = new System.Drawing.Point(128, 59);
            this.cboLanguages.Name = "cboLanguages";
            this.cboLanguages.Size = new System.Drawing.Size(121, 21);
            this.cboLanguages.TabIndex = 1;
            // 
            // lblArticle
            // 
            this.lblArticle.AutoSize = true;
            this.lblArticle.Location = new System.Drawing.Point(17, 24);
            this.lblArticle.Name = "lblArticle";
            this.lblArticle.Size = new System.Drawing.Size(36, 13);
            this.lblArticle.TabIndex = 2;
            this.lblArticle.Text = "Article";
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(17, 62);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(88, 13);
            this.lblLanguage.TabIndex = 3;
            this.lblLanguage.Text = "Source language";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(3, 323);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // TranslatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.lblArticle);
            this.Controls.Add(this.cboLanguages);
            this.Controls.Add(this.txtArticle);
            this.MaximumSize = new System.Drawing.Size(276, 349);
            this.MinimumSize = new System.Drawing.Size(276, 349);
            this.Name = "TranslatorControl";
            this.Size = new System.Drawing.Size(276, 349);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.ComboBox cboLanguages;
        private System.Windows.Forms.Label lblArticle;
        private System.Windows.Forms.Label lblLanguage;
        internal System.Windows.Forms.Button btnGo;
    }
}
