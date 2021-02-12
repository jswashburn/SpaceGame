using System;
using System.Threading;


namespace SpaceGame
{
    // RANDOM EVENT SYSTEM DEMO

    class Program
    {
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
                //TODO maksim static gameover method()
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


            //start game
                //show main menu (jason)
            Menu menu = new Menu();
            menu.ShowMainMenu();

            if (menu.GetUserInput(3) == 1) // NEW GAME, GET USERNAME
            {
                // difficulty (jason TODO)
                Difficulty difficulty = Difficulty.Easy; //this will be intialized by the player
                // initialize planets
                Planet p1 = new Planet("Gallifrey", "Gold", difficulty, 15, 100, 100);
                Planet p2 = new Planet("Cadia", "Gold", difficulty, 15, 100, 100);
                Planet p3 = new Planet("Caprica", "Fuel", difficulty, 100, 15, 100);
                Planet p4 = new Planet("Dagobah", "Fuel", difficulty, 100, 15, 100);
                Planet p5 = new Planet("Cybertron", "Hull Material", difficulty, 100, 200, 15);
                Planet[] planetArray = {p1, p2, p3, p4, p5};
                // initialize random object
                Random rng = new Random();
                // initialize events

                // initialize ship
                Ship ship = new Ship();
                Planet StartingPlanet = planetArray[rng.Next(5)]; //gets random starting planet
                // show game setting/start scenario
                Utility.ShowSetting(StartingPlanet, ship.Time);
                Planet CurrentPlanet = StartingPlanet;
                // while (!GameOver(checks fuel, hull, etc))
                while (GameOver() == false)//TODO maksim static gameover method()
                {
                    CurrentPlanet.ShowPlanetOptions();//"mine, store, sell, buy, getN&R, travel"
                    
                    int userInput = menu.GetUserInput(4); //Because there are 4 things to do in the planet menu
                    if (userInput == (int)PlanetOptions.store)
                    {
                        //p1.ShowStore FOR STARTING PLANET
                        userInput = menu.GetUserInput(2);
                        if (userInput == (int)Store.sell) //input == 1
                        {
                            //sell(name, amount)
                            // Event
                        }
                        else if (userInput == (int)Store.buy) //input == 2
                        {
                            //buy(name, amount)
                            // Event
                        }
                        else //return user to prevoius menu
                        {
                            Console.WriteLine("Returning to planet menu...");
                            Thread.Sleep(2000);
                        }

                    }
                    else if (userInput == (int)PlanetOptions.mine)
                    {
                        //mine(days), run random event
                    }
                    else if (userInput == (int)PlanetOptions.get_name_and_resources)
                    {
                        CurrentPlanet.GetNnameAndResource(); //GetNameResources()
                    }
                    else if (userInput == (int)PlanetOptions.travel)
                    {

                            //show all plannets except current
                            //Show each planet natural resource and name
                                //show DistanceToShip(shipX, shipY) = distance in days to planet
                                //get user input
                                //if user input == earth
                                    //if all upgrades TRUE and fuel and hull full then WinGame()
                                //change planet, subtract days, run random event, shipX ShipY = PlanetX PlanetY
                    }
                    
                }
            }
            else if (menu.GetUserInput(3) == 2) // LOAD GAME
            {
                //TODO prompt for username and load game if exists
            }
            else //EXIT
            {
                Console.WriteLine("Thank you for playing!");
                Thread.Sleep(2000);
                Environment.Exit(1);
            }

        }
    }
}
