using UnityEngine;
using UkensJoker.DataArchitecture;
using TMPro;

namespace UkensJoker.UI
{
    public class InteractPromptUI : MonoBehaviour
    {
        [SerializeField] private StringReference _interactPrompt;
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            _interactPrompt.RegisterListener(UpdateInteractPrompt);
        }

        private void OnDisable()
        {
            _interactPrompt.UnregisterListener(UpdateInteractPrompt);
        }

        private void UpdateInteractPrompt(string interactPrompt)
        {
            _text.text = interactPrompt;
        }
    }
}