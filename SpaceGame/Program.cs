using System;
using System.Threading;
using System.IO;
using System.Text.Json;

namespace SpaceGame
{
    class Program
    {
        static GameStateManager game;

        static void Main(string[] args)
        {
            StartMenu();

            while (!game.GameOver)
                PlanetMenu();
        }

        #region StartMenu
        static void StartMenu()
        {
            Console.SetWindowSize(150, 30);

            //show main menu
            new Menu().ShowMainMenu();
            int MenuOpt = Menu.GetUserInput(3);
            Console.Clear();

            switch (MenuOpt)
            {
                case 1:
                    {
                        StartNewGame();
                        ShowStartScenario();
                        break;
                    }
                case 2:
                    {
                        Load();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Thank you for playing!");
                        Thread.Sleep(2000);
                        Environment.Exit(1);
                        break;
                    }
                default:
                    break;
            }
        }

        static void StartNewGame()
        {
            // Start a new game, initialize objects
            game = new GameStateManager(Utility.PromptUserForDifficulty());
            Console.Clear();
        }

        static void ShowStartScenario()
        {
            Utility.ShowSetting(game.CurrentPlanet, game.Ship.Time);
            Console.Write("\nPress any key to continue ");
            Console.ReadKey();
        }
        #endregion

        #region Planet Menu
        static void PlanetMenu()
        {
            ShowPlanetOptions();

            int planetMenuOption = Menu.GetUserInput(5);
            Console.Clear();
            switch (planetMenuOption)
            {
                case (int)PlanetOptions.Store:
                    {
                        StoreMenu();
                        break;
                    }
                case (int)PlanetOptions.Mine:
                    {
                        MineMenu();
                        break;
                    }
                case (int)PlanetOptions.Travel:
                    {
                        TravelMenu();
                        break;
                    }
                case (int)PlanetOptions.Save:
                    {
                        Save();
                        Console.WriteLine("Saved successfully");
                        Console.ReadLine();
                        break;
                    }
                case (int)PlanetOptions.Quit:
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Enter something valid. Press any key to continue...");
                        Console.ReadLine();
                        break;
                    }
            }
        }

        static void TravelMenu()
        {
            ShowTravelOptions();

            int numberPlanets = game.Planets.Length;
            int selectedPlanet = Menu.GetUserInput(numberPlanets + 1, true);

            if (selectedPlanet == numberPlanets) // Selected Earth
            {
                // Player does not have all upgrades
                if (!game.Ship.CanTravelToEarth)
                {
                    Console.WriteLine(
                        "\nSorry but your ship will not make the journey, select a different planet\nPress any key to continue...");
                    Console.ReadLine();
                }
            }
            else if (selectedPlanet == numberPlanets + 1) { } // Return to Planet Menu
            else
                TravelTo(selectedPlanet);
        }

        static void MineMenu()
        {
            Menu.ShowBanner(game.CurrentPlanet.PlanetName, game.Ship);
            Console.WriteLine("How many days would you like to mine for?");
            int NumDaysToMine = Menu.GetUserInput(game.Ship.Time);
            Console.WriteLine(game.CurrentPlanet.Mine(NumDaysToMine, game.Ship, game.CurrentPlanet));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void StoreMenu()
        {
            Menu.ShowBanner(game.CurrentPlanet.PlanetName + " Store", game.Ship);
            Console.WriteLine("Select [1] to buy, [2] to sell or [3] to cancel: \n");

            // Show store selection
            game.CurrentPlanet.ShowStore();

            switch (Menu.GetUserInput(3))
            {
                case (int)Store.Buy:
                    {
                        // Get requested material and amount
                        string material = Menu.StoreBuyMenu(game.CurrentPlanet, game.Ship);
                        if (material == "gold" || material == "fuel" || material == "hull")
                        {
                            int amount = Menu.GetBuySellAmount("buy");
                            Console.WriteLine(game.CurrentPlanet.Buy(material, amount, game.Ship, game.CurrentPlanet));
                            Console.WriteLine(game.CoinEvent.Trigger(game.Ship));
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine(Planet.UpgradeShip(material, game.Ship));
                            Console.WriteLine(game.CoinEvent.Trigger(game.Ship));
                            Console.ReadLine();
                        }

                        break;
                    }
                case (int)Store.Sell:
                    {
                        // Get requested material and amount
                        string material = Menu.StoreSellMenu(game.CurrentPlanet, game.Ship);
                        int amount = Menu.GetBuySellAmount("sell");
                        Console.WriteLine(game.CurrentPlanet.Sell(material, amount, game.Ship, game.CurrentPlanet));
                        Console.WriteLine(game.CoinEvent.Trigger(game.Ship));
                        Console.ReadLine();
                        break;
                    }
                case (int)Store.Cancel: //return user to previous menu
                    {
                        Console.WriteLine("Returning to planet menu...");
                        Console.WriteLine(game.EasterEgg.Trigger(game.Ship));
                        Thread.Sleep(2000);
                        break;
                    }
                default:
                    Console.WriteLine("What kind of input was that??");
                    break;
            }
            return;
        }

        static void ShowPlanetOptions()
        {
            Console.Clear();
            Menu.ShowBanner(game.CurrentPlanet.PlanetName, game.Ship);
            game.CurrentPlanet.PlanetOptions();
        }

        static void ShowTravelOptions()
        {
            Console.WriteLine($"Current Planet: {game.CurrentPlanet.PlanetName}\n");
            Menu.ShowBanner("Select Planet to travel to", game.Ship);
            game.Planets.ShowDetails(game.CurrentPlanet);
        }

        static void TravelTo(int selectedPlanet)
        {
            // Gets days travel it takes to get to destination planet
            int distanceToPlanet = game.Planets[selectedPlanet]
                .DistanceToShip(game.Planets[selectedPlanet].PlanetCordsX,
                game.Planets[selectedPlanet].PlanetCordsY, game.CurrentPlanet);

            Console.WriteLine($"Traveling to planet {game.Planets[selectedPlanet].PlanetName}....");
            Menu.LoadingDisplay();
            Console.WriteLine($"{distanceToPlanet} days later...");

            // Cycle to next planet, subtract days, run random events
            game.CurrentPlanet = game.Planets[selectedPlanet];
            game.Ship.Time -= distanceToPlanet;

            Console.Clear();
            Console.WriteLine(game.TimeEvent.Trigger(game.Ship));
            Console.WriteLine(game.HullEvent.Trigger(game.Ship));
            Console.WriteLine(game.FuelEvent.Trigger(game.Ship));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        #endregion

        #region JSON Save/Load
        static void Save()
        {
            string json = JsonSerializer.Serialize(game);
            File.WriteAllText("game.json", json); // Overwrites file if already exists
        }

        static void Load()
        {
            try
            {
                if (File.Exists("./game.json"))
                {
                    string json = File.ReadAllText("./game.json");
                    game = JsonSerializer.Deserialize<GameStateManager>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error loading your files:\n\n\n{e}");
            }
        }
        #endregion
    }
}
