using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.UI
{
    public class ShakeUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private IntReference _shakePixelIntensity;
        [SerializeField] private FloatReference _shakeCooldown;
        [SerializeField] private IntReference _shakeAmount;

        private int _baseX;
        private int _offsetX;
        private float _shakeCurrent;
        private int _shakesLeft;

        private void Awake()
        {
            if (_rectTransform == null && GetComponent<RectTransform>() != null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            _baseX = (int)_rectTransform.anchoredPosition.x;
        }

        private void Update()
        {
            if (_shakesLeft <= 0)
                return;

            _shakeCurrent -= Time.deltaTime;
            if (_shakeCurrent <= 0)
            {
                Debug.Log(_shakesLeft);
                _shakeCurrent = _shakeCooldown.Value;
                _shakesLeft -= 1;
                if (_shakesLeft <= 0)
                    _offsetX = 0;
                else
                    _offsetX *= -1;
                _rectTransform.anchoredPosition = new Vector2(_baseX + _offsetX, _rectTransform.anchoredPosition.y);
            }
        }

        public void DoShake()
        {
            _shakesLeft = _shakeAmount.Value;
            _shakeCurrent = _shakeCooldown.Value;
            _offsetX = _shakePixelIntensity.Value;
        }
    }
}
