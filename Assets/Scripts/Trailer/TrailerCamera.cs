using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UkensJoker.Engine;
using UnityEngine;
using UnityEngine.Events;

public class TrailerCamera : MonoBehaviour
{
    [SerializeField] private GameEvent _numberInput;
    [SerializeField] private GameEvent _wTriggered;
    [SerializeField] private StringVariable _passwordString;
    [SerializeField] private IntVariable _baseNum0;
    [SerializeField] private IntVariable _baseNum1;
    [SerializeField] private IntVariable _baseNum2;
    [SerializeField] private IntVariable _baseNum3;
    [SerializeField] private IntVariable _baseNum4;
    [SerializeField] private IntVariable _rightNum0;
    [SerializeField] private IntVariable _rightNum1;
    [SerializeField] private IntVariable _rightNum2;
    [SerializeField] private IntVariable _rightNum3;
    [SerializeField] private IntVariable _rightNum4;

    [SerializeField] private PlayerRoom _kitchen;
    [SerializeField] private PlayerRoom _bedroom;
    [SerializeField] private PlayerRoom _livingRoom;
    [SerializeField] private GameEvent _updateConnected;

    [SerializeField] private CinemachineVirtualCamera _establishingCam1;
    [SerializeField] private CinemachineVirtualCamera _establishingCam2;
    [SerializeField] private CinemachineVirtualCamera _establishingCam3;
    [SerializeField] private CinemachineVirtualCamera _printer;
    [SerializeField] private CinemachineVirtualCamera _password;
    [SerializeField] private CinemachineVirtualCamera _finalCam;

    [SerializeField] private UnityEvent _prepareTrailer;
    [SerializeField] private UnityEvent _onStartTrailer;
    [SerializeField] private UnityEvent _onEstablishingShot1;
    [SerializeField] private float _establishingStot1Time;
    [SerializeField] private UnityEvent _onEstablishingShot2;
    [SerializeField] private float _establishingStot2Time;
    [SerializeField] private UnityEvent _onEstablishingShot3;
    [SerializeField] private float _establishingStot3Time;

    [SerializeField] private UnityEvent _onPrinter;
    [SerializeField] private float _printerTime;

    [SerializeField] private UnityEvent _onPassword;
    [SerializeField] private float _passwordTime;

    [SerializeField] private UnityEvent _onFinal;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _fovShrinkRate;

    private float _timeSincePress;
    private int presses = 0;
    [SerializeField] private float _timeBetweenPress;
    [SerializeField] private UnityEvent _onWrong;
    [SerializeField] private Transform _rover;
    [SerializeField] private RøvernSpot _spot;
    [SerializeField] private FloatReference _anticipationTime;
    [SerializeField] private FloatReference _wrongTime;

    private void Awake()
    {
        StartCoroutine(TrailerSequence());
    }

    IEnumerator TrailerSequence()
    {
        yield return new WaitForEndOfFrame();
        _prepareTrailer.Invoke();
        _passwordString.Value = "233156660000";
        _baseNum0.Value = 4;
        _baseNum1.Value = 6;
        _baseNum2.Value = 2;
        _baseNum3.Value = 4;
        _baseNum4.Value = 1;
        _rightNum0.Value = 7;
        _rightNum1.Value = 8;
        _rightNum2.Value = 3;
        _rightNum3.Value = 9;
        _rightNum4.Value = 0;
        yield return new WaitForSeconds(5f);
        _onStartTrailer.Invoke();
        yield return new WaitForEndOfFrame();
        _onEstablishingShot1.Invoke();
        StartCoroutine(EstablishingShot1());
        yield return new WaitForSeconds(_establishingStot1Time);
        _onEstablishingShot2.Invoke();
        StartCoroutine(EstablishingShot2());
        yield return new WaitForSeconds(_establishingStot2Time);
        _onEstablishingShot3.Invoke();
        StartCoroutine(EstablishingShot3());
        yield return new WaitForSeconds(_establishingStot3Time);
        _onPrinter.Invoke();
        StartCoroutine(Printer());
        yield return new WaitForSeconds(_printerTime);
        _onPassword.Invoke();
        StartCoroutine(Password());
        yield return new WaitForSeconds(_passwordTime);
        _timeSincePress = 0.5f;
        _onFinal.Invoke();
        StartCoroutine(FinalCamZoom());
    }

    IEnumerator EstablishingShot1()
    {
        _establishingCam1.Priority = 100;
        _livingRoom.SetPlayer(false);
        _kitchen.SetPlayer(true);
        _updateConnected.Raise(this, null);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator EstablishingShot2()
    {
        _establishingCam1.Priority = 0;
        _establishingCam2.Priority = 100;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator EstablishingShot3()
    {
        _establishingCam2.Priority = 0;
        _establishingCam3.Priority = 100;
        _kitchen.SetPlayer(false);
        _bedroom.SetPlayer(false);
        _updateConnected.Raise(this, null);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator Printer()
    {
        _establishingCam3.Priority = 0;
        _printer.Priority = 100;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator Password()
    {
        _printer.Priority = 0;
        _password.Priority = 100;
        _bedroom.SetPlayer(false);
        _livingRoom.SetPlayer(false);
        _updateConnected.Raise(this, null);
        int currentNumber = 0;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(_passwordTime / 4f);
            _numberInput.Raise(this, int.Parse(_passwordString.Value[currentNumber].ToString()));
            currentNumber++;
        }
    }

    IEnumerator FinalCamZoom()
    {
        _password.Priority = 0;
        _finalCam.Priority = 100;
        _finalCam.transform.position = _finalCam.transform.position + _finalCam.transform.forward * Time.deltaTime * _zoomSpeed;
        _finalCam.m_Lens.FieldOfView -= _fovShrinkRate * Time.deltaTime;
        _timeSincePress += Time.deltaTime;
        if (_timeSincePress >= _timeBetweenPress && presses < 5)
        {
            _timeSincePress -= _timeBetweenPress;
            _wTriggered.Raise(this, null);
            presses += 1;
        }
        else if (_timeSincePress >= _anticipationTime.Value && presses == 5)
        {
            _rover.position = _spot.Position;
            presses += 1;
        }
        else if (_timeSincePress >= _anticipationTime.Value + _wrongTime.Value && presses == 6)
        {
            _onWrong.Invoke();
            presses += 1;
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(FinalCamZoom());
    }
}
