using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] FloatVariable _willpower;
        [SerializeField] FloatReference _willpowerMax;
        [SerializeField] IntVariable _money;
        [SerializeField] IntReference _moneyStart;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void PlayGame()
        {
            _willpower.Value = _willpowerMax.Value;
            _money.Value = _moneyStart.Value;

            SceneManager.LoadScene(1);
        }
    }
}
