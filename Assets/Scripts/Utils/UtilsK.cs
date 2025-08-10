
namespace KeceK.Utils
{
    public static class UtilsK
    {
        /// <summary>
        /// All shader properties used in the game.
        /// </summary>
        public enum ShaderProperty
        {
            _ShineLocation,
            _FadeAmount,
            _HologramBlend,
        }

        public static string GetShaderProperty(ShaderProperty shaderProperty) => shaderProperty.ToString();
    }
}
