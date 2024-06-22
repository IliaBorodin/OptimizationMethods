
namespace HomeLab
{
    partial class BasisVariables
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
            this.SetBasisBtn = new System.Windows.Forms.Button();
            this.BasisVariablesGriedView = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BasisVariablesGriedView)).BeginInit();
            this.SuspendLayout();
            // 
            // SetBasisBtn
            // 
            this.SetBasisBtn.Location = new System.Drawing.Point(182, 354);
            this.SetBasisBtn.Name = "SetBasisBtn";
            this.SetBasisBtn.Size = new System.Drawing.Size(180, 47);
            this.SetBasisBtn.TabIndex = 0;
            this.SetBasisBtn.Text = "Задать базис";
            this.SetBasisBtn.UseVisualStyleBackColor = true;
            this.SetBasisBtn.Click += new System.EventHandler(this.SetBasisBtn_Click);
            // 
            // BasisVariablesGriedView
            // 
            this.BasisVariablesGriedView.AllowUserToAddRows = false;
            this.BasisVariablesGriedView.AllowUserToDeleteRows = false;
            this.BasisVariablesGriedView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BasisVariablesGriedView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Column1});
            this.BasisVariablesGriedView.Location = new System.Drawing.Point(12, 12);
            this.BasisVariablesGriedView.Name = "BasisVariablesGriedView";
            this.BasisVariablesGriedView.RowHeadersWidth = 51;
            this.BasisVariablesGriedView.RowTemplate.Height = 24;
            this.BasisVariablesGriedView.Size = new System.Drawing.Size(531, 260);
            this.BasisVariablesGriedView.TabIndex = 1;
            // 
            // Variable
            // 
            this.Variable.HeaderText = "Переменная";
            this.Variable.MinimumWidth = 6;
            this.Variable.Name = "Variable";
            this.Variable.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Базисная";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // BasisVariables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(555, 445);
            this.Controls.Add(this.BasisVariablesGriedView);
            this.Controls.Add(this.SetBasisBtn);
            this.Name = "BasisVariables";
            this.Text = "Выбор базисных переменных";
            this.Load += new System.EventHandler(this.BasisVariables_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.BasisVariablesGriedView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SetBasisBtn;
        private System.Windows.Forms.DataGridView BasisVariablesGriedView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    }
}