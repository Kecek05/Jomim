using KeceK.General;
using UnityEngine;

namespace KeceK.Input
{
    public class InputEnabler : MonoBehaviour
    {
        [SerializeField] private PlayerType _thisPlayerType;
        [SerializeField] private InputReader _inputReader;
        
        private void Start()
        {
            _inputReader.SetPlayerType(_thisPlayerType);
        }
    }
}
