namespace Core.Materials
{
    public enum ShaderType
    {
        Standard, 
        Urp 
    }
    
    public static class ShadersExtensions
    {
        public static string GetAlbedoParam(this ShaderType shaderType)
        {
            switch (shaderType)
            {
                case ShaderType.Urp:
                    return "_BaseColor";
                case ShaderType.Standard:
                    return "_AlbedoColor";
                default:
                    return "_Color";
            }
        }
        
        public static string GetEmissionParam(this ShaderType shaderType)
        {
            switch (shaderType)
            {
                case ShaderType.Urp:
                case ShaderType.Standard:
                default:
                    return "_EmissionColor";
            }
        }
        
    }
}