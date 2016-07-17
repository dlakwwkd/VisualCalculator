using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VisualCalculator.Calculator
{
    class ShuntingYardAlgorithm
    {

        public async Task Run(Panel _panel, List<IObject> _infixExpr)
        {
            Init(_panel);
            infixExpr_ = _infixExpr;

            await SetInput();
            await Task.Delay(500);


            await Temp();
        }



        private void Init(Panel _panel)
        {
            inputPanel_.Controls.Clear();
            inputPanel_.FlowDirection = FlowDirection.LeftToRight;
            inputPanel_.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            inputPanel_.Location = new Point(_panel.Width - 5, 5);
            inputPanel_.Padding = new Padding(3);
            inputPanel_.Size = new Size(5, 5);
            inputPanel_.AutoSize = true;
            inputPanel_.BackColor = Color.Brown;

            infixPanel_.Controls.Clear();
            infixPanel_.FlowDirection = FlowDirection.LeftToRight;
            infixPanel_.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            infixPanel_.Location = new Point(_panel.Width - 5, 30);
            infixPanel_.Padding = new Padding(3);
            infixPanel_.Size = new Size(5, 5);
            infixPanel_.AutoSize = true;
            infixPanel_.BackColor = Color.Brown;

            postfixPanel_.Controls.Clear();
            postfixPanel_.FlowDirection = FlowDirection.RightToLeft;
            postfixPanel_.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            postfixPanel_.Location = new Point(5, 30);
            postfixPanel_.Padding = new Padding(3);
            postfixPanel_.Size = new Size(5, 5);
            postfixPanel_.AutoSize = true;
            postfixPanel_.BackColor = Color.Brown;

            _panel.Controls.Clear();
            _panel.Controls.Add(inputPanel_);
            _panel.Controls.Add(infixPanel_);
            _panel.Controls.Add(postfixPanel_);
        }

        private async Task SetInput()
        {
            foreach (var obj in infixExpr_)
            {
                var label = new Label();
                label.BackColor = Color.Snow;
                label.Margin = new Padding(1);
                label.AutoSize = true;
                label.Text = obj.Name;
                inputPanel_.Controls.Add(label);
                await Task.Delay(200);
            }
        }

        private Task Temp()
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }



        private List<IObject>   infixExpr_      = new List<IObject>();
        private List<IObject>   postfixExpr_    = new List<IObject>();
        private FlowLayoutPanel inputPanel_     = new FlowLayoutPanel();
        private FlowLayoutPanel infixPanel_     = new FlowLayoutPanel();
        private FlowLayoutPanel postfixPanel_   = new FlowLayoutPanel();
    }
}
