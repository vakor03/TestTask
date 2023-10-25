using System;
using _Project.Scripts.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class PlanetVisuals : IDisposable
    {
        private const float EARTH_LOCAL_SCALE = 2f;

        private PlanetVisuals() {}

        private GameObject GameObject { get; set; }
        private double AngularSpeed { get; set; }

        public void RotateAroundCenter(double deltaTime, Vector3 systemCenter)
        {
            GameObject.transform.RotateAround(systemCenter, Vector3.up, (float)(AngularSpeed * deltaTime));
        }

        public static PlanetVisuals New(IPlaneteryObject planeteryObject, Vector3 initialPosition, double angularSpeed, double radius)
        {
            GameObject instance = Object.Instantiate(PlanetStatsHelper.GetPrefab(planeteryObject.MassClass));
            instance.name = planeteryObject.MassClass.ToString();
            instance.transform.localScale = Vector3.one * EARTH_LOCAL_SCALE * (float)radius;
            instance.transform.position = initialPosition;

            return new PlanetVisuals
            {
                GameObject = instance,
                AngularSpeed = angularSpeed,
            };
        }

        public void Dispose()
        {
            Object.Destroy(GameObject);
        }
    }
}