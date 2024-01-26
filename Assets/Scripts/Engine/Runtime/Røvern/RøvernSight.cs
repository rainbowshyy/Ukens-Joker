using UnityEngine;
using UkensJoker.DataArchitecture;

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

        private void Update()
        {
            float value = _sightDanger.Value;

            if (IsInSight())
            {
                value += Time.deltaTime * _sightDangerMultiplier.Value;
            }
            else
            {
                value -= Time.deltaTime * _sightDangerDecayMultiplier.Value;
            }

            _sightPosition.Value = GetScreenPos();
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
    }
}
