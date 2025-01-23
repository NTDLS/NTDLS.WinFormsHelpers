namespace TestHarness
{
    partial class FormMain
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
            buttonProgress = new Button();
            buttonProgresssAsync = new Button();
            SuspendLayout();
            // 
            // buttonProgress
            // 
            buttonProgress.Location = new Point(186, 62);
            buttonProgress.Name = "buttonProgress";
            buttonProgress.Size = new Size(75, 23);
            buttonProgress.TabIndex = 0;
            buttonProgress.Text = "Progress";
            buttonProgress.UseVisualStyleBackColor = true;
            buttonProgress.Click += buttonProgress_Click;
            // 
            // buttonProgresssAsync
            // 
            buttonProgresssAsync.Location = new Point(186, 91);
            buttonProgresssAsync.Name = "buttonProgresssAsync";
            buttonProgresssAsync.Size = new Size(125, 23);
            buttonProgresssAsync.TabIndex = 1;
            buttonProgresssAsync.Text = "Progress Async";
            buttonProgresssAsync.UseVisualStyleBackColor = true;
            buttonProgresssAsync.Click += buttonProgressAsync_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 258);
            Controls.Add(buttonProgresssAsync);
            Controls.Add(buttonProgress);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMain";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonProgress;
        private Button buttonProgresssAsync;
    }
}