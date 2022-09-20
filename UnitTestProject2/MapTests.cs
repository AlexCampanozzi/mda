/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Explorus;
using System.Drawing;
using System.Windows.Forms;
using Explorus.Model;
/*
namespace ExplorusTests

{
    [TestClass]
    public class MapTests
    {
        Map map = Map.GetInstance();

        [TestMethod]
        public void GetMapInstance()
        {
            Assert.IsNotNull(map);
        }

        [TestMethod]
        public void CorrectMapGeneration()
        {
            bool containsPlayer = false;
            bool containsGem = false;
            bool containsDoor = false;
            bool containsSlime = false;

            objectTypes[,] typeMap = map.GetTypeMap();

            for (int x = 0; x < typeMap.GetLength(0); x++)
            {
                for (int y = 0; y < typeMap.GetLength(1); y++)
                {
                    if (typeMap[x, y] != objectTypes.Empty)
                    {
                        switch (typeMap[x, y])
                        {
                            case objectTypes.Player:
                                containsPlayer = true;  
                                break;
                            case objectTypes.Gem:
                                containsGem = true;
                                break;
                            case objectTypes.Slime:
                                containsSlime = true;
                                break;
                            case objectTypes.Door:
                                containsDoor = true;    
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }

            if (containsPlayer && containsGem && containsDoor && containsSlime)
            {
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MapSize()
        {
            Assert.AreEqual(120, map.GetTypeMap().GetLength(0) * map.GetTypeMap().GetLength(1));
        }

        [TestMethod]
        public void RemoveObjectFromMap()
        {
            objectTypes[,] typeMap = map.GetTypeMap();
            map.removeObjectFromMap(3, 2);
            Assert.AreEqual(objectTypes.Empty, typeMap[3,2]);
        }
    }
}*/
