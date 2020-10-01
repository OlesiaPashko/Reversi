using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class GameManager
    {
        Player firstPlayer = new HumanPlayer();
        Player secondPlayer;
        Field field;

        public GameManager(Action cellChangedColor)
        {
            field = new Field(cellChangedColor);
        }

        public List<Tuple<int, int>> GetAvailableCells(Player player)
        {
            return field.GetAvailableCells(player.Color);
        }

        public void SetCell(Player player, Tuple<int, int> coolds)
        {
            field.SetCell(player.Color, coolds);
        }
        public void ResetGame()
        {

        }

        public void StartGame(bool withAI)
        {
            if (withAI)
            {
                secondPlayer = new AIPlayer();
            }
        }

        public void FinishGame()
        {

        }
    }
}
