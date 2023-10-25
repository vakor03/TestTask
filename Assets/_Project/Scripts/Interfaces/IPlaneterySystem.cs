using System.Collections.Generic;

namespace _Project.Scripts.Interfaces
{
    public interface IPlaneterySystem
    {
        IEnumerable<IPlaneteryObject> PlaneteryObjects { get; }
        void Update(double deltaTime);
    }
}