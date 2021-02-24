using System;

namespace SpaceGame
{
    public class GameStateManager
    {
        public Planet CurrentPlanet { get; set; }
        public Planet[] Planets { get; set; }

        public FuelEvent FuelEvent { get; set; }
        public CoinEvent CoinEvent { get; set; }
        public HullEvent HullEvent { get; set; }
        public TimeEvent TimeEvent { get; set; }
        public EasterEggEvent EasterEgg { get; set; }

        public Ship Ship { get; set; }
        public Difficulty Difficulty { get; set; }

        public bool GameOver
        {
            get
            {
                if (Ship != null)
                    return Ship.Hull <= 0 || Ship.Time <= 0;
                else return true;
            }
        }

        public GameStateManager() { }

        public GameStateManager(Difficulty difficulty)
        {
            Difficulty = difficulty;
            Ship = new Ship(difficulty);
            LoadPlanets(difficulty);
            LoadEvents(difficulty);
            CurrentPlanet = this.Planets[new Random().Next(Planets.Length)];
        }

        void LoadPlanets(Difficulty difficulty)
        {
            Planets = new Planet[]
            {
                new Planet("Gallifrey", "Gold", difficulty, 15, 100, 100),
                new Planet("Cadia", "Gold", difficulty, 15, 100, 100),
                new Planet("Caprica", "Fuel", difficulty, 100, 15, 100),
                new Planet("Dagobah", "Fuel", difficulty, 100, 15, 100),
                new Planet("Cybertron", "Hull Material", difficulty, 100, 200, 15)
            };
        }

        void LoadEvents(Difficulty difficulty)
        {
            FuelEvent = new FuelEvent(difficulty);
            CoinEvent = new CoinEvent(difficulty);
            HullEvent = new HullEvent(difficulty);
            TimeEvent = new TimeEvent(difficulty);
            EasterEgg = new EasterEggEvent(difficulty);
        }
    }
}
