using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Project.Scripts.Interfaces;
using _Project.Scripts.ScriptableObjects;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private List<PlanetStatsSO> planetStatsSOs;
        [SerializeField] private LineRenderer lineRendererPrefab;

        [CanBeNull] private IPlaneterySystem _planeterySystem;

        private IPlaneterySystemFactory _planeterySystemFactory;
        public static GameController Instance { get; private set; }
        public LineRenderer LineRendererPrefab => lineRendererPrefab;

        private void Awake()
        {
            Instance = this;
            
            _planeterySystemFactory = new PlaneterySystemFactory();
            
            PlanetStatsHelper.Initialize(planetStatsSOs);
        }

        private void Update()
        {
            if (_planeterySystem == null) return;
            
            _planeterySystem.Update(Time.deltaTime);
        }

        public void GenerateNewSystem(double mass)
        {
            DisposePreviousSystem();
            
            _planeterySystem = _planeterySystemFactory.Create(mass);
            LogPlanetarySystem(_planeterySystem);
        }

        private void DisposePreviousSystem()
        {
            if (_planeterySystem is IDisposable disposable)
            {
                disposable.Dispose();
            }
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