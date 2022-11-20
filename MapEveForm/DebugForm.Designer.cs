namespace MapEveForm
{
    partial class DebugForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.checkedLBEVE = new System.Windows.Forms.CheckedListBox();
            this.btSave = new System.Windows.Forms.Button();
            this.LbPlayerName = new System.Windows.Forms.Label();
            this.CLBPlayer = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkedLBEVE
            // 
            this.checkedLBEVE.FormattingEnabled = true;
            this.checkedLBEVE.Location = new System.Drawing.Point(289, 31);
            this.checkedLBEVE.Name = "checkedLBEVE";
            this.checkedLBEVE.Size = new System.Drawing.Size(254, 310);
            this.checkedLBEVE.TabIndex = 1;
            this.checkedLBEVE.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedLBEVE_ItemCheck);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(12, 360);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(109, 44);
            this.btSave.TabIndex = 2;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // LbPlayerName
            // 
            this.LbPlayerName.AutoSize = true;
            this.LbPlayerName.Location = new System.Drawing.Point(12, 9);
            this.LbPlayerName.Name = "LbPlayerName";
            this.LbPlayerName.Size = new System.Drawing.Size(74, 15);
            this.LbPlayerName.TabIndex = 3;
            this.LbPlayerName.Text = "Player Name";
            // 
            // CLBPlayer
            // 
            this.CLBPlayer.FormattingEnabled = true;
            this.CLBPlayer.ItemHeight = 15;
            this.CLBPlayer.Location = new System.Drawing.Point(12, 31);
            this.CLBPlayer.Name = "CLBPlayer";
            this.CLBPlayer.Size = new System.Drawing.Size(253, 304);
            this.CLBPlayer.TabIndex = 4;
            this.CLBPlayer.SelectedIndexChanged += new System.EventHandler(this.CLBPlayer_SelectedIndexChanged);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 450);
            this.Controls.Add(this.CLBPlayer);
            this.Controls.Add(this.LbPlayerName);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.checkedLBEVE);
            this.Controls.Add(this.button1);
            this.Name = "DebugForm";
            this.Text = "ChatSelection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private CheckedListBox checkedLBEVE;
        private Button btSave;
        private Label LbPlayerName;
        private ListBox CLBPlayer;
    }
}