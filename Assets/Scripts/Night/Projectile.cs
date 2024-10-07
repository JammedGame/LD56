using System;
using UnityEngine;

namespace Night
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private AnimationCurve flightCurve;

        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Action onReachTarget;
        private float duration;

        private float progress;

        public void Initialize(Vector3 startPosition, Vector3 targetPosition, Action onReachTarget)
        {
            this.startPosition = startPosition;
            this.targetPosition = targetPosition;
            this.onReachTarget = onReachTarget;
            duration = flightCurve[flightCurve.length - 1].time;
        }

        private void Update()
        {
            progress += Time.deltaTime;
            if (progress >= duration)
            {
                onReachTarget?.Invoke();
                Destroy(gameObject);
                return;
            }

            var newPosition = Vector3.Lerp(startPosition, targetPosition, progress / duration);
            var newY = newPosition.y + flightCurve.Evaluate(progress);
            transform.position = new Vector3(newPosition.x, newY, newPosition.z);
        }
    }
}