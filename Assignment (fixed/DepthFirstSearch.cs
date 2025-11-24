using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal class DepthFirstSearch
    {
        public static void DFS(string[,] grid, int dim, LinkedQueue<Coordinate> openList, LinkedList<Coordinate> closedList, Coordinate player)
        {
            //adds players starting location to openlist
            openList.Enqueue(player);
            Coordinate current = player;

            //while will return true when exit is found breaking the loop
            while (grid[current.Row, current.Col] != "E")
            {
                
            }
        }
    }
}
