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

        private bool isPaused = false;
        private bool isOver = false;

        private Image iPausedImage;
        private Image iEndImage;

        private Map map;
        private Header header;

        private bool formOpen;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            oGameForm.Paint += GameRenderer;
            formOpen = true;
            oGameForm.FormClosed += new FormClosedEventHandler(FormClosed);
            map = Map.GetInstance(); //caller le singleton de map
            header = Header.GetInstance();

            iPausedImage = Image.FromFile("./Resources/pause.png");
            // TODO: use the interface size instead
            iPausedImage = resizeImage(iPausedImage, new Size(500, 500));

            iEndImage = Image.FromFile("./Resources/EndOfLevel.png");
        }

        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            formOpen = false;
        }

        public bool notClosed()
        {
            return formOpen;
        }

        public void Show() { Application.Run(oGameForm); }

        public void Render()
        {
            try
            {
                if (oGameForm.Visible)
                    oGameForm.BeginInvoke((MethodInvoker)delegate
                    {
                        oGameForm.Refresh();
                    });
            }
            catch
            {

            }
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
            if (isOver)
            {
                e.Graphics.DrawImage(iEndImage, new Point(0, 0));
            }
            else if (isPaused)
            {
                e.Graphics.DrawImage(iPausedImage, new Point(0, 0));
            }
            else
            {
                Graphics graphic = e.Graphics;
                graphic.Clear(Color.Black);
                
                //oGameForm.Text = "Labo GEI794 – FPS " + Convert.ToString(getFPS());
                int xSize = map.GetTypeMap().GetLength(0) * 96;
                int ySize = map.GetTypeMap().GetLength(1) * 96 + 96;

                float xScaling = ((float)oGameForm.Size.Width - 16) / (float)xSize;
                float yScaling = ((float)oGameForm.Size.Height - 42) / (float)ySize;

                float minScale = Math.Min(xScaling, yScaling);
                int xOffset = 0;
                int yOffset = 0;

                if (xScaling > yScaling)
                {
                    xOffset = (oGameForm.Size.Width - 16 - (int)(yScaling * (float)xSize)) / 2;
                }
                else
                {
                    yOffset = (oGameForm.Size.Height - (int)(xScaling * (float)ySize)) / 2;
                }

                List<GameObject> compoundGameObjectList = map.GetCompoundGameObject().getComponentGameObjetList();
                for (int i = 0; i < compoundGameObjectList.Count; i++)
                {
                    Image img = compoundGameObjectList[i].GetImage();
                    e.Graphics.DrawImage(img, new Rectangle(new Point((int)(compoundGameObjectList[i].GetPosition().X * minScale) + xOffset, (int)(compoundGameObjectList[i].GetPosition().Y * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))));
                }
                e.Graphics.DrawImage(header.getHeaderImage(), new Rectangle(new Point(xOffset, yOffset + (int)(60.0 * yScaling)), new Size((int)(1152.0 * minScale), (int)(96.0 * minScale))));             
            }
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

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public bool getIsPaused()
        {
            return isPaused;
        }
        public void setIsPaused(bool state)
        {
            isPaused = state;
        }

        public bool getIsOver()
        {
            return isOver;
        }

        public void setIsOver(bool state)
        {
            isOver = state;
        }
        public Map getMap()
        {
            return map;
        }

        public Header getHeader()
        {
            return header;
        }
    }
}
