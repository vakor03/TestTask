using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class GenerateNewSystemButtonUI : MonoBehaviour
    {
        [SerializeField] private Button generateNewSystemButton;
        [SerializeField] private TMP_InputField inputField;
        
        private void Start()
        {
            generateNewSystemButton.onClick.AddListener(GenerateNewSystem);
        }

        private void GenerateNewSystem()
        {
            if (float.TryParse(inputField.text.Trim(), out float result))
            {
                GameController.Instance.GenerateNewSystem(result);
            }else
            {
                Debug.Log("Invalid system mass");
            }
        }
        
        private void OnDestroy()
        {
            generateNewSystemButton.onClick.RemoveListener(GenerateNewSystem);
        }
    }
}