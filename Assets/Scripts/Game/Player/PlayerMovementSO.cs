using UnityEngine;

namespace KeceK.Game
{
    [CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "Scriptable Objects/PlayerMovementSO")]
    public class PlayerMovementSO : ScriptableObject
    {
        public float speed;
        public float maxSpeed;
        public float jumpForce;
    }
}
