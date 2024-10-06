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
        
        public UnitModifier(float duration, float moveSpeedMod = 1f, Action onEnd = null)
        {
            startTime = Time.time;
            this.duration = duration;
            MoveSpeedMod = moveSpeedMod;
            this.onEnd = onEnd;
        }

        public bool ShouldRemove()
        {
            bool shouldRemove = Time.time - startTime > duration;
            if (shouldRemove)
            {
                onEnd?.Invoke();    
            }
            
            return shouldRemove;
        }
    }
}