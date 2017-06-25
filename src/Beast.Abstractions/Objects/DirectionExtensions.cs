namespace Beast.Objects
{
    public static class DirectionExtensions
    {
        public static Direction Counter(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.East:
                    return Direction.West;
                case Direction.West:
                    return Direction.East;
                case Direction.Northeast:
                    return Direction.Southwest;
                case Direction.Northwest:
                    return Direction.Southeast;
                case Direction.Southeast:
                    return Direction.Northwest;
                case Direction.Southwest:
                    return Direction.Northeast;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                default:
                    return Direction.Undefined;
            }
        }

        public static Direction ToDirection(this Direction value)
        {
            switch (value)
            {
                case Direction.North:
                    return Direction.North;
                case Direction.South:
                    return Direction.South;
                case Direction.East:
                    return Direction.East;
                case Direction.West:
                    return Direction.West;
                case Direction.Northeast:
                    return Direction.Northeast;
                case Direction.Northwest:
                    return Direction.Northwest;
                case Direction.Southeast:
                    return Direction.Southeast;
                case Direction.Southwest:
                    return Direction.Southwest;
                case Direction.Up:
                    return Direction.Up;
                case Direction.Down:
                    return Direction.Down;
                default:
                    return Direction.Undefined;
            }
        }
    }
}
