using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
    abstract class RandomEvent
    {
        protected int difficulty;
        protected bool shouldTrigger => new Random().Next(0, 100) < difficulty;

        public RandomEvent()
        {
            this.difficulty = 10;
        }

        public RandomEvent(int difficulty)
        {
            this.difficulty = difficulty * 10;
        }

        public virtual void Trigger(Ship ship)
        {
            if (shouldTrigger)
            {
                if (shouldTrigger) // positive events are twice as unlikely to happen
                    this.PositiveEvent(ship);
                else
                    this.NegativeEvent(ship);
            }
        }

        public virtual void Trigger(Ship ship, int chance)
        {
            if (new Random().Next(0, 100) < chance)
            {
                if (new Random().Next(0, 100) < chance)
                    this.PositiveEvent(ship);
                else
                    this.NegativeEvent(ship);
            }
        }

        public virtual void Trigger(Ship ship, int chance, int positiveChance)
        {
            if (new Random().Next(0, 100) < chance)
            {
                if (new Random().Next(0, 100) < positiveChance)
                    this.PositiveEvent(ship);
                else
                    this.NegativeEvent(ship);
            }
        }

        public abstract void NegativeEvent(Ship ship);
        public abstract void PositiveEvent(Ship ship);
        public abstract string GetNegativeEventMessage();
        public abstract string GetPositiveEventMessage();
    }
}
