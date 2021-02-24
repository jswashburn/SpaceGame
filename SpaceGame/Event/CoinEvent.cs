using System;

namespace SpaceGame
{
    public class CoinEvent : RandomEvent
    {
        // Property getters scale amount of gold lost/gained with difficulty
        int coinsLost => new Random().Next(difficulty / 3, difficulty * 2);
        int coinsGained => new Random().Next(difficulty, difficulty * 2);

        CoinEvent() : base() { }

        public CoinEvent(Difficulty difficulty) : base(difficulty) { }

        protected override string NegativeEvent(Ship ship)
        {
            ship.Coins -= coinsLost;
            return GetNegativeEventMessage();
        }

        protected override string PositiveEvent(Ship ship)
        {
            ship.Coins += coinsGained;
            if (ship.Coins > ship.GoldMax)
                ship.Coins = ship.GoldMax;

            return GetPositiveEventMessage();
        }

        protected override string GetNegativeEventMessage()
        {
            string[] messages = new string[]
            {
                $"Oh no! You have tripped and {coinsLost} gold has flown out of your pocket!",
                $"While browsing comic books, a thief pickpockets {coinsLost} gold.",
                $"You were scammed, and have lost {coinsLost} gold.",
                $"Uh oh! You forgot to renew your ships registration, and have been charged a {coinsLost} gold fee.",
                $"You stop by some slot machines while shopping. You lost {coinsLost} gold."
            };

            return messages[new Random().Next(messages.Length)];
        }

        protected override string GetPositiveEventMessage()
        {
            string[] messages = new string[]
            {
                $"You stop by some slot machines while shopping. You won {coinsGained} gold!",
                $"One of the local aliens dropped their wallet. You return it (keeping {coinsGained} gold for yourself).",
                $"Nice! You check your data-pad, and see that your {coinsGained} gold stimulus payment has arrived!",
                $"You notice there are no wet floor signs in the shop. After threatening to sue the owner, he pays you {coinsGained} gold to keep quiet.",
                $"Your dogecoin stock has earned you {coinsGained} gold!"
            };

            return messages[new Random().Next(messages.Length)];
        }
    }
}
