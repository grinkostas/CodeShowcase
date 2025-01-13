using System.Collections.Generic;
using System.Linq;
using Game.Characters;
using Game.Interact.Api;
using JetBrains.Annotations;
using Core.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Interact
{
    public class InteractModule : MonoBehaviour
    {
        [Inject, UsedImplicitly] public Player player { get; }
        private Camera _camera;
        public Camera cam => _camera ??= Camera.main;
        
        private List<IPointerListener> _listeners = new();
        private List<IPointerInteractItem> _interactables = new();

        public Blocker interactBlocker { get; } = new();

        private IInteractor _interactor;
        
        public void SetInteractor(IInteractor interactor) => _interactor = interactor;
        
        public void Interact()
        {
            if(interactBlocker.isBlocked)
                return;
            
            foreach (var interactable in _interactables)
            {
                interactable.Interact(player);
            }
        }

        private void OnEnable()
        {
            interactBlocker.onBlock.On(OnBlock);
        }

        private void OnDisable()
        {
            interactBlocker.onBlock.Off(OnBlock);
        }

        private void OnBlock()
        {
            foreach (var listener in _listeners)
            {
                listener.OnPointerExit();
            }
            _listeners.Clear();
            _interactables.Clear();
        }

        private void Update()
        {
            if(interactBlocker.isBlocked)
                return;
            RaycastHit[] results = new RaycastHit[5];
            var hits = GetHits(results);
            if (hits == 0)
            {
                ExitPointers();
                return;
            }

            var newListeners = HandleHits(results, hits, out var alreadyTouched);
            ExitPointers(except:alreadyTouched);
            EnterPointers(newListeners);

            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log(_interactables.Count);
                foreach (var interactable in _interactables)
                {
                    interactable.Interact(_interactor);
                }
            }
        }

        private int GetHits(RaycastHit[] results)
        {
            return Physics.RaycastNonAlloc(cam.transform.position, cam.transform.forward, results, 100f);
        }
        

        private List<IPointerListener> HandleHits(RaycastHit[] results, int hits, out List<IPointerListener> alreadyTouched)
        {
            alreadyTouched = new();
            List<IPointerListener> newTouched = new();
            List<IPointerInteractItem> interactables = new();
            for (int i = 0; i < hits; i++)
            {
                if(results[i].collider.TryGetComponent(out IPointerInteractItem interactable))
                    interactables.Add(interactable);
                    
                if(results[i].collider.TryGetComponent(out IPointerListener listener) == false)
                    continue;

                if(listener.CanInteract(_interactor) == false)
                    continue;
                if (_listeners.Contains(listener))
                {
                    alreadyTouched.Add(listener);
                    continue;
                }
                newTouched.Add(listener);
            }

            _interactables = interactables;
            return newTouched;
        }


        private void EnterPointers(IEnumerable<IPointerListener> listeners)
        {
            foreach (var listener in listeners)
            {
                listener.OnPointerEnter();
                _listeners.Add(listener);
            }
        }
        
        private void ExitPointers(IEnumerable<IPointerListener> except = null)
        {
            if (except == null)
            {
                foreach (var listener in _listeners)
                    listener?.OnPointerExit();
                _listeners.Clear();
                return;
            }

            var listenersToExit = new List<IPointerListener>(_listeners.Except(except));
            foreach (var listener in listenersToExit)
            {
                listener.OnPointerExit();
                _listeners.Remove(listener);
            }
        }
    }
}