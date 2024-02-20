using System;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class ChromaticNoise
    {
        public int Hash;
        public bool Continuous;
        public float BaseValue;
        public float DecayValue;
        public Vector3 Position;

        public ChromaticNoise(bool continuous, float baseValue, float decayValue, Vector3 position)
        {
            Hash = Guid.NewGuid().GetHashCode();
            Continuous = continuous;
            BaseValue = baseValue;
            DecayValue = decayValue;
            Position = position;
        }

        public ChromaticNoise(ChromaticNoise noise)
        {
            Hash = noise.Hash;
            Continuous = noise.Continuous;
            BaseValue = noise.BaseValue;
            DecayValue = noise.DecayValue;
            Position = noise.Position;
        }
    }
}
