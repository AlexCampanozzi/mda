/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Collections;
using System.Drawing;
using System;
using System.Collections.Generic;
using Explorus.Properties;

// This class is a singleton
namespace Explorus.Model
{
    public sealed class Map 
    {
        private static readonly Map instance = new Map();
        private static readonly object padlock = new object();

        private List<GameObject> objectList;
        private CompoundGameObject compoundGameObject;
        private objectTypes[,] typeMap = null;

        private int lastID = -1;

        static Map()
        {

        }
        
        private Map()
        {
            objectList = createObjectsFromMapFactory(mapParser(new Bitmap("./Resources/map_valid.png")));
        }

        public static Map Instance
        {
            get
            {
                return instance;
            }
            /*if(instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Map();
                    }

                    return instance;
                }
            }*/
        }

        public int getID()
        {
            return lastID += 1;
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
                    return objectTypes.Slime;
                case "ff0000ff"://blue
                    return objectTypes.Player;
                case "ff00ff00"://green
                    return objectTypes.ToxicSlime;
                default:
                    return objectTypes.Empty;
            }
        }

        private List<GameObject> createObjectsFromMapFactory(objectTypes[,] typeMap)
        {
            List<GameObject> oMap = new List<GameObject>();    
            ImageLoader loader = new ImageLoader();

            compoundGameObject = new CompoundGameObject();

            for (int x = 0; x< typeMap.GetLength(0); x++)
            {
                for(int y = 0; y < typeMap.GetLength(1); y++)
                {
                    if (typeMap[x, y] != objectTypes.Empty)
                    {
                        switch(typeMap[x, y])
                        {
                            case objectTypes.Player:
                                compoundGameObject.add(new Slimus(new Point(x * 96, y * 96), loader, getID()), x, y);
                                break;
                            case objectTypes.Wall:
                                compoundGameObject.add(new Wall(new Point(x* 96, y * 96), loader, getID()), x, y);
                                break;
                            case objectTypes.Gem:
                                compoundGameObject.add(new Gem(new Point(x * 96, y * 96), loader, getID()), x, y);
                                break;
                            case objectTypes.Slime:
                                compoundGameObject.add(new Slime(new Point(x * 96, y * 96), loader, getID()), x, y);
                                break;
                            case objectTypes.Door:
                                compoundGameObject.add(new Door(new Point(x * 96, y * 96), loader, getID()), x, y);
                                break;
                            case objectTypes.ToxicSlime:
                                compoundGameObject.add(new ToxicSlime(new Point(x * 96, y * 96), loader, getID()), x, y);
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }

            return compoundGameObject.getComponentGameObjetList();
        }

        public void removeObjectFromMap(int x, int y)
        {
            typeMap[x, y] = objectTypes.Empty;
        }
        public List<GameObject> GetObjectList()
        { 
            return objectList; 
        }

        public objectTypes[,] GetTypeMap() 
        { 
            return typeMap;
        }

        public CompoundGameObject GetCompoundGameObject()
        {
            return compoundGameObject;
        }
    }
}