using UnityEngine;

namespace KeceK.General
{
    public static class MathK
    {
        //Velocity threshold
        private static readonly float _velocityThreshold = 0.01f;
        
        public static float VelocityThreshold => _velocityThreshold;

        public static bool IsVelocityAboveThreshold(float velocity)
        {
            return Mathf.Abs(velocity) > _velocityThreshold;
        }
    }
}
