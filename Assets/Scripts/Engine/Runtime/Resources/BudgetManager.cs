using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UkensJoker.UI;
using UkensJoker.DataArchitecture;

namespace UkensJoker.Engine
{
    public class BudgetManager : MonoBehaviour
    {
        [SerializeField] private IntReference _day;
        [SerializeField] private Day[] _days;
        [SerializeField] private BudgetElementUI[] _uiElements;
        [SerializeField] private BudgetElement[] _currentBudgetElements;

        private void Start()
        {
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
            for (int i = 0; i < _days[day].BudgetTypeTables.Length; i++)
            {
                BudgetElement[] roll = _days[day].BudgetTypeTables[i].BudgetType.GetRandomElements(_days[day].BudgetTypeTables[i].Count);
                for (int x = 0; x < roll.Length; x++)
                {
                    _currentBudgetElements[i + x] = roll[x];
                }
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < _uiElements.Length; i++)
            {
                if (i < _currentBudgetElements.Length)
                {
                    _uiElements[i].SetValues(
                        _currentBudgetElements[i].Required,
                        _currentBudgetElements[i].Name,
                        _currentBudgetElements[i].Delta,
                        _currentBudgetElements[i].Required ? _currentBudgetElements[i].WillpowerActive : _currentBudgetElements[i].WillpowerNotActive,
                        _currentBudgetElements[i].Required
                        );
                }
                else
                {
                    _uiElements[i].ResetValues();
                }
            }
        }
    }
}
