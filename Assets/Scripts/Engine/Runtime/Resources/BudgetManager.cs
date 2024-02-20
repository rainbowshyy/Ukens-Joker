using UnityEngine;
using UkensJoker.UI;
using UkensJoker.DataArchitecture;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _startText;
        [SerializeField] private string _cannotStartText;
        [SerializeField] private string _canStartText;

        private bool[] _actives;
        private float _startWillpower;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

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
            count = 0;
            for (int i = 0; i < _days[day].BudgetTypeTables.Length; i++)
            {
                BudgetElement[] roll = _days[day].BudgetTypeTables[i].BudgetType.GetRandomElements(_days[day].BudgetTypeTables[i].Count);
                for (int x = 0; x < roll.Length; x++)
                {
                    _currentBudgetElements[count] = roll[x];
                    _actives[count] = _currentBudgetElements[count].Required;
                    count++;
                }
            }
        }

        private void UpdateUI()
        {
            string savingsText = "";
            for (int i = 0; i < _money.Value.ToString().Length; i++)
            {
                if (i != 0 && (_money.Value.ToString().Length - i) % 3 == 0)
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

                    _currentBudgetElements[i].Bought = _actives[i];
                }
                else
                {
                    _uiElements[i].ResetValues();
                }
            }

            string needText = "";
            int moneyNumber, needNumber;
            (moneyNumber, needNumber) = GetNeededSum();

            for (int i = 0; i < moneyNumber.ToString().Length; i++)
            {
                if ((moneyNumber.ToString().Length - i) % 3 == 0)
                    needText += " ";

                needText += moneyNumber.ToString()[i];
            }
            needText += " kr / ";

            for (int i = 0; i < needNumber.ToString().Length; i++)
            {
                if ((needNumber.ToString().Length - i) % 3 == 0)
                    needText += " ";

                needText += needNumber.ToString()[i];
            }
            _needText.text = needText + " kr";

            UpdateWillpower();

            _startButton.interactable = _willpower.Value > 0f;
            _startButton.GetComponent<Image>().enabled = _willpower.Value > 0f;
            _startText.text = _willpower.Value > 0f ? _canStartText : _cannotStartText;
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

        private (int, int) GetNeededSum()
        {
            int money = _money.Value;
            int loss = 0;
            for (int i = 0; i < _currentBudgetElements.Length; i++)
            {
                if (_actives[i])
                {
                    if (_currentBudgetElements[i].Delta > 0)
                        money += _currentBudgetElements[i].Delta;
                    else
                        loss += Mathf.Abs(_currentBudgetElements[i].Delta);
                }
            }
            return (money, loss);
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
            int moneySum, neededSum;
            (moneySum, neededSum) = GetNeededSum();

            _money.Value = moneySum;
            _moneyNeeded.Value = neededSum;

            _willpower.Value = Mathf.Clamp(_willpower.Value, 0f, _willpowerMax.Value);

            SceneManager.LoadScene(2);
        }
    }
}
