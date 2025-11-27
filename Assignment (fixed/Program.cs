using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //initialises grid and values on grid
            const int dim = 12;
            const int mid = dim / 2;
            bool gameOver = false;
            int moves = 0;
            Coordinate exit = new Coordinate(0, 0);
            int rows = 12;
            int cols = 12;
            string[,] grid = new string[rows, cols];

            //creates coordinate history linked list to store previous player positions for undo function
            var history = new LinkedList<Coordinate>();
            //stores the path for searches
            LinkedList<Coordinate> path = new LinkedList<Coordinate>();

            Coordinate player = new Coordinate(mid, mid);

            //user can choose to load or generate random map
            Console.WriteLine("A = Load Map... B = Generate Random 12x12 map");
            var inp = Console.ReadKey();
            if (inp.Key == ConsoleKey.B)
            {
                for (int i = 0; i < dim; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        grid[i, j] = "1";
                    }
                }

                //creates obstacles at random points on the grid
                Random rnd = new Random();
                for (int j = 0; j < 20; j++)
                {
                    grid[rnd.Next(dim), rnd.Next(dim)] = "0";
                }
                //creates player and exit on grid
                grid[rnd.Next(dim), rnd.Next(dim)] = "E";
                grid[mid, mid] = "P";
                player = new Coordinate(mid, mid);
            }
            if (inp.Key == ConsoleKey.A)
            {
                //takes map name and creates variable for the file path used later to load map
                Console.Write("Enter map name:");
                string mapName = Console.ReadLine();
                string projectPath = @"C:\Users\harry\source\repos\Assignment (fixed\Assignment (fixed\maps";
                string filePath = Path.Combine(projectPath, mapName + ".txt");
                LoadMap(filePath, ref grid, ref rows, ref cols, ref player, ref exit);
            }

            //main game loop
            while (!gameOver)
            {
                //displays grid and instructions
                Console.Clear();
                Console.WriteLine("Press Q to quit, Backspace to undo, P to BFS, O to DFS, I to HillClimbSearch and WASD to move");
                Console.WriteLine("Player Moves: " + moves);
                draw(grid, rows, cols);

                //gets players current location from the grid (in case it changed externally)
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (grid[i, j] == "P")
                        {
                            player = new Coordinate(i, j);
                        }
                    }
                }

                //gets user input and determines what should happen
                var inp2 = Console.ReadKey();
                if (inp2.Key == ConsoleKey.Q)
                {
                    gameOver = true;
                    continue;
                }
                //undo moves
                if (inp2.Key == ConsoleKey.Backspace)
                {
                    Coordinate last = default;
                    //checks if there is a previous move to undo
                    if (history.GetFront(ref last))
                    {
                        //remove stored position from history
                        history.PopFront();

                        grid[player.Row, player.Col] = "1";

                        //move back to previous position
                        player = last;
                        grid[player.Row, player.Col] = "P";
                        moves++;
                    }
                }
                if (inp2.Key == ConsoleKey.P)
                {
                    //creates a search node for player so it can be passed to BFS algo
                    SearchNode playerNode = new SearchNode(player);
                    BreadthFirstSearch.BFS(grid, dim, playerNode, ref path);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                if (inp2.Key == ConsoleKey.O)
                {
                    SearchNode playerNode = new SearchNode(player);
                    DepthFirstSearch.DFS(grid, dim, playerNode, ref path);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                if (inp2.Key == ConsoleKey.I)
                {
                    SearchNode playerNode = new SearchNode(player);
                    HillClimbSearch.HCS(grid, dim, playerNode, ref path);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }

                //variables to determine what the next move should be
                int newRow = player.Row;
                int newCol = player.Col;

                //creates the value for the new potential move
                if (inp2.Key == ConsoleKey.W) newRow--;
                if (inp2.Key == ConsoleKey.S) newRow++;
                if (inp2.Key == ConsoleKey.A) newCol--;
                if (inp2.Key == ConsoleKey.D) newCol++;

                //checks if new move is within the grid bounds
                if (newRow >= 0 && newRow < dim && newCol >= 0 && newCol < dim)
                {
                    //checks if new move is exit, obstacle or valid move
                    if (grid[newRow, newCol] == "E")
                    {
                        //game win state achieved
                        Console.Clear();
                        Console.WriteLine("you win!");
                        gameOver = true;
                        continue;
                    }
                    //checks that move isnt an obstacle or already visited
                    if (newRow >= 0 && newRow < dim && newCol >= 0 && newCol < dim)
                    {
                        //save current position
                        history.PushFront(player);

                        //moves player uses coordinate struct
                        grid[player.Row, player.Col] = "*";
                        player = new Coordinate(newRow, newCol);
                        grid[player.Row, player.Col] = "P";
                        moves++;
                    }
                }

            }
        }

        //method to display grid 
        static void draw(string[,] grid, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }
        //method to load map
        static bool LoadMap(string filePath,ref string[,] grid,ref int rows,ref int cols,ref Coordinate start,ref Coordinate exit)
        {
            //try catch to inform user if there was an error loading map
            try
            {
                //reads entire text file and puts it into an array of strings by line. lines[0] = 1st line of txt file
                string[] lines = File.ReadAllLines(filePath);
                //gets grid size
                string[] dim = lines[0].Split(' ');
                rows = int.Parse(dim[0]);
                cols = int.Parse(dim[1]);

                //gets start and exit pos
                string[] startPos = lines[1].Split(' ');
                start = new Coordinate(int.Parse(startPos[0]), int.Parse(startPos[1]));

                string[] goalPos = lines[2].Split(' ');
                exit = new Coordinate(int.Parse(goalPos[0]), int.Parse(goalPos[1]));

                //2D array to store map
                grid = new string[rows, cols];
                //reads and assigns actual grid layout
                for (int r = 0; r < rows; r++)
                {
                    string[] rowValues = lines[3 + r].Split(' ');
                    for (int c = 0; c < cols; c++)
                    {
                        grid[r, c] = rowValues[c];
                    }
                }

                // place start and goal
                grid[start.Row, start.Col] = "P";
                grid[exit.Row, exit.Col] = "E";

                return true;
            }
            catch
            {
                Console.WriteLine("Error loading map file!");
                return false;
            }
        }
    }
}
