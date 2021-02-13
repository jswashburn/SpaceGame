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
        get_name_and_resources,
        travel
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
        static int ShipGoldUpgrade { get; set; }
        static int ShipFuelUpgrade { get; set; }
        static int ShipHullUpgrade { get; set; }

        public string ShowPlanetMenu(Planet planet)
        {
            return $"Planet: {PlanetName}. Natural resource: {PlanetResource}.\n[1] Store\n[2] Mine\n[3] Travel ";
        }

        public string ShowPlanetStoreMenu(Planet planet, Ship ship)
        {
            Menu.ShowBanner(planet.PlanetName, ship);
            return $"Planet Resource = {planet.PlanetResource}\n[1] Gold: {planet.PlanetGoldCost}\n" +
                              $"[2] Fuel: {planet.PlanetFuelCost}\n[3] Hull: {planet.PlanetHullCost}";
        }
        
        internal Planet(string name, string resource, Difficulty difficulty, int goldCost, int fuelCost, int hullCost)
        {
            Random r = new Random();
            this.PlanetName = name;
            this.PlanetResource = resource;
            this.PlanetDifficulty = (int)difficulty++;
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

        internal string ShowStore()
        {
            return $"Gold is available for {PlanetGoldCost} coin\n" +
                $"Fuel is available for {PlanetFuelCost} coin\n" +
                $"Hull repair parts are avable for {PlanetHullCost} coin\n\n" +
                $"Ship gold capacity upgrade costs {ShipGoldUpgrade} coin\n" +
                $"Ship fuel capacity upgrade costs {ShipFuelUpgrade} coin\n" +
                $"Ship hull capacity upgrade costs {ShipHullUpgrade} coin";
        }

        internal string Sell(string switchCase, int amount, Ship ship)
        {
            switch (switchCase.ToLower())
            {
                case "gold":
                    if (amount > ship.Gold) amount = ship.Gold;
                    ship.Coins += amount * PlanetGoldCost / PlanetDifficulty;
                    ship.Gold -= amount;
                    break;
                case "fuel":
                    if (amount > ship.Fuel) amount = ship.Fuel;
                    ship.Coins = amount * PlanetFuelCost / PlanetDifficulty;
                    ship.Fuel -= amount;
                    break;
                case "hull":
                    if (amount > ship.Hull) amount = ship.Hull;
                    ship.Coins = amount * PlanetHullCost / PlanetDifficulty;
                    ship.Hull -= amount;
                    break;
            }
            return $"You sold {amount} of {switchCase}. You now have {ship.Coins} coin.";
        }

        internal string Buy(string switchCase, int amount, Ship ship)
        {
            string message = "";
            switch (switchCase.ToLower())
            {
                case "gold":
                    if (amount + ship.Gold > ship.GoldMax) amount = ship.GoldMax - ship.Gold;
                    if (amount * PlanetGoldCost > ship.Coins) amount = ship.Coins / PlanetGoldCost;
                    ship.Gold += amount;
                    ship.Coins -= amount * PlanetGoldCost;
                    message = $"You  bought {amount} gold. You have {ship.Coins} coin left.";
                    break;
                case "fuel":
                    if (amount + ship.Fuel > ship.FuelMax) amount = ship.FuelMax - ship.Fuel;
                    if (amount * PlanetFuelCost > ship.Coins) amount = ship.Coins / PlanetFuelCost;
                    ship.Fuel += amount;
                    ship.Coins -= amount * PlanetFuelCost;
                    message = $"You  bought {amount} Fuel. You have {ship.Coins} coin left.";
                    break;
                case "hull":
                    if (amount + ship.Hull > ship.HullMax) amount = ship.HullMax - ship.Hull;
                    if (amount * PlanetHullCost > ship.Coins) amount = ship.Coins / PlanetHullCost;
                    ship.Hull += amount;
                    ship.Coins -= amount * PlanetHullCost;
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
                case "gold":
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
                case "fuel":
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
                case "hull":
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

        internal string Mine(int days, Ship ship)
        {
            //TODO mining does not collect resource
            string message = "";
            if (days > ship.Time) message = "You can't mine this long or Earth will perish!";
            else
            {
                int x = 0;
                int z = 0;
                string mining = "";
                string shipGold = nameof(ship.Gold).ToString().ToLower();
                string shipFuel = nameof(ship.Fuel).ToString().ToLower();
                string shipHull = nameof(ship.Hull).ToString().ToLower();
                if (PlanetResource == shipGold)
                {
                    mining = "gold";
                    x += days * PlanetDifficulty * PlanetDifficulty;
                    ship.Gold += x;
                    if (ship.Gold > ship.GoldMax) z = ship.Gold - ship.GoldMax; ship.Gold -= z;
                }
                if (PlanetResource == shipFuel)
                {
                    mining = "fuel";
                    x += days * PlanetDifficulty * PlanetDifficulty;
                    ship.Fuel += x;
                    if (ship.Fuel > ship.Fuel) z = ship.Fuel - ship.FuelMax; ship.Fuel -= z;
                }
                if (PlanetResource == shipHull)
                {
                    mining = "hull";
                    x += days * PlanetDifficulty * PlanetDifficulty;
                    ship.Hull += x;
                    if (ship.Hull > ship.HullMax) z = ship.Hull - ship.HullMax; ship.Hull -= z;
                }
                ship.Time -= days;
                if (z == 0) message = $"You mined {x} {mining}. This cost you {days} days";
                else message = $"You mined {x} {mining} but had to leave {z} behind because you ran out of space. This cost you {days} days";
            }
            return message;
        }

        internal string GetNnameAndResource() => $"Planet: {PlanetName}. Natural resource: {PlanetResource}.";
    }
}