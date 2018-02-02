using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    public static class TraceHelper
    {
        public static void Print(Maze maze, Robot robot)
        {
            Console.Clear();
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    var mark = " ";
                    if (robot.Position.X == x && robot.Position.Y == y)
                    {
                        mark = "R";
                    }
                    else
                    {
                        var next = maze[y, x];
                        switch (next.Type)
                        {
                            case CellType.Empty:
                                break;
                            case CellType.Wall:
                                mark = "#";
                                break;
                            case CellType.Exit:
                                mark = "E";
                                break;
                            default:
                                break;
                        }
                    }

                    Console.Write(mark);
                }

                Console.WriteLine();
            }
        }
    }
}
