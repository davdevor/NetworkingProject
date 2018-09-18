using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class Checkers
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
        public bool Move(int fromX, int fromY, int toX, int toY)
        {
            if (!ValidMove(fromX, fromY, toX, toY))
            {
                return false;
            }
            _board[toX, toY] = _board[fromX, fromY];
            _board[fromX, fromY] = 0;
            return true;
        }
        public int CheckForWinState()
        {
            if(_firstPlayerCount == 0)
            {
                return 2;
            }
            if(_secondPlayerCount == 0)
            {
                return 1;
            }
            return 0;
        }
        public bool ValidMove(int fromX, int fromY, int toX, int toY)
        {
            bool validMove;
            int player = _board[fromX, fromY];
            if (player == 0 || _board[toX, toY] != 0)
            {
                return false;
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
                            if(_board[fromX + 1, fromY + 1] != 1)
                            {
                                _board[fromX + 1, fromY + 1] = 0;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if(_board[fromX + 1, fromY - 1] != 1)
                            {
                                _board[fromX + 1, fromY - 1] = 0;

                            }
                            else
                            {
                                return false;
                            }
                        }
                        --_secondPlayerCount;

                    }
                }
            }
            else
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
                        if(fromY < toY)
                        {
                            if(_board[fromX - 1, fromY + 1] != 2)
                            {
                                _board[fromX - 1, fromY + 1] = 0;

                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if(_board[fromX - 1, fromY - 1] != 2)
                            {
                                _board[fromX - 1, fromY - 1] = 0;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        --_firstPlayerCount;

                    }
                }
            }
            return validMove;
        }
    }
}