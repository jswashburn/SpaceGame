using System;

namespace SpaceGame
{
    class FuelEvent : RandomEvent
    {
        // Property getters scale amount of fuel lost/gained with difficulty
        int fuelLost => new Random().Next(difficulty / 3, difficulty * 2);
        int fuelGained => new Random().Next(difficulty, difficulty * 2);

        // Use the base class constructor
        public FuelEvent(Difficulty difficulty) : base(difficulty) { }

        protected override string NegativeEvent(Ship ship)
        {
            ship.Fuel -= fuelLost;
            return GetNegativeEventMessage();
        }

        protected override string PositiveEvent(Ship ship)
        {
            ship.Fuel += fuelGained;
            return GetPositiveEventMessage();
        }

        protected override string GetNegativeEventMessage()
        {
            string[] fuelMessages = new string[]
            {
                $"The power has gone out and you had to use the generator, costing you {fuelLost} fuel.",
                $"You were holding the map upside down and strayed off course, costing you {fuelLost} fuel.",
                $"You realize you forgot your wallet at the last planet. You turn around and use and extra {fuelLost} fuel.",
                $"You have to go around an asteroid field, and use an extra {fuelLost} fuel.",
                $"A fuel leak you failed to identify during last weeks pm's has cost you {fuelLost} fuel."
            };

            return fuelMessages[new Random().Next(fuelMessages.Length)];
        }

        protected override string GetPositiveEventMessage()
        {
            string[] fuelMessages = new string[]
            {
                $"Your GPS has shown you a faster route to the next planet, saving you {fuelGained} fuel.",
                $"While traveling, you encounter a destroyed ship and manage to salvage {fuelGained} fuel.",
                $"After scanning a nearby asteroid for resources, you see it contains dark matter - the exact type of fuel that your ship uses! You manage to extract {fuelGained} fuel from the asteroid.",
                $"Your dark matter harvester has returned you {fuelGained} fuel."
            };

            return fuelMessages[new Random().Next(fuelMessages.Length)];
        }
    }
}
