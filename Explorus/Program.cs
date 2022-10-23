/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using Explorus.Controller;

namespace Explorus
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            IGameEngine ge = new IGameEngine();

            ge.Start();


            //User can use this function to stop the game from external ressources
            /*Thread.Sleep(10000);
            ge.Stop();*/ 
        }
    }
}
