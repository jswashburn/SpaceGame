namespace SpaceGame
{
    class Ship
    {
        public int Time { get; set; }    
        public int Coins { get; set; }      
        public int Fuel { get; set; }
        public int Gold { get; set; }
        public int Hull { get; set; }    
        public int FuelMax { get; set; }
        public int GoldMax { get; set; }      
        public int HullMax { get; set; }     
        public bool FuelUpgrade { get; set; }
        public bool GoldUpgrade { get; set; }
        public bool HullUpgrade { get; set; }

        public Ship(Difficulty difficulty)
        {
            Time = 500 * (int)difficulty;  //days
            Coins = 100000 / (int)difficulty;
            Fuel = 1000 / (int)difficulty;
            Gold = 100;
            Hull = 1000 / (int)difficulty;
            FuelMax = 1000 / (int)difficulty;
            GoldMax = 1000 / (int)difficulty;
            HullMax = 1000 / (int)difficulty;
            FuelUpgrade = false;
            GoldUpgrade = false;
            HullUpgrade = false;
        }

        public override string ToString()
        {
            return $"[SHIP] Hull integrity: {Hull}/{HullMax}, Coin: {Coins}, Fuel: {Fuel}/{FuelMax}, Gold resource: {Gold}\n" +
                $"[DAYS UNTIL EARTHS DESTRUCTION]: {Time}";
        }
    }
}
