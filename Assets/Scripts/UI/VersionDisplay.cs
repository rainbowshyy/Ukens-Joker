using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UkensJoker.UI
{
    public class VersionDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        private void Awake()
        {
            _text.text = "Version: " + Application.version;
        }
    }
}
