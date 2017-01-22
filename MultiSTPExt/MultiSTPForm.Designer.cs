namespace MultiSTPExt
{
    partial class MultiSTPForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.StpListTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(294, 112);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // StpListTextBox
            // 
            this.StpListTextBox.Location = new System.Drawing.Point(13, 13);
            this.StpListTextBox.Multiline = true;
            this.StpListTextBox.Name = "StpListTextBox";
            this.StpListTextBox.Size = new System.Drawing.Size(356, 86);
            this.StpListTextBox.TabIndex = 1;
            // 
            // MultiSTPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 147);
            this.Controls.Add(this.StpListTextBox);
            this.Controls.Add(this.okButton);
            this.Name = "MultiSTPForm";
            this.Text = "Set multiple STP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.TextBox StpListTextBox;
    }
}

