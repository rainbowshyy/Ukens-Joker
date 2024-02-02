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

        [SerializeField] private UnityEvent _onKeyPress;

        private string _passwordCurrent = "";

        public void InputCharacter(Component sender, object data)
        {
            if (data is not int)
                return;

            _onKeyPress.Invoke();

            if (_passwordCurrent.Length >= 12)
                return;

            _passwordCurrent += data.ToString();
            _passwordText.text = _passwordCurrent;
        }
    }
}
