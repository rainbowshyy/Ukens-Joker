using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class WillpowerEmitter : MonoBehaviour
    {
        [SerializeField] private FloatVariable _willpower;
        [SerializeField] private FloatReference _willpowerChange;

        public void EmitWillpower()
        {
            _willpower.Value = Mathf.Clamp(_willpower.Value + _willpowerChange.Value, 0f, 100f);
        }
    }
}
