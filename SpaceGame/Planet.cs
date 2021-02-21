using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
    public enum Store
    {
        buy = 1,
        sell
    }

    public enum PlanetOptions
    {
        store = 1,
        mine,
        travel,
        returnToStore,
    }

    internal class Planet
    {
        public string PlanetName { get; private set; }
        public string PlanetResource { get; private set; }
        int PlanetDifficulty { get; set; }
        public (int, int) PlanetCords { get; set; }
        public int PlanetGoldCost { get; private set; }
        public int PlanetFuelCost { get; private set; }
        public int PlanetHullCost { get; private set; }
        public static int ShipGoldUpgrade { get; set; }
        public static int ShipFuelUpgrade { get; set; }
        public static int ShipHullUpgrade { get; set; }

        public string ShowPlanetMenu(Planet planet)
        {
            return $"Planet: {PlanetName}. Natural resource: {PlanetResource}.\n[1] Store\n[2] Mine\n[3] Travel ";
        }

        
        internal Planet(string name, string resource, Difficulty difficulty, int goldCost, int fuelCost, int hullCost)
        {
            Random r = new Random();
            this.PlanetName = name;
            this.PlanetResource = resource;
            this.PlanetDifficulty = (int)difficulty + 1;
            this.PlanetCords = (r.Next(1, 251), r.Next(1, 251));
            this.PlanetGoldCost = goldCost;
            this.PlanetFuelCost = fuelCost;
            this.PlanetHullCost = hullCost;
            ShipGoldUpgrade = 1200 / (int)difficulty;
            ShipFuelUpgrade = 3000 / (int)difficulty;
            ShipHullUpgrade = 3000 / (int)difficulty;
        }

        internal int DistanceToShip(int x, int y, Planet CurrentPlanet) //returns difference of caller and current planet
        {
            double z = Math.Sqrt((x * x) + (y * y));
            (int px, int py) = CurrentPlanet.PlanetCords;
            double pz = Math.Sqrt((px * px) + (py * py));
            return ((int)Math.Round(Math.Abs(z - pz))) / 10;
        }

        internal string ShowStore(Planet planet, Ship ship)
        {
            Menu.ShowBanner(planet.PlanetName, ship);
            return $"Gold is available for {planet.PlanetGoldCost} coin\n" +
                $"Fuel is available for {planet.PlanetFuelCost} coin\n" +
                $"Hull repair parts are avable for {planet.PlanetHullCost} coin\n" +
                $"Ship gold capacity upgrade costs {ShipGoldUpgrade} coin\n" +
                $"Ship fuel capacity upgrade costs {ShipFuelUpgrade} coin\n" +
                $"Ship hull capacity upgrade costs {ShipHullUpgrade} coin\n\n";
        }

        internal string Sell(string switchCase, int amount, Ship ship, Planet planet)
        {
            int sale = 0;
            switch (switchCase.ToLower())
            {
                case "gold":
                    if (amount > ship.Gold) amount = ship.Gold;
                    sale = amount * planet.PlanetGoldCost / planet.PlanetDifficulty;
                    ship.Coins += sale;
                    ship.Gold -= amount;
                    break;
                case "fuel":
                    if (amount > ship.Fuel) amount = ship.Fuel;
                    sale = amount * planet.PlanetFuelCost / planet.PlanetDifficulty;
                    ship.Coins += sale;
                    ship.Fuel -= amount;
                    break;
                case "hull":
                    if (amount > ship.Hull) amount = ship.Hull;
                    sale = amount * planet.PlanetHullCost / planet.PlanetDifficulty;
                    ship.Coins += sale;
                    ship.Hull -= amount;
                    break;
            }
            return $"You sold {amount} of {switchCase} for {sale} coin. You now have {ship.Coins} coin.";
        }

        internal string Buy(string switchCase, int amount, Ship ship, Planet planet)
        {
            string message = "";
            switch (switchCase.ToLower())
            {
                case "gold":
                    if (amount + ship.Gold > ship.GoldMax) amount = ship.GoldMax - ship.Gold;
                    if (amount * planet.PlanetGoldCost > ship.Coins) amount = ship.Coins / planet.PlanetGoldCost;
                    ship.Gold += amount;
                    ship.Coins -= amount * planet.PlanetGoldCost;
                    message = $"You  bought {amount} gold. You have {ship.Coins} coin left.";
                    break;
                case "fuel":
                    if (amount + ship.Fuel > ship.FuelMax) amount = ship.FuelMax - ship.Fuel;
                    if (amount * planet.PlanetFuelCost > ship.Coins) amount = ship.Coins / planet.PlanetFuelCost;
                    ship.Fuel += amount;
                    ship.Coins -= amount * planet.PlanetFuelCost;
                    message = $"You  bought {amount} Fuel. You have {ship.Coins} coin left.";
                    break;
                case "hull":
                    if (amount + ship.Hull > ship.HullMax) amount = ship.HullMax - ship.Hull;
                    if (amount * planet.PlanetHullCost > ship.Coins) amount = ship.Coins / planet.PlanetHullCost;
                    ship.Hull += amount;
                    ship.Coins -= amount * planet.PlanetHullCost;
                    message = $"You  bought {amount} hull material. You have {ship.Coins} coin left.";
                    break;
            }
            return message;
        }
        internal static string UpgradeShip(string switchCase, Ship ship)
        {
            string message = "Upgrade Completed!";
            switch (switchCase.ToLower())
            {
                case "gold ship":
                    if (ship.GoldUpgrade)
                    {
                        message = "You already have that upgrade";
                        break;
                    }
                    if (ship.Coins < ShipGoldUpgrade) message = "You can't afford this upgrade.";
                    else
                    {
                        ship.Coins -= ShipGoldUpgrade;
                        ship.GoldUpgrade = true;
                        ship.GoldMax *= 2;
                    }
                    break;
                case "fuel ship":
                    if (ship.FuelUpgrade)
                    {
                        message = "You already have that upgrade";
                        break;
                    }
                    if (ship.Coins < ShipFuelUpgrade) message = "You can't afford this upgrade.";
                    else
                    {
                        ship.Coins -= ShipFuelUpgrade;
                        ship.FuelUpgrade = true;
                        ship.FuelMax *= 2;
                    }
                    break;
                case "hull ship":
                    if (ship.HullUpgrade)
                    {
                        message = "You already have that upgrade";
                        break;
                    }
                    if (ship.Coins < ShipHullUpgrade) message = "You can't afford this upgrade.";
                    else
                    {
                        ship.Coins -= ShipHullUpgrade;
                        ship.HullUpgrade = true;
                        ship.HullMax *= 2;
                    }
                    break;
            }
            return message;
        }

        internal string Mine(int days, Ship ship, Planet planet)
        {
            string message = "";
            if (days > ship.Time) message = "You can't mine this long or Earth will perish!";
            else
            {
                int x = 0;
                int z = 0;
                string mining = "";
                string shipGold = nameof(ship.Gold).ToString();
                string shipFuel = nameof(ship.Fuel).ToString();
                string shipHull = nameof(ship.Hull).ToString() + " Material";
                if (planet.PlanetResource == shipGold)
                {
                    mining = "Gold";
                    x += days * planet.PlanetDifficulty * planet.PlanetDifficulty;
                    ship.Gold += x;
                    if (ship.Gold > ship.GoldMax) z = ship.Gold - ship.GoldMax; ship.Gold -= z;
                }
                if (planet.PlanetResource == shipFuel)
                {
                    mining = "Fuel";
                    x += days * planet.PlanetDifficulty * planet.PlanetDifficulty;
                    ship.Fuel += x;
                    if (ship.Fuel > ship.FuelMax) z = ship.Fuel - ship.FuelMax; ship.Fuel -= z;
                }
                if (planet.PlanetResource == shipHull)
                {
                    mining = "Hull Material";
                    x += days * planet.PlanetDifficulty * planet.PlanetDifficulty;
                    ship.Hull += x;
                    if (ship.Hull > ship.HullMax) z = ship.Hull - ship.HullMax; ship.Hull -= z;
                }
                ship.Time -= days;
                if (z == 0) message = $"You mined {x} {mining}. This cost you {days} days";
                else message = $"You mined {x} {mining} but had to leave {z} behind because you ran out of space. This cost you {days} days";
            }
            return message;
        }

        internal string GetNameAndResource() => $"Planet: {PlanetName}. Natural resource: {PlanetResource}.";
    }
}