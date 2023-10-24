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

        private static Dictionary<MassClassEnum, PlanetStatsSO> _planetStatsMap; /*= new()
        {
            {
                MassClassEnum.Asteroidan,
                new PlanetStatsSO() { MinMass = 0, MaxMass = 0.00001, MinRadius = 0, MaxRadius = 0.03 }
            },
            {
                MassClassEnum.Mercurian,
                new PlanetStatsSO() { MinMass = 0.00001, MaxMass = 0.1, MinRadius = 0.03, MaxRadius = 0.7 }
            },
            {
                MassClassEnum.Subterran,
                new PlanetStatsSO() 
                { MinMass = 0.1, MaxMass = 0.5, MinRadius = 0.5, MaxRadius = 1.2 }
            },
            {
                MassClassEnum.Terran, new PlanetStatsSO() 
                { MinMass = 0.5, MaxMass = 2, MinRadius = 0.8, MaxRadius = 1.9 }
            },
            {
                MassClassEnum.Superterran,
                new PlanetStatsSO() 
                { MinMass = 2, MaxMass = 10, MinRadius = 1.3, MaxRadius = 3.3 }
            },
            {
                MassClassEnum.Neptunian,
                new PlanetStatsSO() 
                { MinMass = 10, MaxMass = 50, MinRadius = 2.1, MaxRadius = 5.7 }
            },
            {
                MassClassEnum.Jovian,
                new PlanetStatsSO() 
                { MinMass = 50, MaxMass = 5000, MinRadius = 3.5, MaxRadius = 27 }
            },
        };*/
        
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
    }
}