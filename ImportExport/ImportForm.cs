using System;
using System.Windows.Forms;
using MControls;
using UpdateStuffs;
using System.Threading;
using LangStuffs;
using ElisDB;
using System.Text;

namespace Elis.ImportExport
{
    public partial class ImportForm : MForm
    {
        public event FileDB.ProcessStateHandler Process;

        DBType type;
        CategoryType catType;

        public ImportForm()
        {
            InitializeComponent();
            //
            type = ProgramState.Selected.DbType;
            catType = ProgramState.Selected.CatType;
            //проверяем, выделена ли БД
            if (type == DBType.Any || type == DBType.None)
                throw new NonCriticalException(Lang.Phrase("Выберите тип базы данных.\r\nДля этого, отобразите дерево программ и выделите раздел, куда вы хотите выполнить импорт."));

            if (type == DBType.Base)
                if (catType == CategoryType.Any || catType == CategoryType.None)
                    throw new NonCriticalException(Lang.Phrase("Выберите раздел категорий.\r\nДля этого, отобразите дерево программ и выделите раздел, куда вы хотите выполнить импорт."));

            #if CLIENT
            if (type != DBType.User)
                throw new NonCriticalException(Lang.Phrase("Вы можете делать импорт только в пользовательскую базу данных"));
            #endif

            //
            Lang.TranslateControlFromDefaultToCurrentLanguage(this);
            ofdImport.Title = Lang.Phrase("Файл CSV для импорта");


            cbEncoding.SelectedIndex = 0;
        }

        private void btSelectFile_Click(object sender, EventArgs e)
        {
            if (ofdImport.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbFile.Text = ofdImport.FileName;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            if (tbFile.Text.Trim() == "")
                return;

            var fileName = tbFile.Text;

            OnProcess(new FileDB.ProcessEventArgs(FileDB.ProcessState.Start, Lang.Phrase("Импорт {0} ...", fileName)));
            try
            {
                //
                var importer = new Import();
                switch (cbEncoding.Text)
                {
                    case "ANSI"://CSV ANSI
                        importer.ImportFromCSV(type,  catType, fileName, Encoding.Default);
                        break;
                    case "UTF8"://CSV UTF8
                        importer.ImportFromCSV(type, catType, fileName, Encoding.UTF8);
                        break;
                    
                }
                //
                MessageBox.Message(Lang.Phrase("Импорт успешно выполнен."));
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (NonCriticalException ex)
            {
                MessageBox.Exclamation(ex.Message);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Error(ex);
                DialogResult = System.Windows.Forms.DialogResult.Abort;
                OnProcess(new FileDB.ProcessEventArgs(FileDB.ProcessState.Error));
                return;
            }

            OnProcess(new FileDB.ProcessEventArgs(FileDB.ProcessState.Complete));
        }

        private void OnProcess(FileDB.ProcessEventArgs e)
        {
            if (Process != null)
                Process(this, e);
        }
    }
}
