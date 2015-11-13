namespace Lawnmowers
{
    public class Mower
    {
        public Point Position { get; }
        public Heading Heading { get; private set; }

        private readonly Point _gardenBound;

        public Mower(Point startingPosition, Heading heading, Point gardenBound)
        {
            Position = startingPosition;
            Heading = heading;
            _gardenBound = gardenBound;
        }

        public string Move(string instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction == 'M' && ValidateMove())
                {
                    switch (Heading)
                    {
                        case Heading.E:
                            Position.X++;
                            break;
                        case Heading.S:
                            Position.Y--;
                            break;
                        case Heading.W:
                            Position.X--;
                            break;
                        case Heading.N:
                            Position.Y++;
                            break;
                    }
                }
                
                if(instruction != 'M')
                    UpdateBearing(instruction);
            }

            return $"{Position.X} {Position.Y} {Heading}";
        }

        private void UpdateBearing(char direction)
        {
            switch (direction)
            {
                case 'L':
                    switch (Heading)
                    {
                        case Heading.E:
                            Heading = Heading.N;
                            break;
                        case Heading.N:
                            Heading = Heading.W;
                            break;
                        case Heading.S:
                            Heading = Heading.E;
                            break;
                        case Heading.W:
                            Heading = Heading.S;
                            break;
                    }
                    break;
                case 'R':
                    switch (Heading)
                    {
                        case Heading.E:
                            Heading = Heading.S;
                            break;
                        case Heading.N:
                            Heading = Heading.E;
                            break;
                        case Heading.S:
                            Heading = Heading.W;
                            break;
                        case Heading.W:
                            Heading = Heading.N;
                            break;
                    }
                    break;
            }
        }

        private bool ValidateMove()
        {
            switch (Heading)
            {
                case Heading.N:
                    return Position.Y != _gardenBound.Y;
                case Heading.S:
                    return Position.Y != 0;
                case Heading.E:
                    return Position.X != _gardenBound.X;
                case Heading.W:
                    return Position.X != 0;
                default:
                    return false;
            }
        }
    }
}