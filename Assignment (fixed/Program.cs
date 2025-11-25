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
            string[,] grid = new string[dim, dim];
            const int mid = dim / 2;
            bool gameOver = false;
            int moves = 0;
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    grid[i, j] = "1";
                }
            }
            //creates coordinate history linked list to store previous player positions for undo function
            var history = new LinkedList<Coordinate>();
            //stores the path for searches
            LinkedList<Coordinate> path = new LinkedList<Coordinate>();

            //creates obstacles at random points on the grid
            Random rnd = new Random();
            for (int j = 0; j < 20; j++)
            {
                grid[rnd.Next(dim), rnd.Next(dim)] = "0";
            }
            //creates player and exit on grid
            grid[rnd.Next(dim), rnd.Next(dim)] = "E";
            grid[mid, mid] = "P";

            
            Coordinate player = new Coordinate(mid, mid);
            draw(grid, dim);
            //BreadthFirstSearch.BFS(grid, dim, player);

            //main game loop
            while (!gameOver)
            {
                //displays grid and instructions
                Console.Clear();
                Console.WriteLine("Press Q to quit, Backspace to undo, P to BFS, O to DFS and WASD to move");
                Console.WriteLine("Player Moves: " + moves);
                draw(grid, dim);

                //gets players current location from the grid (in case it changed externally)
                for (int i = 0; i < dim; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (grid[i, j] == "P")
                        {
                            player = new Coordinate(i, j);
                        }
                    }
                }

                //gets user input and determines what should happen
                var inp1 = Console.ReadKey();
                if (inp1.Key == ConsoleKey.Q)
                {
                    gameOver = true;
                    continue;
                }
                //undo moves
                if (inp1.Key == ConsoleKey.Backspace)
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
                if (inp1.Key == ConsoleKey.P)
                {
                    SearchNode playerNode = new SearchNode(player);
                    BreadthFirstSearch.BFS(grid, dim, playerNode, ref path);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                if (inp1.Key == ConsoleKey.O)
                {
                    SearchNode playerNode = new SearchNode(player);
                    DepthFirstSearch.DFS(grid, dim, playerNode, ref path);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }

                //variables to determine what the next move should be
                int newRow = player.Row;
                int newCol = player.Col;

                //creates the value for the new potential move
                if (inp1.Key == ConsoleKey.W) newRow--;
                if (inp1.Key == ConsoleKey.S) newRow++;
                if (inp1.Key == ConsoleKey.A) newCol--;
                if (inp1.Key == ConsoleKey.D) newCol++;

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
                    if (grid[newRow, newCol] != "0" && grid[newRow, newCol] != "*")
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
        static void draw(string[,] grid, int dim)
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}

