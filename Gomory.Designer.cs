
namespace HomeLab
{
    partial class Gomory
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
            this.GomoryVisualization = new System.Windows.Forms.DataGridView();
            this.HistoryBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.GomoryVisualization)).BeginInit();
            this.SuspendLayout();
            // 
            // GomoryVisualization
            // 
            this.GomoryVisualization.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GomoryVisualization.Location = new System.Drawing.Point(13, 31);
            this.GomoryVisualization.Name = "GomoryVisualization";
            this.GomoryVisualization.RowHeadersWidth = 51;
            this.GomoryVisualization.RowTemplate.Height = 24;
            this.GomoryVisualization.Size = new System.Drawing.Size(1055, 385);
            this.GomoryVisualization.TabIndex = 0;
            // 
            // HistoryBox
            // 
            this.HistoryBox.FormattingEnabled = true;
            this.HistoryBox.ItemHeight = 16;
            this.HistoryBox.Location = new System.Drawing.Point(12, 470);
            this.HistoryBox.Name = "HistoryBox";
            this.HistoryBox.Size = new System.Drawing.Size(907, 228);
            this.HistoryBox.TabIndex = 1;
            this.HistoryBox.SelectedIndexChanged += new System.EventHandler(this.HistoryBox_SelectedIndexChanged);
            // 
            // Gomory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 725);
            this.Controls.Add(this.HistoryBox);
            this.Controls.Add(this.GomoryVisualization);
            this.Name = "Gomory";
            this.Text = "Gomory";
            ((System.ComponentModel.ISupportInitialize)(this.GomoryVisualization)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GomoryVisualization;
        private System.Windows.Forms.ListBox HistoryBox;
    }
}