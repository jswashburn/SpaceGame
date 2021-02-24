using System;

namespace SpaceGame
{
    public class HullEvent : RandomEvent
    {
        // Property getters scale amount of hull integrity lost/gained with difficulty
        int hullLost => new Random().Next(difficulty / 3, difficulty * 2);
        int hullGained => new Random().Next(difficulty, difficulty * 2);

        HullEvent() : base() { }

        public HullEvent(Difficulty difficulty) : base(difficulty) { }

        protected override string NegativeEvent(Ship ship)
        {
            ship.Hull -= hullLost;
            return GetNegativeEventMessage();
        }

        protected override string PositiveEvent(Ship ship)
        {
            ship.Hull += hullGained;
            if (ship.Hull > ship.HullMax)
                ship.Hull = ship.HullMax;

            return GetPositiveEventMessage();
        }

        protected override string GetNegativeEventMessage()
        {
            string[] hullMessages = new string[]
            {
                $"You fell asleep in the cockpit and have hit a small asteroid! The window cracks, causing {hullLost} damage to the hull.",
                $"You took fire from a group of orbital space bandits! -{hullLost} hull integrity.",
                $"Whoops! Someone left the window down and your toolbox flew out the window. -{hullLost} hull integrity.",
                $"Before launching, you notice that a pack of alien bandits has vandalised your ship. -{hullLost} hull integrity."
            };

            return hullMessages[new Random().Next(hullMessages.Length)];
        }

        protected override string GetPositiveEventMessage()
        {
            string[] hullMessages = new string[]
            {
                $"Bored on your way to the next planet, you read about a new trick in the Ship Mechanics Weekly magazine, and after following a tutorial, restore {hullGained} hull integrity!",
                $"You find some spare WD-40 in a cabinet, and decide to lube up some of the blasters, restoring {hullGained} hull integrity.",
                $"You discover a free ship repair coupon in your wallet! You redeem it at a nearby SpaceLube and restore {hullGained} hull integrity.",
                $"Your shield generator starts working again for no apparant reason! +{hullGained} hull integrity."
            };

            return hullMessages[new Random().Next(hullMessages.Length)];
        }
    }
}
