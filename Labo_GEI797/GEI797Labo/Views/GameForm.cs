using System.Windows.Forms;

namespace GEI797Labo
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.DoubleBuffered = true;
            SetStyle(
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.UserPaint,
                   true);
        }
    }
}
