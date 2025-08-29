using UnityEngine;

namespace KeceK.General
{
    
    public enum InteractorColor
    {
        White,
        Red,
        Blue,
        Purple,
        Orange,
        Cyan,
        Black,
    }
    
    public static class GeneralInfo
    {
        public static Color GetColorByInteractorColor(InteractorColor color)
        {
            return color switch
            {
                InteractorColor.White => Color.white,
                InteractorColor.Red => Color.red,
                InteractorColor.Blue => Color.blue,
                InteractorColor.Purple => new Color(0.5f, 0f, 0.5f),
                InteractorColor.Orange => new Color(1f, 0.65f, 0f),
                InteractorColor.Black => Color.black,
                InteractorColor.Cyan => Color.cyan,
                _ => Color.white
            };
        }
    }
}
