/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Explorus.Controller;

namespace Explorus
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            /*
            Console.WriteLine("***Command Pattern Demonstration*** \n");

            RemoteControl invoker = new RemoteControl();

            Game gameName = new Game("Golf");

            GameStartCommand gameStartCommand = new GameStartCommand(gameName);

            Console.WriteLine("**Starting the game and upgrading the level 3 times**");
            invoker.SetCommand(gameStartCommand);
            invoker.ExecuteCommand();
            invoker.ExecuteCommand();
            invoker.ExecuteCommand();
            invoker.ExecuteCommand();

            Console.WriteLine("\n Undoing the previous command now");
            invoker.UndoCommand();
            invoker.UndoCommand();

            Console.WriteLine("\n Starting the game again. Then stopping it and undoing the stop operation");
            invoker.ExecuteCommand();
            invoker.ExecuteCommand();
            invoker.UndoAll();*/


            
            IGameEngine ge = new IGameEngine();

            ge.Start();


            //User can use this function to stop the game from external ressources
            /*Thread.Sleep(10000);
            ge.Stop();*/
        }
    }

    //Receiver class
    public class Game
    {
        string gameName;
        public int level;
        public Game(string gameName)
        {
            this.gameName = gameName;
            level = -1;
            Console.WriteLine($"Game started");
        }
        public void DisplayLevel()
        {
            Console.WriteLine($"Current level is set to {level}");
        }
        public void UpLevel()
        {
            level++;
            Console.WriteLine("level upgraded");
        }
        public void DownLevel()
        {
            level--;
            Console.WriteLine("level downgraded");
        }
        public void Finish()
        {
            Console.WriteLine($"---The game of {gameName} is over.---");
        }
    }
}
