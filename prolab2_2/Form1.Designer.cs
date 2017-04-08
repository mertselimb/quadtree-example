namespace prolab2_2
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.randomNode = new System.Windows.Forms.Button();
            this.deleteNode = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 512);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // randomNode
            // 
            this.randomNode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.randomNode.Location = new System.Drawing.Point(538, 21);
            this.randomNode.Name = "randomNode";
            this.randomNode.Size = new System.Drawing.Size(149, 23);
            this.randomNode.TabIndex = 1;
            this.randomNode.Text = "Create Random Node";
            this.randomNode.UseVisualStyleBackColor = false;
            this.randomNode.Click += new System.EventHandler(this.randomNode_Click);
            // 
            // deleteNode
            // 
            this.deleteNode.Location = new System.Drawing.Point(538, 50);
            this.deleteNode.Name = "deleteNode";
            this.deleteNode.Size = new System.Drawing.Size(149, 23);
            this.deleteNode.TabIndex = 2;
            this.deleteNode.Text = "Delete a Node";
            this.deleteNode.UseVisualStyleBackColor = true;
            this.deleteNode.Click += new System.EventHandler(this.deleteNode_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(538, 79);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(149, 23);
            this.clear.TabIndex = 3;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 544);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.deleteNode);
            this.Controls.Add(this.randomNode);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button randomNode;
        private System.Windows.Forms.Button deleteNode;
        private System.Windows.Forms.Button clear;
    }
}

