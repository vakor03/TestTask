using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlaneterySystem : IPlaneterySystem
    {
        private readonly List<IPlaneteryObject> _planeteryObjects;

        private List<PlanetVisuals> _planetsVisuals = new();

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
                double orbitRadius = previousRadius + Random.Range(1f, 6f);

                CreatePlanet(_planeteryObjects[i], orbitRadius);
                CreateOrbit((float)orbitRadius, i);
                
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

        private void CreateOrbit(float orbitRadius, int i)
        {
            const int steps = 50;
            var orbit = new GameObject($"Orbit_{i}").AddComponent<LineRenderer>();
            DrawCircle(steps, orbitRadius, orbit);
        }

        private void DrawCircle(int steps, float radius, LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = steps + 1;

            var angle = 20f;
            for (int i = 0; (i < steps + 1); i++)
            {
                var x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                var y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                lineRenderer.SetPosition(i, new Vector3(x, 0, y));

                angle += (360f / steps);
            }
        }
    }
}