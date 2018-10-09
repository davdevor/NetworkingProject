using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.Domain.Objects;

namespace Checkers.Domain.Tests
{
    [TestClass]
    public class CheckersGameTests
    {
        [TestMethod]
        public void _2545(){
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            var result = target.Move(2, 5, 4, 5);

            // assert
            Assert.IsTrue(!result.ValidMove);
            Assert.AreEqual(target.GetBoard()[2, 5], 1);
            Assert.AreEqual(target.GetBoard()[4, 5], 0);
        }
        [TestMethod]
        public void CanMovePlayer1ToEmptySpace()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            var result = target.Move(2, 1, 3, 0);

            // assert
            Assert.IsTrue(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[3, 0], 1);
            Assert.AreEqual(target.GetBoard()[2, 1], 0);
        }

        [TestMethod]
        public void CanMovePlayer2ToEmptySpace()
        {
            // arrange 
            CheckersGame target = new CheckersGame();

            // act
            var result = target.Move(5, 0, 4, 1);

            // assert
            Assert.IsTrue(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[5, 0], 0);
            Assert.AreEqual(target.GetBoard()[4, 1], 2);
        }
        [TestMethod]
        public void CannotMoveOntoOwnPiece()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            var result = target.Move(1, 0, 2, 1);

            // assert
            Assert.IsFalse(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[1, 0], 1);
            Assert.AreEqual(target.GetBoard()[2, 1], 1);
        }

        [TestMethod]
        public void CannotMoveOntoAnotherPlayer()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            target.Move(2, 1, 3, 2);
            target.Move(5, 0, 4, 1);
            var result = target.Move(3, 2, 4, 1);

            // assert
            Assert.IsFalse(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[3, 2], 1);
            Assert.AreEqual(target.GetBoard()[4, 1], 2);
        }

        [TestMethod]
        public void CannotMoveOffTheBoard()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            var result = target.Move(1, 0, 2, -1);

            // assert
            Assert.IsFalse(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[1, 0], 1);
        }

        [TestMethod]
        public void CanJumpAEnemyToAEmptySpace()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            target.Move(2, 1, 3, 2);
            target.Move(5, 0, 4, 1);
            var result = target.Move(3, 2, 5, 0);

            // assert
            Assert.IsTrue(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[5, 0], 1);
            Assert.AreEqual(target.GetBoard()[4, 1], 0);
        }

        [TestMethod]
        public void CannotJumpAEnemyToAFilledSpace()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            target.Move(2, 1, 3, 2);
            target.Move(3, 2, 4, 3);
            var result = target.Move(4, 3, 6, 5);

            // assert
            Assert.IsFalse(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[4, 3], 1);
            Assert.AreEqual(target.GetBoard()[6, 5], 2);
        }

        [TestMethod]
        public void CannontMoveMoreThanOneSpaceForRegularMove()
        {
            // arrange
            CheckersGame target = new CheckersGame();

            // act
            var result = target.Move(2, 1, 4, 3);

            // assert
            Assert.IsFalse(result.ValidMove);
            Assert.AreEqual(target.GetBoard()[4, 3], 0);
            Assert.AreEqual(target.GetBoard()[2, 1], 1);
        }
    }
}
