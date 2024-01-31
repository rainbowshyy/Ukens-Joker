using UnityEngine;
using UnityEngine.VFX;
using UkensJoker.DataArchitecture;

namespace UkensJoker.Audio
{
    public class SpatialAudioPlayer : AudioPlayer
    {
        [SerializeField] private Vector3Reference _playerPosition;
        [SerializeField] private FloatReference _dryLevelDistanceModifier;

        [SerializeField] private VisualEffect _vfx;

        private AudioReverbFilter _reverb;

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
    }
}
