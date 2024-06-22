
namespace HomeLab
{
    partial class GraphicalMethod
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.GraphPaintBox = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(9, 10);
            this.textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(323, 596);
            this.textBox.TabIndex = 0;
            // 
            // GraphPaintBox
            // 
            this.GraphPaintBox.Location = new System.Drawing.Point(336, 10);
            this.GraphPaintBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GraphPaintBox.Name = "GraphPaintBox";
            this.GraphPaintBox.Size = new System.Drawing.Size(759, 582);
            this.GraphPaintBox.TabIndex = 1;
            this.GraphPaintBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphPaintBox_Paint);
            // 
            // GraphicalMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 592);
            this.Controls.Add(this.GraphPaintBox);
            this.Controls.Add(this.textBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GraphicalMethod";
            this.Text = "GraphicalMethod";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Panel GraphPaintBox;
    }
}