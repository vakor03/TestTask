using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public static class PlanetStatsHelper
    {
        public static void Set(List<PlanetStatsSO> planetStatsSos)
        {
            _planetStatsMap = new Dictionary<MassClassEnum, PlanetStatsSO>();
            foreach (var planetStatsSo in planetStatsSos)
            {
                _planetStatsMap.Add(planetStatsSo.MassClass, planetStatsSo);
            }
        }

        private static Dictionary<MassClassEnum, PlanetStatsSO> _planetStatsMap;

        public static double GetRadius(MassClassEnum massClass, double mass)
        {
            PlanetStatsSO planetStats = _planetStatsMap[massClass];
            return Interpolate(mass,
                planetStats.MinMass,
                planetStats.MaxMass,
                planetStats.MinRadius,
                planetStats.MaxRadius
            );
        }

        private static double Interpolate(double x, double x0, double x1, double y0, double y1)
        {
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }

        public static MassClassEnum GetMassClass(double mass)
        {
            foreach (var (classEnum, planetStats) in _planetStatsMap)
            {
                if (planetStats.MinMass <= mass && mass < planetStats.MaxMass)
                {
                    return classEnum;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(mass), mass, "Invalid mass");
        }

        public static GameObject GetPrefab(MassClassEnum massClass)
        {
            return _planetStatsMap[massClass].Prefab;
        }

        public static Color GetColor(MassClassEnum massClass)
        {
            return _planetStatsMap[massClass].OrbitColor;
        }
    }
}