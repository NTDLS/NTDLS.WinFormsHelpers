namespace NTDLS.WinFormsHelpers
{
    /// <summary>
    /// Various WinForms Control extensions for invoking common tasks to prevent cross-thread-operations.
    /// </summary>
    public static class ControlInvokeExtensions
    {
        /// <summary>
        /// Invokes the form to enable or disable a control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="enabled"></param>
        public static void FormInvokeEnableControl(this Control control, bool enabled)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => control.Enabled = enabled));
            }
            else
            {
                control.Enabled = enabled;
            }
        }
    }
}
