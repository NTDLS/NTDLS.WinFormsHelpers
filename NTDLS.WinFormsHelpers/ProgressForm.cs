using static System.Net.Mime.MediaTypeNames;

namespace NTDLS.WinFormsHelpers
{
    /// <summary>
    /// Progress form used for multi-threaded progress reporting.
    /// </summary>
    public class ProgressForm
    {
        private const string LockObject = "FormProgress.Singleton.LockObject";

        private readonly FormProgress _form;

        /// <summary>
        /// Delegate used for OnCancel event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EventOnCancel(object sender, OnCancelInfo e);

        /// <summary>
        /// Cancel event parameter.
        /// </summary>
        public class OnCancelInfo
        {
            /// <summary>
            /// Set to true to cancel the cancel operation.
            /// </summary>
            public bool Cancel = false;
        }

        /// <summary>
        /// Creates a new instance of the FormProgress which is used for multi-threaded progress reporting.
        /// </summary>
        public ProgressForm()
        {
            _form = new FormProgress();
        }

        /// <summary>
        /// Used by the user to set proprietary state information;
        /// </summary>
        public object? UserData { get; set; } = null;

        /// <summary>
        /// Indicates whether the form has been shown or not.
        /// </summary>
        public bool HasBeenShown { get => _form.HasBeenShown; }

        /// <summary>
        /// Indicates whether a cancel operation has been started.
        /// </summary>
        public bool IsCancelPending { get => _form.IsCancelPending; }


        /// <summary>
        /// Shows a new progress form and returns the result when its closed.
        /// </summary>
        /// <param name="titleText"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(string titleText)
        {
            lock (LockObject)
            {
                _form.SetTitleText(titleText);
            }

            return _form.ShowDialog();
        }

        /// <summary>
        /// Shows a new progress form and returns the result when its closed.
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="headerText"></param>
        /// <param name="bodyText"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(string titleText, string headerText, string bodyText)
        {
            lock (LockObject)
            {
                _form.SetTitleText(titleText);
                _form.SetHeaderText(headerText);
                _form.SetBodyText(bodyText);
            }

            return _form.ShowDialog();
        }

        /// <summary>
        /// Shows a new progress form and returns the result when its closed.
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="bodyText"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(string headerText, string bodyText)
        {
            lock (LockObject)
            {
                _form.SetHeaderText(headerText);
                _form.SetBodyText(bodyText);
            }

            return _form.ShowDialog();
        }

        /// <summary>
        /// Shows a new progress form and returns the result when its closed.
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="onCancel"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(string headerText, EventOnCancel onCancel)
        {
            lock (LockObject)
            {
                _form.SetHeaderText(headerText);
                _form.OnCancel += onCancel;
                _form.SetCanCancel(true);
            }

            return _form.ShowDialog();
        }

        /// <summary>
        /// Shows a new progress form and returns the result when its closed.
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="bodyText"></param>
        /// <param name="onCancel"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(string headerText, string bodyText, EventOnCancel onCancel)
        {
            lock (LockObject)
            {
                _form.OnCancel += onCancel;
                _form.SetHeaderText(headerText);
                _form.SetBodyText(bodyText);
                _form.SetCanCancel(true);
            }

            return _form.ShowDialog();
        }

        /// <summary>
        /// Waits for the form to become visible. This is typically done from within the thread that will be controlling the form.
        /// </summary>
        public void WaitForVisible()
        {
            while (true)
            {
                lock (LockObject)
                {
                    if (_form != null && _form.HasBeenShown == true)
                    {
                        break;
                    }
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Closes the form with the given dialog result in a thread safe manner.
        /// </summary>
        /// <param name="result"></param>
        public void Close(DialogResult result)
            => _form.Close(result);

        /// <summary>
        /// Closes the form in a thread safe manner.
        /// </summary>
        public void Close()
            => _form.Close();

        /// <summary>
        /// Sets the header label text in a thread safe manner (this is not the title).
        /// </summary>
        /// <param name="text"></param>
        public void SetHeaderText(string text)
            => _form.SetHeaderText(text);

        /// <summary>
        /// Sets the body label text in a thread safe manner (this is not the title).
        /// </summary>
        /// <param name="text"></param>
        public void SetBodyText(string text)
            => _form.SetBodyText(text);

        /// <summary>
        /// Sets the form title text in a thread safe manner.
        /// </summary>
        /// <param name="text"></param>
        public void SetTitleText(string text)
            => _form.SetTitleText(text);

        /// <summary>
        /// Sets the progress bar minimum value in a thread safe manner.
        /// </summary>
        /// <param name="value"></param>
        public void SetProgressMinimum(int value)
            => _form.SetProgressMinimum(value);

        /// <summary>
        /// Sets the progress bar maximum value in a thread safe manner.
        /// </summary>
        /// <param name="value"></param>
        public void SetProgressMaximum(int value)
            => _form.SetProgressMaximum(value);

        /// <summary>
        /// Increments the progress bar value in a thread safe manner.
        /// </summary>
        public void IncrementProgressValue()
            => _form.IncrementProgressValue();

        /// <summary>
        /// Sets the progress bar value in a thread safe manner.
        /// </summary>
        /// <param name="value"></param>
        public void SetProgressValue(int value)
            => _form.SetProgressValue(value);

        /// <summary>
        /// Sets the progress bar style in a thread safe manner.
        /// </summary>
        /// <param name="value"></param>
        public void SeProgressStyle(ProgressBarStyle value)
            => _form.SeProgressStyle(value);

        /// <summary>
        /// Enables or disabled cancelation support in a thread safe manner.
        /// </summary>
        /// <param name="value"></param>
        public void SetCanCancel(bool value)
            => _form.SetCanCancel(value);
    }
}
