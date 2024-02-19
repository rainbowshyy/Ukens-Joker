using TMPro;
using UnityEngine;

namespace UkensJoker.UI
{
    public class BudgetElementUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _delta;
        [SerializeField] private TMP_Text _willpower;
        [SerializeField] private GameObject _toggleButton;

        [SerializeField] private Color _negativeColor;
        [SerializeField] private Color _notActiveColor;

        public void SetValues(bool active, string name, int delta, float willpower, bool required)
        {
            _name.text = name;
            _name.color = active ? Color.white : _notActiveColor;

            string deltaText = "";
            for (int i = 0; i < delta.ToString().Length; i++)
            {
                if (i != 0 && (delta.ToString().Length - i) % 3 == 0 && (i != 1 || delta.ToString()[0].ToString() != "-"))
                    deltaText += " ";

                deltaText += delta.ToString()[i];
            }
            if (delta > 0)
                deltaText = "+" + deltaText;

            _delta.text = deltaText + " kr";

            _delta.color = active ? (delta < 0 ? _negativeColor : Color.white) : _notActiveColor;

            if (willpower == 0)
            {
                _willpower.text = "";
            }
            else
            {
                string willpowerChar = willpower < 0 ? "-" : "+";
                string willpowerText = "";
                _willpower.color = willpower < 0 ? _negativeColor : Color.white;

                for (int i = 0; i < Mathf.Ceil(Mathf.Abs(willpower)); i++)
                {
                    willpowerText += willpowerChar;
                }

                _willpower.text = willpowerText;
            }

            _toggleButton.SetActive(!required);
        }
        
        public void ResetValues()
        {
            _name.text = "";
            _delta.text = "";
            _willpower.text = "";
            _toggleButton.SetActive(false);
        }
    }
}
