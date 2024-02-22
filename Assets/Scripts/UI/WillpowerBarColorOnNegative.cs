using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.UI;

namespace UkensJoker.UI
{
    public class WillpowerBarColorOnNegative : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _negativeColor;
        [SerializeField] private FloatReference _willpower;

        private void OnEnable()
        {
            _willpower.RegisterListener(SetColor);
        }

        private void OnDisable()
        {
            _willpower.UnregisterListener(SetColor);
        }

        private void SetColor(float value)
        {
            _image.color = value <= 0f ? _negativeColor : Color.white;
        }
    }
}
