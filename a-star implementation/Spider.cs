using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public class MySpiderNode {
        IsType _type = IsType.Big;
        
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Boolean IsDead { get; set; }
        public enum IsType { Big, Medium, Small };

        public IsType setType
        {
            get { return _type; }
            set { _type = value; }
        }

        public void moveSpiders(MyPathNode[,] grid, int gridSize)
        {
            Random r = new Random();
            if (r.Next() % 2 == 0)
            { // move spiders
                int action = (r.Next() % 4) + 1;
                doStuff(grid, action, 1, gridSize);
            }

            else
            { // jump spiders
                int action = (r.Next() % 4) + 1;
                doStuff(grid, action, 2, gridSize);
            }
        }

        public void doStuff(MyPathNode[,] maze, int action, int steps, int dimension)
        {
            switch (action)
            {

                case 1: // Move / Jump Right
                    if (X < (dimension - steps) && maze[(X + steps), Y].IsWall)
                    {
                        maze[X,Y].IsWall = true;
                        X += steps;
                        if (setType == IsType.Big)
                        {
                            maze[X, Y].IsSpider = "big";
                            maze[X, Y].IsWall = false;
                        }
                        else if (setType == IsType.Medium)
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "medium";
                        }
                        else
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "small";
                        }
                        break;
                    }
                    break;
                case 2: // Move / Jump left
                    if ((X - steps) > 0 && maze[(X-steps), Y].IsWall)
                    {
                        maze[X, Y].IsWall = true;
                        X -= steps;
                        if (setType == IsType.Big)
                        {
                            maze[X, Y].IsSpider = "big";
                            maze[X, Y].IsWall = false;
                        }
                        else if (setType == IsType.Medium)
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "medium";
                        }
                        else
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "small";
                        }
                        break;
                    }
                    break;
                case 3: // Move / Jump Top
                    if ((Y - steps) > 0 && maze[X, (Y - steps)].IsWall)
                    {
                        maze[X, Y].IsWall = true;
                        Y -= steps;
                        if (setType == IsType.Big)
                        {
                            maze[X, Y].IsSpider = "big";
                            maze[X, Y].IsWall = false;
                        }
                        else if (setType == IsType.Medium)
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "medium";
                        }
                        else
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "small";
                        }
                        break;
                    }
                    break;
                case 4: // Move / Jump Bottom
                    if (Y < (dimension - steps) && maze[X, (Y + steps)].IsWall)
                    {
                        maze[X, Y].IsWall = true;
                        Y += steps;
                        if (setType == IsType.Big)
                        {
                            maze[X, Y].IsSpider = "big";
                            maze[X, Y].IsWall = false;
                        }
                        else if (setType == IsType.Medium)
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "medium";
                        }
                        else
                        {
                            maze[X, Y].IsWall = false;
                            maze[X, Y].IsSpider = "small";
                        }
                        break;
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
