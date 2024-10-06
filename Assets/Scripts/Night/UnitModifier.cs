using System;
using UnityEngine;

namespace Night
{
    public class UnitModifier
    {
        private readonly float startTime;
        private readonly float duration;
        private readonly Action onEnd;
        
        public float MoveSpeedMod { get; private set; }
        public Color? ColorTint { get; private set; }
        
        public UnitModifier(float duration, float moveSpeedMod = 1f, Color? colorTint = null)
        {
            startTime = Time.time;
            this.duration = duration;
            MoveSpeedMod = moveSpeedMod;
            ColorTint = colorTint;
        }

        public bool ShouldRemove()
        {
            return Time.time - startTime > duration;
        }
    }
}