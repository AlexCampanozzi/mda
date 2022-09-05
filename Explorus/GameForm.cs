using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus
{
    public partial class GameForm : Form
    {
        public Keys currentInput;
        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            this.KeyDown += new KeyEventHandler(readKeyboardInput);
        }

        private void readKeyboardInput(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            currentInput = e.KeyCode;
        }

        public Keys getCurrentInput()
        {
            return currentInput;
        }

        public void resetCurrentInput()
        {
            currentInput = Keys.None;
        }
    }
}
