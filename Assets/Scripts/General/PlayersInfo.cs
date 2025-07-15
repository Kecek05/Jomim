

using UnityEngine;

namespace KeceK.General
{
    public enum PlayerType
    {
        None,
        Player1,
        Player2,
    }
    
    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Fall,
    }
    
    public static class PlayersInfo
    {
        //Layers
        private static readonly int _player1LayerMaskIndex = 7;
        private static readonly int _player2LayerMaskIndex = 8;
        public static int Player1LayerMaskIndex => _player1LayerMaskIndex;
        public static int Player2LayerMaskIndex => _player2LayerMaskIndex;
        
        //Colors
        private static readonly Color _player1Color = new Color32(106, 255, 255, 255);
        private static readonly Color _player2Color = new Color32(255, 0, 243, 255);
        public static Color Player1Color => _player1Color;
        public static Color Player2Color => _player2Color;
    }
}