using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlaneterySystem : IPlaneterySystem
    {
        private List<IPlaneteryObject> _planeteryObjects;
        private List<PlanetVisuals> _planetsVisuals = new();
        private OrbitBuilder _orbitBuilder = new();

        public PlaneterySystem(List<IPlaneteryObject> planeteryObjects)
        {
            _planeteryObjects = planeteryObjects;
        }

        public Vector3 SystemCenter { get; set; } = Vector3.zero;
        public IEnumerable<IPlaneteryObject> PlaneteryObjects => _planeteryObjects;

        public void Update(double deltaTime)
        {
            foreach (var planet in _planetsVisuals)
            {
                planet.RotateAroundCenter(deltaTime, SystemCenter);
            }
        }

        public void Initialize()
        {
            double previousRadius = 0;
            for (var i = 0; i < _planeteryObjects.Count; i++)
            {
                double orbitRadius = previousRadius + Random.Range(3f, 10f);

                CreatePlanet(_planeteryObjects[i], orbitRadius);
                CreateOrbit(_planeteryObjects[i], orbitRadius);
                
                previousRadius = orbitRadius;
            }
        }

        private void CreatePlanet(IPlaneteryObject planeteryObject, double orbitRadius)
        {
            Vector3 initialPosition = SystemCenter + Vector3.right * (float)orbitRadius;
            float angularSpeed = Random.Range(20f, 100f);

            PlanetVisuals planet = PlanetVisuals.New(planeteryObject, initialPosition, angularSpeed);
            _planetsVisuals.Add(planet);
        }

        private void CreateOrbit(IPlaneteryObject planeteryObject, double orbitRadius)
        {
            Color orbitColor = PlanetStatsHelper.GetColor(planeteryObject.MassClass);
            _orbitBuilder.WithRadius(orbitRadius).WithColor(orbitColor).Build();
        }
    }
}