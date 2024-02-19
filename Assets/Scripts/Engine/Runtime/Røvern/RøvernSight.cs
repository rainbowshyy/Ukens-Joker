using UnityEngine;
using UkensJoker.DataArchitecture;
using UnityEngine.Events;
using UnityEngine.AI;

namespace UkensJoker.Engine
{
    public class RøvernSight : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _playerPosition;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _camera;

        [SerializeField] private FloatVariable _sightDanger;
        [SerializeField] private FloatReference _sightDangerMax;
        [SerializeField] private FloatReference _sightDangerMultiplier;
        [SerializeField] private FloatReference _sightDangerDecayMultiplier;
        [SerializeField] private Vector2Variable _sightPosition;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private FloatVariable _sightTimeBeforeChase;
        [SerializeField] private FloatVariable _sightTimeWindowMultipler;
        [SerializeField] private FloatVariable _sightTimeStopChase;
        [SerializeField] private UnityEvent<bool> _onSightChase;
        private float _sightTimeCurrent;
        private bool _chasing;

        private void Awake()
        {
            _sightDanger.Value = 0f;
        }

        private void Update()
        {
            Debug.Log(IsInSight());
            _sightPosition.Value = GetScreenPos();

            if (_chasing)
            {
                _sightDanger.Value = _sightDangerMax.Value;
                if (IsInSight())
                {
                    _sightTimeCurrent = 0f;
                }
                else
                {
                    _sightTimeCurrent += Time.deltaTime;
                }

                if (_sightTimeCurrent >= _sightTimeStopChase.Value)
                {
                    Chase(false);
                }
                return;
            }

            float value = _sightDanger.Value;

            if (IsInSight())
            {
                _sightTimeCurrent += _agent.enabled ? Time.deltaTime : Time.deltaTime * _sightTimeWindowMultipler.Value;
                value += Time.deltaTime * _sightDangerMultiplier.Value;
            }
            else
            {
                _sightTimeCurrent = 0f;
                value -= Time.deltaTime * _sightDangerDecayMultiplier.Value;
            }

            if (_sightTimeCurrent >= _sightTimeBeforeChase.Value)
            {
                Chase(true);
            }

            _sightDanger.Value = Mathf.Clamp(value, 0f, _sightDangerMax.Value);
        }

        private bool IsInSight()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _playerPosition.Value - transform.position, out hit, (_playerPosition.Value - transform.position).magnitude - 0.5f, _layerMask))
            {
                if (hit.collider != null) 
                    return false;
            }
            return true;
        }

        private Vector2 GetScreenPos()
        {
            return _camera.WorldToViewportPoint(transform.position);
        }

        public void Chase(bool chase)
        {
            _chasing = chase;
            _sightTimeCurrent = 0f;
            _onSightChase.Invoke(chase);
        }
    }
}
