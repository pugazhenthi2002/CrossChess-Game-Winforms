namespace Winforms_Practice
{
    partial class Draft
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
            this.singlePlayerUC1 = new Winforms_Practice.SinglePlayerUC();
            this.SuspendLayout();
            // 
            // singlePlayerUC1
            // 
            this.singlePlayerUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.singlePlayerUC1.Location = new System.Drawing.Point(0, 0);
            this.singlePlayerUC1.Name = "singlePlayerUC1";
            this.singlePlayerUC1.Size = new System.Drawing.Size(800, 450);
            this.singlePlayerUC1.TabIndex = 0;
            // 
            // Draft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.singlePlayerUC1);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "Draft";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Draft";
            this.Load += new System.EventHandler(this.Draft_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Draft_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Draft_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Draft_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Draft_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Draft_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private SinglePlayerUC singlePlayerUC1;
    }
}