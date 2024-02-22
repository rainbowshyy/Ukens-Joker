using Cinemachine;
using System.Collections;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class RøvernKill : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onKill;

        [SerializeField] private FloatReference _dieTime;
        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private Transform _røvernFace;
        [SerializeField] private GameEvent _playerControls;
        [SerializeField] private FloatVariable _danger;

        private bool _lost;

        private void OnTriggerEnter(Collider other)
        {
            LoseGame();
        }

        public void LoseGame()
        {
            StartCoroutine(LoseGameCoroutine());
        }

        private void Update()
        {
            if (!_lost)
                return;

            _danger.Value += Time.deltaTime / _dieTime.Value;
        }

        IEnumerator LoseGameCoroutine()
        {
            _lost = true;
            _onKill.Invoke();
            _playerControls.Raise(this, false);
            CinemachineVirtualCamera vCam = (CinemachineVirtualCamera)_brain.ActiveVirtualCamera;
            CinemachineComposer composer = vCam.AddCinemachineComponent<CinemachineComposer>();
            composer.m_DeadZoneWidth = 0f;
            composer.m_DeadZoneHeight = 0f;
            vCam.LookAt = _røvernFace.transform;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
            
            asyncLoad.allowSceneActivation = false;
            yield return new WaitForSeconds(_dieTime.Value);
            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            asyncLoad = SceneManager.UnloadSceneAsync(2);
        }
    }
}
