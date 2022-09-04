using System.Collections;
using System.Drawing;

namespace Explorus
{
    public class Image2D
    {
        int ID, Type;
        Bitmap Image;
        // Use this for initialization
        public Image2D(int iID, int iType, Bitmap iImage)
        {
            ID = iID;
            Type = iType;
            Image = iImage;
        }
    }
}