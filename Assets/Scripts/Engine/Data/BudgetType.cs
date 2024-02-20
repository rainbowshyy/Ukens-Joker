using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    [CreateAssetMenu(menuName = "Budget/Create BudgetType", fileName = "BudgetType")]
    [Serializable]
    public class BudgetType : ScriptableObject
    {
        public BudgetElement[] BudgetElements;

        public BudgetElement[] GetRandomElements(int count)
        {
            List<int> available = new List<int>();
            for (int i = 0; i < BudgetElements.Length; i++)
            {
                available.Add(i);
            }

            BudgetElement[] toReturn = new BudgetElement[count];
            for (int i = 0; i < count; i++)
            {
                int rand = Mathf.FloorToInt(UnityEngine.Random.Range(0, available.Count));
                toReturn[i] = BudgetElements[available[rand]];
                available.RemoveAt(rand);
            }

            return toReturn;
        }
    }
}
