using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlaneterySystem : IPlaneterySystem
    {
        public IEnumerable<IPlaneteryObject> PlaneteryObjects => _planeteryObjects;
        
        public Vector3 SystemCenter { get; set; } = Vector3.zero;

        public void Update(double deltaTime)
        {
            foreach (var planet in _planets)
            {
                planet.GameObject.transform.RotateAround(SystemCenter, planet.Normal, (float) (planet.AngularSpeed * deltaTime));
            }
        }

        private readonly List<IPlaneteryObject> _planeteryObjects;

        public PlaneterySystem(List<IPlaneteryObject> planeteryObjects)
        {
            _planeteryObjects = planeteryObjects;
        }

        private List<PlanetVisuals> _planets = new();
        public void Initialize()
        {
            
            for (var i = 0; i < _planeteryObjects.Count; i++)
            {
                var planeteryObject = _planeteryObjects[i];
                var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject.transform.localScale = Vector3.one * (float)planeteryObject.Radius;
                double distanceToCenter = (i+1)*10;

                var planet = new PlanetVisuals()
                {
                    PlaneteryObject = planeteryObject, GameObject = gameObject,
                    DistanceToCenter = distanceToCenter, AngularSpeed = Random.Range(20f, 100f),
                };  
                
                gameObject.transform.position = SystemCenter + Vector3.right * (float)distanceToCenter;

                _planets.Add(planet);
                
                var orbit = new GameObject($"Orbit_{i}").AddComponent<LineRenderer>();
                DrawCircle(50, (float)planet.DistanceToCenter, orbit);
            }
        }

        private void DrawCircle(int steps, float radius, LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = steps + 1;

            var angle = 20f;
            for (int i = 0; (i < steps + 1); i++)
            {
                var x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
                var y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

                lineRenderer.SetPosition (i,new Vector3(x,0,y) );

                angle += (360f / steps);
            }
        }
    }

    public class PlanetVisuals
    {
        public IPlaneteryObject PlaneteryObject;
        public GameObject GameObject;
        public double DistanceToCenter;
        public double AngularSpeed;
        public double Angle;
        public Vector3 Normal => Vector3.up;
    }
}