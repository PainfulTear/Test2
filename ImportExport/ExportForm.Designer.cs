namespace Elis.ImportExport
{
    partial class ExportForm
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
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btSelectFile = new MControls.MButton();
            this.btExport = new MControls.MButton();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
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
            this.btCancel.Location = new System.Drawing.Point(273, 94);
            this.btCancel.Name = "btCancel";
            this.btCancel.PressedImage = null;
            this.btCancel.Size = new System.Drawing.Size(106, 34);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Отмена";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
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
            // btExport
            // 
            this.btExport.BackColor = System.Drawing.Color.Silver;
            this.btExport.Location = new System.Drawing.Point(161, 94);
            this.btExport.Name = "btExport";
            this.btExport.PressedImage = null;
            this.btExport.Size = new System.Drawing.Size(106, 34);
            this.btExport.TabIndex = 18;
            this.btExport.Text = "Экспортировать";
            this.btExport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "csv";
            this.sfdExport.Filter = "CSV (*.csv)|*.csv";
            this.sfdExport.Title = "Файл CSV для экспорта";
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 139);
            this.ControlBox = false;
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.btSelectFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.mLabel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ExportForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспорт БД в файл CSV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MControls.MLabel mLabel2;
        private MControls.MButton btCancel;
        private System.Windows.Forms.TextBox tbFile;
        private MControls.MButton btSelectFile;
        private MControls.MButton btExport;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.SaveFileDialog sfdExport;
    }
}