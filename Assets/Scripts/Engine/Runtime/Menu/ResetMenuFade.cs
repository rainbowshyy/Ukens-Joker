using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class ResetMenuFade : MonoBehaviour
    {
        [SerializeField] private Material _menuMaterial;

        //game event for when done fading and new scene is active
        private void Update()
        {
            _menuMaterial.SetFloat("_Fade", 1f);
        }
    }
}
