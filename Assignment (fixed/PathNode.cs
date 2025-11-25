using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Assignment__fixed
{
    //public class PathNode
    //{
    //    private Coordinate _location;
    //    private int _cost = 0; //heuristics
    //    private int _score = 0; //terrain
    //    private PathNode? _predecessor = null;
    //}

    public class SearchNode
    {
        public Coordinate Position { get; }
        public int Cost { get; set; } // cumulative terrain cost (for Dijkstra and A*)
        public int Score { get; set; } // heuristic function (for Hillclimbing onwards)
        public int Estimate => Cost + Score; // Estimated path cost (for A*)

        public SearchNode? Predecessor { get; set; }

        // contructor
        public SearchNode(Coordinate pos, int cost = 0, int score = 0, SearchNode? pred = null)
        {
            Position = pos;
            Cost = cost;
            Score = score;
            Predecessor = pred;
        }
    }

    public static class SearchUtilities
    {
        // Builds a path list by walking the predecessor references between nodes
        // and extracting the coodinates from the visited nodes.
        public static LinkedList<Coordinate> BuildPathList(SearchNode? goal)
        {
            LinkedList<Coordinate> path = new LinkedList<Coordinate>();

            // start at the goal and walk backwards
            SearchNode node = goal;
            while (node != null)
            {
                path.PushFront(node.Position);
                node = node.Predecessor;
            }

            return path;
        }

        public static int ManhattanDistance(Coordinate current, Coordinate goal)
        {
            // TODO: This would be a good place to put your heuristic function

            return 0;
        }
        public static bool ContainsNodeWithCoordinate(this LinkedList<SearchNode> list, int row, int col)
        {
            foreach (var node in list.Enumerate())
            {
                if (node.Position.Row == row && node.Position.Col == col)
                    return true;
            }
            return false;
        }

        //public static void CheckRules(SearchNode current, LinkedQueue<SearchNode> openList, LinkedQueue<SearchNode> closedList, int dim, string[] grid)
        //{
        //    //checks north
        //    int nr = current.Position.Row - 1;
        //    int nc = current.Position.Col;
        //    //checks if new move is valid or already discovered (on closed list)
        //    if (nr >= 0 && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
        //    {
        //        //queues valid move onto the open list 
        //        new SearchNode add = new SearchNode(current.Position.
        //        openList.Enqueue(new SearchNode(nr, nc));
        //    }

        //    //checks east
        //    nr = current.Position.Row;
        //    nc = current.Position.Col + 1;
        //    if (nc < dim && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
        //    {
        //        openList.Enqueue(new Coordinate(nr, nc));
        //    }

        //    //checks south
        //    nr = current.Position.Row + 1;
        //    nc = current.Position.Col;
        //    if (nr < dim && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
        //    {
        //        openList.Enqueue(new Coordinate(nr, nc));
        //    }

        //    //checks west
        //    nr = current.Position.Row;
        //    nc = current.Position.Col - 1;
        //    if (nc >= 0 && grid[nr, nc] != "0" && !closedList.Contains(new Coordinate(nr, nc)))
        //    {
        //        openList.Enqueue(new Coordinate(nr, nc));
        //    }
        //}
    }

}
