using UnityEngine;
using UnityEngine.VFX;
using UkensJoker.DataArchitecture;
using UkensJoker.Engine;

namespace UkensJoker.Audio
{
    public class SpatialAudioPlayer : AudioPlayer
    {
        [SerializeField] private Vector3Reference _playerPosition;
        [SerializeField] private FloatReference _dryLevelDistanceModifier;
        [SerializeField] private FloatReference _lowPassNoPlayer;
        [SerializeField] private FloatReference _lowPassNotConnected;

        [SerializeField] private VisualEffect _vfx;
        [SerializeField] private float _vfxIntensity;

        [SerializeField] private PlayerRoom _room;

        private AudioReverbFilter _reverb;
        private AudioLowPassFilter _lowPass;

        private bool _roomPlayer;
        private bool _roomConnected;

        protected override void Awake()
        {
            base.Awake();
            _audioSource.spatialBlend = 1f;

            _reverb = gameObject.AddComponent<AudioReverbFilter>();

            _reverb.room = _audio.Reverb.Room;
            _reverb.roomHF = _audio.Reverb.RoomHF;
            _reverb.roomLF = _audio.Reverb.RoomLF;
            _reverb.decayTime = _audio.Reverb.DecayTime;
            _reverb.decayHFRatio = _audio.Reverb.DecayHFRatio;
            _reverb.reflectionsLevel = _audio.Reverb.ReflectionsLevel;
            _reverb.reflectionsDelay = _audio.Reverb.ReflectionsDelay;
            _reverb.reverbLevel = _audio.Reverb.ReverbLevel;
            _reverb.reverbDelay = _audio.Reverb.ReverbDelay;
            _reverb.hfReference = _audio.Reverb.HFReference;
            _reverb.lfReference = _audio.Reverb.LFReference;
            _reverb.diffusion = _audio.Reverb.Diffusion;
            _reverb.density = _audio.Reverb.Density;

            if (_room != null)
            {
                _lowPass = gameObject.AddComponent<AudioLowPassFilter>();
                _lowPass.cutoffFrequency = 20000;

                _room.PlayerInRoom += SetPlayer;
                _room.PlayerConnectedToRoom += SetConnected;
            }

            if (_vfx != null)
            {
                _vfxIntensity = _vfx.GetFloat("Intensity");
            }

        }

        public override void Play()
        {
            base.Play();
            if (_vfx != null)
                _vfx.Play();
        }

        public override void Stop()
        {
            base.Stop();
            if (_vfx != null)
                _vfx.Stop();
        }

        private void Update()
        {
            float distance = (_playerPosition.Value - transform.position).magnitude;

            _reverb.dryLevel = distance * distance * -_dryLevelDistanceModifier.Value;
        }

        private void SetPlayer(bool value)
        {
            _roomPlayer = value;
            UpdateLowPass();
        }

        private void SetConnected(bool value)
        {
            _roomConnected = value;
            UpdateLowPass();
        }

        private void UpdateLowPass()
        {
            if (_roomConnected && _roomPlayer)
            {
                _lowPass.cutoffFrequency = 20000;
                if (_vfx != null)
                    _vfx.SetFloat("Intensity", _vfxIntensity);
            }
            else if (_roomConnected)
            {
                _lowPass.cutoffFrequency = _lowPassNoPlayer.Value;
                if (_vfx != null)
                    _vfx.SetFloat("Intensity", _vfxIntensity * 0.75f);
            }
            else
            {
                _lowPass.cutoffFrequency = _lowPassNotConnected.Value;
                if (_vfx != null)
                    _vfx.SetFloat("Intensity", _vfxIntensity * 0.25f);
            }
        }
    }
}
