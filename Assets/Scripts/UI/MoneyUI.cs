using TMPro;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private IntReference _money;
        [SerializeField] private IntReference _moneyGoal;

        [SerializeField] private TMP_Text _text;

        [SerializeField] private UnityEvent _onNewMoney;

        private bool _brokenSink;

        private void OnEnable()
        {
            _money.RegisterListener(UpdateMoney);
        }

        private void OnDisable()
        {
            _money.UnregisterListener(UpdateMoney);
        }

        private void Start()
        {
            UpdateMoney(_money.Value);
        }

        private void UpdateMoney(int value)
        {
            string valueText = "", goalText = "";
            for (int i = 0; i < value.ToString().Length; i++)
            {
                if ((value.ToString().Length - i) % 3 == 0)
                    valueText += " ";

                valueText += value.ToString()[i];
            }

            for (int i = 0; i < _moneyGoal.Value.ToString().Length; i++)
            {
                if ((_moneyGoal.Value.ToString().Length - i) % 3 == 0)
                    goalText += " ";

                goalText += _moneyGoal.Value.ToString()[i];
            }
            _text.text = $"{valueText} kr / {goalText} kr";
            if (_brokenSink)
                _text.text += "\nBathroom sink is leaking!";
            if (value >= _moneyGoal.Value)
                _text.text += "\nYou can now go to sleep!";

            _onNewMoney.Invoke();
        }

        public void OnSinkChanged(Component sender, object broken)
        {
            if (broken is bool)
            {
                _brokenSink = (bool)broken;
            }
        }
    }
}
