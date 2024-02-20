using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class PowerListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onPowerOn;
        [SerializeField] private UnityEvent _onPowerOff;

        public void PowerChanged(Component sender, object status)
        {
            if (status is bool)
            {
                if ((bool)status)
                    _onPowerOn.Invoke();
                else
                    _onPowerOff.Invoke();
            }
        }
    }
}
