namespace Unterrichtsbewertungstool
{
    partial class ConnectForm
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
            this.btnconnect = new System.Windows.Forms.Button();
            this.lblipandport = new System.Windows.Forms.Label();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnconnect
            // 
            this.btnconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnconnect.Location = new System.Drawing.Point(12, 108);
            this.btnconnect.Name = "btnconnect";
            this.btnconnect.Size = new System.Drawing.Size(260, 57);
            this.btnconnect.TabIndex = 3;
            this.btnconnect.Text = "btnconnect";
            this.btnconnect.UseVisualStyleBackColor = true;
            this.btnconnect.Click += new System.EventHandler(this.Btnconnect_Click);
            // 
            // lblipandport
            // 
            this.lblipandport.AutoSize = true;
            this.lblipandport.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblipandport.Location = new System.Drawing.Point(12, 19);
            this.lblipandport.Name = "lblipandport";
            this.lblipandport.Size = new System.Drawing.Size(136, 26);
            this.lblipandport.TabIndex = 4;
            this.lblipandport.Text = "lblipandport";
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(17, 62);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(131, 20);
            this.tbxIP.TabIndex = 5;
            this.tbxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxIP.TextChanged += new System.EventHandler(this.TbxIP_TextChanged);
            this.tbxIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxIP_KeyDown);
            this.tbxIP.Leave += new System.EventHandler(this.TbxIP_Leave);
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(154, 62);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(118, 20);
            this.tbxPort.TabIndex = 6;
            this.tbxPort.TextChanged += new System.EventHandler(this.TbxPort_TextChanged);
            this.tbxPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxPort_KeyDown);
            this.tbxPort.Leave += new System.EventHandler(this.TbxPort_Leave);
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 177);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.tbxIP);
            this.Controls.Add(this.lblipandport);
            this.Controls.Add(this.btnconnect);
            this.Name = "ConnectForm";
            this.Text = "Verbinden";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnconnect;
        private System.Windows.Forms.Label lblipandport;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.TextBox tbxPort;

    }
}