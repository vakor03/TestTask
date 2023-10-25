using System;
using System.Collections.Generic;
using _Project.Scripts.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class PlaneterySystem : IPlaneterySystem, IDisposable
    {
        private OrbitBuilder _orbitBuilder = new();
        private List<IPlaneteryObject> _planeteryObjects;
        private List<PlanetVisuals> _planetsVisuals = new();
        private List<GameObject> _orbits = new();

        public Vector3 SystemCenter { get; set; } = Vector3.zero;
        public IEnumerable<IPlaneteryObject> PlaneteryObjects => _planeteryObjects;

        public PlaneterySystem(List<IPlaneteryObject> planeteryObjects)
        {
            _planeteryObjects = planeteryObjects;
        }

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
            foreach (var planetaryObject in _planeteryObjects)
            {
                double planetRadius = PlanetStatsHelper.GetRadius(planetaryObject.MassClass, planetaryObject.Mass);
                double orbitRadius = previousRadius + Random.Range(3f, 10f) + planetRadius;

                CreatePlanetVisuals(planetaryObject, orbitRadius, planetRadius);
                CreateOrbit(planetaryObject, orbitRadius);

                previousRadius = orbitRadius;
            }
        }

        private void CreatePlanetVisuals(IPlaneteryObject planeteryObject, double orbitRadius, double planetRadius)
        {
            Vector3 initialPosition = SystemCenter + Vector3.right * (float)orbitRadius;
            float angularSpeed = Random.Range(20f, 100f);

            PlanetVisuals planetVisuals = PlanetVisuals.New(planeteryObject, initialPosition, angularSpeed, planetRadius);
            _planetsVisuals.Add(planetVisuals);
        }

        private void CreateOrbit(IPlaneteryObject planeteryObject, double orbitRadius)
        {
            Color orbitColor = PlanetStatsHelper.GetColor(planeteryObject.MassClass);
            GameObject orbit = _orbitBuilder.WithRadius(orbitRadius).WithColor(orbitColor).Build();
            _orbits.Add(orbit);
        }

        public void Dispose()
        {
            foreach (GameObject orbit in _orbits)
            {
                Object.Destroy(orbit);
            }

            foreach (var planetsVisual in _planetsVisuals)
            {
                planetsVisual.Dispose();
            }
        }
    }
}