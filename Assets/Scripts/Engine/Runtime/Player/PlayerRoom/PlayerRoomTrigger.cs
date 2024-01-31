using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class PlayerRoomTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvent _updateConnected;

        [SerializeField] private PlayerRoom _room;

        private void OnTriggerEnter(Collider other)
        {
            _room.SetPlayer(true);
            _updateConnected.Raise(this, null);
        }

        private void OnTriggerExit(Collider other)
        {
            _room.SetPlayer(false);
            _updateConnected.Raise(this, null);
        }

        public void UpdateConnected()
        {
            _room.UpdateConnected();
        }
    }
}
