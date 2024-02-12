using UnityEngine;
using UnityEngine.Events;
using UkensJoker.DataArchitecture;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

namespace UkensJoker.Engine
{
    public class JokerScreen : MonoBehaviour
    {
        [SerializeField] protected Sprite _emptyNumberFrame;
        [SerializeField] private Sprite _filledNumberFrame;

        [SerializeField] private IntReference[] _baseNumbers;
        [SerializeField] private IntReference[] _rightNumbers;

        private int _currentNumber = 0;

        [SerializeField] private FloatReference _anticipationTime;
        [SerializeField] private UnityEvent _onStartAnticipation;
        [SerializeField] private UnityEvent _onEndAnticipation;

        private bool _anticipating;

        [Serializable]
        class Column
        {
            public NumberSlot Up;
            public NumberSlot Down;
            public TMP_Text Text;
        }

        [Serializable]
        class NumberSlot
        {
            public Image Image;
            public TMP_Text Text;
            public int Number;

            public bool Reveal(bool correct, Sprite filledSprite)
            {
                Text.text = Number.ToString();
                Image.sprite = filledSprite;
                Image.color = correct ? Color.white : Color.black;
                Text.color = correct ? Color.black : Color.white;
                return correct;
            }

            public void Reset(Sprite emptySprite, int number)
            {
                Text.text = "";
                Image.sprite = emptySprite;
                Image.color = Color.white;
                Number = number;
            }
        }

        [SerializeField] private Column[] _columns;

        [SerializeField] private UnityEvent _onWrong;
        [SerializeField] private UnityEvent _onCorrect;

        [SerializeField] private UnityEvent _onFinished;
        [SerializeField] private GameEvent _generateNewPassword;

        public void ResetColumns()
        {
            _currentNumber = 0;
            for (int i = 0; i < _columns.Length; i++)
            {
                _columns[i].Up.Reset(_emptyNumberFrame, _rightNumbers[i].Value);
                _columns[i].Down.Reset(_emptyNumberFrame, _rightNumbers[i].Value);
                _columns[i].Text.text = _baseNumbers[i].Value.ToString();
            }
        }

        public void OnPressW()
        {
            StartCoroutine(AnticipationBeforeReveal(() => { return _columns[_currentNumber].Up.Reveal(_rightNumbers[_currentNumber].Value > _baseNumbers[_currentNumber].Value, _filledNumberFrame); }));
        }

        public void OnPressS()
        {
            StartCoroutine(AnticipationBeforeReveal(() => { return _columns[_currentNumber].Down.Reveal(_rightNumbers[_currentNumber].Value < _baseNumbers[_currentNumber].Value, _filledNumberFrame); }));
        }

        private IEnumerator AnticipationBeforeReveal(Func<bool> revealAction)
        {
            if (_anticipating)
                yield break;

            _anticipating = true;

            _onStartAnticipation.Invoke();
            yield return new WaitForSeconds(_anticipationTime.Value);
            _onEndAnticipation.Invoke();
            if (revealAction.Invoke())
                _onCorrect.Invoke();
            else
                _onWrong.Invoke();
            _currentNumber++;
            _anticipating = false;

            if (_currentNumber >= _rightNumbers.Length)
                _onFinished.Invoke();
        }

        public void DoGenerateNewPassword()
        {
            _generateNewPassword.Raise(this, null);
        }
    }
}
