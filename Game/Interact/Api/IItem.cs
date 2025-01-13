using Game.Interact.Configs;
using UnityEngine;

namespace Game.Interact.Api
{
    public interface IItem
    {
        public InteractItemConfig config { get; }
        public GameObject itemGO { get; }
    }
}