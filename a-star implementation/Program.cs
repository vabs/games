/*
 * References: 
 *  1. http://www.codeproject.com/Articles/118015/Fast-A-Star-2D-Implementation-for-C
 *  2. http://en.wikipedia.org/wiki/A*_search_algorithm
 *  3. http://stackoverflow.com/questions/5704427/how-can-a-variable-typed-as-an-enum-take-a-value-that-is-out-of-range-of-its-ele
 *  4. http://msdn.microsoft.com/en-us/library/system.timers.timer.aspx
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication4
{
    class Program
    {
        static bool quit = false;
        static int fps = 1;
        static int gridSize = 200;
        static MyPathNode[,] grid;
        static IEnumerable<MyPathNode> path = null;
        static List<MySpiderNode> spiders = null;
        static int spiderCount = 6;

        static void Main(string[] args)
        {
            int frame = 0;
            //Whether or not to cap the frame rate
            bool cap = true;

            System.Timers.Timer t = new System.Timers.Timer();
            t.Start(); 
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();


            while (path == null)
            {
                grid = testGrid();
                //printGrid();

                //watch.Start();
                path = findPath();
                //watch.Stop();
            }
            watch.Start();
            path = findPath();
            watch.Stop();

            Console.WriteLine("Time: " +  watch.ElapsedMilliseconds);
            Console.Read();
            /*
            #region If Able to find a path

            if (path != null)
            {
                // Setting up Spiders
                placeSpiders();
                // End Spiders


                MyPathNode current = new MyPathNode { X = path.First().X, Y = path.First().Y };
                int currentIndex = 0;
                int pathLength = path.Count();

                while (!quit)
                {
                    Console.Clear();
                    
        
                    frame++;

                    grid[current.X, current.Y].IsTheseus = true;
                    printGrid();
                    fightSpiders();
                    foreach (MySpiderNode spider in spiders)
                    {
                        spider.moveSpiders(grid, gridSize);
                        if (!isThesusSafe(current, spider)) {
                            quit = true;
                            Console.WriteLine("Thesus Dead");
                            Console.WriteLine("Quitting in 10 seconds");
                            Console.WriteLine("Thesus: " +  current.X + " " + current.Y);
                            Console.WriteLine("Spider: " + spider.X + " " + spider.Y);
                            Thread.Sleep(10000);
                        }
                    }

                    if ((currentIndex + 1) < pathLength)
                    {
                        grid[current.X, current.Y].IsTheseus = false;
                        currentIndex++;
                        current = path.ElementAt(currentIndex);

                        //If we want to cap the frame rate
                        if ((cap == true) && (t.Interval < 1000 / fps))
                        {
                            //Sleep the remaining frame time
                            Thread.Sleep(1000);
                            Console.WriteLine("Hello!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Reached Goal");
                        Console.WriteLine("Quitting in 2 seconds");
                        Thread.Sleep(2000);
                        quit = true;
                    }

                }
            }
            #endregion
             */
        }

        private static void fightSpiders()
        {
            foreach (MySpiderNode killer in spiders)
            {
                if (killer.setType == MySpiderNode.IsType.Big)
                {
                    foreach (MySpiderNode opponent in spiders)
                    {
                        if ((opponent.setType == MySpiderNode.IsType.Small || opponent.setType == MySpiderNode.IsType.Medium) && !opponent.IsDead)
                        {

                            if ((Math.Abs(killer.X - opponent.X) + Math.Abs(killer.Y - opponent.Y)) <= 2)
                            {
                                opponent.IsDead = true;
                                grid[killer.X, killer.Y].IsWall = true;
                                killer.X = opponent.X;
                                killer.Y = opponent.Y;
                                grid[killer.X, killer.Y].IsSpider = "big";

                            }

                        }
                    }
                }
                else if (killer.setType == MySpiderNode.IsType.Medium)
                {
                    foreach (MySpiderNode opponent in spiders)
                    {
                        if ((opponent.setType == MySpiderNode.IsType.Small) && !opponent.IsDead)
                        {

                            if ((Math.Abs(killer.X - opponent.X) + Math.Abs(killer.Y - opponent.Y)) <= 2)
                            {
                                opponent.IsDead = true;
                                grid[killer.X, killer.Y].IsWall = true;
                                killer.X = opponent.X;
                                killer.Y = opponent.Y;
                                grid[killer.X, killer.Y].IsSpider = "medium";

                            }

                        }
                    }
                }
                else { }
            }
        }

        private static void placeSpiders()
        {
            Random r = new Random();
            spiders = new List<MySpiderNode>();
            while (spiderCount > 0)
            {
                int xl = r.Next() % gridSize;
                int yl = r.Next() % gridSize;
                MySpiderNode s;

                if (grid[xl, yl].IsWall)
                {
                    if (r.Next() % 3 == 0)
                    {
                        grid[xl, yl].IsSpider = "big";
                        grid[xl, yl].IsWall = false;

                        s = new MySpiderNode() { X = xl, Y = yl, IsDead = false };
                        s.setType = MySpiderNode.IsType.Big;
                        spiders.Add(s);
                    }
                    else if (r.Next() % 3 == 1)
                    {
                        grid[xl, yl].IsSpider = "medium";
                        grid[xl, yl].IsWall = false;

                        s = new MySpiderNode() { X = xl, Y = yl, IsDead = false };
                        s.setType = MySpiderNode.IsType.Medium;
                        spiders.Add(s);

                    }

                    else if (r.Next() % 3 == 2)
                    {
                        grid[xl, yl].IsSpider = "small";
                        grid[xl, yl].IsWall = false;

                        s = new MySpiderNode() { X = xl, Y = yl, IsDead = false };
                        s.setType = MySpiderNode.IsType.Small;
                        spiders.Add(s);

                    }
                    spiderCount--;
                }
            }
        }

        private static bool isThesusSafe(MyPathNode thesus, MySpiderNode spider) {

            if ((Math.Abs(spider.X - thesus.X) + Math.Abs(spider.Y - thesus.Y)) <= 2)
            {
                return false;
            }
            return true;
        }

        private unsafe static MyPathNode[,] testGrid()
        {
            Random rnd = new Random();
            MyPathNode[,] grid = new MyPathNode[gridSize, gridSize];

            {
                // setup grid with walls
                for (int x = 0; x < gridSize; x++)
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        Boolean isWall = ((y % 2) != 0) && (rnd.Next(0, 10) != 8);

                        grid[x, y] = new MyPathNode()
                        {
                            IsWall = isWall,
                            X = x,
                            Y = y,
                        };
                    }
                }
            }

            return grid;
        }

        private static IEnumerable<MyPathNode> findPath()
        {
            grid[gridSize / 2, gridSize / 2].IsWall = false;

            MySolver<MyPathNode, Object> aStar = new MySolver<MyPathNode, Object>(grid);
            IEnumerable<MyPathNode> path = aStar.Search(new Point(0, 0), new Point(gridSize / 2, gridSize / 2), null);

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            {
                aStar.Search(new Point(0, 0), new Point(gridSize - 2, gridSize - 2), null);
            }
            watch.Stop();

            Console.WriteLine("Pathfinding took " + watch.ElapsedMilliseconds + "ms to complete.");
            return path;
        }

        private static void printGrid()
        {
            Console.Clear();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j].IsWall)
                    {
                        Console.Write("#");
                    }
                    else if (grid[i, j].IsTheseus == true)
                    {
                        Console.Write((char)0x2560);
                    }
                    else if (grid[i, j].IsSpider == "big")
                    {
                        Console.Write("B");
                    }
                    else if (grid[i, j].IsSpider == "medium")
                    {
                        Console.Write("M");
                    }
                    else if (grid[i, j].IsSpider == "small")
                    {
                        Console.Write("S");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
