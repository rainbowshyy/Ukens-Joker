using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class WagerScreen : MonoBehaviour
    {
        [SerializeField] private IntVariable _wager;
        [SerializeField] private IntReference _money;
        [SerializeField] private UnityEvent _onWagerSet;

        public void SetWager(Component sender, object data)
        {
            if (data is int)
            {
                if ((int)data * 100 > _money.Value || (int)data > 5 || (int)data == 0)
                    return;

                _wager.Value = (int)data * 100;
                _onWagerSet.Invoke();
            }
        }
    }
}
