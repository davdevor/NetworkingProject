using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Domain.Objects
{
    public class CheckersGame
    {
        private int _firstPlayerCount = 12;
        private int _secondPlayerCount = 12;
        private readonly int[,] _board = new int[,]
        {
            { 0,1,0,1,0,1,0,1 },
            { 1,0,1,0,1,0,1,0 },
            { 0,1,0,1,0,1,0,1 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 2,0,2,0,2,0,2,0 },
            { 0,2,0,2,0,2,0,2 },
            { 2,0,2,0,2,0,2,0 }
        };

        public int[,] GetBoard()
        {
            return _board;
        }
        public Move Move(int fromRow, int fromCol, int toRow, int toCol)
        {
            Tuple<bool, bool> result = ValidMove(fromRow, fromCol, toRow, toCol);
            if (!result.Item1)
            {
                return new Move();
            }
            _board[toRow, toCol] = _board[fromRow, fromCol];
            _board[fromRow, fromCol] = 0;
            if (result.Item2)
            {
                return new Move() { ValidMove = true, AvailableMoves = AvailableJumps(toRow, toCol) };
            }
            else
            {
                return new Move() { ValidMove = true };
            }
        }
        public int CheckForWinState()
        {
            if (_firstPlayerCount == 0)
            {
                return 2;
            }
            if (_secondPlayerCount == 0)
            {
                return 1;
            }
            return 0;
        }
        public List<Tuple<int, int>> AvailableJumps(int fromRow, int fromCol)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            int player = _board[fromRow, fromCol];

            if (player == 1)
            {
                if (fromRow + 2 <= 7)
                {
                    if (fromCol + 2 <= 7 && _board[fromRow + 1, fromCol + 1] != 1 && fromCol + 2 <= 7 && _board[fromRow + 1, fromCol + 1] != 0 && _board[fromRow + 2, fromCol + 2] == 0)
                    {
                        moves.Add(new Tuple<int, int>(fromRow + 2, fromCol + 2));
                    }
                    if (fromCol - 2 >= 0 && _board[fromRow + 1, fromCol - 1] != 1 && fromCol - 2 >= 0 && _board[fromRow + 1, fromCol - 1] != 0 && _board[fromRow + 2, fromCol - 2] == 0)
                    {
                        moves.Add(new Tuple<int, int>(fromRow + 2, fromCol - 2));
                    }
                }
            }
            else
            {
                if (fromRow - 2 >= 0)
                {

                    if (fromCol + 2 <= 7 && _board[fromRow - 1, fromCol + 1] != 2 && fromCol + 2 <= 7 && _board[fromRow - 1, fromCol + 1] != 0 && _board[fromRow - 2, fromCol + 2] == 0)
                    {
                        moves.Add(new Tuple<int, int>(fromRow - 2, fromCol + 2));
                    }
                    if (fromCol - 2 >= 0 && _board[fromRow - 1, fromCol - 1] != 2 && fromCol - 2 >= 0 && _board[fromRow - 1, fromCol - 1] != 0 && _board[fromRow - 2, fromCol - 2] == 0)
                    {
                        moves.Add(new Tuple<int, int>(fromRow - 2, fromCol + 2));
                    }
                }
            }
            return moves;
        }

        /// <summary>
        /// returns a tuple first item is for valid move and second item is for jump
        /// </summary>
        /// <param name="fromX"></param>
        /// <param name="fromY"></param>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        /// <returns></returns>
        public Tuple<bool, bool> ValidMove(int fromX, int fromY, int toX, int toY)
        {

            if (fromX < 0 || fromX > 7 || fromY < 0 || fromY > 7 ||
                toX < 0 || toX > 7 || toY < 0 || toY > 7)
            {
                return new Tuple<bool, bool>(false, false);
            }
            bool validMove;
            int player = _board[fromX, fromY];
            if (player == 0 || _board[toX, toY] != 0)
            {
                return new Tuple<bool, bool>(false, false); ;
            }
            if (player == 1)
            {
                bool xCheck = fromX + 1 == toX;
                bool yCheck = fromY - 1 == toY || fromY + 1 == toY;
                validMove = xCheck && yCheck;
                if (!validMove)
                {
                    //check for jump
                    xCheck = fromX + 2 == toX;
                    yCheck = fromY - 2 == toY || fromY + 2 == toY;
                    validMove = xCheck && yCheck;
                    if (validMove)
                    {
                        if (fromY < toY)
                        {
                            if (_board[fromX + 1, fromY + 1] != 1 && _board[fromX + 1, fromY + 1] != 0)
                            {
                                _board[fromX + 1, fromY + 1] = 0;
                            }
                            else
                            {
                                return new Tuple<bool, bool>(false, false); ;
                            }
                        }
                        else
                        {
                            if (_board[fromX + 1, fromY - 1] != 1 && _board[fromX + 1, fromY - 1] != 0 )
                            {
                               _board[fromX + 1, fromY - 1] = 0;
                            }
                            else
                            {
                                return new Tuple<bool, bool>(false, false); ;
                            }
                        }
                        --_secondPlayerCount;
                        return new Tuple<bool, bool>(true, true);
                    }
                    else
                    {
                        return new Tuple<bool, bool>(false, false);
                    }
                }
              
            }
            else // player two
            {
                bool xCheck = fromX - 1 == toX;
                bool yCheck = fromY - 1 == toY || fromY + 1 == toY;
                validMove = xCheck && yCheck;
                if (!validMove)
                {
                    //check for jump
                    xCheck = fromX - 2 == toX;
                    yCheck = fromY - 2 == toY || fromY + 2 == toY;
                    validMove = xCheck && yCheck;
                    if (validMove)
                    {
                        if (fromY < toY)
                        {
                            if (_board[fromX - 1, fromY + 1] != 2 && _board[fromX - 1, fromY + 1] != 0)
                            {
                                _board[fromX - 1, fromY + 1] = 0;

                            }
                            else
                            {
                                return new Tuple<bool, bool>(false, false); ;
                            }
                        }
                        else
                        {
                            if (_board[fromX - 1, fromY - 1] != 2 && _board[fromX - 1, fromY - 1] != 0)
                            {
                                _board[fromX - 1, fromY - 1] = 0;
                            }
                            else
                            {
                                return new Tuple<bool, bool>(false, false); ;
                            }
                        }
                        --_firstPlayerCount;
                        return new Tuple<bool, bool>(true, true);

                    }
                    else
                    {
                        return new Tuple<bool, bool>(false, false);
                    }
                }
               
            }
            return new Tuple<bool, bool>(true, false);
        }
    }
}
