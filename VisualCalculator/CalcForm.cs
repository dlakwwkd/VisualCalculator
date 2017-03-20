using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisualCalculator
{
    public partial class CalcForm : Form
    {
        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public CalcForm()
        {
            InitializeComponent();
            Calculator = new Calculator.Calculator(this);

            KeyDown         += new KeyEventHandler(CalcForm_KeyDown);
            SyaPanel.Paint  += new PaintEventHandler(panel_sya_Paint);
        }

        public Panel    InputPanel  => panel_input;
        public Panel    SyaPanel    => panel_sya;
        public Panel    TreePanel   => panel_exprTree;
        public Panel    ResultPanel => panel_result;
        public bool     InputEnable { get; set; } = true;
        public string   Expr
        {
            get => expression.Text;
            set => expression.Text = value;
        }

        public bool     GetX(out double x) => double.TryParse(inputX.Text, out x);
        public bool     GetY(out double y) => double.TryParse(inputY.Text, out y);
        public bool     GetZ(out double z) => double.TryParse(inputZ.Text, out z);
        public void     SetResult(double result)
        {
            label_result.Text = result.ToString();
        }

        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        // - Input Handler
        //------------------------------------------------------------------------------------
        private void CalcForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Oemtilde: negation_Click(sender, e);  break;
                    case Keys.Oemplus:  plus_Click(sender, e);      break;
                    case Keys.D8:       mult_Click(sender, e);      break;
                    case Keys.D9:       bracketL_Click(sender, e);  break;
                    case Keys.D0:       bracketR_Click(sender, e);  break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad0:  case Keys.D0:           num0_Click(sender, e);  break;
                    case Keys.NumPad1:  case Keys.D1:           num1_Click(sender, e);  break;
                    case Keys.NumPad2:  case Keys.D2:           num2_Click(sender, e);  break;
                    case Keys.NumPad3:  case Keys.D3:           num3_Click(sender, e);  break;
                    case Keys.NumPad4:  case Keys.D4:           num4_Click(sender, e);  break;
                    case Keys.NumPad5:  case Keys.D5:           num5_Click(sender, e);  break;
                    case Keys.NumPad6:  case Keys.D6:           num6_Click(sender, e);  break;
                    case Keys.NumPad7:  case Keys.D7:           num7_Click(sender, e);  break;
                    case Keys.NumPad8:  case Keys.D8:           num8_Click(sender, e);  break;
                    case Keys.NumPad9:  case Keys.D9:           num9_Click(sender, e);  break;
                    case Keys.Add:      /* Shift + Oemplus */   plus_Click(sender, e);  break;
                    case Keys.Subtract: case Keys.OemMinus:     minus_Click(sender, e); break;
                    case Keys.Multiply: /* Shift + D8 */        mult_Click(sender, e);  break;
                    case Keys.Divide:   case Keys.OemQuestion:  div_Click(sender, e);   break;
                    case Keys.Decimal:  case Keys.OemPeriod:    dot_Click(sender, e);   break;
                    case Keys.X:                                x_Click(sender, e);     break;
                    case Keys.Y:                                y_Click(sender, e);     break;
                    case Keys.Z:                                z_Click(sender, e);     break;
                    case Keys.Back:                             erase_Click(sender, e); break;
                    case Keys.Delete:                           ce_Click(sender, e);    break;
                    case Keys.Escape:                           c_Click(sender, e);     break;
                    case Keys.Enter:    case Keys.Oemplus:      enter_Click(sender, e); break;
                }
            }
        }

        private void num0_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('0'); }
        private void num1_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('1'); }
        private void num2_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('2'); }
        private void num3_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('3'); }
        private void num4_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('4'); }
        private void num5_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('5'); }
        private void num6_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('6'); }
        private void num7_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('7'); }
        private void num8_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('8'); }
        private void num9_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('9'); }
        private void plus_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('+'); }
        private void minus_Click(object sender, EventArgs e)    { if (InputEnable) Calculator.AddValue('-'); }
        private void mult_Click(object sender, EventArgs e)     { if (InputEnable) Calculator.AddValue('*'); }
        private void div_Click(object sender, EventArgs e)      { if (InputEnable) Calculator.AddValue('/'); }
        private void x_Click(object sender, EventArgs e)        { if (InputEnable) Calculator.AddValue('x'); }
        private void y_Click(object sender, EventArgs e)        { if (InputEnable) Calculator.AddValue('y'); }
        private void z_Click(object sender, EventArgs e)        { if (InputEnable) Calculator.AddValue('z'); }
        private void dot_Click(object sender, EventArgs e)      { if (InputEnable) Calculator.AddValue('.'); }
        private void bracketL_Click(object sender, EventArgs e) { if (InputEnable) Calculator.AddValue('('); }
        private void bracketR_Click(object sender, EventArgs e) { if (InputEnable) Calculator.AddValue(')'); }
        private void negation_Click(object sender, EventArgs e) { if (InputEnable) Calculator.NegationProc(); }
        private void erase_Click(object sender, EventArgs e)    { if (InputEnable) Calculator.RemoveValue(); }
        private void ce_Click(object sender, EventArgs e)       { if (InputEnable) Calculator.Init(); }
        private void c_Click(object sender, EventArgs e)        { if (InputEnable) Calculator.Init(); }
        private void enter_Click(object sender, EventArgs e)    { if (InputEnable) Calculator.EnterProc(); }

        private void button_calc_Click(object sender, EventArgs e) => Calculator.CalcProc();
        private void button_reset_Click(object sender, EventArgs e)
        {
            Calculator.ResetProc();
            inputX.Text = "";
            inputY.Text = "";
            inputZ.Text = "";
            label_result.Text = "";
            TreePanel.Controls.Clear();
            TreePanel.Invalidate();
        }

        // - Draw Handler
        //------------------------------------------------------------------------------------
        private void panel_sya_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            var h = expression.Height;

            var s1 = new Point(0, h);
            var e1 = new Point(p.Width, h);
            g.DrawLine(Pen, s1.X, s1.Y, e1.X, e1.Y);

            var arcSize = new Size(p.Width, p.Height - h);
            var arc1 = new Rectangle(new Point(p.Width / -2, h), arcSize);
            var arc2 = new Rectangle(new Point(p.Width / +2, h), arcSize);
            g.DrawArc(Pen, arc1, -90, +90);
            g.DrawArc(Pen, arc2, -90, -90);

            var s2 = new Point(p.Width / 2, (p.Height + h) / 2);
            var e2 = new Point(p.Width / 2, p.Height);
            g.DrawLine(Pen, s2.X, s2.Y, e2.X, e2.Y);
        }

        //------------------------------------------------------------------------------------
        private Calculator.Calculator   Calculator  { get; } = null;
        private Pen                     Pen         { get; } = new Pen(Color.Black);
    }
}
