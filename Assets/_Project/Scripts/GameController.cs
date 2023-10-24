using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [SerializeField] private List<PlanetStatsSO> planetStatsSOs;
        [SerializeField] private LineRenderer lineRendererPrefab;
        public LineRenderer LineRendererPrefab => lineRendererPrefab;
        
        private IPlaneterySystemFactory _planeterySystemFactory;
        
        [CanBeNull] private IPlaneterySystem _planeterySystem;
        
        private void Awake()
        {
            Instance = this;
            
            _planeterySystemFactory = new PlaneterySystemFactory();
            
            PlanetStatsHelper.Set(planetStatsSOs);
        }
        public void GenerateNewSystem(double mass)
        {
            _planeterySystem = _planeterySystemFactory.Create(mass);
            LogPlanetarySystem(_planeterySystem);
        }

        private void Update()
        {
            if (_planeterySystem == null) return;
            
            _planeterySystem.Update(Time.deltaTime);
        }

        private void LogPlanetarySystem(IPlaneterySystem planeterySystem)
        {
            StringBuilder sb = new();
            sb.AppendLine($"Planetary System: {planeterySystem.PlaneteryObjects.Count()}");
            foreach (var planeteryObject in planeterySystem.PlaneteryObjects)
            {
                sb.AppendLine($"\tMassClass: {planeteryObject.MassClass}, Mass: {planeteryObject.Mass}");
            }
            Debug.Log(sb.ToString());
        }
    }
}