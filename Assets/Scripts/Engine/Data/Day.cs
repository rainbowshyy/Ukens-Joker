using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    [CreateAssetMenu(menuName = "Budget/Create Day", fileName = "Day")]
    public class Day : ScriptableObject
    {
        public BudgetTypeTable[] BudgetTypeTables;

        [Serializable]
        public struct BudgetTypeTable
        {
            public BudgetType BudgetType;
            public int Count;
        }
    }
}
