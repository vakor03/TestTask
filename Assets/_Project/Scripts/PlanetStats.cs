using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Create PlanetStatsSO", fileName = "PlanetStatsSO", order = 0)]
    public class PlanetStatsSO : ScriptableObject
    {
        [field: SerializeField] public MassClassEnum MassClass { get; set; }
        [field: SerializeField] public GameObject Prefab { get; set; }
        [field: SerializeField] public double MinMass { get; set; }
        [field: SerializeField] public double MaxMass { get; set; }
        [field: SerializeField] public double MinRadius { get; set; }
        [field: SerializeField] public double MaxRadius { get; set; }
        [field: SerializeField] public Color OrbitColor { get; set; }
    }
}