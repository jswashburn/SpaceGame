using System;

namespace SpaceGame
{
    class HullEvent : RandomEvent
    {
        // Property getters scale amount of hull integrity lost/gained with difficulty
        int hullIntegrityLost => new Random().Next(difficulty / 3, difficulty * 2);
        int hullIntegrityGained => new Random().Next(difficulty, difficulty * 2);

        // Use the base class constructor
        public HullEvent(Difficulty difficulty) : base(difficulty) { }

        protected override string NegativeEvent(Ship ship)
        {
            ship.HullIntegrity -= hullIntegrityLost;
            return GetNegativeEventMessage();
        }

        protected override string PositiveEvent(Ship ship)
        {
            ship.HullIntegrity += hullIntegrityGained;
            return GetPositiveEventMessage();
        }

        protected override string GetNegativeEventMessage()
        {
            string[] hullMessages = new string[]
            {
                $"You fell asleep in the cockpit and have hit a small asteroid! The window cracks, causing {hullIntegrityLost} damage to the hull.",
                $"You took fire from a group of orbital space bandits! -{hullIntegrityLost} hull integrity.",
                $"Whoops! Someone left the window down and your toolbox flew out the window. -{hullIntegrityLost} hull integrity.",
                $"Before launching, you notice that a pack of alien bandits has vandalised your ship. -{hullIntegrityLost} hull integrity."
            };

            return hullMessages[new Random().Next(0, hullMessages.Length)];
        }

        protected override string GetPositiveEventMessage()
        {
            string[] hullMessages = new string[]
            {
                $"Bored on your way to the next planet, you read about a new trick in the Ship Mechanics Weekly magazine, and after following a tutorial, restore {hullIntegrityGained} hull integrity!",
                $"You find some spare WD-40 in a cabinet, and decide to lube up some of the blasters, restoring {hullIntegrityGained} hull integrity.",
                $"You discover a free ship repair coupon in your wallet! You redeem it at a nearby SpaceLube and restore {hullIntegrityGained} hull integrity.",
                $"Your shield generator starts working again for no apparant reason! +{hullIntegrityGained} hull integrity."
            };

            return hullMessages[new Random().Next(0, hullMessages.Length)];
        }
    }
}
