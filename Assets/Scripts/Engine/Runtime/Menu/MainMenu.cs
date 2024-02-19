using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
