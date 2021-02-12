using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
    class Utility
    {
        public void ShowDistanceFromPlanets()
        {
            int currentXPosition = 0;
            int currentYPosition = 0;

            //foreach (Planet planet in planetList)
            //{
            //    double distance = Math.Sqrt(Math.Pow(Math.Abs(currentXPosition - planet.XPosition), 2) +
            //        Math.Pow(Math.Abs(currentYPosition - planet.YPosition), 2));

            //    Console.WriteLine($"Distance to {planet.Name} is {distance}");
            //}
        }



        static public void ShowSetting(Planet planet, int DaysLeft)
        {
            Console.WriteLine($"You are marooned on a desolate world, light years from Earth." +
                              $"The name of this planet is {planet.PlanetName}. You remember your purpose, " +
                              $"the Earth neeeds you and your haul of dark matter to power weapons that will" +
                              $"subdue an incoming alien invasion. These aliens are {DaysLeft} days from" +
                              $"Earth. Fully stock your supplies so you can make the perilous journey" +
                              $"across the cosmos to defend your home.");
        }

    }

}