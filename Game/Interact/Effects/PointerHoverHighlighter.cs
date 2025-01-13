using System.Collections.Generic;
using Game.Interact.Api;
using Core.Materials;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Interact
{
    public class PointerHoverHighlighter : MonoBehaviour
    {
        [SerializeField] private ShaderType _shaderType;
        [SerializeField] private bool _changeAlbedo;
        
        [SerializeField, ColorUsage(true), ShowIf(nameof(_changeAlbedo))] 
        private Color _highlightColor;
        
        [SerializeField] private bool _changeEmission;
        [SerializeField, ColorUsage(true, true), ShowIf(nameof(_changeEmission))] 
        private Color _emissionColor;

        private IPointerListener _pointerListener;
        public IPointerListener listener => _pointerListener ??= GetComponent<IPointerListener>();

        private List<Renderer> _renderer;
        public List<Renderer> renderers
        {
            get
            {
                if (_renderer != null)
                    return _renderer;
                _renderer = new();
                _renderer.AddRange(GetComponentsInChildren<MeshRenderer>());
                _renderer.AddRange(GetComponentsInChildren<SkinnedMeshRenderer>());
                return _renderer;
            }
        }

        private List<MaterialFadeModel> _materialModels = new();
        
        private void Awake()
        {
            foreach (var rend in renderers)
            {
                foreach (var material in rend.materials)
                {
                    _materialModels.Add(new MaterialFadeModel(material, _shaderType));
                }
            }
        }

        private void OnEnable()
        {
            listener.onPointerEnter.On(PointerEnter);
            listener.onPointerExit.On(PointerExit);
        }

        private void OnDisable()
        {
            listener.onPointerEnter.Off(PointerEnter);
            listener.onPointerExit.Off(PointerExit);
        }

        public void PointerEnter()
        {
            foreach (var materialModel in _materialModels)
            {
                if(_changeAlbedo)
                    materialModel.FadeColor(_highlightColor);
                if(_changeEmission) 
                    materialModel.FadeEmission(_emissionColor);
            }
        }

        public void PointerExit()
        {
            foreach (var materialModel in _materialModels)
            {
                if(_changeAlbedo)
                    materialModel.FadeToDefaultColor();
                if(_changeEmission) 
                    materialModel.FadeToDefaultEmission();
            }
        }
        
    }
}