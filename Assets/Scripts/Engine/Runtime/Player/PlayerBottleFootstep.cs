using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class PlayerBottleFootstep : MonoBehaviour
    {
        [SerializeField] private PlayerRoom _kitchen;

        [SerializeField] private FloatReference _bottleSoundChance;

        [SerializeField] private UnityEvent _onBottleFootstep;

        public void TryDoBottleFootstep()
        {
            if (!_kitchen.HasPlayer())
                return;

            if (Random.Range(0f, 1f) > _bottleSoundChance.Value)
                return;

            _onBottleFootstep.Invoke();
        }
    }
}
