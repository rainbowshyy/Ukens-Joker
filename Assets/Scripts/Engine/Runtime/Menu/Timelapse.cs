using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class Timelapse : MonoBehaviour
    {
        private bool _playing;

        [SerializeField] private ScriptableObject[] _scriptableObjectsToKeepLoaded;

        [SerializeField] private FloatReference _timelapseTime;

        [SerializeField] private UnityEvent _onWalkTo;
        [SerializeField] private UnityEvent _stopWalkTo;
        [SerializeField] private Transform _walkToCam;
        private Coroutine _walkTo;

        [SerializeField] private UnityEvent _onTrain;
        [SerializeField] private UnityEvent _stopTrain;
        [SerializeField] private Transform _trainCam;
        private Coroutine _train;

        [SerializeField] private UnityEvent _onWork;
        [SerializeField] private UnityEvent _stopWork;
        [SerializeField] private Transform _workCam;
        private Coroutine _work;
        

        private void Start()
        {
            StartCoroutine(DoTimelapse());
            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            while (SceneManager.GetSceneByBuildIndex(1).isLoaded)
                yield return null;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;

            while (_playing)
                yield return null;

            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        IEnumerator DoTimelapse()
        {
            _playing = true;

            _onWalkTo.Invoke();
            _walkTo = StartCoroutine(WalkTo());
            yield return new WaitForSeconds(_timelapseTime.Value);
            StopCoroutine(_walkTo);
            _stopWalkTo.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timelapseTime.Value);
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onWork.Invoke();
            _work = StartCoroutine(Work(true, 10f));
            yield return new WaitForSeconds(_timelapseTime.Value);
            StopCoroutine(_work);
            _stopWork.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timelapseTime.Value);
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onWalkTo.Invoke();
            _walkTo = StartCoroutine(WalkTo());
            yield return new WaitForSeconds(_timelapseTime.Value);
            StopCoroutine(_walkTo);
            _stopWalkTo.Invoke();

            _playing = false;
        }

        IEnumerator WalkTo()
        {
            _walkToCam.transform.position += Vector3.right * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _walkTo = StartCoroutine(WalkTo());
        }

        IEnumerator Train(bool last)
        {
            yield return new WaitForSeconds(0.1f);
            _trainCam.transform.localPosition = new Vector3(_trainCam.localPosition.x, last ? 1.2f : 1.21f, _trainCam.localPosition.z);
            _train = StartCoroutine(Train(!last));
        }

        IEnumerator Work(bool first, float speed)
        {
            if (first)
                _workCam.localEulerAngles = new Vector3(_workCam.localEulerAngles.x, 90f, _workCam.localEulerAngles.z);

            _workCam.localEulerAngles = new Vector3(_workCam.localEulerAngles.x, _workCam.localEulerAngles.y - Time.deltaTime * speed, _workCam.localEulerAngles.z);
            yield return new WaitForEndOfFrame();
            _work = StartCoroutine(Work(false, speed + Time.deltaTime * 30f));
        }
    }
}
