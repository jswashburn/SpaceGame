using System;

namespace SpaceGame
{
    // RANDOM EVENT SYSTEM DEMO
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void DisplayMessageOnEnter(string message)
        {
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(message);
        }

        static void RandomEventsDemo(Difficulty difficulty)
        {
            // Difficulty:

            // Hard - event runs 30% of the time
            // Medium - event runs 20% of the time
            // Easy - event runs 10% of the time

            // Events - set to hard difficulty for the demo
            HullEvent hullEvent = new HullEvent(difficulty);
            FuelEvent fuelEvent = new FuelEvent(difficulty);
            GoldEvent goldEvent = new GoldEvent(difficulty);
            TimeEvent timeEvent = new TimeEvent(difficulty);

            Console.WriteLine("Starting sim [CTRL-C to exit]");
            while (true)
            {
                // DEMO ONLY

                // Random events - These can be triggered at any point we want throughout the program, and return a string describing
                // the event. Pass in the players ship so the event has something to affect.
                // When creating events, just pass in a Difficulty enum

                // --- Simulation for testing random events ---

                // create an example ship
                Ship vulcan = new Ship();

                // show its initial stats
                DisplayMessageOnEnter($"Created new ship\n{vulcan}");

                // shops at store...
                DisplayMessageOnEnter("You go shopping at the local market...");

                // we went to the store, so lets trigger a GoldEvent
                DisplayMessageOnEnter(goldEvent.Trigger(vulcan));

                DisplayMessageOnEnter(vulcan.ToString());

                // travels to planet...
                DisplayMessageOnEnter("You start your journey to <planet> ...");

                // we are traveling, so lets trigger a hull event and a fuel event, even a time event
                DisplayMessageOnEnter(timeEvent.Trigger(vulcan));
                DisplayMessageOnEnter(hullEvent.Trigger(vulcan));
                DisplayMessageOnEnter(fuelEvent.Trigger(vulcan));

                DisplayMessageOnEnter(vulcan.ToString());

                DisplayMessageOnEnter("Press enter to run simulation again");
            }
        }
    }
}
