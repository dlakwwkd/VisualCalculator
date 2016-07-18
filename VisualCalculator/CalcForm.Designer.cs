namespace VisualCalculator
{
    partial class CalcForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.expression = new System.Windows.Forms.Label();
            this.negation = new System.Windows.Forms.Button();
            this.num0 = new System.Windows.Forms.Button();
            this.dot = new System.Windows.Forms.Button();
            this.enter = new System.Windows.Forms.Button();
            this.plus = new System.Windows.Forms.Button();
            this.num3 = new System.Windows.Forms.Button();
            this.num2 = new System.Windows.Forms.Button();
            this.num1 = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.num6 = new System.Windows.Forms.Button();
            this.num5 = new System.Windows.Forms.Button();
            this.num4 = new System.Windows.Forms.Button();
            this.mult = new System.Windows.Forms.Button();
            this.num9 = new System.Windows.Forms.Button();
            this.num8 = new System.Windows.Forms.Button();
            this.num7 = new System.Windows.Forms.Button();
            this.div = new System.Windows.Forms.Button();
            this.erase = new System.Windows.Forms.Button();
            this.c = new System.Windows.Forms.Button();
            this.ce = new System.Windows.Forms.Button();
            this.bracketR = new System.Windows.Forms.Button();
            this.x = new System.Windows.Forms.Button();
            this.bracketL = new System.Windows.Forms.Button();
            this.y = new System.Windows.Forms.Button();
            this.z = new System.Windows.Forms.Button();
            this.panel_calc = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_sya = new System.Windows.Forms.Panel();
            this.stackLabel = new System.Windows.Forms.Label();
            this.postfixLabel = new System.Windows.Forms.Label();
            this.infixLabel = new System.Windows.Forms.Label();
            this.panel_exprTree = new System.Windows.Forms.Panel();
            this.panel_result = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button_calc = new System.Windows.Forms.Button();
            this.label_result = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_reset = new System.Windows.Forms.Button();
            this.panel_calc.SuspendLayout();
            this.panel_sya.SuspendLayout();
            this.panel_result.SuspendLayout();
            this.SuspendLayout();
            // 
            // expression
            // 
            this.expression.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.expression.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expression.CausesValidation = false;
            this.expression.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.expression.Location = new System.Drawing.Point(12, 12);
            this.expression.Margin = new System.Windows.Forms.Padding(2);
            this.expression.Name = "expression";
            this.expression.Size = new System.Drawing.Size(256, 52);
            this.expression.TabIndex = 0;
            this.expression.Text = "0";
            this.expression.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // negation
            // 
            this.negation.CausesValidation = false;
            this.negation.Location = new System.Drawing.Point(63, 215);
            this.negation.Margin = new System.Windows.Forms.Padding(1);
            this.negation.Name = "negation";
            this.negation.Size = new System.Drawing.Size(50, 35);
            this.negation.TabIndex = 1;
            this.negation.TabStop = false;
            this.negation.Text = "+/-";
            this.negation.UseVisualStyleBackColor = true;
            this.negation.Click += new System.EventHandler(this.negation_Click);
            // 
            // num0
            // 
            this.num0.CausesValidation = false;
            this.num0.Location = new System.Drawing.Point(115, 215);
            this.num0.Margin = new System.Windows.Forms.Padding(1);
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(50, 35);
            this.num0.TabIndex = 2;
            this.num0.TabStop = false;
            this.num0.Text = "0";
            this.num0.UseVisualStyleBackColor = true;
            this.num0.Click += new System.EventHandler(this.num0_Click);
            // 
            // dot
            // 
            this.dot.CausesValidation = false;
            this.dot.Location = new System.Drawing.Point(167, 215);
            this.dot.Margin = new System.Windows.Forms.Padding(1);
            this.dot.Name = "dot";
            this.dot.Size = new System.Drawing.Size(50, 35);
            this.dot.TabIndex = 3;
            this.dot.TabStop = false;
            this.dot.Text = ".";
            this.dot.UseVisualStyleBackColor = true;
            this.dot.Click += new System.EventHandler(this.dot_Click);
            // 
            // enter
            // 
            this.enter.CausesValidation = false;
            this.enter.Location = new System.Drawing.Point(219, 215);
            this.enter.Margin = new System.Windows.Forms.Padding(1);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(50, 35);
            this.enter.TabIndex = 4;
            this.enter.TabStop = false;
            this.enter.Text = "=";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // plus
            // 
            this.plus.CausesValidation = false;
            this.plus.Location = new System.Drawing.Point(219, 178);
            this.plus.Margin = new System.Windows.Forms.Padding(1);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(50, 35);
            this.plus.TabIndex = 8;
            this.plus.TabStop = false;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // num3
            // 
            this.num3.CausesValidation = false;
            this.num3.Location = new System.Drawing.Point(167, 178);
            this.num3.Margin = new System.Windows.Forms.Padding(1);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(50, 35);
            this.num3.TabIndex = 7;
            this.num3.TabStop = false;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            this.num3.Click += new System.EventHandler(this.num3_Click);
            // 
            // num2
            // 
            this.num2.CausesValidation = false;
            this.num2.Location = new System.Drawing.Point(115, 178);
            this.num2.Margin = new System.Windows.Forms.Padding(1);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(50, 35);
            this.num2.TabIndex = 6;
            this.num2.TabStop = false;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            this.num2.Click += new System.EventHandler(this.num2_Click);
            // 
            // num1
            // 
            this.num1.CausesValidation = false;
            this.num1.Location = new System.Drawing.Point(63, 178);
            this.num1.Margin = new System.Windows.Forms.Padding(1);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(50, 35);
            this.num1.TabIndex = 5;
            this.num1.TabStop = false;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            this.num1.Click += new System.EventHandler(this.num1_Click);
            // 
            // minus
            // 
            this.minus.CausesValidation = false;
            this.minus.Location = new System.Drawing.Point(219, 141);
            this.minus.Margin = new System.Windows.Forms.Padding(1);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(50, 35);
            this.minus.TabIndex = 12;
            this.minus.TabStop = false;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // num6
            // 
            this.num6.CausesValidation = false;
            this.num6.Location = new System.Drawing.Point(167, 141);
            this.num6.Margin = new System.Windows.Forms.Padding(1);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(50, 35);
            this.num6.TabIndex = 11;
            this.num6.TabStop = false;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            this.num6.Click += new System.EventHandler(this.num6_Click);
            // 
            // num5
            // 
            this.num5.CausesValidation = false;
            this.num5.Location = new System.Drawing.Point(115, 141);
            this.num5.Margin = new System.Windows.Forms.Padding(1);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(50, 35);
            this.num5.TabIndex = 10;
            this.num5.TabStop = false;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            this.num5.Click += new System.EventHandler(this.num5_Click);
            // 
            // num4
            // 
            this.num4.CausesValidation = false;
            this.num4.Location = new System.Drawing.Point(63, 141);
            this.num4.Margin = new System.Windows.Forms.Padding(1);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(50, 35);
            this.num4.TabIndex = 9;
            this.num4.TabStop = false;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            this.num4.Click += new System.EventHandler(this.num4_Click);
            // 
            // mult
            // 
            this.mult.CausesValidation = false;
            this.mult.Location = new System.Drawing.Point(219, 104);
            this.mult.Margin = new System.Windows.Forms.Padding(1);
            this.mult.Name = "mult";
            this.mult.Size = new System.Drawing.Size(50, 35);
            this.mult.TabIndex = 16;
            this.mult.TabStop = false;
            this.mult.Text = "*";
            this.mult.UseVisualStyleBackColor = true;
            this.mult.Click += new System.EventHandler(this.mult_Click);
            // 
            // num9
            // 
            this.num9.CausesValidation = false;
            this.num9.Location = new System.Drawing.Point(167, 104);
            this.num9.Margin = new System.Windows.Forms.Padding(1);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(50, 35);
            this.num9.TabIndex = 15;
            this.num9.TabStop = false;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            this.num9.Click += new System.EventHandler(this.num9_Click);
            // 
            // num8
            // 
            this.num8.CausesValidation = false;
            this.num8.Location = new System.Drawing.Point(115, 104);
            this.num8.Margin = new System.Windows.Forms.Padding(1);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(50, 35);
            this.num8.TabIndex = 14;
            this.num8.TabStop = false;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            this.num8.Click += new System.EventHandler(this.num8_Click);
            // 
            // num7
            // 
            this.num7.CausesValidation = false;
            this.num7.Location = new System.Drawing.Point(63, 104);
            this.num7.Margin = new System.Windows.Forms.Padding(1);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(50, 35);
            this.num7.TabIndex = 13;
            this.num7.TabStop = false;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            this.num7.Click += new System.EventHandler(this.num7_Click);
            // 
            // div
            // 
            this.div.CausesValidation = false;
            this.div.Location = new System.Drawing.Point(219, 67);
            this.div.Margin = new System.Windows.Forms.Padding(1);
            this.div.Name = "div";
            this.div.Size = new System.Drawing.Size(50, 35);
            this.div.TabIndex = 20;
            this.div.TabStop = false;
            this.div.Text = "/";
            this.div.UseVisualStyleBackColor = true;
            this.div.Click += new System.EventHandler(this.div_Click);
            // 
            // erase
            // 
            this.erase.CausesValidation = false;
            this.erase.Location = new System.Drawing.Point(167, 67);
            this.erase.Margin = new System.Windows.Forms.Padding(1);
            this.erase.Name = "erase";
            this.erase.Size = new System.Drawing.Size(50, 35);
            this.erase.TabIndex = 19;
            this.erase.TabStop = false;
            this.erase.Text = "<-";
            this.erase.UseVisualStyleBackColor = true;
            this.erase.Click += new System.EventHandler(this.erase_Click);
            // 
            // c
            // 
            this.c.CausesValidation = false;
            this.c.Location = new System.Drawing.Point(115, 67);
            this.c.Margin = new System.Windows.Forms.Padding(1);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(50, 35);
            this.c.TabIndex = 18;
            this.c.TabStop = false;
            this.c.Text = "C";
            this.c.UseVisualStyleBackColor = true;
            this.c.Click += new System.EventHandler(this.c_Click);
            // 
            // ce
            // 
            this.ce.CausesValidation = false;
            this.ce.Location = new System.Drawing.Point(63, 67);
            this.ce.Margin = new System.Windows.Forms.Padding(1);
            this.ce.Name = "ce";
            this.ce.Size = new System.Drawing.Size(50, 35);
            this.ce.TabIndex = 17;
            this.ce.TabStop = false;
            this.ce.Text = "CE";
            this.ce.UseVisualStyleBackColor = true;
            this.ce.Click += new System.EventHandler(this.ce_Click);
            // 
            // bracketR
            // 
            this.bracketR.CausesValidation = false;
            this.bracketR.Location = new System.Drawing.Point(11, 215);
            this.bracketR.Margin = new System.Windows.Forms.Padding(1);
            this.bracketR.Name = "bracketR";
            this.bracketR.Size = new System.Drawing.Size(50, 35);
            this.bracketR.TabIndex = 21;
            this.bracketR.TabStop = false;
            this.bracketR.Text = ")";
            this.bracketR.UseVisualStyleBackColor = true;
            this.bracketR.Click += new System.EventHandler(this.bracketR_Click);
            // 
            // x
            // 
            this.x.CausesValidation = false;
            this.x.Location = new System.Drawing.Point(11, 67);
            this.x.Margin = new System.Windows.Forms.Padding(1);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(50, 35);
            this.x.TabIndex = 25;
            this.x.TabStop = false;
            this.x.Text = "x";
            this.x.UseVisualStyleBackColor = true;
            this.x.Click += new System.EventHandler(this.x_Click);
            // 
            // bracketL
            // 
            this.bracketL.CausesValidation = false;
            this.bracketL.Location = new System.Drawing.Point(11, 178);
            this.bracketL.Margin = new System.Windows.Forms.Padding(1);
            this.bracketL.Name = "bracketL";
            this.bracketL.Size = new System.Drawing.Size(50, 35);
            this.bracketL.TabIndex = 22;
            this.bracketL.TabStop = false;
            this.bracketL.Text = "(";
            this.bracketL.UseVisualStyleBackColor = true;
            this.bracketL.Click += new System.EventHandler(this.bracketL_Click);
            // 
            // y
            // 
            this.y.CausesValidation = false;
            this.y.Location = new System.Drawing.Point(11, 104);
            this.y.Margin = new System.Windows.Forms.Padding(1);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(50, 35);
            this.y.TabIndex = 24;
            this.y.TabStop = false;
            this.y.Text = "y";
            this.y.UseVisualStyleBackColor = true;
            this.y.Click += new System.EventHandler(this.y_Click);
            // 
            // z
            // 
            this.z.CausesValidation = false;
            this.z.Location = new System.Drawing.Point(11, 141);
            this.z.Margin = new System.Windows.Forms.Padding(1);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(50, 35);
            this.z.TabIndex = 23;
            this.z.TabStop = false;
            this.z.Text = "z";
            this.z.UseVisualStyleBackColor = true;
            this.z.Click += new System.EventHandler(this.z_Click);
            // 
            // panel_calc
            // 
            this.panel_calc.BackColor = System.Drawing.Color.White;
            this.panel_calc.CausesValidation = false;
            this.panel_calc.Controls.Add(this.expression);
            this.panel_calc.Controls.Add(this.x);
            this.panel_calc.Controls.Add(this.ce);
            this.panel_calc.Controls.Add(this.c);
            this.panel_calc.Controls.Add(this.erase);
            this.panel_calc.Controls.Add(this.div);
            this.panel_calc.Controls.Add(this.y);
            this.panel_calc.Controls.Add(this.num7);
            this.panel_calc.Controls.Add(this.num8);
            this.panel_calc.Controls.Add(this.num9);
            this.panel_calc.Controls.Add(this.mult);
            this.panel_calc.Controls.Add(this.z);
            this.panel_calc.Controls.Add(this.num4);
            this.panel_calc.Controls.Add(this.num5);
            this.panel_calc.Controls.Add(this.num6);
            this.panel_calc.Controls.Add(this.minus);
            this.panel_calc.Controls.Add(this.bracketL);
            this.panel_calc.Controls.Add(this.num1);
            this.panel_calc.Controls.Add(this.num2);
            this.panel_calc.Controls.Add(this.num3);
            this.panel_calc.Controls.Add(this.plus);
            this.panel_calc.Controls.Add(this.bracketR);
            this.panel_calc.Controls.Add(this.negation);
            this.panel_calc.Controls.Add(this.num0);
            this.panel_calc.Controls.Add(this.dot);
            this.panel_calc.Controls.Add(this.enter);
            this.panel_calc.Location = new System.Drawing.Point(12, 12);
            this.panel_calc.Margin = new System.Windows.Forms.Padding(0);
            this.panel_calc.Name = "panel_calc";
            this.panel_calc.Padding = new System.Windows.Forms.Padding(10);
            this.panel_calc.Size = new System.Drawing.Size(280, 260);
            this.panel_calc.TabIndex = 22;
            // 
            // panel_sya
            // 
            this.panel_sya.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel_sya.Controls.Add(this.stackLabel);
            this.panel_sya.Controls.Add(this.postfixLabel);
            this.panel_sya.Controls.Add(this.infixLabel);
            this.panel_sya.Enabled = false;
            this.panel_sya.Location = new System.Drawing.Point(298, 12);
            this.panel_sya.Name = "panel_sya";
            this.panel_sya.Size = new System.Drawing.Size(874, 260);
            this.panel_sya.TabIndex = 23;
            // 
            // stackLabel
            // 
            this.stackLabel.AutoSize = true;
            this.stackLabel.CausesValidation = false;
            this.stackLabel.Location = new System.Drawing.Point(388, 248);
            this.stackLabel.Name = "stackLabel";
            this.stackLabel.Size = new System.Drawing.Size(36, 12);
            this.stackLabel.TabIndex = 28;
            this.stackLabel.Text = "Stack";
            // 
            // postfixLabel
            // 
            this.postfixLabel.AutoSize = true;
            this.postfixLabel.CausesValidation = false;
            this.postfixLabel.Location = new System.Drawing.Point(3, 69);
            this.postfixLabel.Name = "postfixLabel";
            this.postfixLabel.Size = new System.Drawing.Size(107, 12);
            this.postfixLabel.TabIndex = 27;
            this.postfixLabel.Text = "PostfixExpression";
            // 
            // infixLabel
            // 
            this.infixLabel.AutoSize = true;
            this.infixLabel.CausesValidation = false;
            this.infixLabel.Location = new System.Drawing.Point(779, 69);
            this.infixLabel.Name = "infixLabel";
            this.infixLabel.Size = new System.Drawing.Size(92, 12);
            this.infixLabel.TabIndex = 26;
            this.infixLabel.Text = "InfixExpression";
            // 
            // panel_exprTree
            // 
            this.panel_exprTree.BackColor = System.Drawing.Color.SeaShell;
            this.panel_exprTree.Enabled = false;
            this.panel_exprTree.Location = new System.Drawing.Point(12, 278);
            this.panel_exprTree.Name = "panel_exprTree";
            this.panel_exprTree.Size = new System.Drawing.Size(1160, 425);
            this.panel_exprTree.TabIndex = 24;
            // 
            // panel_result
            // 
            this.panel_result.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel_result.BackColor = System.Drawing.Color.Black;
            this.panel_result.Controls.Add(this.label1);
            this.panel_result.Controls.Add(this.textBox1);
            this.panel_result.Controls.Add(this.label2);
            this.panel_result.Controls.Add(this.textBox2);
            this.panel_result.Controls.Add(this.label3);
            this.panel_result.Controls.Add(this.textBox3);
            this.panel_result.Controls.Add(this.label5);
            this.panel_result.Controls.Add(this.label_result);
            this.panel_result.Controls.Add(this.button_calc);
            this.panel_result.Controls.Add(this.button_reset);
            this.panel_result.Enabled = false;
            this.panel_result.Location = new System.Drawing.Point(12, 709);
            this.panel_result.Name = "panel_result";
            this.panel_result.Padding = new System.Windows.Forms.Padding(5);
            this.panel_result.Size = new System.Drawing.Size(1160, 40);
            this.panel_result.TabIndex = 25;
            this.panel_result.TabStop = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "x";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox1.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(35, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 24);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(141, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "y";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox2.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox2.Location = new System.Drawing.Point(167, 8);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 24);
            this.textBox2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(273, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "z";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox3.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox3.Location = new System.Drawing.Point(299, 8);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 24);
            this.textBox3.TabIndex = 5;
            // 
            // button_calc
            // 
            this.button_calc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_calc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_calc.Location = new System.Drawing.Point(1024, 8);
            this.button_calc.Name = "button_calc";
            this.button_calc.Size = new System.Drawing.Size(90, 25);
            this.button_calc.TabIndex = 6;
            this.button_calc.Text = "Calculate";
            this.button_calc.UseVisualStyleBackColor = true;
            // 
            // label_result
            // 
            this.label_result.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_result.BackColor = System.Drawing.Color.White;
            this.label_result.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_result.ForeColor = System.Drawing.Color.Black;
            this.label_result.Location = new System.Drawing.Point(481, 10);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(537, 20);
            this.label_result.TabIndex = 7;
            this.label_result.Text = "0";
            this.label_result.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(412, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Result";
            // 
            // button_reset
            // 
            this.button_reset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_reset.BackColor = System.Drawing.Color.Gold;
            this.button_reset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_reset.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_reset.Location = new System.Drawing.Point(1120, 8);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(25, 25);
            this.button_reset.TabIndex = 9;
            this.button_reset.Text = "R";
            this.button_reset.UseVisualStyleBackColor = false;
            // 
            // CalcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.panel_result);
            this.Controls.Add(this.panel_exprTree);
            this.Controls.Add(this.panel_sya);
            this.Controls.Add(this.panel_calc);
            this.KeyPreview = true;
            this.Name = "CalcForm";
            this.Text = "Calculator";
            this.panel_calc.ResumeLayout(false);
            this.panel_sya.ResumeLayout(false);
            this.panel_sya.PerformLayout();
            this.panel_result.ResumeLayout(false);
            this.panel_result.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label expression;
        private System.Windows.Forms.Button negation;
        private System.Windows.Forms.Button num0;
        private System.Windows.Forms.Button dot;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button num3;
        private System.Windows.Forms.Button num2;
        private System.Windows.Forms.Button num1;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button num6;
        private System.Windows.Forms.Button num5;
        private System.Windows.Forms.Button num4;
        private System.Windows.Forms.Button mult;
        private System.Windows.Forms.Button num9;
        private System.Windows.Forms.Button num8;
        private System.Windows.Forms.Button num7;
        private System.Windows.Forms.Button div;
        private System.Windows.Forms.Button erase;
        private System.Windows.Forms.Button c;
        private System.Windows.Forms.Button ce;
        private System.Windows.Forms.Button bracketR;
        private System.Windows.Forms.Button x;
        private System.Windows.Forms.Button bracketL;
        private System.Windows.Forms.Button y;
        private System.Windows.Forms.Button z;
        private System.Windows.Forms.FlowLayoutPanel panel_calc;
        private System.Windows.Forms.Panel panel_sya;
        private System.Windows.Forms.Panel panel_exprTree;
        private System.Windows.Forms.Label postfixLabel;
        private System.Windows.Forms.Label infixLabel;
        private System.Windows.Forms.Label stackLabel;
        private System.Windows.Forms.FlowLayoutPanel panel_result;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button_calc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_result;
        private System.Windows.Forms.Button button_reset;
    }
}

