using NTDLS.WinFormsHelpers;
using NTDLS.WinFormsHelpers.Forms;
using System.Resources;

namespace TestHarness
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            Theming.InitializeForm(this);
        }

        private void buttonProgress_Click(object sender, EventArgs e)
        {
            var progressForm = new ProgressForm();

            progressForm.Execute(() =>
            {
                progressForm.SetProgressMaximum(30);

                for (int i = 0; i < 30; i++)
                {
                    Thread.Sleep(100);

                    if (i == 15)
                    {
                        progressForm.MessageBox("Half way there!", "Caption");

                    }

                    progressForm.SetProgressValue(i);
                }
            });
        }

        private async void buttonProgressAsync_Click(object sender, EventArgs e)
        {
            var progressForm = new ProgressForm();

            await progressForm.ExecuteAsync(async () =>
            {
                progressForm.SetProgressMaximum(30);

                for (int i = 0; i < 30; i++)
                {
                    await Task.Delay(100);
                    if (i == 15)
                    {
                        progressForm.MessageBox("Half way there!", "Caption");

                    }

                    progressForm.SetProgressValue(i);
                }
            });
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            using var form = new FormAbout("NetTunnel", this.Icon, Resources.Logo128);
            form.ShowDialog();
        }
    }
}
