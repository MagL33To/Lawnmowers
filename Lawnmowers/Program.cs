using System;
using System.Collections.Generic;
using System.Linq;

namespace Lawnmowers
{
    public class Program
    {
        public static Point MaxGardenBound { private get; set; }
        private static readonly Dictionary<Mower, string> Lawnmowers = new Dictionary<Mower, string>();
        private const int NumMowers = 2;

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the garden bounds separated by a space:");
            MaxGardenBound = GenerateGarden(Console.ReadLine());
            while (MaxGardenBound == null)
            {
                Console.WriteLine("Please enter a valid garden bound. E.G.: 5 5. (N.B. Minimum garden size is 2 2):");
                MaxGardenBound = GenerateGarden(Console.ReadLine());
            }

            for (var i = 1; i < NumMowers+1; i++)
            {
                MowerInit(i);
            }

            foreach (var lawnmower in Lawnmowers)
            {
                Console.WriteLine(lawnmower.Key.Move(lawnmower.Value));
            }
            Console.ReadLine();
        }

        public static Point GenerateGarden(string str)
        {
            var point = GetPointFromString(str);
            return point == null || (point.X < 3 && point.Y == 1) || (point.X == 1 && point.Y < 3) ? null : point;
        }

        public static Point GetPointFromString(string str)
        {
            var nums = str.Split(' ');
            int x, y;

            if (nums.Length != 2 || !int.TryParse(nums[0], out x) || !int.TryParse(nums[1], out y))
                return null;

            return new Point(x, y);
        }

        private static void MowerInit(int mowerNumber)
        {
            Console.WriteLine($"Please enter the position and heading of lawnmower number {mowerNumber}:");
            var mower = GenerateMower(Console.ReadLine());
            while (mower == null)
            {
                Console.WriteLine($"Please enter a valid starting position and heading bearing in mind your garden size is {MaxGardenBound.X} x {MaxGardenBound.Y}. E.G.: {MaxGardenBound.X - 1} {MaxGardenBound.Y - 1} N:");
                mower = GenerateMower(Console.ReadLine());
            }
            Console.WriteLine($"Please enter the instructions for lawnmower number {mowerNumber}:");
            var mowerInstructions = Console.ReadLine();
            var mowerInstructionsValid = ValidateMoveInstructions(mowerInstructions);
            while (!mowerInstructionsValid)
            {
                Console.WriteLine("Please enter valid instructions. Valid commands are L, R and M. E.G.: LMLMLMLMM:");
                mowerInstructions = Console.ReadLine();
                mowerInstructionsValid = ValidateMoveInstructions(mowerInstructions);
            }

            Lawnmowers.Add(mower, mowerInstructions);
        }

        public static Mower GenerateMower(string startingDetails)
        {
            if (String.IsNullOrEmpty(startingDetails) || startingDetails.Length < 5)
                return null;

            var point = GetPointFromString(startingDetails.Substring(0, startingDetails.Length - 2));
            Heading heading;

            if (point == null || point.X > MaxGardenBound.X || point.Y > MaxGardenBound.Y || char.IsNumber(char.Parse(startingDetails.Substring(startingDetails.Length - 1))) || !Enum.TryParse(startingDetails.Substring(startingDetails.Length - 1), out heading))
                return null;

            return new Mower(point, heading, MaxGardenBound);
        }

        public static bool ValidateMoveInstructions(string instructions)
        {
            var validInstructions = new List<char> { 'L', 'R', 'M' };
            return instructions.All(x => validInstructions.Contains(x)) && !String.IsNullOrEmpty(instructions);
        }
    }
}