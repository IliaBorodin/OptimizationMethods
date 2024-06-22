
namespace HomeLab
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.taskConditionPage = new System.Windows.Forms.TabPage();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.InfoBtn = new System.Windows.Forms.Button();
            this.DecideBtn = new System.Windows.Forms.Button();
            this.SetParametersBtn = new System.Windows.Forms.Button();
            this.RestrictionsDisplay = new System.Windows.Forms.DataGridView();
            this.FunctionDisplay = new System.Windows.Forms.DataGridView();
            this.BasisComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TypeOfFractionsComboBox = new System.Windows.Forms.ComboBox();
            this.OptimizationComboBox = new System.Windows.Forms.ComboBox();
            this.TypeOfTaskcomboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NumOfRestrictionsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NumOfVariablesTextBox = new System.Windows.Forms.TextBox();
            this.artificialBasisPage = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TransitionBtn = new System.Windows.Forms.Button();
            this.NextStepBtn = new System.Windows.Forms.Button();
            this.PreviousStepBtn = new System.Windows.Forms.Button();
            this.artificialBasisVisualization = new System.Windows.Forms.DataGridView();
            this.simplexMethodPage = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.labelForResponse = new System.Windows.Forms.Label();
            this.nextStepSMBtn = new System.Windows.Forms.Button();
            this.prevStepSMBtn = new System.Windows.Forms.Button();
            this.simplexMethodVisualization = new System.Windows.Forms.DataGridView();
            this.GomoryBtn = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.taskConditionPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RestrictionsDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionDisplay)).BeginInit();
            this.artificialBasisPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artificialBasisVisualization)).BeginInit();
            this.simplexMethodPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simplexMethodVisualization)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.taskConditionPage);
            this.mainTabControl.Controls.Add(this.artificialBasisPage);
            this.mainTabControl.Controls.Add(this.simplexMethodPage);
            this.mainTabControl.Location = new System.Drawing.Point(8, 17);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1445, 683);
            this.mainTabControl.TabIndex = 0;
            // 
            // taskConditionPage
            // 
            this.taskConditionPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.taskConditionPage.Controls.Add(this.SaveBtn);
            this.taskConditionPage.Controls.Add(this.LoadBtn);
            this.taskConditionPage.Controls.Add(this.InfoBtn);
            this.taskConditionPage.Controls.Add(this.DecideBtn);
            this.taskConditionPage.Controls.Add(this.SetParametersBtn);
            this.taskConditionPage.Controls.Add(this.RestrictionsDisplay);
            this.taskConditionPage.Controls.Add(this.FunctionDisplay);
            this.taskConditionPage.Controls.Add(this.BasisComboBox);
            this.taskConditionPage.Controls.Add(this.label6);
            this.taskConditionPage.Controls.Add(this.TypeOfFractionsComboBox);
            this.taskConditionPage.Controls.Add(this.OptimizationComboBox);
            this.taskConditionPage.Controls.Add(this.TypeOfTaskcomboBox);
            this.taskConditionPage.Controls.Add(this.label5);
            this.taskConditionPage.Controls.Add(this.label4);
            this.taskConditionPage.Controls.Add(this.label3);
            this.taskConditionPage.Controls.Add(this.NumOfRestrictionsTextBox);
            this.taskConditionPage.Controls.Add(this.label2);
            this.taskConditionPage.Controls.Add(this.label1);
            this.taskConditionPage.Controls.Add(this.NumOfVariablesTextBox);
            this.taskConditionPage.Location = new System.Drawing.Point(4, 34);
            this.taskConditionPage.Name = "taskConditionPage";
            this.taskConditionPage.Padding = new System.Windows.Forms.Padding(3);
            this.taskConditionPage.Size = new System.Drawing.Size(1437, 645);
            this.taskConditionPage.TabIndex = 0;
            this.taskConditionPage.Text = "Условие задачи";
            // 
            // SaveBtn
            // 
            this.SaveBtn.Image = global::HomeLab.Properties.Resources.save;
            this.SaveBtn.Location = new System.Drawing.Point(1226, 588);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(52, 54);
            this.SaveBtn.TabIndex = 21;
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.Image = global::HomeLab.Properties.Resources.load;
            this.LoadBtn.Location = new System.Drawing.Point(1168, 588);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(52, 54);
            this.LoadBtn.TabIndex = 20;
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // InfoBtn
            // 
            this.InfoBtn.Image = global::HomeLab.Properties.Resources.info;
            this.InfoBtn.Location = new System.Drawing.Point(1284, 588);
            this.InfoBtn.Name = "InfoBtn";
            this.InfoBtn.Size = new System.Drawing.Size(52, 54);
            this.InfoBtn.TabIndex = 19;
            this.InfoBtn.UseVisualStyleBackColor = true;
            this.InfoBtn.Click += new System.EventHandler(this.InfoBtn_Click);
            // 
            // DecideBtn
            // 
            this.DecideBtn.Location = new System.Drawing.Point(659, 593);
            this.DecideBtn.Name = "DecideBtn";
            this.DecideBtn.Size = new System.Drawing.Size(221, 34);
            this.DecideBtn.TabIndex = 18;
            this.DecideBtn.Text = "Решать";
            this.DecideBtn.UseVisualStyleBackColor = true;
            this.DecideBtn.Click += new System.EventHandler(this.DecideBtn_Click);
            // 
            // SetParametersBtn
            // 
            this.SetParametersBtn.Location = new System.Drawing.Point(6, 592);
            this.SetParametersBtn.Name = "SetParametersBtn";
            this.SetParametersBtn.Size = new System.Drawing.Size(207, 35);
            this.SetParametersBtn.TabIndex = 17;
            this.SetParametersBtn.Text = "Применить";
            this.SetParametersBtn.UseVisualStyleBackColor = true;
            this.SetParametersBtn.Click += new System.EventHandler(this.SetParametersBtn_Click);
            // 
            // RestrictionsDisplay
            // 
            this.RestrictionsDisplay.AllowUserToAddRows = false;
            this.RestrictionsDisplay.AllowUserToDeleteRows = false;
            this.RestrictionsDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RestrictionsDisplay.Location = new System.Drawing.Point(358, 220);
            this.RestrictionsDisplay.Name = "RestrictionsDisplay";
            this.RestrictionsDisplay.RowHeadersWidth = 51;
            this.RestrictionsDisplay.RowTemplate.Height = 24;
            this.RestrictionsDisplay.Size = new System.Drawing.Size(774, 346);
            this.RestrictionsDisplay.TabIndex = 16;
            // 
            // FunctionDisplay
            // 
            this.FunctionDisplay.AllowUserToAddRows = false;
            this.FunctionDisplay.AllowUserToDeleteRows = false;
            this.FunctionDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FunctionDisplay.Location = new System.Drawing.Point(358, 64);
            this.FunctionDisplay.Name = "FunctionDisplay";
            this.FunctionDisplay.RowHeadersWidth = 51;
            this.FunctionDisplay.RowTemplate.Height = 24;
            this.FunctionDisplay.Size = new System.Drawing.Size(775, 122);
            this.FunctionDisplay.TabIndex = 15;
            // 
            // BasisComboBox
            // 
            this.BasisComboBox.FormattingEnabled = true;
            this.BasisComboBox.Items.AddRange(new object[] {
            "Искусственный",
            "На выбор"});
            this.BasisComboBox.Location = new System.Drawing.Point(7, 534);
            this.BasisComboBox.Name = "BasisComboBox";
            this.BasisComboBox.Size = new System.Drawing.Size(207, 33);
            this.BasisComboBox.TabIndex = 14;
            this.BasisComboBox.Text = "Искусственный";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 489);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Базис";
            // 
            // TypeOfFractionsComboBox
            // 
            this.TypeOfFractionsComboBox.FormattingEnabled = true;
            this.TypeOfFractionsComboBox.Items.AddRange(new object[] {
            "Обыкновенные",
            "Десятичные"});
            this.TypeOfFractionsComboBox.Location = new System.Drawing.Point(6, 428);
            this.TypeOfFractionsComboBox.Name = "TypeOfFractionsComboBox";
            this.TypeOfFractionsComboBox.Size = new System.Drawing.Size(207, 33);
            this.TypeOfFractionsComboBox.TabIndex = 12;
            this.TypeOfFractionsComboBox.Text = "Обыкновенные";
            // 
            // OptimizationComboBox
            // 
            this.OptimizationComboBox.FormattingEnabled = true;
            this.OptimizationComboBox.Items.AddRange(new object[] {
            "Максимизировать",
            "Минимизировать"});
            this.OptimizationComboBox.Location = new System.Drawing.Point(7, 331);
            this.OptimizationComboBox.Name = "OptimizationComboBox";
            this.OptimizationComboBox.Size = new System.Drawing.Size(207, 33);
            this.OptimizationComboBox.TabIndex = 11;
            this.OptimizationComboBox.Text = "Минимизировать";
            // 
            // TypeOfTaskcomboBox
            // 
            this.TypeOfTaskcomboBox.FormattingEnabled = true;
            this.TypeOfTaskcomboBox.Items.AddRange(new object[] {
            "Симплекс метод",
            "Графический метод"});
            this.TypeOfTaskcomboBox.Location = new System.Drawing.Point(6, 240);
            this.TypeOfTaskcomboBox.Name = "TypeOfTaskcomboBox";
            this.TypeOfTaskcomboBox.Size = new System.Drawing.Size(207, 33);
            this.TypeOfTaskcomboBox.TabIndex = 10;
            this.TypeOfTaskcomboBox.Text = "Симплекс метод";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 388);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Вид дробей";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Задача оптимизации";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Метод решения";
            // 
            // NumOfRestrictionsTextBox
            // 
            this.NumOfRestrictionsTextBox.Location = new System.Drawing.Point(6, 156);
            this.NumOfRestrictionsTextBox.Name = "NumOfRestrictionsTextBox";
            this.NumOfRestrictionsTextBox.Size = new System.Drawing.Size(208, 30);
            this.NumOfRestrictionsTextBox.TabIndex = 3;
            this.NumOfRestrictionsTextBox.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(279, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Число ограничений(до 16)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Число переменных(до 16)";
            // 
            // NumOfVariablesTextBox
            // 
            this.NumOfVariablesTextBox.Location = new System.Drawing.Point(6, 64);
            this.NumOfVariablesTextBox.Name = "NumOfVariablesTextBox";
            this.NumOfVariablesTextBox.Size = new System.Drawing.Size(208, 30);
            this.NumOfVariablesTextBox.TabIndex = 0;
            this.NumOfVariablesTextBox.Text = "3";
            // 
            // artificialBasisPage
            // 
            this.artificialBasisPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.artificialBasisPage.Controls.Add(this.label8);
            this.artificialBasisPage.Controls.Add(this.label7);
            this.artificialBasisPage.Controls.Add(this.button2);
            this.artificialBasisPage.Controls.Add(this.button1);
            this.artificialBasisPage.Controls.Add(this.TransitionBtn);
            this.artificialBasisPage.Controls.Add(this.NextStepBtn);
            this.artificialBasisPage.Controls.Add(this.PreviousStepBtn);
            this.artificialBasisPage.Controls.Add(this.artificialBasisVisualization);
            this.artificialBasisPage.Location = new System.Drawing.Point(4, 34);
            this.artificialBasisPage.Name = "artificialBasisPage";
            this.artificialBasisPage.Padding = new System.Windows.Forms.Padding(3);
            this.artificialBasisPage.Size = new System.Drawing.Size(1437, 645);
            this.artificialBasisPage.TabIndex = 1;
            this.artificialBasisPage.Text = "Метод искусственного базиса";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(804, 389);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(327, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Возможный опорный элемент";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(804, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(282, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Лучший опорный элемент";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Green;
            this.button2.Location = new System.Drawing.Point(723, 387);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 29);
            this.button2.TabIndex = 5;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(723, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // TransitionBtn
            // 
            this.TransitionBtn.Location = new System.Drawing.Point(723, 218);
            this.TransitionBtn.Name = "TransitionBtn";
            this.TransitionBtn.Size = new System.Drawing.Size(357, 39);
            this.TransitionBtn.TabIndex = 3;
            this.TransitionBtn.Text = "Переход к симплекс-методу";
            this.TransitionBtn.UseVisualStyleBackColor = true;
            this.TransitionBtn.Visible = false;
            this.TransitionBtn.Click += new System.EventHandler(this.TransitionBtn_Click);
            // 
            // NextStepBtn
            // 
            this.NextStepBtn.Location = new System.Drawing.Point(723, 133);
            this.NextStepBtn.Name = "NextStepBtn";
            this.NextStepBtn.Size = new System.Drawing.Size(357, 39);
            this.NextStepBtn.TabIndex = 2;
            this.NextStepBtn.Text = "Следующий шаг";
            this.NextStepBtn.UseVisualStyleBackColor = true;
            this.NextStepBtn.Visible = false;
            this.NextStepBtn.Click += new System.EventHandler(this.NextStepBtn_Click);
            // 
            // PreviousStepBtn
            // 
            this.PreviousStepBtn.Location = new System.Drawing.Point(723, 46);
            this.PreviousStepBtn.Name = "PreviousStepBtn";
            this.PreviousStepBtn.Size = new System.Drawing.Size(357, 39);
            this.PreviousStepBtn.TabIndex = 1;
            this.PreviousStepBtn.Text = "Предыдущий шаг";
            this.PreviousStepBtn.UseVisualStyleBackColor = true;
            this.PreviousStepBtn.Visible = false;
            this.PreviousStepBtn.Click += new System.EventHandler(this.PreviousStepBtn_Click);
            // 
            // artificialBasisVisualization
            // 
            this.artificialBasisVisualization.AllowUserToAddRows = false;
            this.artificialBasisVisualization.AllowUserToDeleteRows = false;
            this.artificialBasisVisualization.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.artificialBasisVisualization.Location = new System.Drawing.Point(0, 3);
            this.artificialBasisVisualization.Name = "artificialBasisVisualization";
            this.artificialBasisVisualization.RowHeadersWidth = 51;
            this.artificialBasisVisualization.RowTemplate.Height = 24;
            this.artificialBasisVisualization.Size = new System.Drawing.Size(667, 484);
            this.artificialBasisVisualization.TabIndex = 0;
            this.artificialBasisVisualization.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.artificialBasisVisualization_CellClick);
            // 
            // simplexMethodPage
            // 
            this.simplexMethodPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.simplexMethodPage.Controls.Add(this.GomoryBtn);
            this.simplexMethodPage.Controls.Add(this.label9);
            this.simplexMethodPage.Controls.Add(this.label10);
            this.simplexMethodPage.Controls.Add(this.button3);
            this.simplexMethodPage.Controls.Add(this.button4);
            this.simplexMethodPage.Controls.Add(this.labelForResponse);
            this.simplexMethodPage.Controls.Add(this.nextStepSMBtn);
            this.simplexMethodPage.Controls.Add(this.prevStepSMBtn);
            this.simplexMethodPage.Controls.Add(this.simplexMethodVisualization);
            this.simplexMethodPage.Location = new System.Drawing.Point(4, 34);
            this.simplexMethodPage.Name = "simplexMethodPage";
            this.simplexMethodPage.Size = new System.Drawing.Size(1437, 645);
            this.simplexMethodPage.TabIndex = 2;
            this.simplexMethodPage.Text = "Симплекс метод";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(762, 426);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(327, 25);
            this.label9.TabIndex = 11;
            this.label9.Text = "Возможный опорный элемент";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(762, 357);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(282, 25);
            this.label10.TabIndex = 10;
            this.label10.Text = "Лучший опорный элемент";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Green;
            this.button3.Location = new System.Drawing.Point(687, 422);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 29);
            this.button3.TabIndex = 9;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Yellow;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(687, 357);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 29);
            this.button4.TabIndex = 8;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // labelForResponse
            // 
            this.labelForResponse.AutoSize = true;
            this.labelForResponse.Location = new System.Drawing.Point(831, 247);
            this.labelForResponse.Name = "labelForResponse";
            this.labelForResponse.Size = new System.Drawing.Size(72, 25);
            this.labelForResponse.TabIndex = 4;
            this.labelForResponse.Text = "Текст";
            // 
            // nextStepSMBtn
            // 
            this.nextStepSMBtn.Location = new System.Drawing.Point(687, 177);
            this.nextStepSMBtn.Name = "nextStepSMBtn";
            this.nextStepSMBtn.Size = new System.Drawing.Size(357, 41);
            this.nextStepSMBtn.TabIndex = 3;
            this.nextStepSMBtn.Text = "Следующий шаг";
            this.nextStepSMBtn.UseVisualStyleBackColor = true;
            this.nextStepSMBtn.Visible = false;
            this.nextStepSMBtn.Click += new System.EventHandler(this.nextStepSMBtn_Click);
            // 
            // prevStepSMBtn
            // 
            this.prevStepSMBtn.Location = new System.Drawing.Point(687, 82);
            this.prevStepSMBtn.Name = "prevStepSMBtn";
            this.prevStepSMBtn.Size = new System.Drawing.Size(357, 39);
            this.prevStepSMBtn.TabIndex = 2;
            this.prevStepSMBtn.Text = "Предыдущий шаг";
            this.prevStepSMBtn.UseVisualStyleBackColor = true;
            this.prevStepSMBtn.Visible = false;
            this.prevStepSMBtn.Click += new System.EventHandler(this.prevStepSMBtn_Click);
            // 
            // simplexMethodVisualization
            // 
            this.simplexMethodVisualization.AllowUserToAddRows = false;
            this.simplexMethodVisualization.AllowUserToDeleteRows = false;
            this.simplexMethodVisualization.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.simplexMethodVisualization.Location = new System.Drawing.Point(4, 5);
            this.simplexMethodVisualization.Name = "simplexMethodVisualization";
            this.simplexMethodVisualization.RowHeadersWidth = 51;
            this.simplexMethodVisualization.RowTemplate.Height = 24;
            this.simplexMethodVisualization.Size = new System.Drawing.Size(632, 484);
            this.simplexMethodVisualization.TabIndex = 0;
            this.simplexMethodVisualization.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.simplexMethodVisualization_CellClick);
            // 
            // GomoryBtn
            // 
            this.GomoryBtn.Location = new System.Drawing.Point(687, 558);
            this.GomoryBtn.Name = "GomoryBtn";
            this.GomoryBtn.Size = new System.Drawing.Size(357, 39);
            this.GomoryBtn.TabIndex = 12;
            this.GomoryBtn.Text = "Метод Гомори";
            this.GomoryBtn.UseVisualStyleBackColor = true;
            this.GomoryBtn.Visible = false;
            this.GomoryBtn.Click += new System.EventHandler(this.GomoryBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1360, 694);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simplex Method";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.taskConditionPage.ResumeLayout(false);
            this.taskConditionPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RestrictionsDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionDisplay)).EndInit();
            this.artificialBasisPage.ResumeLayout(false);
            this.artificialBasisPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artificialBasisVisualization)).EndInit();
            this.simplexMethodPage.ResumeLayout(false);
            this.simplexMethodPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simplexMethodVisualization)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage taskConditionPage;
        private System.Windows.Forms.TabPage artificialBasisPage;
        private System.Windows.Forms.DataGridView artificialBasisVisualization;
        private System.Windows.Forms.TabPage simplexMethodPage;
        private System.Windows.Forms.DataGridView simplexMethodVisualization;
        private System.Windows.Forms.ComboBox OptimizationComboBox;
        private System.Windows.Forms.ComboBox TypeOfTaskcomboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NumOfRestrictionsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NumOfVariablesTextBox;
        private System.Windows.Forms.ComboBox BasisComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TypeOfFractionsComboBox;
        private System.Windows.Forms.Button SetParametersBtn;
        private System.Windows.Forms.DataGridView RestrictionsDisplay;
        private System.Windows.Forms.DataGridView FunctionDisplay;
        private System.Windows.Forms.Button DecideBtn;
        private System.Windows.Forms.Button TransitionBtn;
        private System.Windows.Forms.Button NextStepBtn;
        private System.Windows.Forms.Button PreviousStepBtn;
        private System.Windows.Forms.Button nextStepSMBtn;
        private System.Windows.Forms.Button prevStepSMBtn;
        private System.Windows.Forms.Label labelForResponse;
        private System.Windows.Forms.Button InfoBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button GomoryBtn;
    }
}