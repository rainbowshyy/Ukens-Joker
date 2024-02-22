using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UkensJoker.Engine
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] FloatVariable _willpower;
        [SerializeField] FloatReference _willpowerMax;
        [SerializeField] IntVariable _money;
        [SerializeField] IntReference _moneyStart;
        [SerializeField] IntVariable _day;

        private bool _starting;
        [SerializeField] private Material _menuMat;
        private float _alpha = 1f;

        [SerializeField] private GameObject[] _objectsToDestroy;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _menuMat.SetFloat("_Fade", _alpha);
        }

        private void Update()
        {
            if (!_starting)
                return;

            _alpha = Mathf.Clamp01(_alpha - Time.deltaTime);

            _menuMat.SetFloat("_Fade", _alpha);
        }

        public void PlayGame()
        {
            if (_starting)
                return;

            _willpower.Value = _willpowerMax.Value;
            _money.Value = _moneyStart.Value;
            _day.Value = 0;

            StartCoroutine(StartAfterDelay());
        }

        IEnumerator StartAfterDelay()
        {
            _starting = true;
            yield return new WaitForSeconds(1f);
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
                yield return null;

            for (int i = 0; i < _objectsToDestroy.Length; i++)
            {
                Destroy(_objectsToDestroy[i]);
            }
        }
    }
}
