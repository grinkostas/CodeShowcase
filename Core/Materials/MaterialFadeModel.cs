using DG.Tweening;
using UnityEngine;

namespace Core.Materials
{
    public class MaterialFadeModel : MaterialModel
    {
        private float _fadeDuration = 0.25f;

        private Tween _colorTween;
        private Tween _emissionTween;
        
        public MaterialFadeModel(Material material, ShaderType shaderType) : base(material, shaderType)
        {
        }

        public void FadeToDefaultColor() => FadeColor(startColor);
        public void FadeColor(Color targetColor)
        {
            _colorTween?.Kill();
            _colorTween = DOVirtual.Color(GetColor(), targetColor, _fadeDuration, color =>
            {
                material.SetColor(shaderType.GetAlbedoParam(), color);
            }).SetUpdate(false);
        }

        public void FadeToDefaultEmission() => FadeEmission(startEmission);
        public void FadeEmission(Color targetColor)
        {
            _emissionTween.Kill();
            _emissionTween = DOVirtual.Color(GetEmission(), targetColor, _fadeDuration, color =>
            {
                material.SetColor(shaderType.GetEmissionParam(), color);
            }).SetUpdate(false);
        }
    }
}