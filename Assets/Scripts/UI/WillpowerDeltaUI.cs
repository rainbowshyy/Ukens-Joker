using TMPro;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.UI
{
    public class WillpowerDeltaUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _negativeColor;
        [SerializeField] private FloatReference _willpowerDeltaUITime;
        private float _timeBeforeReset;

        private void Update()
        {
            if (_timeBeforeReset > 0f)
            {
                _timeBeforeReset -= Time.deltaTime;
                if (_timeBeforeReset <= 0f)
                    _text.text = "";
            }
        }

        public void OnDelta(float delta)
        {
            string willpowerChar = delta < 0 ? "-" : "+";
            string willpowerText = "";
            _text.color = delta < 0 ? _negativeColor : Color.white;

            for (int i = 0; i < Mathf.Ceil(Mathf.Abs(delta)); i++)
            {
                willpowerText += willpowerChar;
            }

            _text.text = willpowerText;

            _timeBeforeReset = _willpowerDeltaUITime.Value;
        }
    }
}
