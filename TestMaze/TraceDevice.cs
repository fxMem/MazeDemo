using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    public class TraceDevice
    {
        struct TraceCoordinates
        {
            public int X { get; set; }

            public int Y { get; set; }

            public TraceCoordinates(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        class TraceCoordinatesComparer : IEqualityComparer<TraceCoordinates>
        {
            public bool Equals(TraceCoordinates x, TraceCoordinates y)
            {
                return x.X == y.X && x.Y == y.Y;
            }

            public int GetHashCode(TraceCoordinates obj)
            {
                return obj.X + 11 * obj.Y;
            }
        }

        private Stack<TraceCoordinates> _path;
        private Dictionary<TraceCoordinates, bool> _scannedCells;
        private TraceCoordinates _currentPosition;

        public TraceDevice()
        {
            _path = new Stack<TraceCoordinates>();
            _scannedCells = new Dictionary<TraceCoordinates, bool>(new TraceCoordinatesComparer());
            _currentPosition = new TraceCoordinates(0, 0);

            _path.Push(_currentPosition);
            _scannedCells.Add(_currentPosition, true);
        }

        public void MoveTo(Direction direction)
        {
            var targetCoords = GetNextCellCoordinates(direction);
            if (!_scannedCells.ContainsKey(targetCoords))
            {
                _scannedCells.Add(targetCoords, true);
            }

            _currentPosition = targetCoords;
            _path.Push(_currentPosition);
        }

        public bool CanGoBack()
        {
            return _path.Count > 1;
        }

        public Direction GoBack()
        {
            var previousCell = _currentPosition;
            _path.Pop();
            _currentPosition = _path.Peek();

            if (previousCell.X < _currentPosition.X)
            {
                return Direction.Right;
            }
            else if (previousCell.X > _currentPosition.X)
            {
                return Direction.Left;
            }
            else if (previousCell.Y < _currentPosition.Y)
            {
                return Direction.Up;
            }
            else if (previousCell.Y > _currentPosition.Y)
            {
                return Direction.Down;
            }

            throw new InvalidOperationException();
        }

        public bool SellWasVisited(Direction direction)
        {
            var targetCoords = GetNextCellCoordinates(direction);
            return _scannedCells.ContainsKey(targetCoords);
        }

        private TraceCoordinates GetNextCellCoordinates(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new TraceCoordinates(_currentPosition.X - 1, _currentPosition.Y);
                case Direction.Right:
                    return new TraceCoordinates(_currentPosition.X + 1, _currentPosition.Y);
                case Direction.Up:
                    return new TraceCoordinates(_currentPosition.X, _currentPosition.Y + 1);
                case Direction.Down:
                    return new TraceCoordinates(_currentPosition.X, _currentPosition.Y - 1);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
