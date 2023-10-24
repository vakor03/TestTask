namespace _Project.Scripts
{
    public class PlanetaryObject : IPlaneteryObject
    {
        public MassClassEnum MassClass { get; private set; }
        public double Mass { get; private set; }


        public static PlanetaryObject New(double mass)
        {
            MassClassEnum massClass = PlanetStatsHelper.GetMassClass(mass);

            return new()
            {
                MassClass = massClass,
                Mass = mass,
            };
        }
    }
}