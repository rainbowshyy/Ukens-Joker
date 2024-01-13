using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    public interface IInteractable
    {
        public string InteractText { get; }
        public void Interact();
    }
}
