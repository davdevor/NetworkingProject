using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Domain.Objects;

namespace Checkers.Domain.Interfaces
{
    public interface ICheckersRepository
    {
        /// <summary>
        /// checks for a winner
        /// </summary>
        /// <returns>0 if none otherwise player id</returns>
        Task<int> CheckForWinnerAsync();
        /// <summary>
        /// gets the state of the checkers board
        /// </summary>
        /// <returns></returns>
        Task<int[][]> GetGameStateAsync();
        /// <summary>
        /// get a player id to assign to yourself
        /// </summary>
        /// <returns>playerid</returns>
        Task<int> GetPlayerIdAsync();
        /// <summary>
        /// checks to see if it is the players turn
        /// </summary>
        /// <param name="playerId">the player to check if it is their turn</param>
        /// <returns>true if it is the players turn</returns>
        Task<bool> IsMyTurnAsync(int playerId);
        /// <summary>
        /// plays a move for a player
        /// </summary>
        /// <param name="fromX">x location to move from</param>
        /// <param name="fromY">y location to move from</param>
        /// <param name="toX">x location to move to</param>
        /// <param name="toY">y location to move to</param>
        /// <returns></returns>
        Task<Move> PlayMoveAsync(int fromX, int fromY, int toX, int toY);
        /// <summary>
        /// resets the entire game
        /// </summary>
        /// <returns></returns>
        Task ResetGameAsync();
    }

}
