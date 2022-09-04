using System.Collections;
using System.Drawing;

namespace Explorus
{
    public class Image2D
    {
        int id, type;
        Bitmap image;
        // Use this for initialization
        public Image2D(int iID, int iType, Bitmap iImage)
        {
            id = iID;
            type = iType;
            image = iImage;
        }

        public Bitmap getImage()
        {
            return image;
        }
    }
}