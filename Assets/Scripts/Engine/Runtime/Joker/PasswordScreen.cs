using UnityEngine;
using UkensJoker.DataArchitecture;
using UnityEngine.Events;
using TMPro;

namespace UkensJoker.Engine
{
    public class PasswordScreen : MonoBehaviour
    {
        [SerializeField] private StringReference _password;

        [SerializeField] private TMP_Text _passwordText;

        [SerializeField] private UnityEvent _onRightPassword;
        [SerializeField] private UnityEvent _onWrongPassword;
        [SerializeField] private UnityEvent _onEnable;

        private string _passwordCurrent = "";

        private void OnEnable()
        {
            _onEnable.Invoke();
        }

        public void InputCharacter(Component sender, object data)
        {
            if (data is not int)
                return;

            if (_passwordCurrent.Length >= 12)
                return;

            _passwordCurrent += data.ToString();
            _passwordText.text = _passwordCurrent;
        }

        public void InputBackspace()
        {
            if (_passwordCurrent.Length > 0)
            {
                _passwordCurrent = _passwordCurrent.Substring(0,_passwordCurrent.Length-1);
                _passwordText.text = _passwordCurrent;
            }
        }

        public void InputEnter()
        {
            if (_passwordCurrent == _password.Value)
                _onRightPassword.Invoke();
            else
                _onWrongPassword.Invoke();
        }
    }
}
