using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    public class Cell
    {
        public CellType Type { get; private set; }

        public Cell(CellType type)
        {
            Type = type;
        }
    }

    public enum CellType
    {
        Empty,
        Wall,
        Exit
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public interface IRobot
    {
        // Sensors
        Cell CurrentCell { get; }
        Cell AdjacentCell(Direction direction);

        // Moving
        void Move(Direction direction);
    }

    public static class PathFinder
    {
        public static void FindExit(IRobot robot)
        {
            var currentCell = robot.CurrentCell;
            if (currentCell.Type == CellType.Exit)
            {
                return;
            }

            var ai = new RobotAI(robot);
            ai.SearchForExit();
        }
    }

    public class Robot : IRobot
    {
        private Maze _maze;
        private MazePosition _currentPosition;

        public MazePosition Position
        {
            get
            {
                return _currentPosition;
            }
        }

        public Cell CurrentCell
        {
            get
            {
                return _maze[_currentPosition];
            }
        }

        public Robot(Maze maze)
        {
            if (maze == null)
            {
                throw new ArgumentNullException("maze");
            }

            _maze = maze;
            _currentPosition = _maze.StartRobotPosition;
        }

        public Cell AdjacentCell(Direction direction)
        {
            return _maze[_currentPosition.GetAdjacentPosition(direction)];
        }

        public void Move(Direction direction)
        {
            _currentPosition = _currentPosition.GetAdjacentPosition(direction);

            // For simplicity putting it here.
            TraceHelper.Print(_maze, this);
            Console.ReadKey();
        }
    }

    
    
}
