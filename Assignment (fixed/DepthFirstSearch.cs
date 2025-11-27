using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal class DepthFirstSearch 
    {
        public static void DFS(string[,] grid, int dim, SearchNode player, ref LinkedList<Coordinate> path)
        {
            //creates open and closed list
            var openList = new Stack<SearchNode>();
            var closedList = new LinkedList<SearchNode>();
            //adds players starting location to openlist
            openList.PushStack(player);
            SearchNode current = player;

            //while will return true when exit is found breaking the loop
            while (grid[current.Position.Row, current.Position.Col] != "E")
            {
                openList.PopStack(ref current);

                int r = current.Position.Row;
                int c = current.Position.Col;

                // Offset directions: N, E, S, W
                int[] dr = { -1, 0, 1, 0 };
                int[] dc = { 0, 1, 0, -1 };

                // Try 4 neighbors
                for (int i = 0; i < 4; i++)
                {
                    int nr = r + dr[i];
                    int nc = c + dc[i];

                    // Bounds check
                    if (nr < 0 || nr >= dim || nc < 0 || nc >= dim)
                        continue;

                    // Skip walls
                    if (grid[nr, nc] == "0")
                        continue;

                    // Skip visited
                    if (closedList.ContainsNodeWithCoordinate(nr, nc))
                        continue;

                    // Create new SearchNode for neighbor
                    var next = new SearchNode(
                        new Coordinate(nr, nc),
                        pred: current
                    );

                    openList.PushStack(next);
                }
                closedList.PushBack(current);
            }

            Console.WriteLine(current);
            Console.WriteLine($"exit found at:  {current.Position.getCoordinate()}");
            path = SearchUtilities.BuildPathList(current);
            Console.WriteLine("Path:");
            foreach (var coord in path.Enumerate())
            {
                Console.WriteLine(coord.getCoordinate());
            }

            string projectPath = @"C:\Users\harry\source\repos\Assignment (fixed\Assignment (fixed\paths";

            string filePath = Path.Combine(projectPath, "DFS.txt");

            StringBuilder sb = new StringBuilder();
            foreach (var coord in path.Enumerate())
            {
                sb.AppendLine(coord.getCoordinate());
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine("path saved to", filePath);
        }
    }
}
