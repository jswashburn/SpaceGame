namespace SpaceGame
{
    // EXAMPLE SHIP FOR DEMO ONLY - Jeffrey
    class Ship
    {
        public int HullIntegrity { get; set; }
        public int Coins { get; set; }
        public int Fuel { get; set; }
        public int Time { get; set; }

        public Ship()
        {
            Coins = 100;
            HullIntegrity = 100;
            Fuel = 100;
            Time = 500;
        }

        public override string ToString()
        {
            return $"[SHIP] Hull integrity: {HullIntegrity}, Coin: {Coins}, Fuel: {Fuel}\n" +
                $"[DAYS UNTIL EARTHS DESTRUCTION]: {Time}";
        }
    }
}
