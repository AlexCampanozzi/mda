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
using Explorus.Controller;
using Explorus.Model.Behavior;

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

        private string mapPath = "carte valid.png";

        static Map()
        {

        }
        
        private Map()
        {
            load();   
        }

        private void load()
        {
            objectList = createObjectsFromMapFactory(mapParser(new Bitmap("./Resources/Maps/" + mapPath)));
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

        public string MapPath { get => this.mapPath; set => this.mapPath = value; }

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

            List<Point> potentialToxicSlimes = new List<Point>();

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
                                potentialToxicSlimes.Add(new Point(x * 96, y * 96));
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
            GameEngine engine = GameEngine.GetInstance();
            Random random = new Random();

            int offset = random.Next(0, 2);

            for(int i = 0; i < engine.GetLevelState().Slimes; i++)
            {
                int strategyid = (i + offset) % 3;

                IBehavior strategy;
                switch (strategyid)
                {
                    case 0:
                        strategy = new AmbushStrategy();
                        break;
                    case 1:
                        strategy = new PursuitStrategy();
                        break;
                    default:
                        strategy = new DualStrategy();
                        break;

                }
                int id = random.Next(0, potentialToxicSlimes.Count - 1);
                Point point = potentialToxicSlimes[id];
                potentialToxicSlimes.RemoveAt(id);
                compoundGameObject.add(new ToxicSlime(point, loader, getID(), strategy), point.X/96, point.Y/96);
            }
            return compoundGameObject.getComponentGameObjetList();
        }
        public void generateMapFromCompound(CompoundGameObject compoundObjects)
        {
            objectList = compoundObjects.getComponentGameObjetList();
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

        public void resetMap()
        {

            Map map = Instance;

            lock (compoundGameObject)
            {
                //instance = null;
                int len = compoundGameObject.getComponentGameObjetList().Count;
                for (int i = 0; i < len; i++)
                {
                    compoundGameObject.getComponentGameObjetList()[len - i - 1].removeItselfFromGame();
                }
            }
            load();
        }

        public void setMap(CompoundGameObject compoundObject)
        {
            CompoundGameObject compoundGameObject = compoundObject;
        }
    }
}