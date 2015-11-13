using System;
using NUnit.Framework;

namespace Lawnmowers.Test
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void GenerateGarden_EmptyString_ReturnsNull()
        {
            var result = Program.GenerateGarden(String.Empty);

            Assert.IsNull(result);
        }

        [TestCase("1 1")]
        [TestCase("1 2")]
        [TestCase("2 1")]
        public void GenerateGarden_GardenSizeIsLessThan2x2_ReturnsNull(string size)
        {
            var result = Program.GenerateGarden(size);

            Assert.IsNull(result);
        }

        [TestCase("1 12")]
        [TestCase("4 1")]
        public void GenerateGarden_OneSideIs1AndTheOtherIsGreaterThan3_ReturnsPoint(string size)
        {
            var result = Program.GenerateGarden(size);

            Assert.IsInstanceOf<Point>(result);
        }

        [TestCase("2 2")]
        [TestCase("7 8")]
        [TestCase("5 5")]
        public void GenerateGarden_GardenSizeIsValid_ReturnsPoint(string size)
        {
            var result = Program.GenerateGarden(size);

            Assert.IsInstanceOf<Point>(result);
        }

        [Test]
        public void GetPointFromString_EmptyString_ReturnsNull()
        {
            var result = Program.GetPointFromString(String.Empty);

            Assert.IsNull(result);
        }

        [TestCase("2")]
        [TestCase("23")]
        [TestCase("2 3 4")]
        public void GetPointFromString_StringWithMoreOrLessThan2Elements_ReturnsNull(string str)
        {
            var result = Program.GetPointFromString(str);

            Assert.IsNull(result);
        }

        [Test]
        public void GetPointFromString_NonNumericalString_ReturnsNull()
        {
            var result = Program.GetPointFromString("A B");

            Assert.IsNull(result);
        }

        [Test]
        public void GetPointFromString_ValidInput_ReturnsPoint()
        {
            var result = Program.GetPointFromString("5 5");

            Assert.IsInstanceOf<Point>(result);
        }

        [TestCase(1,2)]
        [TestCase(3,7)]
        [TestCase(5,5)]
        [TestCase(12,9)]
        public void GetPointFromString_ValidInput_PointReturnedHasCorrectValues(int x, int y)
        {
            var str = $"{x} {y}";

            var result = Program.GetPointFromString(str);

            Assert.AreEqual(x, result.X);
            Assert.AreEqual(y, result.Y);
        }

        [Test]
        public void GenerateMower_EmptyString_ReturnNull()
        {
            Program.MaxGardenBound = new Point(20, 20);

            var result = Program.GenerateMower("");

            Assert.IsNull(result);
        }

        [TestCase("1")]
        [TestCase("2 3")]
        [TestCase("2 3 N M")]
        public void GenerateMower_StringWithMoreOrLessThan3Elements_ReturnsNull(string str)
        {
            Program.MaxGardenBound = new Point(20, 20);

            var result = Program.GenerateMower(str);

            Assert.IsNull(result);
        }

        [TestCase("A B 1")]
        [TestCase("1 N 2")]
        [TestCase("1 2 1")]
        [TestCase("N 1 2")]
        public void GenerateMower_IncorrectlyFormattedString_ReturnsNull(string str)
        {
            Program.MaxGardenBound = new Point(20, 20);

            var result = Program.GenerateMower(str);

            Assert.IsNull(result);
        }

        [Test]
        public void GenerateMower_ValidInput_ReturnsMower()
        {
            Program.MaxGardenBound = new Point(20, 20);

            var result = Program.GenerateMower("1 2 N");

            Assert.IsInstanceOf<Mower>(result);
        }

        [TestCase("1 7 N")]
        [TestCase("7 1 N")]
        [TestCase("7 7 N")]
        public void GenerateMower_InputStartingPositionIsOutsideOfGardenBounds_ReturnsNull(string str)
        {
            Program.MaxGardenBound = new Point(5, 5);

            var result = Program.GenerateMower(str);

            Assert.IsNull(result);
        }

        [TestCase(1, 2, "N")]
        [TestCase(3, 7, "E")]
        [TestCase(5, 5, "S")]
        [TestCase(12, 9, "W")]
        public void GenerateMower_ValidInput_MowerReturnedHasCorrectValues(int x, int y, string h)
        {
            var str = $"{x} {y} {h}";

            Program.MaxGardenBound = new Point(20, 20);

            var result = Program.GenerateMower(str);

            Assert.AreEqual(x, result.Position.X);
            Assert.AreEqual(y, result.Position.Y);
            Assert.AreEqual(Enum.Parse(typeof(Heading), h), result.Heading);
        }

        [Test]
        public void ValidateMoveInstructions_EmptyString_ReturnFalse()
        {
            var result = Program.ValidateMoveInstructions(String.Empty);

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateMoveInstructions_StringWithInvalidCharacters_ReturnFalse()
        {
            var result = Program.ValidateMoveInstructions("LRMYMCA");

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateMoveInstructions_StringWithOnlyValidCharacters_ReturnTrue()
        {
            var result = Program.ValidateMoveInstructions("LMLMLMLMM");

            Assert.IsTrue(result);
        }
    }
}