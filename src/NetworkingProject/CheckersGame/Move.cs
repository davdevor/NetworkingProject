using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class Move
    {
        public bool ValidMove { get; set; } = false;

        public List<Tuple<int, int>> AvailableMoves { get; set; } = new List<Tuple<int, int>>();
    }
}
