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

        /// <summary>
        /// Used to get a random float value between the given range.
        /// </summary>
        /// <param name="range"> Vector 2 with the range. X min, Y Max</param>
        /// <returns> Retuns a random float between X and Y values</returns>
        public static float GetRandomFloatByRange(Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }
    }
}
