using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math;
using org.mariuszgromada.math.mxparser;

namespace LabsCSharp
{
    public partial class MainForm : Form
    {
        GraphBuilder builder;
        const int cellSize = 15;
        public MainForm()
        {
            InitializeComponent();
            builder = new GraphBuilder(grid.Width, grid.Height, cellSize);
            grid.Image = builder.Reset();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateExpression(inputTextBox.Text)) return;
            grid.Image = builder.BuildGraph(inputTextBox.Text);
        }

        private bool ValidateExpression(string expression)
        {
            Function f = new Function(expression.Replace("y", "f(x)"));
            if (!f.checkSyntax())
            {
                MessageBox.Show("Ошибка в выражении.");
                return false;
            }
            return true;
        }
    }
}
