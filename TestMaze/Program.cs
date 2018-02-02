using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    

    class Program
    {
        static void Main(string[] args)
        {
            CheckMap(@"
#######
#     #
# # E #
## #  #
# R#  #
#     #
#######");
        }

        static void CheckMap(string mapData)
        {
            var maze = new Maze(mapData);
            var robot = new Robot(maze);

            TraceHelper.Print(maze, robot);

            Console.ReadKey();

            try
            {
                PathFinder.FindExit(robot);

                Console.WriteLine("Exit found!");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
