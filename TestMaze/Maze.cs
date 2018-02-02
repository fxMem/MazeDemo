using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    public struct MazePosition
    {
        public int Y { get; set; }

        public int X { get; set; }

        public MazePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MazePosition GetAdjacentPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new MazePosition(X - 1, Y);
                case Direction.Right:
                    return new MazePosition(X + 1, Y);
                case Direction.Up:
                    return new MazePosition(X, Y - 1);
                case Direction.Down:
                    return new MazePosition(X, Y + 1);
            }

            throw new ArgumentException($"Unknown direction {direction}!");
        }
    }

    public class Maze
    {
        private MazePosition _startPosition;
        private Cell[,] _map;

        public int Height
        {
            get
            {
                return _map.GetLength(0);
            }
        }

        public int Width
        {
            get
            {
                return _map.GetLength(1);
            }
        }

        public MazePosition StartRobotPosition
        {
            get
            {
                return _startPosition;
            }
        }

        public Cell this[int y, int x]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    return null;
                }

                return _map[y, x];
            }
        }

        public Cell this[MazePosition pos]
        {
            get
            {
                return this[pos.Y, pos.X];
            }
        }

        public Maze(Cell[,] map)
        {
            _map = map;
        }

        public Maze(string map)
        {
            ParceMap(map);
        }

        private void ParceMap(string map)
        {
            var lines = map.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var height = lines.Length;

            // Assuming all lines have equal length
            var width = lines[0].Length;

            _map = new Cell[height, width];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    var nextChar = line[j];
                    var cell = new Cell(CellType.Empty);
                    if (nextChar == '#')
                    {
                        cell = new Cell(CellType.Wall);
                    }
                    else if (nextChar == 'E')
                    {
                        cell = new Cell(CellType.Exit);
                    }

                    _map[i, j] = cell;

                    if (nextChar == 'R')
                    {
                        _startPosition = new MazePosition(j, i);
                    }
                }
            }
        }
    }
}
