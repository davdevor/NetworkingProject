using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour;
namespace ConnectFourTests
{
    [TestClass]
    public class ConnectFourGameTests
    {
        [TestMethod]
        public void CanPlayOnEmptyColumn()
        {
            // arrange
            ConnectFourGame target = new ConnectFourGame();
            int playerId = 1, col = 0;

            // act
            bool result = target.PlayMove(col, playerId);

            // assert
            Assert.IsTrue(target.GetBoard()[target._height - 1, col] == playerId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanPlayOnTopOfAnotherPlay()
        {
            // arrange
            ConnectFourGame target = new ConnectFourGame();
            int playerId = 1, col = 0;

            // act
            target.PlayMove(col, playerId);
            bool result = target.PlayMove(col, playerId);

            // assert
            Assert.IsTrue(target.GetBoard()[target._height - 2, col] == playerId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotPlayOnAFullRow()
        {
            // arrange
            ConnectFourGame target = new ConnectFourGame();
            int playerId = 1, col = 0;

            // act
            int i = 0;
            bool result = true;
            while(i <= target._height)
            {
                result = target.PlayMove(col, playerId);
                ++i;
            }

            // assert
            Assert.IsFalse(result);
        }
    }
}
