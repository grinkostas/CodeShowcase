using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Utilities
{
    public class InjectStarter : MonoBehaviour
    {
        [SerializeField] private int _awakeWaitFrames;
        [SerializeField] private int _startWaitFrames;
        [SerializeField] private int _completeWaitFrames;
        
        public enum Stage
        {
            None = 0, 
            Awake = 2, 
            Start = 3, 
            Complete = 4
        }

        private Stage _stage = Stage.None;
        
        private List<InjectableMono> _injectables = new();
        private List<InjectableMono> _awakenInjectables = new();
        private List<InjectableMono> _startedInjectables = new();

        [Inject]
        private void Inject()
        {
            StartCoroutine(Initialize());
        }
        
        public void Add(InjectableMono mono)
        {
            switch (_stage)
            {
                case Stage.None:
                    _injectables.Add(mono);
                    return;
                case Stage.Awake:
                    mono.InjectAwake();
                    _awakenInjectables.Add(mono);
                    return;
                case Stage.Start:
                    mono.InjectAwake();
                    mono.InjectStart();
                    _startedInjectables.Add(mono);
                    return;
                case Stage.Complete:
                    mono.InjectAwake();
                    mono.InjectStart();
                    mono.InjectComplete();
                    return;
            }
        }
        
        private IEnumerator Initialize()
        {
            yield return WaitFrames(_awakeWaitFrames);
            _stage = Stage.Awake;
            OnAwake();

            yield return WaitFrames(_startWaitFrames);
            _stage = Stage.Start;
            OnStart();
            
            yield return WaitFrames(_completeWaitFrames);
            _stage = Stage.Complete;
            OnComplete();
        }

        private void OnAwake()
        {
            _injectables.ForEach(x=>x.InjectAwake());
        }

        private void OnStart()
        {
            _injectables.ForEach(x=>x.InjectStart());
            _awakenInjectables.ForEach(x=>x.InjectStart());
        }

        private void OnComplete()
        {
            _injectables.ForEach(x=>x.InjectComplete());
            _awakenInjectables.ForEach(x=>x.InjectComplete());
            _startedInjectables.ForEach(x=>x.InjectComplete());
        }

        private IEnumerator WaitFrames(int framesCount)
        {
            for (int i = 0; i < framesCount; i++)
                yield return new WaitForEndOfFrame();
        }
    }
}