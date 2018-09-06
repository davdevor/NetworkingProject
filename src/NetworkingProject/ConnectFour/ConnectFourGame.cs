using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class ConnectFourGame
    {
        private readonly int _width = 7;
        private readonly int _height = 6;
        private readonly int[,] _board;
        
        public int[,] GetBoard()
        {
            return _board;
        }
        public ConnectFourGame()
        {
            _board = new int[_height,_width];
        }

        public void PlayMove(int col, int playerId)
        {
            int i = _height - 1;
            while(i > -1 && _board[i,col] != 0 )
            {
                --i;
            }
            _board[i, col] = playerId;
        }

        public bool CheckForWinState(int player)
        {
            // horizontalCheck 
            for (int j = 0; j < _width - 3; j++)
            {
                for (int i = 0; i < _height; i++)
                {
                    if (_board[i,j] == player && _board[i, j + 1] == player && _board[i,j + 2] == player && _board[i,j + 3] == player)
                    {
                        return true;
                    }
                }
            }
            // verticalCheck
            for (int i = 0; i < _height - 3; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_board[i,j] == player && _board[i + 1,j] == player && _board[i + 2,j] == player && _board[i + 3,j] == player)
                    {
                        return true;
                    }
                }
            }
            // ascendingDiagonalCheck 
            for (int i = 3; i < _height; i++)
            {
                for (int j = 0; j < _width - 3; j++)
                {
                    if (_board[i,j] == player && _board[i - 1,j + 1] == player && _board[i - 2,j + 2] == player && _board[i - 3,j + 3] == player)
                        return true;
                }
            }
            // descendingDiagonalCheck
            for (int i = 3; i < _height; i++)
            {
                for (int j = 3; j < _width; j++)
                {
                    if (_board[i,j] == player && _board[i - 1,j - 1] == player && _board[i - 2,j - 2] == player && _board[i - 3,j - 3] == player)
                        return true;
                }
            }
            return false;
        }
    }
}
