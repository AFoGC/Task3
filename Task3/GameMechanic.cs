using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    public enum GameOver
    {
        PlayerWin,
        ComputerWin,
        Draw
    }
    public class GameMechanic 
    {
        private readonly string[] args;
        public GameMechanic(string[] args)
        {
            this.args = args;
        }

        public GameOver PlayerWon(string playerMove, string botMove)
        {
            int half = (args.Length - 1) / 2;
            int playerMoveIndex = Array.IndexOf(args, playerMove);
            if (playerMove == botMove) return GameOver.Draw;

            GameOver playerWon = GameOver.PlayerWin;
            

            for (int i = 0, j = playerMoveIndex + 1; i < half; i++)
            {
                if (j == args.Length)
                    j = 0;

                if (args[j] == botMove)
                {
                    playerWon = GameOver.ComputerWin;
                    break;
                }
                j++;
            }

            return playerWon;
        }

        public string[] GetWinArray(string move)
        {
            string[] export = new string[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                switch (PlayerWon(args[i], move))
                {
                    case GameOver.PlayerWin:
                        export[i] = "Player win";
                        break;
                    case GameOver.ComputerWin:
                        export[i] = "Computer win";
                        break;
                    default:
                        export[i] = "Draw";
                        break;
                }
            }

            return export;
        }
    }
}
