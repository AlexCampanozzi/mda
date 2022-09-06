using System.Collections;
using System.Drawing;
using System;
using System.Collections.Generic;
using Explorus.Properties;

// This class is a singleton(on essaye)
namespace Explorus
{
    public sealed class Map 
    {
        private static Map instance = null;
        private static readonly object padlock = new object();

        private List<GameObject> objectList;
        private objectTypes[,] typeMap = null;

        private Map()
        {
            objectList = createObjectsFromMap(mapParser(new Bitmap("./Resources/map.png")));
        }

        public static Map GetInstance()
        {
            if(instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Map();
                    }
                }
            }
            return instance;
        }

        private objectTypes[,] mapParser(Bitmap mapImage)
        {
            GraphicsUnit pixelRef = GraphicsUnit.Pixel;
            RectangleF mapSizeFloat;
            mapSizeFloat = mapImage.GetBounds(ref pixelRef);
            int[] mapSize = new int[2]{ (int)mapSizeFloat.Width, (int)mapSizeFloat.Height };

            typeMap = new objectTypes[mapSize[0], mapSize[1]];

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

            GameObject currentObject;

            for (int x = 0; x< typeMap.GetLength(0); x++)
            {
                for(int y = 0; y < typeMap.GetLength(1); y++)
                {
                    if (typeMap[x, y] != objectTypes.Empty && typeMap[x,y] != objectTypes.Door)
                    {
                        switch(typeMap[x, y])
                        {
                            case objectTypes.Player:
                                currentObject = new Slimus(x * 96, y * 96);
                                break;
                            case objectTypes.Wall:
                                currentObject = new Wall(x*96, y * 96);
                                break;
                            case objectTypes.Key:
                                currentObject = new Key(x * 96, y * 96);
                                break;
                            default:
                                continue;
                        }
                        currentObject.SetGridPosition(new Point(x, y));
                        oMap.Add(currentObject);

                    }
                }
            }

            return oMap;
        }
        public List<GameObject> GetObjectList()
        { 
            return objectList; 
        }

        public objectTypes[,] GetTypeMap() 
        { 
            return typeMap;
        }
    }
}