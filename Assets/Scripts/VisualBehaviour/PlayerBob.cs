using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class PlayerBob : MonoBehaviour
    {
        [SerializeField] private FloatReference _bobIntensityHorizontal;
        [SerializeField] private FloatReference _bobIntensityVertical;
        [SerializeField] private FloatReference _bobFrequency;
        [SerializeField] private FloatReference _bobAcceleration;

        private Vector3 _initialPosition;

        private bool _isBobbing;

        private float _bobVelocity;
        private float _bobTime;
        private Vector3 _lastBobPosition;

        private void Awake()
        {
            _initialPosition = transform.localPosition;
        }

        private void Update()
        {
            if (!_isBobbing && _bobVelocity <= 0f)
            {
                return;
            }
            _bobVelocity += _isBobbing ? Time.deltaTime * _bobAcceleration.Value : -Time.deltaTime * _bobAcceleration.Value;
            _bobVelocity = Mathf.Clamp(_bobVelocity, 0f, _bobFrequency.Value);

            _bobTime += Time.deltaTime * _bobVelocity;

            transform.localPosition = _initialPosition + new Vector3(Mathf.Sin(_bobTime) * _bobIntensityHorizontal.Value, -Mathf.Abs(Mathf.Cos(_bobTime)) * _bobIntensityVertical.Value, 0f);
        }

        public void SetBobbing(bool isBobbing)
        {
            _isBobbing = isBobbing;
        }
    }
}
