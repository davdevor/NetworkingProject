using System.Threading.Tasks;

namespace ConnectFourClient.Interfaces
{
    public interface IConnectFourAPIRepository
    {
        Task<int> CheckForWinnerAsync();
        Task<int[][]> GetGameStateAsync();
        Task<int> GetPlayerIdAsync();
        Task<bool> IsMyTurnAsync(int playerId);
        Task<bool> PlayMoveAsync(int col, int playerId);
        Task ResetGameAsync();
    }
}