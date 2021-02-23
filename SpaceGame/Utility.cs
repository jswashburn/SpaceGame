using System;
using color = SpaceGame.ConsoleColorManager;

namespace SpaceGame
{
    class Utility
    {
        static public Difficulty PromptUserForDifficulty()
        {
            Console.WriteLine("Select Difficulty: \n[1] Easy\n[2] Medium\n[3] Hard");
            int input = Menu.GetUserInput(3);
            return (Difficulty)input;
        }

        static public void ShowSetting(Planet planet, int daysLeft)
        {
            "You are marooned on a desolate world, light years from Earth. The name of this planet is ".Write();
            planet.PlanetName.WriteLine(planet.PlanetNameCColor);
            "You remember your purpose, the Earth needs you and your haul of ".Write();
            "dark matter ".Write(color.ResourceColor("Fuel"));
            "to power a weapon that will subdue the incoming alien invasion.\nThe aliens are ".Write();
            daysLeft.ToString().Write(color.ResourceColor("Time"));
            " days from Earth.\n\nFully upgrade and stock your supplies so you can make the perilous journey across the cosmos to defend your home.".WriteLine();
        }
    }
}