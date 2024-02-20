using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class ComputerWrong : MonoBehaviour
    {
        [SerializeField] private Material _computerMat;
        [SerializeField] private Light[] _lights;
        private Color[] _lightColors;

        [SerializeField] private UnityEvent _onPasswordWrong;
        [SerializeField] private UnityEvent _onJokerWrong;

        [SerializeField] private FloatReference _wrongTime;

        [SerializeField] private GameObject _logic;

        private float _wrongTimeCurrent;
        private bool _wrong;
        private bool _computerIsInteractedWith;

        private void Awake()
        {
            _lightColors = new Color[_lights.Length];
            for (int i = 0; i < _lights.Length; i++)
            {
                _lightColors[i] = _lights[i].color;
            }
        }

        private void Update()
        {
            if (_wrong)
            {
                _wrongTimeCurrent -= Time.deltaTime;
                if (_wrongTimeCurrent <= 0 )
                {
                    _wrong = false;
                    _computerMat.SetInt("_Wrong", 0);
                    for (int i = 0; i < _lightColors.Length; i++)
                    {
                        _lights[i].color = _lightColors[i];
                    }
                    _logic.SetActive(_computerIsInteractedWith);
                }
            }
        }

        public void DoPasswordWrong()
        {
            _onPasswordWrong.Invoke();
            Wrong();
        }

        public void DoJokerWrong()
        {
            _onJokerWrong.Invoke();
            Wrong();
        }

        private void Wrong()
        {
            _wrongTimeCurrent = _wrongTime.Value;
            _computerMat.SetInt("_Wrong", 1);
            for (int i = 0; i < _lightColors.Length; i++)
            {
                _lights[i].color = Color.red;
            }
            _wrong = true;
            _logic.SetActive(false);
        }

        public void SetComputerInteracted(bool interacted)
        {
            _computerIsInteractedWith = interacted;
        }
    }
}
