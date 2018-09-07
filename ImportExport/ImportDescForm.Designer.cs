namespace Elis.ImportExport
{
    partial class ImportDescForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mLabel2 = new MControls.MLabel();
            this.btCancel = new MControls.MButton();
            this.mLabel4 = new MControls.MLabel();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btSelectFile = new MControls.MButton();
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            this.cbEncoding = new System.Windows.Forms.ComboBox();
            this.btImport = new MControls.MButton();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.mLabel1 = new MControls.MLabel();
            this.SuspendLayout();
            // 
            // mLabel2
            // 
            this.mLabel2.LineColor = System.Drawing.Color.Silver;
            this.mLabel2.Location = new System.Drawing.Point(7, 7);
            this.mLabel2.Name = "mLabel2";
            this.mLabel2.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            this.mLabel2.Size = new System.Drawing.Size(372, 15);
            this.mLabel2.TabIndex = 3;
            this.mLabel2.Text = "Файл";
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.Silver;
            this.btCancel.Location = new System.Drawing.Point(269, 143);
            this.btCancel.Name = "btCancel";
            this.btCancel.PressedImage = null;
            this.btCancel.Size = new System.Drawing.Size(106, 34);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Отмена";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // mLabel4
            // 
            this.mLabel4.LineColor = System.Drawing.Color.Silver;
            this.mLabel4.Location = new System.Drawing.Point(7, 63);
            this.mLabel4.Name = "mLabel4";
            this.mLabel4.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            this.mLabel4.Size = new System.Drawing.Size(121, 15);
            this.mLabel4.TabIndex = 11;
            this.mLabel4.Text = "Кодировка";
            this.ttMain.SetToolTip(this.mLabel4, "Кодировка файла");
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(7, 28);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(350, 20);
            this.tbFile.TabIndex = 13;
            // 
            // btSelectFile
            // 
            this.btSelectFile.BackColor = System.Drawing.Color.Silver;
            this.btSelectFile.Location = new System.Drawing.Point(356, 28);
            this.btSelectFile.Name = "btSelectFile";
            this.btSelectFile.PressedImage = null;
            this.btSelectFile.RoundRadius = 4;
            this.btSelectFile.ShadowSize = 0;
            this.btSelectFile.Size = new System.Drawing.Size(23, 20);
            this.btSelectFile.TabIndex = 14;
            this.btSelectFile.Text = "...";
            this.btSelectFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btSelectFile.Click += new System.EventHandler(this.btSelectFile_Click);
            // 
            // ofdImport
            // 
            this.ofdImport.Filter = "Text (*.txt)|*.txt|All files|*.*";
            // 
            // cbEncoding
            // 
            this.cbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEncoding.FormattingEnabled = true;
            this.cbEncoding.Items.AddRange(new object[] {
            "ANSI",
            "UTF8"});
            this.cbEncoding.Location = new System.Drawing.Point(7, 84);
            this.cbEncoding.Name = "cbEncoding";
            this.cbEncoding.Size = new System.Drawing.Size(121, 21);
            this.cbEncoding.TabIndex = 15;
            this.ttMain.SetToolTip(this.cbEncoding, "Кодировка файла");
            // 
            // btImport
            // 
            this.btImport.BackColor = System.Drawing.Color.Silver;
            this.btImport.Location = new System.Drawing.Point(157, 143);
            this.btImport.Name = "btImport";
            this.btImport.PressedImage = null;
            this.btImport.Size = new System.Drawing.Size(106, 34);
            this.btImport.TabIndex = 18;
            this.btImport.Text = "Импортировать";
            this.btImport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // cbLang
            // 
            this.cbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Location = new System.Drawing.Point(160, 84);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(121, 21);
            this.cbLang.TabIndex = 20;
            this.ttMain.SetToolTip(this.cbLang, "Язык описаний");
            // 
            // mLabel1
            // 
            this.mLabel1.LineColor = System.Drawing.Color.Silver;
            this.mLabel1.Location = new System.Drawing.Point(160, 63);
            this.mLabel1.Name = "mLabel1";
            this.mLabel1.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            this.mLabel1.Size = new System.Drawing.Size(121, 15);
            this.mLabel1.TabIndex = 19;
            this.mLabel1.Text = "Язык";
            this.ttMain.SetToolTip(this.mLabel1, "Язык описаний");
            // 
            // ImportDescForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 183);
            this.ControlBox = false;
            this.Controls.Add(this.cbLang);
            this.Controls.Add(this.mLabel1);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.cbEncoding);
            this.Controls.Add(this.btSelectFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.mLabel4);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.mLabel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ImportDescForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Импорт описаний программ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MControls.MLabel mLabel2;
        private MControls.MButton btCancel;
        private MControls.MLabel mLabel4;
        private System.Windows.Forms.TextBox tbFile;
        private MControls.MButton btSelectFile;
        private System.Windows.Forms.OpenFileDialog ofdImport;
        private System.Windows.Forms.ComboBox cbEncoding;
        private MControls.MButton btImport;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.ComboBox cbLang;
        private MControls.MLabel mLabel1;
    }
}