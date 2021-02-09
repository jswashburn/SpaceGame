using System;

namespace SpaceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting sim [CTRL-C to exit]");
            while (true)
            {
                // DEMO ONLY

                // create a new ship
                Ship pillarOfAutumn = new Ship();

                // show its initial stats
                Console.WriteLine("Created new ship\n");
                Console.WriteLine(pillarOfAutumn);

                // shops at store...
                Console.WriteLine("You go shopping at the local market...");

                // trigger a random event (hard), passing in the ship
                new StoreEvent((int)Difficulty.Hard).Trigger(pillarOfAutumn); // runs about 30% of the time
                Console.WriteLine("\n");

                // travels to planet...
                Console.WriteLine("You start your journey to <planet> ...");

                // trigger a random event (hard), passing in the ship
                new TravelEvent((int)Difficulty.Hard).Trigger(pillarOfAutumn); // runs about 30% of the time
                Console.WriteLine("\n");

                Console.WriteLine(pillarOfAutumn);

                Console.WriteLine("Press enter to run simulation again");
                Console.ReadLine();
                Console.WriteLine("\n\n\n");
            }
        }
    }
}
