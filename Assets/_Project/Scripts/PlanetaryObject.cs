using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;

namespace _Project.Scripts
{
    public class PlanetaryObject : IPlaneteryObject
    {
        public MassClassEnum MassClass { get; private set; }
        public double Mass { get; private set; }

        public PlanetaryObject(double mass)
        {
            MassClassEnum massClass = PlanetStatsHelper.GetMassClass(mass);

            MassClass = massClass;
            Mass = mass;
        }
    }
}