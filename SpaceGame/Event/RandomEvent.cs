using System;

namespace SpaceGame
{
    abstract class RandomEvent
    {
        protected int difficulty;
        protected bool shouldTrigger => new Random().Next(0, 100) < difficulty;

        public RandomEvent(Difficulty difficulty)
        {
            this.difficulty = (int)difficulty * 10;
        }

        public virtual string Trigger(Ship ship, string defaultMessage = "")
        {
            // ship - The players ship
            // defaultMessage - A message to return when the event did not trigger

            if (shouldTrigger)
            {
                if (shouldTrigger)
                    return PositiveEvent(ship);
                else
                    return NegativeEvent(ship);
            }

            return defaultMessage;
        }

        // Use below overloads if you want to pass in a different probability for the event to occur
        public virtual string Trigger(Ship ship, int chance, string defaultMessage = "")
        {
            // chance - A chance out of 100 that this event will trigger

            if (new Random().Next(0, 100) < chance)
            {
                if (new Random().Next(0, 100) < chance)
                    return PositiveEvent(ship);
                else
                    return NegativeEvent(ship);
            }

            return defaultMessage;
        }

        public virtual string Trigger(Ship ship, int chance, int positiveChance, string defaultMessage = "")
        {
            // positiveChance - A chance out of 100 that this event will trigger a PositiveEvent

            if (new Random().Next(0, 100) < chance)
            {
                if (new Random().Next(0, 100) < positiveChance)
                    return PositiveEvent(ship);
                else
                    return NegativeEvent(ship);
            }

            return defaultMessage;
        }

        protected abstract string NegativeEvent(Ship ship); // do something bad to the ship and return a random message
        protected abstract string PositiveEvent(Ship ship); // do something good to the ship and return a random message
        protected abstract string GetNegativeEventMessage(); // return a random negative message
        protected abstract string GetPositiveEventMessage(); // return a random positive message
    }
}
