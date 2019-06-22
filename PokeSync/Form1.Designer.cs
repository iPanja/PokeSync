namespace PokeSync {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.LinkStatusLabel = new MetroFramework.Controls.MetroLabel();
            this.LinkAccountButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // LinkStatusLabel
            // 
            this.LinkStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.LinkStatusLabel.Location = new System.Drawing.Point(449, 88);
            this.LinkStatusLabel.Name = "LinkStatusLabel";
            this.LinkStatusLabel.Size = new System.Drawing.Size(168, 19);
            this.LinkStatusLabel.TabIndex = 0;
            this.LinkStatusLabel.Text = "Not Linked";
            this.LinkStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LinkStatusLabel.UseCustomForeColor = true;
            // 
            // LinkAccountButton
            // 
            this.LinkAccountButton.Location = new System.Drawing.Point(623, 84);
            this.LinkAccountButton.Name = "LinkAccountButton";
            this.LinkAccountButton.Size = new System.Drawing.Size(154, 23);
            this.LinkAccountButton.TabIndex = 1;
            this.LinkAccountButton.Text = "Link Dropbox Account";
            this.LinkAccountButton.UseSelectable = true;
            this.LinkAccountButton.Click += new System.EventHandler(this.LinkAccountButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LinkAccountButton);
            this.Controls.Add(this.LinkStatusLabel);
            this.Name = "Form1";
            this.Text = "PokeSync";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel LinkStatusLabel;
        private MetroFramework.Controls.MetroButton LinkAccountButton;
    }
}

