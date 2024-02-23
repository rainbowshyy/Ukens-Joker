using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class WinScreen : MonoBehaviour
    {
        private bool _done;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private FloatReference _waitTime;
        
        [Serializable]
        class WinScreenElement
        {
            [TextArea] public string Text;
            public float time;
            public UnityEvent OnShow;
        }

        [SerializeField] private WinScreenElement[] _winScreenElements;

        private void Start()
        {
            StartCoroutine(LoadMainMenuAsync());
            StartCoroutine(ShowWinScreen());
        }

        IEnumerator LoadMainMenuAsync()
        {
            while (SceneManager.GetSceneByBuildIndex(2).isLoaded)
                yield return null;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;
            while (!_done)
                yield return null;

            asyncLoad.allowSceneActivation = true;
        }

        private IEnumerator ShowWinScreen()
        {
            for (int i = 0; i < _winScreenElements.Length; i++)
            {
                _winScreenElements[i].OnShow.Invoke();
                _text.text = _winScreenElements[i].Text;
                yield return new WaitForSeconds(_winScreenElements[i].time);
                _text.text = "";
                yield return new WaitForSeconds(_waitTime.Value);
            }
            PlayerPrefs.SetInt("Won", 1);
            _done = true;
        }
    }
}
