using UnityEngine;

namespace Core.Materials
{
    public class MaterialModel
    {
        public Material material { get; }
        public ShaderType shaderType { get; }
        
        public Color startColor { get; }
        public Color startEmission { get; }
        
        
        public MaterialModel(Material material, ShaderType shaderType)
        {
            this.material = material;
            this.shaderType = shaderType;
            startColor = GetColor();
            startEmission = GetEmission();
        }
        
        public Color GetColor() => material.GetColor(shaderType.GetAlbedoParam());
        public Color GetEmission() => material.GetColor(shaderType.GetEmissionParam());
        
    }
}