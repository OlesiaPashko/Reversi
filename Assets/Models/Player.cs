using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public abstract class Player
    {
        public CellState Color { get; set; }
    }
    class HumanPlayer : Player
    {
    }

    class AIPlayer : Player
    {
    }
}
