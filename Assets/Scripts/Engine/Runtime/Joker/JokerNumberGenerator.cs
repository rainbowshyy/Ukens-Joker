using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class JokerNumberGenerator : MonoBehaviour
    {
        [SerializeField] private IntVariable[] _baseNumbers;
        [SerializeField] private IntVariable[] _rightNumbers;

        private void Start()
        {
            GenerateNumbers();
        }

        public void GenerateNumbers()
        {
            for (int i = 0; i< _baseNumbers.Length; i++)
            {
                _baseNumbers[i].Value = Mathf.FloorToInt(Random.Range(1, 9));

                _rightNumbers[i].Value = (Random.Range(0f, 1f) > 0.5f) ? Mathf.CeilToInt(Random.Range(_baseNumbers[i].Value, 10)) : Mathf.FloorToInt(Random.Range(0, _baseNumbers[i].Value));
            }
        }
    }
}
