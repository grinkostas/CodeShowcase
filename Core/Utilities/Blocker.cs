using System.Collections.Generic;
using Core.Signals;

namespace Core.Utilities
{
    public class Blocker
    {
        private List<object> _blockObjects { get; }

        public bool isBlocked => _blockObjects.Count > 0;
        public int blockersCount => _blockObjects.Count;

        public Signal onBlock { get; } = new();
        public Signal onUnblock { get; } = new();
        
        public Blocker()
        {
            _blockObjects = new();
        }

        public void Add(object sender)
        {
            if(_blockObjects.Contains(sender))
                return;
            _blockObjects.Add(sender);
            if(blockersCount == 1)
                onBlock.Dispatch();
        }

        public void Remove(object sender)
        {
            if(_blockObjects.Remove(sender))
                onUnblock.Dispatch();
        }

        public void ClearAll()
        {
            _blockObjects.Clear();
            onUnblock.Dispatch();
        }
    }
}