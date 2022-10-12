using GEI797Labo.Controllers;
using GEI797Labo.Models;
using GEI797Labo.Models.Definitions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GEI797Labo.Views
{
    internal class GameView : IGameView
    {
        private const string DEFAULT_TITLE = "GEI797 Laboratoire";
        private const int HEADER_HEIGHT = 40;
        private const int PADDING_WIDTH = 18;

        private readonly IGameController oController;
        private readonly IGameModel oModel;
        private readonly GameForm oGameForm;

        public GameView(IGameModel model, IGameController controller)
        {
            oModel = model;
            oController = controller;

            int width = oController.DefaultSize + PADDING_WIDTH;
            int height = oController.DefaultSize + HEADER_HEIGHT;

            oGameForm = new GameForm()
            {
                Text = DEFAULT_TITLE,
                Size = new Size(width, height),
                MinimumSize = new Size(width, height),
                MaximumSize = new Size(width, height),
            };
            oGameForm.Paint += GameForm_Renderer;
            oGameForm.KeyUp += GameForm_KeyUp;
            oGameForm.KeyDown += GameForm_KeyDown;
            oGameForm.FormClosing += GameForm_Closing;
        }

        #region IGameView

        public void CloseWindow()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate { oGameForm.Close(); });
        }

        public void Render()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate { oGameForm.Refresh(); });
        }

        public void Show()
        {
            if (!oGameForm.Visible)
                Application.Run(oGameForm);
        }

        #endregion

        #region GameForm Event

        private void GameForm_Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            for (int index = 0; index < oModel.Shapes.Count; index++)
            {
                Shape shape = oModel.Shapes.GetItem(index);
                Rectangle rect = new Rectangle((int)shape.Position_X, (int)shape.Position_Y, shape.Width, shape.Height);
                Pen pen = new Pen(shape.Color);

                if (shape.GetShapeType == ShapeType.Rectangle)
                    g.DrawRectangle(pen, rect);
                else if (shape.GetShapeType == ShapeType.Circle)
                    g.DrawEllipse(pen, rect);
            }
        }

        private void GameForm_Closing(object sender, EventArgs e)
        {
            oController.StopGame();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            oController.User_KeyDown(e);
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            oController.User_KeyUp(e);
        }


        #endregion
    }
}
