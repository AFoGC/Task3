using ConsoleTables;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
	class Program
	{
		private static MoveGenerator moveGenerator;
		private static GameMechanic gameMechanic;
		static void Main(string[] args)
		{
			CheckArgs(args);

			moveGenerator = new MoveGenerator(args);
			gameMechanic = new GameMechanic(args);

            while (true)
            {
				moveGenerator.NextMove();

				string playerMove = GetAnswer(args);
				PrintResult(playerMove);
				
			}
		}

		public static void PrintMenu(string[] args)
        {
			Console.WriteLine("\n");
			Console.WriteLine("HMAC: " + moveGenerator.HMAC);
			Console.WriteLine("Available moves: ");
			for (int i = 0; i < args.Length; i++)
			{
				Console.WriteLine((i + 1) + " - " + args[i]);
			}
			Console.WriteLine("0 - exit");
			Console.WriteLine("? - help");
		}

		public static string GetAnswer(string[] args)
        {
			int intAnswer;
			string answer = String.Empty;
			bool @continue = true;
            while (@continue)
            {
				PrintMenu(args);
				Console.Write("Enter your move: ");
				string readLine = Console.ReadLine();
				if (readLine == "0") Environment.Exit(0);
				if (readLine == "?") ShowHelp(args);

                try
                {
					intAnswer = Convert.ToInt32(readLine);
					@continue = false;
					answer = args[intAnswer - 1];
                }
                catch { }
			}
			return answer;
		}

		public static void PrintResult(string playerMove)
        {
			Console.WriteLine("Your move: " + playerMove);
			Console.WriteLine("Computer move: " + moveGenerator.ComputerMove);

            switch (gameMechanic.PlayerWon(playerMove, moveGenerator.ComputerMove))
            {
				case GameOver.PlayerWin:
					Console.WriteLine("You win");
					break;
				case GameOver.ComputerWin:
					Console.WriteLine("Computer win");
					break;
				default:
					Console.WriteLine("Draw, nobody won");
					break;
            }

			Console.WriteLine("HMAC key: " + moveGenerator.Key);
		}

		public static void ShowHelp(string[] args)
        {
			string[] columnsName = new string[args.Length + 1];
			columnsName[0] = String.Empty;
			
            for (int i = 1; i < columnsName.Length; i++)
            {
				columnsName[i] = "player " + args[i - 1];
            }

			var table = new ConsoleTable(columnsName);

			
			foreach (var item in args)
            {
				string[] winArray = gameMechanic.GetWinArray(item);
				string[] rowsArray = new string[args.Length + 1];
				rowsArray[0] = "bot " + item;
				for (int i = 1; i < rowsArray.Length; i++)
				{
					rowsArray[i] = winArray[i-1];
				}

				table.AddRow(rowsArray);
            }

			table.Write();
        }

		public static void CheckArgs(string[] args)
        {
			if (args.Length < 3)
			{
				Console.WriteLine("Need a number greater than 3");
				Environment.Exit(0);
			}

			if ((args.Length % 2) == 0)
			{
				Console.WriteLine("You need to enter an odd number");
				Environment.Exit(0);
			}

			if (args.GroupBy(x => x).Any(g => g.Count() > 1))
			{
				Console.WriteLine("Arguments must not contain duplicates");
				Environment.Exit(0);
			}
		}
	}
}
