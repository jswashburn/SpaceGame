using System;

namespace SpaceGame
{
    public static class PlanetArrayExtensions
    {
        public static void ShowDetails(this Planet[] original, Planet currentPlanet)
        {
            for (int i = 1; i < original.Length; i++)
            {
                Planet p = original[i];

                if (p != currentPlanet)
                {
                    $"[{i}] ".Write();
                    $"{p.PlanetName} ".Write(p.PlanetResourceCColor);

                    int days = p.DistanceToShip(p.PlanetCordsX, p.PlanetCordsY, currentPlanet);

                    " Distance to ship in days: ".Write();
                    days.ToString().WriteLine(ConsoleColor.DarkGray);
                }
            }
            $"[{original.Length}] ".Write();
            "Planet: Earth".WriteLine(ConsoleColor.Green);
            $"[{original.Length + 1}] Return to Store".WriteLine();
        }
    }
}
