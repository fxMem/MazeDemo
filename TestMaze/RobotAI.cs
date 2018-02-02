using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    public class RobotAI
    {
        private IRobot _robot;
        private TraceDevice _traceDevice;

        public RobotAI(IRobot robot)
        {
            _robot = robot;
            _traceDevice = new TraceDevice();
        }

        public void SearchForExit()
        {
            var foundExit = ScanAndMove();
            if (!foundExit)
            {
                throw new InvalidOperationException("Cannot find exit from the maze! Sorry...");
            }
        }

        private bool ScanAndMove()
        {
            var exitFound = TryMove(Direction.Up);
            if (exitFound)
            {
                return true;
            }

            exitFound = TryMove(Direction.Down);
            if (exitFound)
            {
                return true;
            }

            exitFound = TryMove(Direction.Left);
            if (exitFound)
            {
                return true;
            }

            exitFound = TryMove(Direction.Right);
            if (exitFound)
            {
                return true;
            }

            if (_traceDevice.CanGoBack())
            {
                var direction = _traceDevice.GoBack();
                _robot.Move(direction);
            }

            return false;
        }

        private bool TryMove(Direction direction)
        {
            var checkResult = CheckDirection(direction);
            if (checkResult.Type == CellType.Exit)
            {
                _robot.Move(direction);
                return true;
            }

            if (checkResult.CanMove)
            {
                _traceDevice.MoveTo(direction);
                _robot.Move(direction);

                return ScanAndMove();
            }

            return false;
        }

        struct CheckResult
        {
            public CellType Type { get; set; }

            public bool CanMove { get; set; }

            public CheckResult(CellType type, bool canMove)
            {
                Type = type;
                CanMove = canMove;
            }
        }

        private CheckResult CheckDirection(Direction direction)
        {
            var cell = _robot.AdjacentCell(direction);
            if (cell == null)
            {
                // Trying to move outside the map. Lets assume there is invisible wall there.
                return new CheckResult(CellType.Wall, canMove: false);
            }

            var canMove = false;
            if (cell.Type == CellType.Empty)
            {
                // So we don't end up moving same path again.
                canMove = !_traceDevice.SellWasVisited(direction);
            }

            return new CheckResult(cell.Type, canMove);
        }
    }
}
