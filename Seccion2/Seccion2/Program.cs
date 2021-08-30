using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Seccion2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            SearchPath.searchPath.CreateBlocks();
            SearchPath.searchPath.BFS();
            SearchPath.searchPath.CreatePath();

            SearchPath.searchPath.solution();
            Console.WriteLine("El programa tomó " + timer.ElapsedMilliseconds + " milisegundos en encontrar el camino mas corto"); 
        }
    }
}
