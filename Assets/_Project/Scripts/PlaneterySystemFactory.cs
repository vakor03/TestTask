using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Interfaces;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class PlaneterySystemFactory : IPlaneterySystemFactory
    {
        private readonly List<double> _planetMasses = new();

        public IPlaneterySystem Create(double mass)
        {
            GenerateRandomPlanetMasses(mass, Random.Range(1, 10));

            var planeterySystem =
                new PlaneterySystem(
                    _planetMasses
                        .Select(m => (IPlaneteryObject)new PlanetaryObject(m))
                        .ToList());

            planeterySystem.Initialize();

            return planeterySystem;
        }

        private void GenerateRandomPlanetMasses(double totalMass, int minPlanetsCount)
        {
            const float maxPossiblePlanetMass = 5000 - float.Epsilon;

            _planetMasses.Clear();

            double massLeft = totalMass;
            for (int i = 0; i < minPlanetsCount - 1; i++)
            {
                double planetMass = RandomMass(massLeft);
                _planetMasses.Add(planetMass);
                massLeft -= planetMass;
            }

            while (massLeft >= maxPossiblePlanetMass)
            {
                double planetMass = RandomMass(massLeft);
                _planetMasses.Add(planetMass);
                massLeft -= planetMass;
            }

            _planetMasses.Add(massLeft);
            
            double RandomMass(double d)
            {
                return Random.Range(0, Math.Min((float)d, maxPossiblePlanetMass));
            }
        }
    }
}