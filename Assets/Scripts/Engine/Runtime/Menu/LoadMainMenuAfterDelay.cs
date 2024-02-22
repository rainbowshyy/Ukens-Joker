using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class LoadMainMenuAfterDelay : MonoBehaviour
    {
        [SerializeField] FloatReference _deadTime;

        private void Start()
        {
            StartCoroutine(LoadMainMenuAsync());
        }

        IEnumerator LoadMainMenuAsync()
        {
            while (SceneManager.GetSceneByBuildIndex(2).isLoaded)
                yield return null;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;
            yield return new WaitForSeconds(_deadTime.Value);
            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }    
    }
}
