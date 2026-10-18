using NTDLS.WinFormsHelpers;

namespace TestHarness
{
    internal class Program
    {
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Theming.InitializeApplication();

            using var form = new FormMain();
            form.ShowDialog();
        }
    }
}
