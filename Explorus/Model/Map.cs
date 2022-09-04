using System.Collections;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace Explorus
{
    public class Map
    {

        public List<GameObject> objectMap;
        public Map(Bitmap mapImage)
        {
            objectMap = createObjectsFromMap(mapParser(mapImage));
        }

        private objectTypes[,] mapParser(Bitmap mapImage)
        {
            GraphicsUnit pixelRef = GraphicsUnit.Pixel;
            RectangleF mapSizeFloat;
            mapSizeFloat = mapImage.GetBounds(ref pixelRef);
            int[] mapSize = new int[2]{ (int)mapSizeFloat.Width, (int)mapSizeFloat.Height };

            objectTypes[,] typeMap = new objectTypes[mapSize[0], mapSize[1]];

            for(int x = 0; x<mapSize[0]; x++)
            {
                for(int y = 0; y<mapSize[1]; y++)
                {
                    typeMap[x, y] = colorToType(mapImage.GetPixel(x, y));
                }
            }
            return typeMap;
        }

        private objectTypes colorToType(Color color)
        {
            switch (color.Name)
            {
                case "ff000000"://Black
                    return objectTypes.Wall;
                case "ffffffff"://White
                    return objectTypes.Empty;
                case "ffff0000"://red
                    return objectTypes.Door;
                case "ffffff00"://yellow
                    return objectTypes.Key;
                case "ff0000ff"://blue
                    return objectTypes.Player;
                default:
                    return objectTypes.Empty;
            }
        }

        private List<GameObject> createObjectsFromMap(objectTypes[,] typeMap)
        {
            List<GameObject> oMap = new List<GameObject>();           

            int i = 0;

            for (int x = 0; x< typeMap.GetLength(0); x++)
            {
                for(int y = 0; y < typeMap.GetLength(1); y++)
                {
                    if (typeMap[x, y] != objectTypes.Empty && typeMap[x,y] != objectTypes.Door)
                    {
                        switch(typeMap[x, y])
                        {
                            case objectTypes.Player:
                                oMap.Add(new Slimus(x * 96, y * 96));
                                break;
                            case objectTypes.Wall:
                                oMap.Add(new Wall(x*96, y * 96));
                                break;
                            case objectTypes.Key:
                                oMap.Add(new Key(x * 96, y * 96));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return oMap;
        }
    }
}