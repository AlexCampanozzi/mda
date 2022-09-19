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
        private List<GameView> subscribers = new List<GameView>();
        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            this.KeyDown += new KeyEventHandler(readKeyboardInput);
            //this.KeyPress += new KeyPressEventHandler(keypressed);
        }

        private void readKeyboardInput(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            currentInput = e.KeyCode;
            for(int i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].inputSubscription(currentInput);
            }
        }

        public void SubscribeToInput(GameView newSub)
        {
            if(!subscribers.Contains(newSub))
            {
                subscribers.Add(newSub);
            }
        }

        public void resetCurrentInput()
        {
            currentInput = Keys.None;
        }
    }
}
