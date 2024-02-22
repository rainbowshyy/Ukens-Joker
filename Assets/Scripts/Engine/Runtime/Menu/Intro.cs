using System.Collections;
using System.Collections.Generic;
using TMPro;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UkensJoker.Engine
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private TMP_Text _subtitle;
        [SerializeField][TextArea] private string _introText;
        [SerializeField][TextArea] private string _jokerExplanation;
        [SerializeField][TextArea] private string _røvernExplanation;
        [SerializeField][TextArea] private string _røvernInDepth;
        [SerializeField][TextArea] private string _finalStatement;
        [SerializeField] private GameObject _blackPanel;
        [SerializeField] private FloatVariable _danger;
        [SerializeField] private Sprite[] _tvSprites;
        [SerializeField] private Image _tv;
        [SerializeField] private Material _tvMat;
        [SerializeField] private Light[] _tvLights;

        [SerializeField] private UnityEvent _onWrong;
        [SerializeField] private UnityEvent _onRøvern;
        [SerializeField] private UnityEvent _onUp;
        [SerializeField] private UnityEvent _onDown;
        [SerializeField] private UnityEvent _onCorrect;
        private bool _screenVisible;
        private bool _dangerStart;

        private void Start()
        {
            StartCoroutine(IntroSequence());
            _danger.Value = 0f;
            _tvMat.SetInt("_Wrong", 0);
            StartCoroutine(TV());
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (!_screenVisible)
                return;

            _camera.fieldOfView -= Time.deltaTime * 2f;

            if (!_dangerStart)
                return;

            _danger.Value += Time.deltaTime * 0.01f;
        }

        IEnumerator TV()
        {
            _tv.sprite = _tvSprites[0];
            yield return new WaitForSeconds(5f);
            _onUp.Invoke();
            yield return new WaitForSeconds(2f);
            _tv.sprite = _tvSprites[1];
            _onCorrect.Invoke();
            yield return new WaitForSeconds(5f);
            _onDown.Invoke();
            yield return new WaitForSeconds(2f);
            _tv.sprite = _tvSprites[2];
            _onCorrect.Invoke();
            yield return new WaitForSeconds(5f);
            _onUp.Invoke();
            yield return new WaitForSeconds(2f);
            _tv.sprite = _tvSprites[3];
            _onCorrect.Invoke();
            yield return new WaitForSeconds(5f);
            _onUp.Invoke();
            yield return new WaitForSeconds(2f);
            _tv.sprite = _tvSprites[4];
            _onCorrect.Invoke();
            yield return new WaitForSeconds(5f);
            _onDown.Invoke();
            yield return new WaitForSeconds(2f);
            _tv.sprite = _tvSprites[5];
            _tvMat.SetInt("_Wrong", 1);
            for (int i = 0; i < _tvLights.Length; i++)
            {
                _tvLights[i].color = Color.red;
            }
            _onWrong.Invoke();
            yield return new WaitForSeconds(0.5f);
            _tv.sprite = _tvSprites[6];
            _onRøvern.Invoke();
        }

        IEnumerator IntroSequence()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(5);
            asyncLoad.allowSceneActivation = false;

            _subtitle.text = _introText;
            yield return new WaitForSeconds(5f);
            _subtitle.text = "";
            _blackPanel.SetActive(false);
            _screenVisible = true;
            yield return new WaitForSeconds(2f);
            _subtitle.text = _jokerExplanation;
            yield return new WaitForSeconds(7f);
            _subtitle.text = "";
            _dangerStart = true;
            yield return new WaitForSeconds(2f);
            _subtitle.text = _røvernExplanation;
            yield return new WaitForSeconds(7f);
            _subtitle.text = "";
            yield return new WaitForSeconds(2f);
            _subtitle.text = _røvernInDepth;
            yield return new WaitForSeconds(7f);
            _subtitle.text = "";
            yield return new WaitForSeconds(4f);
            _subtitle.text = _finalStatement;
            yield return new WaitForSeconds(2f);
            _blackPanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            asyncLoad.allowSceneActivation = true;
           
        }
    }
}
