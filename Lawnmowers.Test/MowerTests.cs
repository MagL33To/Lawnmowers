using NUnit.Framework;

namespace Lawnmowers.Test
{
    [TestFixture]
    public class MowerTests
    {
        [TestCase(Heading.N, "3 4 N")]
        [TestCase(Heading.E, "4 3 E")]
        [TestCase(Heading.S, "3 2 S")]
        [TestCase(Heading.W, "2 3 W")]
        public void Move_ValidMoveOneSpace_MowerMovesOneSpace(Heading heading, string expected)
        {
            var mower = new Mower(new Point(3,3), heading, new Point(5,5));

            var result = mower.Move("M");

            Assert.AreEqual(expected, result);
        }

        [TestCase(0, 5, Heading.N, 5, 5, "MR", "0 5 E")]
        [TestCase(0, 5, Heading.W, 5, 5, "MR", "0 5 N")]
        [TestCase(5, 0, Heading.S, 5, 5, "MR", "5 0 W")]
        [TestCase(5, 0, Heading.E, 5, 5, "MR", "5 0 S")]
        [TestCase(0, 5, Heading.N, 5, 5, "ML", "0 5 W")]
        [TestCase(0, 5, Heading.W, 5, 5, "ML", "0 5 S")]
        [TestCase(5, 0, Heading.S, 5, 5, "ML", "5 0 E")]
        [TestCase(5, 0, Heading.E, 5, 5, "ML", "5 0 N")]
        public void Move_InvalidMoveOneSpaceValidTurn_MowerDoesnNotMoveButDoesTurn(int spx, int spy, Heading heading, int gbx, int gby, string instructions, string expected)
        {
            var mower = new Mower(new Point(spx,spy), heading, new Point(gbx,gby));

            var result = mower.Move(instructions);

            Assert.AreEqual(expected, result);
        }
    }
}