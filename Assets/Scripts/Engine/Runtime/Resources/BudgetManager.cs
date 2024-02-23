using UnityEngine;
using UkensJoker.UI;
using UkensJoker.DataArchitecture;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

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

        private bool _start;

        [SerializeField] private UnityEvent<float> _onWillpowerDelta;
        [SerializeField] private CanvasGroup _black;

        [SerializeField] private TMP_Text _gambleText;
        [SerializeField] private FloatVariable _danger;
        [SerializeField] private Image[] _willpowerImages;

        [SerializeField] private TMP_Text _monthText;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _startWillpower = _willpower.Value;
            RollBudgetElementsForDay(_day.Value);
            UpdateUI();

            _monthText.text = "Budget - Month " + (_day.Value + 1).ToString();

            StartCoroutine(LoadTimelapse());
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
            needText += " kr / <color=#E6284D>-";

            for (int i = 0; i < needNumber.ToString().Length; i++)
            {
                if ((needNumber.ToString().Length - i) % 3 == 0)
                    needText += " ";

                needText += needNumber.ToString()[i];
            }
            _needText.text = needText + " kr</color>";

            if (moneyNumber < needNumber)
            {
                string delta = ((int)(needNumber - moneyNumber)).ToString();
                string deltaText = "";
                for (int i = 0; i < delta.ToString().Length; i++)
                {
                    if ((delta.ToString().Length - i) % 3 == 0)
                        deltaText += " ";

                    deltaText += delta.ToString()[i];
                }
                _gambleText.text = $"Will need to win {deltaText} kr from playing Joker.";
            }
            else
            {
                _gambleText.text = "";
            }

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

            _willpower.Value = Mathf.Clamp(newWillPower, 0f, _willpowerMax.Value);
            _danger.Value = (_willpowerMax.Value - _willpower.Value) * 0.05f;
            for (int i = 0; i < _willpowerImages.Length; i++)
            {
                _willpowerImages[i].color = Color.white * ((_willpower.Value / _willpowerMax.Value) * 0.2f + 0.8f);
            }
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
                _onWillpowerDelta.Invoke(_actives[activeIndex] ? _currentBudgetElements[activeIndex].WillpowerNotActive : _currentBudgetElements[activeIndex].WillpowerActive);

                _actives[activeIndex] = !_actives[activeIndex];

                if (_currentBudgetElements[activeIndex].TimelapseElement != null)
                    _currentBudgetElements[activeIndex].TimelapseElement.Value = _actives[activeIndex];

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

            _start = true;
        }

        IEnumerator FadeToBlack()
        {
            while (_black.alpha < 1f)
            {
                _black.alpha += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator LoadTimelapse()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;
            while (!_start)
            {
                yield return null;
            }
            _black.gameObject.SetActive(true);
            yield return FadeToBlack();
            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            if (SceneManager.GetSceneByBuildIndex(5).isLoaded)
                asyncLoad = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(5));
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            asyncLoad = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(1));
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
