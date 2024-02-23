using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class JokerNumberGenerator : MonoBehaviour
    {
        [SerializeField] private IntVariable[] _baseNumbers;
        [SerializeField] private IntVariable[] _rightNumbers;

        [SerializeField] private IntReference _day;

        private void Start()
        {
            GenerateNumbers();
        }

        public void GenerateNumbers()
        {
            if (_day.Value >= 4)
            {
                for (int i = 0; i < _baseNumbers.Length; i++)
                {
                    List<int> validNumbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    validNumbers.RemoveAt(_baseNumbers[i].Value);
                    _rightNumbers[i].Value = validNumbers[Mathf.FloorToInt(Random.Range(0f, validNumbers.Count))];
                }
            }

            for (int i = 0; i< _baseNumbers.Length; i++)
            {
                _baseNumbers[i].Value = Mathf.FloorToInt(Random.Range(1, 9));

                _rightNumbers[i].Value = (Random.Range(0f, 1f) > 0.5f) ? Mathf.CeilToInt(Random.Range(_baseNumbers[i].Value + 0.01f, 10)) : Mathf.FloorToInt(Random.Range(0, _baseNumbers[i].Value - 0.01f));
            }
        }
    }
}
