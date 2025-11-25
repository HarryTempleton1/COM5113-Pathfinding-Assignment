using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Assignment__fixed;

namespace Assignment__fixed
{
    internal class BreadthFirstSearch
    {
        public static void BFS(string[,] grid,int dim, SearchNode player, ref LinkedList<Coordinate> path)
        {
            //creates open and closed lists to store cooardinates for search algorithms
            var openList = new LinkedQueue<SearchNode>();
            var closedList = new LinkedList<SearchNode>();

            //adds players starting location to openlist
            openList.Enqueue(player);
            SearchNode current = player;

            //while will return true when exit is found breaking the loop
            while (grid[current.Position.Row,current.Position.Col] != "E")
            {
                //dequeues current coord from open list
                openList.Dequeue(ref current);

                //gets row and collumn of current and saves it to r c ints for neighbour checking later
                int r = current.Position.Row;
                int c = current.Position.Col;

                //stores row/col offsets for each direction (-1, 0 = north, 1, 0 = south ect)
                int[] dRow = { -1, 0, 1, 0 };
                int[] dCol = { 0, 1, 0, -1 };

                //for loop iterates 4 times because 4 directions
                for (int i = 0; i < 4; i++)
                {
                    //nr/nc = new neighbour row/col
                    //takes current row col and applies the offsets initialised above prevents using 4 different if statements 
                    int nr = r + dRow[i];
                    int nc = c + dCol[i];

                    //checks if in bounds of grid
                    if (nr < 0 || nr >= dim || nc < 0 || nc >= dim)
                        continue;

                    //skip walls
                    if (grid[nr, nc] == "0")
                        continue;

                    //skip visited
                    if (closedList.ContainsNodeWithCoordinate(nr, nc))
                        continue;

                    //creates new coord for created nrow ncol values. makes all of their predecessors the current
                    var next = new SearchNode(
                        new Coordinate(nr, nc),
                        pred: current
                    );

                    openList.Enqueue(next);
                }
                closedList.PushBack(current);

                ////creates new collummn and row variables to check cardinal directions for new coords to move
                ////checks north
                //int nr = current.Position.Row - 1;
                //int nc = current.Position.Col;

                ////checks if new move is valid or already discovered (on closed list)
                //if(nr>= 0 && grid[nr,nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                //{
                //    //queues valid move onto the open list 
                //    openList.Enqueue(new Coordinate(nr,nc));
                //}

                ////checks east
                //nr = current.Position.Row;
                //nc = current.Position.Col + 1;
                //if (nc < dim && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                //{
                //    openList.Enqueue(new Coordinate(nr, nc));
                //}

                ////checks south
                //nr = current.Position.Row + 1;
                //nc = current.Position.Col;
                //if (nr < dim  && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                //{
                //    openList.Enqueue(new Coordinate(nr, nc));
                //}

                ////checks west
                //nr = current.Position.Row;
                //nc = current.Position.Col - 1;
                //if (nc >= 0 && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
                //{
                //    openList.Enqueue(new Coordinate(nr, nc));
                //}
                //marks current node as visited

            }
            Console.WriteLine(current);
            Console.WriteLine($"exit found at:  {current.Position.getCoordinate()}");
            path = SearchUtilities.BuildPathList(current);
            Console.WriteLine("Path:");
            foreach(var coord in path.Enumerate())
            {
                Console.WriteLine(coord.getCoordinate());
            }
        }
    }
}

