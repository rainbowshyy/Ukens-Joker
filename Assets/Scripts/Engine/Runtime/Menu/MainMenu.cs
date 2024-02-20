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
        [SerializeField] IntVariable _day;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void PlayGame()
        {
            _willpower.Value = _willpowerMax.Value;
            _money.Value = _moneyStart.Value;
            _day.Value = 0;

            SceneManager.LoadScene(1);
        }
    }
}
