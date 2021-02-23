using System;

namespace SpaceGame
{
    class EasterEggEvent : RandomEvent
    {
        protected override bool shouldTrigger => 5 > new Random().Next(0, 100);
        protected override bool shouldTriggerPositive => 5 > new Random().Next(0, 100);

        EasterEggEvent() : base() { }
        public EasterEggEvent(Difficulty difficulty) : base(difficulty) { }

        protected override string NegativeEvent(Ship ship) => GetNegativeEventMessage();

        protected override string PositiveEvent(Ship ship) => GetPositiveEventMessage();

        protected override string GetNegativeEventMessage()
        {
            string[] easterEggMessages = new string[]
            {
                "Why did the cow go in the spaceship? It wanted to see the mooooooooon.",
                "What did Neil Armstrong do after he stepped on Buzz Aldrins toe? He Apollo-gized.",
                "How often do you like jokes about elements? Periodically.",
                "I'm throwing a party on earth... can you help me planet?"
            };

            return easterEggMessages[new Random().Next(easterEggMessages.Length)];
        }

        protected override string GetPositiveEventMessage()
        {
            string[] easterEggMessages = new string[]
            {
                "Why did people not like the restaurant on the moon? Because there was no atmosphere.",
                "A photon checks into a hotel and is asked if he needs any help with his luggage. He says" +
                " 'No, I'm traveling light.'",
                "Why didn't the sun go to college? Because it already had a million degrees!"
            };

            return easterEggMessages[new Random().Next(easterEggMessages.Length)];
        }
    }
}
