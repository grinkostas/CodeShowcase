using UnityEngine;

namespace Core.Utilities.Enums
{
    public static class MiscExtensions
    {
        public static T GetComponent<T>(this GameObject obj, HierarchyLocation location) where T : Component
        {
            switch (location)
            {
                case HierarchyLocation.Parent:
                    return obj.GetComponentInParent<T>();
                case HierarchyLocation.Child:
                    return obj.GetComponentInChildren<T>();
                case HierarchyLocation.ParentOfParent:
                    return obj.transform.parent.GetComponentInParent<T>();
               default:
                    return obj.GetComponent<T>();
            }
        }
    }
}