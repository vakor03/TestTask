using System.Collections.Generic;

namespace _Project.Scripts
{
    public interface IPlaneterySystem
    {
        IEnumerable<IPlaneteryObject> PlaneteryObjects { get; }
        
        void Update(double deltaTime);
    }
}