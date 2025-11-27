using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    public class HillClimbSearch
    {
        public static void HCS(string[,] grid, int dim, SearchNode player, ref LinkedList<Coordinate> path)
        {
            //creates open and closed lists to store cooardinates for search algorithms
            var openList = new LinkedQueue<SearchNode>();
            var closedList = new LinkedList<SearchNode>();

            //gets coord for goal so score can be calculated
            Coordinate goal = FindGoal(grid, dim);

            //adds players starting location to openlist
            openList.Enqueue(player);
            SearchNode current = player;

            //while will return true when exit is found breaking the loop
            while (grid[current.Position.Row, current.Position.Col] != "E")
            {
                int r = current.Position.Row;
                int c = current.Position.Col;

                int[] dRow = { -1, 0, 1, 0 };
                int[] dCol = { 0, 1, 0, -1 };

                //Temporary list to hold neighbors
                var neighbors = new LinkedList<SearchNode>();

                // Generate neighbors
                for (int i = 0; i < 4; i++)
                {
                    int nr = r + dRow[i];
                    int nc = c + dCol[i];

                    if (nr < 0 || nr >= dim || nc < 0 || nc >= dim)
                        continue;

                    if (grid[nr, nc] == "0")
                        continue;

                    if (ContainsCoordinate(closedList, nr, nc))
                        continue;
                    int cost = Score(new Coordinate(nr, nc), goal);

                    neighbors.PushBack(new SearchNode(
                        pos: new Coordinate(nr, nc),
                        cost: cost,      // distance to goal
                        pred: current
                    ));
                }

                if (neighbors.IsEmpty())
                {
                    Console.WriteLine("Hill climb failed – no neighbors available.");
                    break;
                }

                SearchNode best = SelectBestNeighbor(neighbors, goal);

                if (Score(best.Position, goal) >= Score(current.Position, goal))
                {
                    Console.WriteLine("Local maximum reached.");
                    break;
                }

                closedList.PushBack(current);
                current = best;
            }
            Console.WriteLine(current);
            Console.WriteLine($"exit found at:  {current.Position.getCoordinate()}");
            path = SearchUtilities.BuildPathList(current);
            Console.WriteLine("Path:");
            foreach (var coord in path.Enumerate())
            {
                Console.WriteLine(coord.getCoordinate());
            }

            //writes path to txt file
            string projectPath = @"C:\Users\harry\source\repos\Assignment (fixed\Assignment (fixed\paths";

            string filePath = Path.Combine(projectPath, "HCS.txt");

            StringBuilder sb = new StringBuilder();
            foreach (var coord in path.Enumerate())
            {
                sb.AppendLine(coord.getCoordinate());
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine("path saved to", filePath);
        }

        private static Coordinate FindGoal(string[,] grid, int dim)
        {
            for (int r = 0; r < dim; r++)
                for (int c = 0; c < dim; c++)
                    if (grid[r, c] == "E")
                        return new Coordinate(r, c);

            throw new Exception("Grid has no exit 'E'.");
        }

        //calculates score. 
        private static int Score(Coordinate a, Coordinate b)
        {
            return (a.Row - b.Row) + (a.Col - b.Col);
        }

        private static SearchNode SelectBestNeighbor(LinkedList<SearchNode> list, Coordinate goal)
        {
            SearchNode? best = default!;
            bool first = true;

            foreach (var node in list.Enumerate())
            {
                if (first)
                {
                    best = node;
                    first = false;
                }
                else
                {
                    if (Score(node.Position, goal) < Score(best.Position, goal))
                        best = node;
                }
            }

            return best!;
        }

        private static bool ContainsCoordinate(LinkedList<SearchNode> list, int row, int col)
        {
            foreach (var node in list.Enumerate())
            {
                if (node.Position.Row == row && node.Position.Col == col)
                    return true;
            }
            return false;
        }
    }
}
