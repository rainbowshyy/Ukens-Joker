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

        [SerializeField] private FloatReference _wrongTime;

        private bool _anticipating;
        private bool _finishing;

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

            public bool Reveal(bool correct)
            {
                Text.text = Number.ToString();
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

            public void Select(string character, Color color, Sprite filledSprite)
            {
                Text.text = character;
                Text.color = color;
                Image.sprite = filledSprite;
                Image.color = Color.black;
            }
        }

        [SerializeField] private Column[] _columns;

        [SerializeField] private UnityEvent _onWrong;
        [SerializeField] private UnityEvent _onCorrect;

        [SerializeField] private UnityEvent _onFinished;
        [SerializeField] private GameEvent _generateNewPassword;

        [SerializeField] private IntVariable _money;
        [SerializeField] private IntReference _wager;
        [SerializeField] private TMP_Text _wagerText;
        [SerializeField] private Color _inputColor;

        public void ResetColumns()
        {
            _currentNumber = 0;
            for (int i = 0; i < _columns.Length; i++)
            {
                _columns[i].Up.Reset(_emptyNumberFrame, _rightNumbers[i].Value);
                _columns[i].Down.Reset(_emptyNumberFrame, _rightNumbers[i].Value);
                _columns[i].Text.text = _baseNumbers[i].Value.ToString();
            }
            _wagerText.text = _wager.Value.ToString() + "\nkr";
            SelectCurrent();
        }

        public void OnPressW()
        {
            StartCoroutine(AnticipationBeforeReveal(() => { return _columns[_currentNumber].Up.Reveal(_rightNumbers[_currentNumber].Value > _baseNumbers[_currentNumber].Value); }));
            _columns[_currentNumber].Down.Reset(_emptyNumberFrame, _rightNumbers[_currentNumber].Value);
        }

        public void OnPressS()
        {
            StartCoroutine(AnticipationBeforeReveal(() => { return _columns[_currentNumber].Down.Reveal(_rightNumbers[_currentNumber].Value < _baseNumbers[_currentNumber].Value); }));
            _columns[_currentNumber].Up.Reset(_emptyNumberFrame, _rightNumbers[_currentNumber].Value);
        }

        private IEnumerator AnticipationBeforeReveal(Func<bool> revealAction)
        {
            if (_anticipating || _finishing)
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
                StartCoroutine(Finish());
            else
                SelectCurrent();
        }

        public void StopAnticipation()
        {
            if (_anticipating)
                SelectCurrent();
            _anticipating = false;
        }

        private void SelectCurrent()
        {
            _columns[_currentNumber].Up.Select("W", _inputColor, _filledNumberFrame);
            _columns[_currentNumber].Down.Select("S", _inputColor, _filledNumberFrame);
        }

        private IEnumerator Finish()
        {
            _finishing = true;
            yield return new WaitForSeconds(_wrongTime.Value);
            _finishing = false;
            _onFinished.Invoke();
        }

        public void DoGenerateNewPassword()
        {
            _generateNewPassword.Raise(this, null);
        }

        public void AddOrRemoveMoney(bool correct)
        {
            if (correct)
                _money.Value += _wager.Value;
            else
            {
                int newMoney = _money.Value;
                newMoney -= _wager.Value;
                if (newMoney < 0)
                    newMoney = 0;

                _money.Value = newMoney;
            }
        }
    }
}
