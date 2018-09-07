using System;
using System.Windows.Forms;
using MControls;
using UpdateStuffs;
using System.Threading;
using LangStuffs;
using ElisDB;
using System.Text;
using DeviceStuffs;

namespace Elis.ImportExport
{
    public partial class ImportDescForm : MForm
    {
        public event FileDB.ProcessStateHandler Process;

        DBType type;
        CategoryType catType;
        DeviceType device;

        public ImportDescForm()
        {
            InitializeComponent();
            //
            type = ProgramState.Selected.DbType;
            device = ProgramState.Selected.DeviceType;

            //проверяем, выделен ли прибор
            if (device == DeviceType.Any || device == DeviceType.None)
                throw new NonCriticalException(Lang.Phrase("Выберите прибор."));

            #if CLIENT
            if (type != DBType.User)
                throw new NonCriticalException(Lang.Phrase("Вы можете делать импорт только в пользовательскую базу данных"));
            #endif

            //
            Lang.TranslateControlFromDefaultToCurrentLanguage(this);
            ofdImport.Title = Lang.Phrase("Файл для импорта");

            BuildLanguagesList(cbLang);


            cbEncoding.SelectedIndex = 0;
        }

        public static void BuildLanguagesList(ComboBox cb)
        {
            try
            {
                foreach (var lang in Lang.GetAccessibleLanguages())
                    cb.Items.Add(new LangInterfaceItem(lang));
                try
                {
                    cb.SelectedItem = new LangInterfaceItem(RunManager.Settings.Language);
                }
                catch { /* в текущих настройках непонятный язык*/ }
            }
            catch
            {/*не удалось прочитать языки*/}
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
                int LCID = (cbLang.SelectedItem as LangInterfaceItem).LCID;
                //
                var importer = new Import();
                switch (cbEncoding.Text)
                {
                    case "ANSI"://CSV ANSI
                        importer.ImportDescriptions(type, device, fileName, LCID, Encoding.Default);
                        break;
                    case "UTF8"://CSV UTF8
                        importer.ImportDescriptions(type, device, fileName, LCID, Encoding.UTF8);
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
