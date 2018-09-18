using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersConsole.Interfaces
{
    public interface ICheckersRepository
    {
        Task<int> CheckForWinnerAsync();
        Task<int[][]> GetGameStateAsync();
        Task<int> GetPlayerIdAsync();
        Task<bool> IsMyTurnAsync(int playerId);
        Task<bool> PlayMoveAsync(int fromX, int fromY, int toX, int toY);
        Task ResetGameAsync();
    }
}
