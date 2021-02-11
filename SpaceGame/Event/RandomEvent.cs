using System;

namespace SpaceGame
{
    abstract class RandomEvent
    {
        protected int difficulty;

        // Check if a random number between 0 and 100 is less than the difficulty (10, 20, or 30)
        protected virtual bool shouldTrigger => difficulty > new Random().Next(0, 100);
        protected virtual bool shouldTriggerPositive => difficulty / 2 > new Random().Next(0, 100);

        public RandomEvent(Difficulty difficulty)
        {
            this.difficulty = (int)difficulty * 10;
        }

        public virtual string Trigger(Ship ship, string defaultMessage = "")
        {
            // ship - The players ship
            // defaultMessage - A message to return when the event did not trigger

            if (shouldTrigger)
                return NegativeEvent(ship);
            else if (shouldTriggerPositive)
                return PositiveEvent(ship);

            return defaultMessage;
        }

        // Use below overloads if you want to pass in a different probability for the event to occur
        public virtual string Trigger(Ship ship, int chanceNegative, string defaultMessage = "")
        {
            // chanceNegative - A chance out of 100 that this event will trigger

            if (chanceNegative > new Random().Next(0, 100))
                return NegativeEvent(ship);
            else if (shouldTriggerPositive)
                return PositiveEvent(ship);

            return defaultMessage;
        }

        public virtual string Trigger(Ship ship, int chanceNegative, int chancePositive, string defaultMessage = "")
        {
            // chancePositive - A chance out of 100 that this event will trigger a PositiveEvent

            if (chanceNegative > new Random().Next(0, 100))
                return NegativeEvent(ship);
            else if (chancePositive > new Random().Next(0, 100))
                return PositiveEvent(ship);

            return defaultMessage;
        }

        protected abstract string NegativeEvent(Ship ship); // do something bad to the ship and return a random message
        protected abstract string PositiveEvent(Ship ship); // do something good to the ship and return a random message
        protected abstract string GetNegativeEventMessage(); // return a random negative message
        protected abstract string GetPositiveEventMessage(); // return a random positive message
    }
}
