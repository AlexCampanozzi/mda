﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Explorus.Model.Behavior
{
    internal class Behaviors
    {

        public Behaviors()
        {}
        public Direction random(ToxicSlime slime, Direction lastDir)
        {
            Random rnd = new Random();
            bool wall = true;
            Direction newDir = new Direction(0, 0);
            // continue path forward until hit wall
            Point gridPosition = slime.GetGridPosition();
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();
            if (lastDir != null)
            {
                objectTypes firstChoice = gridMap[gridPosition.X + lastDir.X, gridPosition.Y + lastDir.Y];
                if (firstChoice != objectTypes.Wall && firstChoice != objectTypes.Door)
                {
                    newDir = lastDir;
                    wall = false;
                }
            }
            if(wall)
            {
                while (wall)
                {
                    int newDirID = rnd.Next(0, 3);
                    switch (newDirID)
                    {
                        case 0:
                            newDir = new Direction(0, 1);
                            break;
                        case 1:
                            newDir = new Direction(1, 0);
                            break;
                        case 2:
                            newDir = new Direction(0, -1);
                            break;
                        case 3:
                            newDir = new Direction(-1, 0);
                            break;
                    }
                    objectTypes nextgrid = gridMap[gridPosition.X + newDir.X, gridPosition.Y + newDir.Y];
                    if (nextgrid != objectTypes.Wall && nextgrid != objectTypes.Door)
                    {
                        wall = false;
                    }
                }
            }
            return newDir;
        }
        public (Direction, int, int, Direction) pursuit(ToxicSlime slime, bool towards)
        {
            objectTypes[,] gridmap = Map.Instance.GetTypeMap();
            //Collider objCollider = obj.GetCollider()
            Slimus slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));

            Direction newDir = new Direction(0, 0);
            bool SlimusFound = false;
            Point gridPosition = slime.GetGridPosition(); 
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();

            // to return last player pos and direction
            Direction SlimusDir = new Direction(0,0);
            int SlimusPosX =0, SlimusPosY = 0;

            Point PlayerGrid = slimus.GetGridPosition();
            objectTypes nextGrid;

                // find slimus in corridor
            // check UP
            for (int i = gridPosition.Y; i > 0; i--)
            {
                nextGrid = gridMap[gridPosition.X, i];
                if (gridPosition.X == PlayerGrid.X && i == PlayerGrid.Y)
                {
                    newDir = new Direction(0, -1);
                    SlimusDir = slimus.last_direction;
                    SlimusFound = true;
                    SlimusPosX = gridPosition.X;
                    SlimusPosY = i;
                    break;
                }
                else if (nextGrid == objectTypes.Wall || nextGrid == objectTypes.Door)
                {
                    break;
                }
            }
            // check DOWN
            if (!SlimusFound)
            {
                for (int i = gridPosition.Y; i < gridMap.GetLength(1); i++)
                {
                    nextGrid = gridMap[gridPosition.X, i];
                    if (gridPosition.X == PlayerGrid.X && i == PlayerGrid.Y)
                    {
                        newDir = new Direction(0, 1);
                        SlimusFound = true;
                        SlimusDir = slimus.last_direction;
                        SlimusPosX = gridPosition.X;
                        SlimusPosY = i;
                    }
                    else if (nextGrid == objectTypes.Wall || nextGrid == objectTypes.Door)
                    {
                        break;
                    }
                }
            }
            if (!SlimusFound)
            {
                // check RIGHT
                for (int i = gridPosition.X; i < gridMap.GetLength(0); i++)
                {
                    nextGrid = gridMap[i, gridPosition.Y];
                    if (i == PlayerGrid.X && gridPosition.Y == PlayerGrid.Y)
                    {
                        newDir = new Direction(1, 0);
                        SlimusFound = true;
                        SlimusDir = slimus.last_direction;
                        SlimusPosX = i;
                        SlimusPosY = gridPosition.Y;
                        break;
                    }
                    else if (nextGrid == objectTypes.Wall || nextGrid == objectTypes.Door)
                    {
                        break;
                    }
                }
            }
            if (!SlimusFound)
            {
                // check LEFT
                for (int i = gridPosition.X; i > 0; i--)
                {
                    nextGrid = gridMap[i, gridPosition.Y];
                    if (i == PlayerGrid.X && gridPosition.Y == PlayerGrid.Y)
                    {
                        newDir = new Direction(-1, 0);
                        SlimusFound = true;
                        SlimusDir = slimus.last_direction;
                        SlimusPosX = i;
                        SlimusPosY = gridPosition.Y;
                        break;
                    }
                    else if (nextGrid == objectTypes.Wall || nextGrid == objectTypes.Door)
                    {
                        break;
                    }
                }
            }

            // if not found add last seen logic
            if (!SlimusFound)
            {
                (int lastX, int lastY, Direction lastDirection) = slime.getLastPlayerInfo();
                if (lastDirection != null)
                {
                    if (lastDirection.X != 0 && lastDirection.Y != 0)
                    {
                        // random fonction should let toxic go towards last seen position (with direction first choice)
                        if (gridPosition.X == lastX && gridPosition.Y == lastY)
                        {
                            newDir = lastDirection;
                            //reset to 0,0 to mark as lost
                            SlimusDir = new Direction(0, 0);
                        }
                    }
                }
            }
            if (!towards)
            {
                newDir = new Direction(-1 * newDir.X, -1 * newDir.Y);
            }
            return (newDir, SlimusPosX, SlimusPosY, SlimusDir);
        }
    }
}
