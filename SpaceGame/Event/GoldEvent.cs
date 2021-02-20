using System;

namespace SpaceGame
{
    class GoldEvent : RandomEvent
    {
        // Property getters scale amount of gold lost/gained with difficulty
        int goldLost => new Random().Next(difficulty / 3, difficulty * 2);
        int goldGained => new Random().Next(difficulty, difficulty * 2);

        // Use the base class constructor
        public GoldEvent(Difficulty difficulty) : base(difficulty) { }

        protected override string NegativeEvent(Ship ship)
        {
            ship.Coins -= goldLost;
            return GetNegativeEventMessage();
        }

        protected override string PositiveEvent(Ship ship)
        {
            ship.Coins += goldGained;
            return GetPositiveEventMessage();
        }

        protected override string GetNegativeEventMessage()
        {
            string[] messages = new string[]
            {
                $"Oh no! You have tripped and {goldLost} gold has flown out of your pocket!",
                $"While browsing comic books, a thief pickpockets {goldLost} gold.",
                $"You were scammed, and have lost {goldLost} gold.",
                $"Uh oh! You forgot to renew your ships registration, and have been charged a {goldLost} gold fee.",
                $"You stop by some slot machines while shopping. You lost {goldLost} gold."
            };

            return messages[new Random().Next(messages.Length)];
        }

        protected override string GetPositiveEventMessage()
        {
            string[] messages = new string[]
            {
                $"You stop by some slot machines while shopping. You won {goldGained} gold!",
                $"One of the local aliens dropped their wallet. You return it (keeping {goldGained} gold for yourself).",
                $"Nice! You check your data-pad, and see that your {goldGained} gold stimulus payment has arrived!",
                $"You notice there are no wet floor signs in the shop. After threatening to sue the owner, he pays you {goldGained} gold to keep quiet.",
                $"Your dogecoin stock has earned you {goldGained} gold!"
            };

            return messages[new Random().Next(messages.Length)];
        }
    }
}
