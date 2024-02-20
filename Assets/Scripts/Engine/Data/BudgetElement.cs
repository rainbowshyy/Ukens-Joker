using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    [CreateAssetMenu(menuName = "Budget/Create BudgetElement", fileName = "BudgetElement")]
    [Serializable]
    public class BudgetElement : ScriptableObject
    {
        public string Name;
        public int Delta;
        public float WillpowerActive;
        public float WillpowerNotActive;
        public bool Required;
        [HideInInspector] public bool Bought;
    }
}
