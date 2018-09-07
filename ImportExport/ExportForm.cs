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
    public partial class ExportForm : MForm
    {
        public event FileDB.ProcessStateHandler Process;

        DBType type;
        CategoryType catType;

        public ExportForm()
        {
            InitializeComponent();
            //
            Lang.TranslateControlFromDefaultToCurrentLanguage(this);
            sfdExport.Title = Lang.Phrase("Файл CSV для экспорта");
        }

        private void btSelectFile_Click(object sender, EventArgs e)
        {
            if (sfdExport.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbFile.Text = sfdExport.FileName;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            if (tbFile.Text.Trim() == "")
                return;

            var fileName = tbFile.Text;

            OnProcess(new FileDB.ProcessEventArgs(FileDB.ProcessState.Start, Lang.Phrase("Экспорт {0} ...", fileName)));
            try
            {
#if CLIENT
                DBType type = DBType.User;//для клиента экспортируем только клиентскую БД
#else
                DBType type = DBType.Any;//для админа экспортируем все 
#endif
                //
                var exporter = new Export();
                exporter.ExportToCSV(type, CategoryType.Any, fileName, Encoding.UTF8, '\t');
                


                //
                MessageBox.Message(Lang.Phrase("Экспорт успешно выполнен."));
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
