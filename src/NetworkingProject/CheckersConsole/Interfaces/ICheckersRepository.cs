using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersGame;
namespace CheckersConsole.Interfaces
{
    public interface ICheckersRepository
    {
        Task<int> CheckForWinnerAsync();
        Task<int[][]> GetGameStateAsync();
        Task<int> GetPlayerIdAsync();
        Task<bool> IsMyTurnAsync(int playerId);
        Task<Move> PlayMoveAsync(int fromX, int fromY, int toX, int toY);
        Task ResetGameAsync();
    }
}
