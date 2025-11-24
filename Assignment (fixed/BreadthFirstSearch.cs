using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal class BreadthFirstSearch
    {
        public static void BFS(string[,] grid,int dim, LinkedQueue<Coordinate> openList, LinkedList<Coordinate> closedList, Coordinate player)
        {
            //adds players starting location to openlist
            openList.Enqueue(player);
            Coordinate current = player;

            //while will return true when exit is found breaking the loop
            while (grid[current.Row,current.Col] != "E")
            {
                //dequeues current coord from open list
                openList.Dequeue(ref current);
                
                //creates new collummn and row variables to check cardinal directions for new coords to move
                //checks north
                int nr = current.Row - 1;
                int nc = current.Col;
                //checks if new move is valid or already discovered (on closed list)
                if(nr>= 0 && grid[nr,nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                {
                    //queues valid move onto the open list 
                    openList.Enqueue(new Coordinate(nr,nc));
                }

                //checks east
                nr = current.Row;
                nc = current.Col + 1;
                if (nc < dim && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                {
                    openList.Enqueue(new Coordinate(nr, nc));
                }

                //checks south
                nr = current.Row + 1;
                nc = current.Col;
                if (nr < dim  && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                {
                    openList.Enqueue(new Coordinate(nr, nc));
                }

                //checks west
                nr = current.Row;
                nc = current.Col - 1;
                if (nc >= 0 && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                {
                    openList.Enqueue(new Coordinate(nr, nc));
                }
                //marks current node as visited
                closedList.PushFront(current);
            }
            Console.WriteLine(current);
            Console.WriteLine($"exit found at:  {current.getCoordinate()}");
        }
    }
}

