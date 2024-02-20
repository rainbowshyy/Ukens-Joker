using UnityEngine;
using UkensJoker.UI;
using UkensJoker.DataArchitecture;
using TMPro;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class BudgetManager : MonoBehaviour
    {
        [SerializeField] private IntVariable _money;
        [SerializeField] private IntVariable _moneyNeeded;
        [SerializeField] private FloatVariable _willpower;
        [SerializeField] private FloatReference _willpowerMax;

        [SerializeField] private IntReference _day;
        [SerializeField] private Day[] _days;
        [SerializeField] private BudgetElementUI[] _uiElements;
        [SerializeField] private BudgetElement[] _currentBudgetElements;

        [SerializeField] private TMP_Text _savingsText;
        [SerializeField] private TMP_Text _needText;

        private bool[] _actives;
        private float _startWillpower;

        private void Start()
        {
            _startWillpower = _willpower.Value;
            RollBudgetElementsForDay(_day.Value);
            UpdateUI();
        }

        private void RollBudgetElementsForDay(int day)
        {
            int count = 0;
            for (int i = 0; i < _days[day].BudgetTypeTables.Length; i++)
            {
                count += _days[day].BudgetTypeTables[i].Count;
            }

            _currentBudgetElements = new BudgetElement[count];
            _actives = new bool[count];
            for (int i = 0; i < _days[day].BudgetTypeTables.Length; i++)
            {
                BudgetElement[] roll = _days[day].BudgetTypeTables[i].BudgetType.GetRandomElements(_days[day].BudgetTypeTables[i].Count);
                for (int x = 0; x < roll.Length; x++)
                {
                    _currentBudgetElements[i + x] = roll[x];
                    _actives[i + x] = _currentBudgetElements[i + x].Required;
                }
            }
        }

        private void UpdateUI()
        {
            string savingsText = "";
            for (int i = 0; i < _money.Value.ToString().Length; i++)
            {
                if ((_money.Value.ToString().Length - i) % 3 == 0)
                    savingsText += " ";

                savingsText += _money.Value.ToString()[i];
            }
            _savingsText.text = "+" + savingsText + " kr";

            for (int i = 0; i < _uiElements.Length; i++)
            {
                if (i < _currentBudgetElements.Length)
                {
                    _uiElements[i].SetValues(
                        _actives[i],
                        _currentBudgetElements[i].Name,
                        _currentBudgetElements[i].Delta,
                        _actives[i] ? _currentBudgetElements[i].WillpowerActive : _currentBudgetElements[i].WillpowerNotActive,
                        _currentBudgetElements[i].Required
                        );
                }
                else
                {
                    _uiElements[i].ResetValues();
                }
            }

            string needText = "";
            string needNumber = GetNeededSum().ToString();
            for (int i = 0; i < needNumber.Length; i++)
            {
                if ((needNumber.Length - i) % 3 == 0)
                    needText += " ";

                needText += needNumber[i];
            }
            _needText.text = "Total : " + needText + " kr";

            UpdateWillpower();
        }

        private void UpdateWillpower()
        {
            float newWillPower = _startWillpower;
            for (int i = 0; i < _currentBudgetElements.Length; i++)
            {
                newWillPower += _actives[i] ? _currentBudgetElements[i].WillpowerActive : _currentBudgetElements[i].WillpowerNotActive;
            }

            _willpower.Value = newWillPower;
        }

        private int GetNeededSum()
        {
            int delta = _money.Value;
            for (int i = 0; i < _currentBudgetElements.Length; i++)
            {
                if (_actives[i])
                {
                    delta += _currentBudgetElements[i].Delta;
                }
            }
            return delta;
        }

        public void ToggleActive(int activeIndex)
        {
            if (activeIndex < _actives.Length)
            {
                _actives[activeIndex] = !_actives[activeIndex];
                UpdateUI();
            }
        }

        public void FinalizeBudget()
        {
            int neededSum = GetNeededSum();
            if (neededSum < 0)
            {
                _money.Value = 0;
                _moneyNeeded.Value = Mathf.Abs(neededSum);
            }
            else
            {
                _money.Value = neededSum;
                _moneyNeeded.Value = _money.Value;
            }

            _willpower.Value = Mathf.Clamp(_willpower.Value, 0f, _willpowerMax.Value);

            SceneManager.LoadScene(2);
        }
    }
}
