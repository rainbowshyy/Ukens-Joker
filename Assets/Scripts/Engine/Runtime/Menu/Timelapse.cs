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
        [SerializeField] private FloatReference _timelapseDecayTime;
        private float _timeCurrent; 

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


        [SerializeField] private UnityEvent _onCafe;
        [SerializeField] private UnityEvent _stopCafe;
        [SerializeField] private Transform _cafeCam;
        private Coroutine _cafe;

        [SerializeField] private UnityEvent _onBar;
        [SerializeField] private UnityEvent _stopBar;
        [SerializeField] private Transform _barCam;
        private Coroutine _bar;

        [SerializeField] private UnityEvent _onWalkFrom;
        [SerializeField] private UnityEvent _stopWalkFrom;
        [SerializeField] private Transform _walkFromCam;
        private Coroutine _walkFrom;

        [SerializeField] private UnityEvent _onSchool;
        [SerializeField] private UnityEvent _stopSchool;
        [SerializeField] private Transform _schoolCam;
        private Coroutine _school;

        private void Start()
        {
            StartCoroutine(DoTimelapse());
            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            while (SceneManager.GetSceneByBuildIndex(1).isLoaded)
                yield return null;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;

            while (_playing)
                yield return null;

            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            asyncLoad = SceneManager.UnloadSceneAsync(4);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        IEnumerator DoTimelapse()
        {
            _playing = true;
            _timeCurrent = _timelapseTime.Value;

            _onWalkTo.Invoke();
            _walkTo = StartCoroutine(WalkTo());
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_walkTo);
            _stopWalkTo.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onSchool.Invoke();
            _school = StartCoroutine(School(2f));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_school);
            _stopSchool.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onWork.Invoke();
            _work = StartCoroutine(Work(true, 10f));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_work);
            _stopWork.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onCafe.Invoke();
            _cafe = StartCoroutine(Cafe(0f));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_cafe);
            _stopCafe.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onBar.Invoke();
            _bar = StartCoroutine(Bar(15f));
            yield return new WaitForSeconds(_timeCurrent);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_bar);
            _stopBar.Invoke();

            _onTrain.Invoke();
            _train = StartCoroutine(Train(true));
            yield return new WaitForSeconds(_timelapseTime.Value * 2f);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_train);
            _stopTrain.Invoke();

            _onWalkFrom.Invoke();
            _walkFrom = StartCoroutine(WalkFrom());
            yield return new WaitForSeconds(_timelapseTime.Value * 2f);
            _timeCurrent -= _timelapseDecayTime.Value;
            StopCoroutine(_walkFrom);
            _stopWalkFrom.Invoke();

            _playing = false;
        }

        IEnumerator WalkTo()
        {
            _walkToCam.transform.position += Vector3.right * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _walkTo = StartCoroutine(WalkTo());
        }

        IEnumerator WalkFrom()
        {
            _walkFromCam.transform.position += Vector3.left * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _walkFrom = StartCoroutine(WalkFrom());
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

        IEnumerator Cafe(float speed)
        { 
            _cafeCam.transform.position = _cafeCam.transform.position + _cafeCam.transform.forward * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _cafe = StartCoroutine(Cafe(speed + 0.2f * Time.deltaTime));
        }

        IEnumerator Bar(float speed)
        {
            _barCam.transform.localEulerAngles = new Vector3(_barCam.localEulerAngles.x, _barCam.localEulerAngles.y + speed * Time.deltaTime, _barCam.localEulerAngles.z);
            yield return new WaitForEndOfFrame();
            _bar = StartCoroutine(Bar(speed - Time.deltaTime * 0.5f));
        }

        IEnumerator School(float speed)
        {
            _schoolCam.localEulerAngles = new Vector3(_schoolCam.localEulerAngles.x + speed * Time.deltaTime, _schoolCam.localEulerAngles.y + speed * Time.deltaTime, _schoolCam.localEulerAngles.z);
            yield return new WaitForEndOfFrame();
            _school = StartCoroutine(School(speed + Time.deltaTime * 2f));
        }
    }
}
