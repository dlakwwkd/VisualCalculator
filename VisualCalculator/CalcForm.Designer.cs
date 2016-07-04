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
            this.SuspendLayout();
            // 
            // expression
            // 
            this.expression.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.expression.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expression.CausesValidation = false;
            this.expression.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.expression.Location = new System.Drawing.Point(12, 22);
            this.expression.Name = "expression";
            this.expression.Size = new System.Drawing.Size(218, 52);
            this.expression.TabIndex = 0;
            this.expression.Text = "0";
            this.expression.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // negation
            // 
            this.negation.CausesValidation = false;
            this.negation.Location = new System.Drawing.Point(12, 242);
            this.negation.Name = "negation";
            this.negation.Size = new System.Drawing.Size(50, 35);
            this.negation.TabIndex = 1;
            this.negation.Text = "+/-";
            this.negation.UseVisualStyleBackColor = true;
            // 
            // num0
            // 
            this.num0.CausesValidation = false;
            this.num0.Location = new System.Drawing.Point(68, 242);
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(50, 35);
            this.num0.TabIndex = 2;
            this.num0.Text = "0";
            this.num0.UseVisualStyleBackColor = true;
            // 
            // dot
            // 
            this.dot.CausesValidation = false;
            this.dot.Location = new System.Drawing.Point(124, 242);
            this.dot.Name = "dot";
            this.dot.Size = new System.Drawing.Size(50, 35);
            this.dot.TabIndex = 3;
            this.dot.Text = ".";
            this.dot.UseVisualStyleBackColor = true;
            // 
            // enter
            // 
            this.enter.CausesValidation = false;
            this.enter.Location = new System.Drawing.Point(180, 242);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(50, 35);
            this.enter.TabIndex = 4;
            this.enter.Text = "=";
            this.enter.UseVisualStyleBackColor = true;
            // 
            // plus
            // 
            this.plus.CausesValidation = false;
            this.plus.Location = new System.Drawing.Point(180, 204);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(50, 35);
            this.plus.TabIndex = 8;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            // 
            // num3
            // 
            this.num3.CausesValidation = false;
            this.num3.Location = new System.Drawing.Point(124, 204);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(50, 35);
            this.num3.TabIndex = 7;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            // 
            // num2
            // 
            this.num2.CausesValidation = false;
            this.num2.Location = new System.Drawing.Point(68, 204);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(50, 35);
            this.num2.TabIndex = 6;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            // 
            // num1
            // 
            this.num1.CausesValidation = false;
            this.num1.Location = new System.Drawing.Point(12, 204);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(50, 35);
            this.num1.TabIndex = 5;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            // 
            // minus
            // 
            this.minus.CausesValidation = false;
            this.minus.Location = new System.Drawing.Point(180, 166);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(50, 35);
            this.minus.TabIndex = 12;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            // 
            // num6
            // 
            this.num6.CausesValidation = false;
            this.num6.Location = new System.Drawing.Point(124, 166);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(50, 35);
            this.num6.TabIndex = 11;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            // 
            // num5
            // 
            this.num5.CausesValidation = false;
            this.num5.Location = new System.Drawing.Point(68, 166);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(50, 35);
            this.num5.TabIndex = 10;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            // 
            // num4
            // 
            this.num4.CausesValidation = false;
            this.num4.Location = new System.Drawing.Point(12, 166);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(50, 35);
            this.num4.TabIndex = 9;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            // 
            // mult
            // 
            this.mult.CausesValidation = false;
            this.mult.Location = new System.Drawing.Point(180, 128);
            this.mult.Name = "mult";
            this.mult.Size = new System.Drawing.Size(50, 35);
            this.mult.TabIndex = 16;
            this.mult.Text = "*";
            this.mult.UseVisualStyleBackColor = true;
            // 
            // num9
            // 
            this.num9.CausesValidation = false;
            this.num9.Location = new System.Drawing.Point(124, 128);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(50, 35);
            this.num9.TabIndex = 15;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            // 
            // num8
            // 
            this.num8.CausesValidation = false;
            this.num8.Location = new System.Drawing.Point(68, 128);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(50, 35);
            this.num8.TabIndex = 14;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            // 
            // num7
            // 
            this.num7.CausesValidation = false;
            this.num7.Location = new System.Drawing.Point(12, 128);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(50, 35);
            this.num7.TabIndex = 13;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            // 
            // div
            // 
            this.div.CausesValidation = false;
            this.div.Location = new System.Drawing.Point(180, 90);
            this.div.Name = "div";
            this.div.Size = new System.Drawing.Size(50, 35);
            this.div.TabIndex = 20;
            this.div.Text = "/";
            this.div.UseVisualStyleBackColor = true;
            // 
            // erase
            // 
            this.erase.CausesValidation = false;
            this.erase.Location = new System.Drawing.Point(124, 90);
            this.erase.Name = "erase";
            this.erase.Size = new System.Drawing.Size(50, 35);
            this.erase.TabIndex = 19;
            this.erase.Text = "<-";
            this.erase.UseVisualStyleBackColor = true;
            // 
            // c
            // 
            this.c.CausesValidation = false;
            this.c.Location = new System.Drawing.Point(68, 90);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(50, 35);
            this.c.TabIndex = 18;
            this.c.Text = "C";
            this.c.UseVisualStyleBackColor = true;
            // 
            // ce
            // 
            this.ce.CausesValidation = false;
            this.ce.Location = new System.Drawing.Point(12, 90);
            this.ce.Name = "ce";
            this.ce.Size = new System.Drawing.Size(50, 35);
            this.ce.TabIndex = 17;
            this.ce.Text = "CE";
            this.ce.UseVisualStyleBackColor = true;
            // 
            // CalcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 286);
            this.Controls.Add(this.div);
            this.Controls.Add(this.erase);
            this.Controls.Add(this.c);
            this.Controls.Add(this.ce);
            this.Controls.Add(this.mult);
            this.Controls.Add(this.num9);
            this.Controls.Add(this.num8);
            this.Controls.Add(this.num7);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.num6);
            this.Controls.Add(this.num5);
            this.Controls.Add(this.num4);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.num3);
            this.Controls.Add(this.num2);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.dot);
            this.Controls.Add(this.num0);
            this.Controls.Add(this.negation);
            this.Controls.Add(this.expression);
            this.Name = "CalcForm";
            this.Text = "Calculator";
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
    }
}

