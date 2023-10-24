namespace _Project.Scripts
{
    public interface IPlaneteryObject
    {
        MassClassEnum MassClass { get; }
        double Mass { get; }
        
        double Radius { get; } // TODO: Remove this property
    }
}