using UnityEngine;
using UnityEngine.AI;
using UkensJoker.DataArchitecture;

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
        [SerializeField] private FloatReference _røvernSpeedCurious;
        [SerializeField] private FloatReference _røvernSpeedChase;

        [Header("Spot Variables")]
        [SerializeField] private FloatReference _spotInterestDecay;
        [SerializeField] private FloatReference _spotInterestMin;
        [SerializeField] private FloatReference _spotInterestMax;

        private float _timeBeforeUpdate;

        private void Start()
        {
            _spotCurrent = _røvernSpots[0];
            _spotCurrent.OnInterestChanged += RemoveTimeOnInterest;
            MoveToCurrentSpot();
            _timeBeforeUpdate = _røvernUpdateTime.Value;
        }

        private void Update()
        {
            _timeBeforeUpdate -= Time.deltaTime;

            if (_timeBeforeUpdate <= 0)
            {
                SetNewCurrentSpot();
                MoveToCurrentSpot();
                UpdateInterests();
                _timeBeforeUpdate = _røvernUpdateTime.Value + (_willpower.Value - _willpowerMax.Value) * _røvernUpdateAngerTime.Value;
            }
        }

        private void MoveToCurrentSpot()
        {
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
            _røvernSpots[0].SetInterest(_spotInterestMax.Value - (_willpower.Value / _willpowerMax.Value + _willpowerOutsideRatioThreshold.Value) * _spotInterestMax.Value); //Outside is unique
            for (int i = 1; i < _røvernSpots.Length; i++)
            {
                _røvernSpots[i].SetInterest(Mathf.Clamp(_røvernSpots[i].Interest * _spotInterestDecay.Value, _spotInterestMin.Value, _spotInterestMax.Value));
            }
        }
    }
}
