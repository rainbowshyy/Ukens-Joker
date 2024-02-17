using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.UI
{
    public class WillpowerBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _bar;
        [SerializeField] private int _barParentHeightDelta;

        [SerializeField] private FloatReference _willpower;
        [SerializeField] private FloatReference _willpowerMax;

        [SerializeField] private UnityEvent _onNewBar;

        private int _maxHeight;
        private int _previousHeight;

        private void Awake()
        {
            RectTransform parent = _bar.parent as RectTransform;
            _maxHeight = (int)parent.rect.height - _barParentHeightDelta;
        }

        private void OnEnable()
        {
            _willpower.RegisterListener(UpdateWillpower);
        }

        private void OnDisable()
        {
            _willpower.UnregisterListener(UpdateWillpower);
        }

        private void UpdateWillpower(float willpower)
        {
            float ratio = _willpower.Value / _willpowerMax.Value;
            int newSize = Mathf.FloorToInt(ratio * _maxHeight);

            if (newSize != _previousHeight)
            {
                _onNewBar.Invoke();
                _previousHeight = newSize;
            }

            _bar.sizeDelta = new Vector2( _bar.sizeDelta.x, newSize);
        }
    }
}
