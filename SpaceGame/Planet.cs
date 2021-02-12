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
        string PlanetName { get; set; }
        string PlanetResource { get; set; }
        int PlanetDifficulty { get; set; }
        (int, int) PlanetCords { get; set; }
        int PlanetGoldCost { get; set; }
        int PlanetFuelCost { get; set; }
        int PlanetHullCost { get; set; }


        internal Planet(string name, string resource, Difficulty difficulty, int goldCost, int fuelCost, int hullCost)
        {
            Random r = new Random();
            this.PlanetName = name;
            this.PlanetResource = resource;
            this.PlanetDifficulty = (int)difficulty++;
            this.PlanetCords = (r.Next(1, 101), r.Next(1, 101));
            this.PlanetGoldCost = goldCost;
            this.PlanetFuelCost = fuelCost;
            this.PlanetHullCost = hullCost;
        }

        internal int DistanceToShip(int x, int y) //returns difference of caller and current planet
        {
            double z = Math.Sqrt((x * x) + (y * y));
            (int px, int py) = PlanetCords;
            double pz = Math.Sqrt((px * px) + (py * py));
            return (int)Math.Round(Math.Abs(z - pz));
            //TODO convert PZ into days
        }

        internal string ShowStore()
        {
            return $"Gold is available for {PlanetGoldCost} coin\nFuel is available for {PlanetFuelCost} coin\nHull repair parts are avable for {PlanetHullCost} coin";
        }

        internal int Sell(string switchCase, int amount)
        {
            int coin = 0;
            switch (switchCase.ToLower())
            {
                case "gold":
                    coin = amount * PlanetGoldCost / PlanetDifficulty;
                    break;
                case "fuel":
                    coin = amount * PlanetFuelCost / PlanetDifficulty;
                    break;
                case "hull":
                    coin = amount * PlanetHullCost / PlanetDifficulty;
                    break;
            }
            return coin;
        }

        internal int Buy(string switchCase, int amount)
        {
            int item = 0;
            switch (switchCase.ToLower())
            {
                case "gold":
                    item = amount * PlanetGoldCost;
                    break;
                case "fuel":
                    item = amount * PlanetFuelCost;
                    break;
                case "hull":
                    item = amount * PlanetHullCost;
                    break;
            }
            return item;
        }

        internal int Mine(int days)
        {
            return days *= PlanetDifficulty * PlanetDifficulty;
        }

        internal (string, string) GetNnameAndResource() => (PlanetName, PlanetResource);
    }
}