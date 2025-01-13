using Game.Interact.Api;
using Game.Interact.Items;
using UnityEngine;

namespace Game.Characters.Modules
{
    public class ThrowItemsContainer : MonoBehaviour
    {
        [SerializeField] private GrenadeItem _grenadeItemTemplate;

        public IThrowItem GetThrowItem()
        {
            return Instantiate(_grenadeItemTemplate, _grenadeItemTemplate.transform.position,
                _grenadeItemTemplate.transform.rotation, _grenadeItemTemplate.transform.parent);
        }
    }
}