using NTDLS.WinFormsHelpers;

namespace TestHarness
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonProgress_Click(object sender, EventArgs e)
        {
            var progressForm = new ProgressForm();

            new Thread(() =>
            {
                progressForm.WaitForVisible();

                progressForm.SetProgressMaximum(30);

                for (int i = 0; i < 30; i++) 
                {
                    Thread.Sleep(100);
                    progressForm.SetProgressValue(i);
                }

                progressForm.Close();
            }).Start();

            var result = progressForm.ShowDialog("Please wait...");
        }
    }
}
