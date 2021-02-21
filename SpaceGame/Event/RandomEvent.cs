using System;

namespace SpaceGame
{
    abstract class RandomEvent
    {
        protected int difficulty;

        protected virtual bool shouldTrigger => difficulty > new Random().Next(0, 100);
        protected virtual bool shouldTriggerPositive => difficulty / 2 > new Random().Next(0, 100);

        public RandomEvent(Difficulty difficulty) => this.difficulty = (int)difficulty * 10;

        public virtual string Trigger(Ship ship, string defaultMessage = "")
        {
            if (shouldTrigger)
                return NegativeEvent(ship);
            else if (shouldTriggerPositive)
                return PositiveEvent(ship);

            return defaultMessage;
        }

        protected abstract string NegativeEvent(Ship ship);
        protected abstract string PositiveEvent(Ship ship);
        protected abstract string GetNegativeEventMessage();
        protected abstract string GetPositiveEventMessage();
    }
}
