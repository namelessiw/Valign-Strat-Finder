namespace VAlignStratFinder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            StartY1 = new TextBox();
            StartY2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            GoalY1 = new TextBox();
            label4 = new Label();
            GoalY2 = new TextBox();
            label5 = new Label();
            FloorY = new TextBox();
            CeilingY = new TextBox();
            label6 = new Label();
            label7 = new Label();
            Results = new ListBox();
            SearchButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 0;
            label1.Text = "Starting Y Range";
            // 
            // StartY1
            // 
            StartY1.Location = new Point(12, 27);
            StartY1.Name = "StartY1";
            StartY1.Size = new Size(118, 23);
            StartY1.TabIndex = 1;
            StartY1.Text = "407.49999999999994";
            StartY1.KeyPress += StartY1_KeyPress;
            // 
            // StartY2
            // 
            StartY2.Location = new Point(154, 27);
            StartY2.Name = "StartY2";
            StartY2.Size = new Size(118, 23);
            StartY2.TabIndex = 2;
            StartY2.Text = "407.1";
            StartY2.KeyPress += StartY2_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(136, 30);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 3;
            label2.Text = "-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 53);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 4;
            label3.Text = "Goal Y Range";
            // 
            // GoalY1
            // 
            GoalY1.Location = new Point(12, 71);
            GoalY1.Name = "GoalY1";
            GoalY1.Size = new Size(118, 23);
            GoalY1.TabIndex = 5;
            GoalY1.Text = "407.35";
            GoalY1.TextChanged += GoalY1_TextChanged;
            GoalY1.KeyPress += GoalY1_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(136, 74);
            label4.Name = "label4";
            label4.Size = new Size(12, 15);
            label4.TabIndex = 6;
            label4.Text = "-";
            // 
            // GoalY2
            // 
            GoalY2.Location = new Point(154, 71);
            GoalY2.Name = "GoalY2";
            GoalY2.Size = new Size(118, 23);
            GoalY2.TabIndex = 7;
            GoalY2.Text = "407.25";
            GoalY2.TextChanged += GoalY2_TextChanged;
            GoalY2.KeyPress += GoalY2_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 97);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 8;
            label5.Text = "Floor Y";
            // 
            // FloorY
            // 
            FloorY.Enabled = false;
            FloorY.Location = new Point(12, 115);
            FloorY.Name = "FloorY";
            FloorY.Size = new Size(100, 23);
            FloorY.TabIndex = 9;
            FloorY.Text = "416";
            // 
            // CeilingY
            // 
            CeilingY.Location = new Point(136, 115);
            CeilingY.Name = "CeilingY";
            CeilingY.Size = new Size(100, 23);
            CeilingY.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(136, 97);
            label6.Name = "label6";
            label6.Size = new Size(54, 15);
            label6.TabIndex = 11;
            label6.Text = "Ceiling Y";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 141);
            label7.Name = "label7";
            label7.Size = new Size(44, 15);
            label7.TabIndex = 12;
            label7.Text = "Results";
            // 
            // Results
            // 
            Results.FormattingEnabled = true;
            Results.ItemHeight = 15;
            Results.Location = new Point(12, 159);
            Results.Name = "Results";
            Results.Size = new Size(118, 199);
            Results.TabIndex = 13;
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(136, 159);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(100, 23);
            SearchButton.TabIndex = 14;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SearchButton);
            Controls.Add(Results);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(CeilingY);
            Controls.Add(FloorY);
            Controls.Add(label5);
            Controls.Add(GoalY2);
            Controls.Add(label4);
            Controls.Add(GoalY1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(StartY2);
            Controls.Add(StartY1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "VAlign Start Finder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox StartY1;
        private TextBox StartY2;
        private Label label2;
        private Label label3;
        private TextBox GoalY1;
        private Label label4;
        private TextBox GoalY2;
        private Label label5;
        private TextBox FloorY;
        private TextBox CeilingY;
        private Label label6;
        private Label label7;
        private ListBox Results;
        private Button SearchButton;
    }
}
