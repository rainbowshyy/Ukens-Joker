using System;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.DataArchitecture
{
    [Serializable]
    public class CustomUnityEvent : UnityEvent<Component, object> {}
}
