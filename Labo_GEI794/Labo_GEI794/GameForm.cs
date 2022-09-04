using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Labo_GEI794
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
    }

    class GameView
    {
        public GameForm oGameForm;
        int x = 0;
        Image2D iPlayerImage = new Image2D();
        public GameView()
        {
            oGameForm = new GameForm();

            oGameForm.Paint += new System.Windows.Forms.PaintEventHandler(this.GameRenderer);
            Bitmap myBitmap = new Bitmap("./Resources/TilesSheet.png");
            Rectangle cloneRect = new Rectangle(0, 96, 96, 96);
            iPlayerImage.Image = myBitmap.Clone(cloneRect, myBitmap.PixelFormat);
        }

        public void Show() { Application.Run(oGameForm); }
        public void Render()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Refresh();
                });
        }

        public void Close()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Close();
                });
        }
        private void GameRenderer(object sender, PaintEventArgs e)
        {
            x++;
            
            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
            Rectangle myRectangle = new Rectangle(x, 0, 20, 20);
                        
            //e.Graphics.FillRectangle(yellowBrush, myRectangle);
            e.Graphics.DrawImage(iPlayerImage.Image, new Point(x, 0));
            oGameForm.BackColor = Color.Black;
            Render();
        }

    }

    class GameEngine
    {
        public GameView oView;
        const int MS_PER_FRAME = 10;
        public GameEngine()
        {
            oView = new GameView();
            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void GameLoop()
        {
            while (true)
            {
                DateTime start = DateTime.UtcNow;

                processInput(); // lecture de clavier
                update(); // déplacement, collision
                render(); // dessin, son

                //Le sleep est pas bon, le temps donne quelque chose qui n'est pas bon
                double timeDiff = (DateTime.UtcNow - start).TotalMilliseconds + MS_PER_FRAME;
                Thread.Sleep(Convert.ToInt32(timeDiff));// startFrameTime + MS_PER_FRAME - endFrameTime);
            }
        }

        private void processInput()
        {

        }

        private void update()
        {

        }

        private void render()
        {
            oView.Render();
        }
    }
}
