using UnityEngine;
using UnityEngine.AI;
using UkensJoker.DataArchitecture;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class RøvernMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        [SerializeField] private RøvernSpot[] _røvernSpots; // Outside is index 0
        private RøvernSpot _spotCurrent;

        [SerializeField] private Vector3Reference _playerPosition;
        [SerializeField] private FloatReference _willpower;
        [SerializeField] private FloatReference _willpowerMax;
        [SerializeField] private FloatReference _willpowerOutsideRatioThreshold;

        [Header("Røvern Variables")]
        [SerializeField] private FloatReference _røvernUpdateTime;
        [SerializeField] private FloatReference _røvernUpdateAngerTime;
        [SerializeField] private FloatReference _røvernSpeedIdle;
        [SerializeField] private FloatReference _røvernSpeedChase;

        [Header("Spot Variables")]
        [SerializeField] private FloatReference _spotInterestDecay;
        [SerializeField] private FloatReference _spotInterestMin;
        [SerializeField] private FloatReference _spotInterestMax;

        private float _timeBeforeUpdate;
        private bool _chasing;

        [SerializeField] private UnityEvent _onPlayerSpot;

        [SerializeField] private UnityEvent _onWindow;

        [SerializeField] private UnityEvent _onStartChase;

        [SerializeField] private FloatReference _footstepFrequencyIdle;
        [SerializeField] private FloatReference _footstepFrequencyChase;
        [SerializeField] private UnityEvent _onFootstepIdle;
        [SerializeField] private UnityEvent _onFootstepChase;
        private float _footstepTimeCurrent;

        private void Start()
        {
            _spotCurrent = _røvernSpots[0];
            _spotCurrent.OnInterestChanged += RemoveTimeOnInterest;
            MoveToCurrentSpot();
            _timeBeforeUpdate = _røvernUpdateTime.Value;
            _agent.speed = _røvernSpeedIdle.Value;
        }

        private void Update()
        {
            if (_spotCurrent.HasPlayer && !_chasing)
            {
                Debug.Log("Is in same spot as player...");
                _onPlayerSpot.Invoke();
            }

            _timeBeforeUpdate -= Time.deltaTime;

            if (_chasing)
            {
                _agent.destination = _playerPosition.Value;
                _footstepTimeCurrent += Time.deltaTime * _footstepFrequencyChase.Value;
                if (_footstepTimeCurrent >= 1f)
                {
                    _footstepTimeCurrent -= 1f;
                    _onFootstepChase.Invoke();
                }
                return;
            }

            if (_timeBeforeUpdate <= 0)
            {
                SetNewCurrentSpot();
                MoveToCurrentSpot();
                UpdateInterests();
                _timeBeforeUpdate = _røvernUpdateTime.Value + (_willpower.Value - _willpowerMax.Value) * _røvernUpdateAngerTime.Value;
            }

            if (!_agent.enabled)
                return;

            if (_agent.velocity.magnitude > 0.1f)
                _footstepTimeCurrent += Time.deltaTime * _footstepFrequencyIdle.Value;

            if (_footstepTimeCurrent >= 1f)
            {
                _footstepTimeCurrent -= 1f;
                _onFootstepIdle.Invoke();
            }
        }

        private void MoveToCurrentSpot()
        {
            if (_agent.enabled != _spotCurrent.PathfindingEnabled)
                _onWindow.Invoke();

            if (_spotCurrent.PathfindingEnabled)
            {
                _agent.enabled = true;
                _agent.destination = _spotCurrent.Position;
            }
            else
            {
                _agent.enabled = false;
                transform.position = _spotCurrent.Position;
            }
        }

        private void SetNewCurrentSpot()
        {
            string debugMessage = "Spots to evaluate: ";
            float range = 0f;
            for (int i = 0; i < _spotCurrent.Connections.Length; i++)
            {
                range += Mathf.Clamp(_spotCurrent.Connections[i].Interest, _spotInterestMin.Value, _spotInterestMax.Value);
                debugMessage += $"{_spotCurrent.Connections[i].name}: {range}, ";
            }

            float rand = Random.Range(0f, range);
            debugMessage += $"Rolled {rand}";
            Debug.Log(debugMessage);
            range = 0f;
            for (int i = 0; i < _spotCurrent.Connections.Length; i++)
            {
                range += Mathf.Clamp(_spotCurrent.Connections[i].Interest, _spotInterestMin.Value, _spotInterestMax.Value);
                if (rand <= range)
                {
                    _spotCurrent.OnInterestChanged -= RemoveTimeOnInterest;

                    _spotCurrent = _spotCurrent.Connections[i];

                    _spotCurrent.OnInterestChanged += RemoveTimeOnInterest;

                    Debug.Log($"Set new spot to {_spotCurrent.name}");
                    return;
                }
            }
            Debug.Log("No new spot found");
            return;
        }

        private void RemoveTimeOnInterest(float interest)
        {
            _timeBeforeUpdate -= interest;
            Debug.Log($"Sped up Røvern by {interest}");
        }

        private void UpdateInterests()
        {
            _spotCurrent.SetInterest(_spotInterestMin.Value);
            _røvernSpots[0].SetInterest((_willpower.Value / _willpowerMax.Value + _willpowerOutsideRatioThreshold.Value) * _spotInterestMax.Value); //Outside is unique
            for (int i = 1; i < _røvernSpots.Length; i++)
            {
                _røvernSpots[i].SetInterest(Mathf.Clamp(_røvernSpots[i].Interest * _spotInterestDecay.Value, _spotInterestMin.Value, _spotInterestMax.Value));
            }
        }

        public void Chase(bool chase)
        {
            _chasing = chase;

            _agent.enabled = chase;
            _agent.speed = chase ? _røvernSpeedChase.Value : _røvernSpeedIdle.Value;

            if (chase)
            {
                _onStartChase.Invoke();
                return;
            }

            SetNewSpotFromPosition();
            MoveToCurrentSpot();
            UpdateInterests();
            _timeBeforeUpdate = _røvernUpdateTime.Value + (_willpower.Value - _willpowerMax.Value) * _røvernUpdateAngerTime.Value;
        }

        private void SetNewSpotFromPosition()
        {
            float minLength = 100;
            int minIndex = -1;
            for (int i = 0; i < _røvernSpots.Length; i++)
            {
                if (_røvernSpots[i].PathfindingEnabled)
                {
                    float length = (_røvernSpots[i].Position - transform.position).magnitude;
                    if (length < minLength)
                    {
                        minLength = length;
                        minIndex = i;
                    }
                }
            }

            if (minIndex != -1)
            {
                _spotCurrent.OnInterestChanged -= RemoveTimeOnInterest;

                _spotCurrent = _røvernSpots[minIndex];

                _spotCurrent.OnInterestChanged += RemoveTimeOnInterest;
            }
        }
    }
}
