using System.Collections;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class TurnOnFlashlightAfterDelay : MonoBehaviour
    {
        [SerializeField] private FloatReference _flashlightDelay;
        [SerializeField] private UnityEvent _onTurnOn;

        private IEnumerator TurnOnFlashlight()
        {
            yield return new WaitForSeconds(_flashlightDelay.Value);
            _onTurnOn.Invoke();

        }

        public void TurnOn()
        {
            StartCoroutine(TurnOnFlashlight()); 
        }
    }
}
