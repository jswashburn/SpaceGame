namespace SpaceGame
{
    // EXAMPLE SHIP FOR DEMO ONLY - Jeffrey
    class Ship
    {
        public int Time { get; set; }
        public int Coins { get; set; }
        public int DarkMatter { get; set; }
        public int DarkMatterMax { get; set; }  //Tuple?
        public int Gold { get; set; }
        public int GoldMax { get; set; }       //Tuple?
        public int Hull { get; set; }
        public int HullMax { get; set; }       //Tuple?
        public string Upgrade { get; set; }
        public int Fuel { get; set; }
        public int FuelCapacity { get; set; }
        public int HullIntegrity { get; set; }
        public int ResourceCapacity { get; set; }
        public Ship()
        {
            Time = 500;
            Coins = 0;
            DarkMatter = 0;
            DarkMatterMax = 5000;
            Gold = 0;
            GoldMax = 5000;
            Hull = 0;
            HullMax = 5000;
            Upgrade = " ";
        }

        public override string ToString()
        {
            return $"[SHIP] Hull integrity: {HullIntegrity}, Coin: {Coins}, Fuel: {Fuel}\n" +
                $"[DAYS UNTIL EARTHS DESTRUCTION]: {Time}";
        }
    }
}
