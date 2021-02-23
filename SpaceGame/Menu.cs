using System.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SpaceGame
{
    class Menu
    {
        const int WINDOW_OFFSET = 30;
        const int TITLE_LENGTH = 50;

        public void ShowMainMenu(int length = 50)
        {
            string message = "Main Menu";
            int frameSize = length;
            int center = ((frameSize / 2) + (message.Length / 2));
            message = message.PadLeft(center, '-').PadRight(frameSize, '-');
            Console.WriteLine(message);
            Console.WriteLine("[1] New Game\n[2] Load Game\n[3] Quit");
        }

        static public void ShowBanner(string message, Ship ship)
        {
            // Center message in dashes
            int center = (TITLE_LENGTH / 2) + (message.Length / 2);
            message = message.PadLeft(center, '-').PadRight(TITLE_LENGTH, '-');
            
            Console.WriteLine(message);

            ShowHUD(ship);

            Console.SetCursorPosition(0, Console.CursorTop - 5);
        }

        static void ShowHUD(Ship ship)
        {
            //Moves cursor up one line and to right edge of console
            Console.SetCursorPosition(Console.WindowWidth - WINDOW_OFFSET, Console.CursorTop + 1);
            Console.Write("Coin: ");
            WriteColored(ship.Coins.ToString(), ConsoleColor.Yellow);

            Console.SetCursorPosition(Console.WindowWidth - WINDOW_OFFSET, Console.CursorTop + 1);
            Console.Write("Gold: ");
            WriteColored($"{ship.Gold}/{ship.GoldMax}", ConsoleColor.DarkYellow);

            Console.SetCursorPosition(Console.WindowWidth - WINDOW_OFFSET, Console.CursorTop + 1);
            Console.Write("Fuel: ");
            WriteColored($"{ship.Fuel}/{ship.FuelMax}", ConsoleColor.DarkMagenta);

            Console.SetCursorPosition(Console.WindowWidth - WINDOW_OFFSET, Console.CursorTop + 1);
            Console.Write("Hull Integrity: ");
            WriteColored($"{ship.Hull}/{ship.HullMax}", ConsoleColor.Blue);

            Console.SetCursorPosition(Console.WindowWidth - WINDOW_OFFSET, Console.CursorTop + 1);
            Console.Write("Time: ");
            WriteColored(ship.Time.ToString(), ConsoleColor.Red);
        }

        public static int GetUserInput(int menuLength, bool ZeroIndex = false)
        {
            Console.Write("Enter your selection: ");
            string input = Console.ReadLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);//Moves cursor up one line
            Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");//Clears current line
            int selection;
            while (true)
            {
                if (!Int32.TryParse(input, out int parsedInput))
                {
                    Console.WriteLine("Please enter a valid number: ");//writes and makes new line
                    input = Console.ReadLine(); //writes on same line as above and then makes new line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");//Clears current line
                    Console.SetCursorPosition(0, Console.CursorTop - 1);//Moves cursor up one line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");//Clears current line
                    Console.SetCursorPosition(0, Console.CursorTop - 1);//Moves cursor up one line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");//Clears current line
                }
                else if (ZeroIndex == false && (parsedInput > menuLength || parsedInput <= 0))
                {
                    Console.WriteLine("Number is not in menu range");
                    input = Console.ReadLine(); //writes on same line as above and then makes new line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r"); //Clears current line
                    Console.SetCursorPosition(0, Console.CursorTop - 1); //Moves cursor up one line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r"); //Clears current line
                    Console.SetCursorPosition(0, Console.CursorTop - 1); //Moves cursor up one line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r"); //Clears current line
                }
                else if (parsedInput > menuLength || parsedInput < 0)
                {
                    Console.WriteLine("Number is not in menu range");
                    input = Console.ReadLine(); //writes on same line as above and then makes new line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r"); //Clears current line
                    Console.SetCursorPosition(0, Console.CursorTop - 1); //Moves cursor up one line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r"); //Clears current line
                    Console.SetCursorPosition(0, Console.CursorTop - 1); //Moves cursor up one line
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r"); //Clears current line
                }
                else
                {
                    selection = parsedInput;
                    break;
                }
            }
            return selection;
        }

        static public string StoreBuyMenu(Planet planet, Ship ship)
        {
            Console.Clear();

            Menu.ShowBanner(planet.PlanetName, ship);
            Console.WriteLine($"[1] Gold is available for {planet.PlanetGoldCost} coin\n" +
                $"[2] Fuel is available for {planet.PlanetFuelCost} coin\n" +
                $"[3] Hull repair parts are available for {planet.PlanetHullCost} coin\n" +
                $"[4] Ship gold capacity upgrade costs {Planet.ShipGoldUpgrade} coin\n" +
                $"[5] Ship fuel capacity upgrade costs {Planet.ShipFuelUpgrade} coin\n" +
                $"[6] Ship hull capacity upgrade costs {Planet.ShipHullUpgrade} coin\n" +
                $"[7] Cancel\n\n");
            int choice = GetUserInput(7);
            string message = "Did you make a selection?";
            switch (choice)
            {
                case 1:
                    message = "gold"; break;
                case 2:
                    message = "fuel"; break;
                case 3:
                    message = "hull"; break;
                case 4:
                    message = "gold ship"; break;
                case 5:
                    message = "fuel ship"; break;
                case 6:
                    message = "hull ship"; break;
                case 7:
                    message = "Returning to planet menu...";
                    Thread.Sleep(2000);
                    break;
            }
            return message;
        }
        static public string StoreSellMenu(Planet planet, Ship ship)
        {
            Console.Clear();
            ShowBanner(planet.PlanetName, ship);
            Console.WriteLine($"Select What you would like to Sell:\n[1] Gold\n[2] Fuel\n[3] Hull");
            int choice = GetUserInput(3);
            if (choice == 1) return "gold";
            else if (choice == 2) return "fuel";
            else if (choice == 3) return "hull";
            else return "Did you make a selection?";
        }

        static public int GetBuySellAmount(string buyOrsell)  //NOT SURE IF THIS WORKS YOLO
        {
            Console.WriteLine($"How much would you like to {buyOrsell}?");
            string input = Console.ReadLine();
            int j;
            while (!Int32.TryParse(input, out j))
            {
                Console.WriteLine("Please enter a valid number");
                Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");
                input = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop - 2);
                Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");
            }
            return j;

        }

        public static void LoadingDisplay()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".", i);
                Thread.Sleep(1000);
            }
            Console.Write("\r" + new string(' ', Console.WindowWidth - 15) + "\r");

        }


        public void GameOver()
        {
            //TODO
        }

        public void Credits()
        {
            //TODO
        }

        static void WriteColored(string s, ConsoleColor color, 
            ConsoleColor defaultColor = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(s);
            Console.ForegroundColor = defaultColor;
        }
    }
}
