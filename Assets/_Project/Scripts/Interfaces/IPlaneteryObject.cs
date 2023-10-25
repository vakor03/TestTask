using _Project.Scripts.Enums;

namespace _Project.Scripts.Interfaces
{
    public interface IPlaneteryObject
    {
        MassClassEnum MassClass { get; }
        double Mass { get; }
    }
}