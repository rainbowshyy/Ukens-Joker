using TMPro;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Cheat : MonoBehaviour, IInteractable
    {
        [SerializeField] private IntReference[] _numbers;
        [SerializeField] private TMP_Text[] _numberTexts;
        [SerializeField] private FloatReference _correctChance;
        [SerializeField] private FloatReference _willpower;
        [SerializeField] private FloatReference _willpowerMax;
        [SerializeField] private FloatReference _willpowerCorrectPenaltyMultiplier;
        [SerializeField] private FloatReference _cheatTime;
        [SerializeField] private UnityEvent _onCheat;
        [SerializeField] private UnityEvent _onCompleteCheat;
        [SerializeField] private FloatReference _randomNumberBlinkFrequency;

        [SerializeField] private string _interactText;
        [SerializeField] private string _cheatingInteractText;

        [SerializeField] private GameObject[] _objectsToDisableOnFinalDay;
        [SerializeField] private IntReference _day;

        private bool _cheating;
        private float _cheatTimeCurrent;

        private void Start()
        {
            if (_day.Value >= 4)
            {
                for (int i = 0; i < _objectsToDisableOnFinalDay.Length; i++)
                {
                    _objectsToDisableOnFinalDay[i].gameObject.SetActive(false);
                }
            }
        }

        private void Update()
        {
            if (!_cheating)
                return;

            int randomNumberStep = Mathf.FloorToInt(_cheatTimeCurrent / _randomNumberBlinkFrequency.Value);
            _cheatTimeCurrent += Time.deltaTime;
            int newRandomNumberStep = Mathf.FloorToInt(_cheatTimeCurrent / _randomNumberBlinkFrequency.Value);
            if (randomNumberStep != newRandomNumberStep)
            {
                SetRandomNumbers();
            }

            if (_cheatTimeCurrent >= _cheatTime.Value)
            {
                _cheating = false;
                _onCompleteCheat.Invoke();
                if (CorrectNumbers())
                {
                    Debug.Log("Cheat.exe generated correct numbers...");
                    for (int i = 0; i < _numbers.Length; i++)
                    {
                        _numberTexts[i].text = _numbers[i].Value.ToString();
                    }
                }
                else
                {
                    Debug.Log("Cheat.exe generated wrong numbers...");
                    SetRandomNumbers();
                }
            }
        }

        public string GetInteractText()
        {
            return _cheating ? _cheatingInteractText : _interactText;
        }

        public void Interact(Vector3 direction)
        {
            if (_cheating)
                return;

            _cheating = true;
            _cheatTimeCurrent = 0f;
            _onCheat.Invoke();
        }

        private void SetRandomNumbers()
        {
            for (int i = 0; i < _numberTexts.Length; i++)
            {
                _numberTexts[i].text = Mathf.FloorToInt(Random.Range(0f, 10f)).ToString();
            }
        }

        private bool CorrectNumbers()
        {
            return Random.Range(0f, 1f) <= _correctChance.Value - _willpowerCorrectPenaltyMultiplier.Value * (1f - _willpower.Value / _willpowerMax.Value);
        }
    }
}
