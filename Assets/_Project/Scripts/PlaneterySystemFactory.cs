using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class PlaneterySystemFactory : IPlaneterySystemFactory
    {
        public IPlaneterySystem Create(double mass)
        {
            var planetMasses = GenerateRandomPlanetMasses(mass, Random.Range(1, 10));
            
            var planeterySystem =
                new PlaneterySystem(planetMasses.Select(m => (IPlaneteryObject)PlanetaryObject.New(m)).ToList());

            planeterySystem.Initialize();

            return planeterySystem;
        }

        private List<double> GenerateRandomPlanetMasses(double totalMass, int planetsCount)
        {
            List<double> planetMasses = new(planetsCount);

            double massLeft = totalMass;
            for (int i = 0; i < planetsCount - 1; i++)
            {
                double planetMass = RandomMass(massLeft);
                planetMasses.Add(planetMass);
                massLeft -= planetMass;
            }

            planetMasses.Add(massLeft);

            return planetMasses;

            double RandomMass(double d)
            {
                return Random.Range(0, (float)d);
            }
        }
    }
}