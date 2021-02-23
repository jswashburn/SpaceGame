using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SpaceGame
{
    class Program
    {
        public static List<Ship> shipList = new List<Ship>();
        public static List<Planet> planetList = new List<Planet>();
        public static List<Difficulty> difficultyList = new List<Difficulty>();
        static void Main(string[] args)
        {
            #region PseudoCode

            {
                //start game
                //show main menu (jason)
                // get username
                // difficulty (jason TODO)
                // initialize planets
                // initialize events
                // initialize ship
                // show game setting/start scenario
                // while (!GameOver(checks fuel, hull, etc))
                //ShowPlanetOptions() "mine, store, sell, buy, getN&R, travel"
                //Get user input
                //if user input == store
                //p1.ShowStore FOR STARTING PLANET
                //if user input == sell
                //sell(name, amount)
                // Event
                //if user input == buy
                //buy(name, amount)
                // Event
                //if user input == mine
                //mine(days), run random event
                //if user input == get name and resources
                //GetNameResources()
                //if user input == travel
                //show all plannets except current
                //Show each planet natural resource and name
                //show DistanceToShip(shipX, shipY) = distance in days to planet
                //get user input
                //if user input == earth
                //if all upgrades TRUE and fuel and hull full then WinGame()
             //change planet, subtract days, run random event, shipX ShipY = PlanetX PlanetY
             }//Collapsed Pseudocode

            #endregion

            Console.SetWindowSize(150, 30);


            //start game
            //show main menu (jason)
            Menu menu = new Menu();
            menu.ShowMainMenu();
            int MenuOpt = Menu.GetUserInput(3);
            Console.Clear();

            if (MenuOpt == 1) // NEW GAME, GET USERNAME, get difficulty
            {
                Difficulty difficulty = Utility.PromptUserForDifficulty(); //this will be default
                difficultyList.Add(difficulty);
                Console.Clear();

                // initialize planets
                Planet p1 = new Planet("Gallifrey", "Gold", difficulty, 15, 100, 100);
                Planet p2 = new Planet("Cadia", "Gold", difficulty, 15, 100, 100);
                Planet p3 = new Planet("Caprica", "Fuel", difficulty, 100, 15, 100);
                Planet p4 = new Planet("Dagobah", "Fuel", difficulty, 100, 15, 100);
                Planet p5 = new Planet("Cybertron", "Hull Material", difficulty, 100, 200, 15);
                Planet[] planetArray = { p1, p2, p3, p4, p5 };
                foreach(Planet planet in planetArray) { planetList.Add(planet); }
                // initialize random object
                Random rng = new Random();

                //TODO check if planets are too close

     

                // initialize ship
                shipList.Add(new Ship(difficulty));
                Planet StartingPlanet = planetArray[rng.Next(5)]; //gets random starting planet

                // show game setting/start scenario
                Utility.ShowSetting(StartingPlanet, shipList[0].Time);
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
                shipList[0].CurrentPlanetName = StartingPlanet.PlanetName;
                

                
            }
            else if (MenuOpt == 2) // LOAD GAME
            {
                LoadJson();
            }
            else if (MenuOpt == 3)//EXIT
            {
                Console.WriteLine("Thank you for playing!");
                Thread.Sleep(2000);
                Environment.Exit(1);
            }
            // while (!GameOver(checks fuel, hull, etc))
            Ship ship = shipList[0];
            while (!CheckGameOver(ship))
            {
                Difficulty difficulty = difficultyList[0];
                // initialize events
                FuelEvent fuelEvent = new FuelEvent(difficulty);
                GoldEvent goldEvent = new GoldEvent(difficulty);
                HullEvent hullEvent = new HullEvent(difficulty);
                TimeEvent timeEvent = new TimeEvent(difficulty);
                EasterEggEvent easterEgg = new EasterEggEvent(difficulty);
                
                Planet[] planetArray = new Planet[planetList.Count];
                for (int i = 0; i < planetList.Count; i++)
                {
                    planetArray[i] = planetList[i];
                }
                Planet CurrentPlanet = new Planet();
                foreach(Planet planet in planetArray)
                {
                    if(planet.PlanetName == ship.CurrentPlanetName)
                    {
                        CurrentPlanet = planet;
                    }
                }

                Console.Clear();
                Menu.ShowBanner(CurrentPlanet.PlanetName, ship);
                Console.WriteLine(CurrentPlanet.ShowPlanetMenu(CurrentPlanet)); //"store, mine, travel, save, quit"

                int userInput = Menu.GetUserInput(5); //Because there are 4 things to do in the planet menu
                Console.Clear();
                if (userInput == (int)PlanetOptions.store)
                {
                    //ShowStore
                    Console.WriteLine(CurrentPlanet.ShowStore(CurrentPlanet, ship));
                    Console.WriteLine("Select [1] to buy, [2] to sell or [3] to cancel: ");
                    //int selection = Menu.GetUserInput(3);
                    //if (selection == 1)

                    userInput = Menu.GetUserInput(3);
                    if (userInput == (int)Store.buy) //input == 1
                    {
                        //get string of material, get amount they want(int)
                        string material = Menu.StoreBuyMenu(CurrentPlanet, ship);
                        if (material == "gold" || material == "fuel" || material == "hull")
                        {
                            int amount = Menu.GetAmount("buy"); //"how much would you like to BUY"
                            Console.WriteLine(CurrentPlanet.Buy(material, amount, ship, CurrentPlanet));//returns string
                            Console.WriteLine(goldEvent.Trigger(ship)); // returns string
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine(Planet.UpgradeShip(material, ship));
                            Console.WriteLine(goldEvent.Trigger(ship));
                            Console.ReadLine();
                        }
                    }
                    else if (userInput == (int)Store.sell) //input == 2
                    {
                        string material = Menu.StoreSellMenu(CurrentPlanet, ship);
                        int amount = Menu.GetAmount("sell"); //"how much would you like to SELL"
                        Console.WriteLine(CurrentPlanet.Sell(material, amount, ship, CurrentPlanet));
                        Console.WriteLine(goldEvent.Trigger(ship));
                        Console.ReadLine();
                    }
                    else if (userInput == (int)Store.cancel) //return user to previous menu
                    {
                        Console.WriteLine("Returning to planet menu...");
                        Console.WriteLine(easterEgg.Trigger(ship));
                        Thread.Sleep(2000);
                    }
                    else Console.WriteLine("What kind of input was that??");
                }
                else if (userInput == (int)PlanetOptions.mine)
                {
                    Menu.ShowBanner(CurrentPlanet.PlanetName, ship);
                    Console.WriteLine("How many days would you like to mine for?");
                    int NumDaysToMine = Menu.GetUserInput(ship.Time); //do error checking
                    Console.WriteLine(CurrentPlanet.Mine(NumDaysToMine, ship, CurrentPlanet));
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }

                else if (userInput == (int)PlanetOptions.travel)
                {
                    Console.WriteLine($"Current Planet: {CurrentPlanet.PlanetName}");
                    Console.WriteLine("Select Planet to travel to");
                    Menu.ShowBanner("", ship);
                    foreach (Planet planet in planetArray)
                    {
                        if (planet != CurrentPlanet)
                        {
                            //Show each planet natural resource and name
                            Console.WriteLine($"[{Array.IndexOf(planetArray, planet)}] {planet.GetNameAndResource()}");
                            //show DistanceToShip(shipX, shipY) = distance in days to planet
                            Console.WriteLine($"Distance from ship in days: {planet.DistanceToShip(planet.PlanetCords.Item1, planet.PlanetCords.Item2, CurrentPlanet)}");
                        }
                    }
                    Console.WriteLine($"[{planetArray.Length}] Planet: Earth");
                    Console.WriteLine($"[{planetArray.Length + 1}] Return to Store");

                    //get user input
                    bool ZeroIndex = true;
                    userInput = Menu.GetUserInput(planetArray.Length + 1, ZeroIndex);
                    if (userInput == planetArray.Length)//user selects earth, the last planet in the options
                    {
                        if (TravelToEarth(ship) == false) //means that not all upgrades are complete
                        {
                            Console.WriteLine(
                                "\nSorry but your ship will not make the journey, select a different planet\nPress any key to continue...");
                            Console.ReadLine();
                        }
                        //else//game == win
                    }
                    else if (userInput == planetArray.Length + 1)
                    {
                        //blank and returns to store
                    }
                    else
                    {
                        int distanceToPlanet = planetArray[userInput].DistanceToShip(planetArray[userInput].PlanetCords.Item1,
                                planetArray[userInput].PlanetCords.Item2, CurrentPlanet);

                        Console.WriteLine($"Traveling to planet {planetArray[userInput].PlanetName}....");
                        Menu.LoadingDisplay();
                        Console.WriteLine($"{distanceToPlanet} days later...");
                        //gets number of days travel it takes to get to destination planet
                        //change planet, subtract days, run random event, shipX ShipY = PlanetX PlanetY
                        CurrentPlanet = planetArray[userInput];
                        ship.Time -= distanceToPlanet;
                        Console.Clear();
                        Console.WriteLine(timeEvent.Trigger(ship));
                        Console.WriteLine(hullEvent.Trigger(ship));
                        Console.WriteLine(fuelEvent.Trigger(ship));
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                }
                else if (userInput == (int)PlanetOptions.save)
                {
                    SaveJson();
                    Console.WriteLine("Saved successfully");
                    Console.ReadLine();
                }
                else if (userInput == (int)PlanetOptions.quit)
                {
                    Environment.Exit(0);
                }

                else { Console.WriteLine("Enter something valid. Press any key to continue..."); Console.ReadLine(); }
            }
        }

        static bool CheckGameOver(Ship ship)
        {
            if (ship.Hull <= 0)
                return true;
            if (ship.Time <= 0)
                return true;
            else return false;
        }

        static bool TravelToEarth(Ship ship)
        {
            if (ship.HullUpgrade && ship.FuelUpgrade && ship.Fuel == ship.FuelMax && ship.Hull == ship.HullMax)
                return true;
            else return false;
        }

        public static void SaveJson()
        {
            if (!File.Exists("./planets.json"))
            {
                File.CreateText("./planets.json");
            }
            if (!File.Exists("./ship.json"))
            {
                File.CreateText("./ship.json");
            }
            if (!File.Exists("./difficulty.json"))
            {
                File.CreateText("./difficulty.json");
            }
            string jsonPlanet = JsonSerializer.Serialize(planetList);
            string jsonShip = JsonSerializer.Serialize(shipList);
            string jsonDifficulty = JsonSerializer.Serialize(difficultyList);
            File.WriteAllText("planets.json", jsonPlanet);
            File.WriteAllText("ship.json", jsonShip);
            File.WriteAllText("difficulty.json", jsonDifficulty);
        }

        public static void LoadJson()
        {
            try
            {
                if (File.Exists("./ship.json") && File.Exists("./planets.json") && File.Exists("./difficulty.json"))
                {
                    string jsonPlanet = File.ReadAllText("./planets.json");
                    string jsonShip = File.ReadAllText("./ship.json");
                    string jsonDifficulty = File.ReadAllText("./difficulty.json");
                    shipList = JsonSerializer.Deserialize<List<Ship>>(jsonShip);
                    planetList = JsonSerializer.Deserialize<List<Planet>>(jsonPlanet);
                    difficultyList = JsonSerializer.Deserialize <List<Difficulty>>(jsonDifficulty);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error loading your files:\n\n\n{e}");
            }
            
        }
    }
}
