using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class GenerateNewSystemButtonUI : MonoBehaviour
    {
        [SerializeField] private Button generateNewSystemButton;

        private void Start()
        {
            generateNewSystemButton.onClick.AddListener(GenerateNewSystem);
        }

        private void GenerateNewSystem()
        {
            GameController.Instance.GenerateNewSystem(GenerateRandomMass());
        }

        private double GenerateRandomMass()
        {
            return 50;
        }
    }
}