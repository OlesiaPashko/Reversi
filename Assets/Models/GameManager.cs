using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public class GameManager
    {
        private Player firstPlayer = new HumanPlayer();
        private Player secondPlayer;
        private Player currentPlayer;
        Field field;
        public event Action<List<List<Cell>>> MoveMade;

        public event Action<List<List<Cell>>> GameStarted;

        public event Action<List<Tuple<int, int>>> AvailableCellsCalculated;

        public event Action<GameFinishState> GameFinished;
        public GameManager()
        {
            field = new Field();
        }

        public List<Tuple<int, int>> GetAvailableCells()
        {
            Console.WriteLine(field);
            var availableCells = field.GetAvailableCells(currentPlayer.Color);
            Console.WriteLine(AvailableCellsCalculated);
            AvailableCellsCalculated?.Invoke(availableCells);
            return availableCells;
        }

        public void MakeMove(Tuple<int, int> coolds)
        {
            field.SetCell(currentPlayer.Color, coolds);
            MoveMade?.Invoke(field.Cells);

            SwitchPlayer();
            if (field.isFull())
                FinishGame();

            GetAvailableCells();
        }
        public void ResetGame(bool isWithAI, CellState firstPlayerColor)
        {
            field = new Field();
            StartGame(isWithAI, firstPlayerColor);
        }

        public void StartGame(bool isWithAI, CellState firstPlayerColor)
        {
            firstPlayer.Color = firstPlayerColor;
            currentPlayer = firstPlayer;

            if (isWithAI)
            {
                secondPlayer = new AIPlayer();
            }
            else
            {
                secondPlayer = new HumanPlayer();
            }
            secondPlayer.Color = field.GetOppositeColor(firstPlayerColor);

            GameStarted?.Invoke(field.Cells);

            GetAvailableCells();
        }

        public void FinishGame()
        {
            int firstPlayerCellsCount = field.CountCells(firstPlayer.Color);
            int secountPlayerCellsCount = field.CountCells(secondPlayer.Color);
            if (firstPlayerCellsCount > secountPlayerCellsCount)
                GameFinished?.Invoke(GameFinishState.FirstPlayerWon);
            else if (secountPlayerCellsCount > firstPlayerCellsCount)
                GameFinished?.Invoke(GameFinishState.SecondPlayerWon);
            else
                GameFinished?.Invoke(GameFinishState.Tie);
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == firstPlayer ? secondPlayer : firstPlayer;
        }
    }
}
