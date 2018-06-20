namespace Unterrichtsbewertungstool
{
    partial class HostForm
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
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.lblport = new System.Windows.Forms.Label();
            this.btnhost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(12, 38);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(260, 20);
            this.tbxPort.TabIndex = 10;
            this.tbxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxPort.TextChanged += new System.EventHandler(this.tbxPort_TextChanged);
            this.tbxPort.Enter += new System.EventHandler(this.tbxPort_Enter);
            this.tbxPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxPort_KeyDown);
            this.tbxPort.Leave += new System.EventHandler(this.tbxPort_Leave);
            // 
            // lblport
            // 
            this.lblport.AutoSize = true;
            this.lblport.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblport.Location = new System.Drawing.Point(12, 9);
            this.lblport.Name = "lblport";
            this.lblport.Size = new System.Drawing.Size(78, 26);
            this.lblport.TabIndex = 8;
            this.lblport.Text = "lblport";
            // 
            // btnhost
            // 
            this.btnhost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhost.Location = new System.Drawing.Point(12, 64);
            this.btnhost.Name = "btnhost";
            this.btnhost.Size = new System.Drawing.Size(260, 57);
            this.btnhost.TabIndex = 7;
            this.btnhost.Text = "btnhost";
            this.btnhost.UseVisualStyleBackColor = true;
            this.btnhost.Click += new System.EventHandler(this.btnhost_Click);
            // 
            // HostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 132);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.lblport);
            this.Controls.Add(this.btnhost);
            this.Name = "HostForm";
            this.Text = "Hosten";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Label lblport;
        private System.Windows.Forms.Button btnhost;
    }
}