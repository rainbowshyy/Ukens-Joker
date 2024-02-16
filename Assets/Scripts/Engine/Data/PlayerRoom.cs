using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    [CreateAssetMenu(menuName = "Create Player Room", fileName = "PlayerRoom")]
    public class PlayerRoom : ScriptableObject
    {
        public event Action<bool> PlayerInRoom;
        public event Action<bool> PlayerConnectedToRoom;

        private List<Door> _doors = new List<Door>();

        private bool _hasPlayer;

        public void AddDoor(Door door)
        {
            _doors.Add(door);
        }

        public void SetPlayer(bool value)
        {
            _hasPlayer = value;
            PlayerInRoom?.Invoke(value);
        }

        public bool HasPlayer()
        {
            return _hasPlayer;
        }

        public void UpdateConnected()
        {
            if (_hasPlayer)
            {
                PlayerConnectedToRoom?.Invoke(true);
                return;
            }
            bool connected = false;
            foreach (Door door in _doors)
            {
                if (!door.IsOpen())
                    continue;

                foreach (PlayerRoom room in door.GetRooms())
                {
                    if (room._hasPlayer)
                    {
                        connected = true;
                        break;
                    }
                }
            }
            PlayerConnectedToRoom?.Invoke(connected);
        }
    }
}
