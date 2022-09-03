using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Explorus
{

    public class GameView
    {
        private double fps;
        private GameForm oGameForm;
        private int rectanglePosition = 0;

        private Image2D iPlayerImage = new Image2D();

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;

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
            rectanglePosition += 1;

            Graphics graphic = e.Graphics;
            graphic.Clear(Color.Black);
            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

            // Create rectangle.
            Rectangle rect = new Rectangle(rectanglePosition, 0, 20, 20);

            // Fill rectangle to screen.
            //graphic.FillRectangle(yellowBrush, rect);

            e.Graphics.DrawImage(iPlayerImage.Image, new Point(rectanglePosition, 20));

            oGameForm.Text = "Labo GEI794 – FPS " + Convert.ToString(getFPS());

        }
        public double getFPS()
        {
            return fps;
        }

        public void setFPS(double value)
        {
            fps = value;
        }

        public Keys getCurrentInput()
        {
            Keys currentInput = oGameForm.getCurrentInput();
            oGameForm.resetCurrentInput();
            return currentInput;
        }
                
    }
}
