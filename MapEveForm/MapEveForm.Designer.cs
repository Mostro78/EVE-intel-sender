namespace MapEveForm
{
    partial class MapEveForm
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
            this.btSave = new System.Windows.Forms.Button();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(472, 6);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(66, 30);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // tbToken
            // 
            this.tbToken.Location = new System.Drawing.Point(12, 11);
            this.tbToken.Name = "tbToken";
            this.tbToken.Size = new System.Drawing.Size(454, 23);
            this.tbToken.TabIndex = 1;
            // 
            // MapEveForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(550, 42);
            this.Controls.Add(this.tbToken);
            this.Controls.Add(this.btSave);
            this.MaximumSize = new System.Drawing.Size(566, 81);
            this.MinimumSize = new System.Drawing.Size(566, 81);
            this.Name = "MapEveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Token";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btSave;
        private TextBox tbToken;
    }
}