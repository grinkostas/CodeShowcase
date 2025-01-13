using UnityEngine;

namespace Core.Utilities
{
    public abstract class ValueProvider : MonoBehaviour
    {
        public abstract object valueRaw { get; }
    }
    
    public abstract class ValueProvider<T> : ValueProvider
    {
        public abstract T value { get; }
        
        public override object valueRaw => value;
    }
    
    public abstract class IntValueProvider : ValueProvider<int>
    {
        public static implicit operator int(IntValueProvider valueProvider) => valueProvider != null ? valueProvider.value : 0;
    }
    
    public abstract class FloatValueProvider : ValueProvider<float>
    {
        public static implicit operator float(FloatValueProvider valueProvider) => valueProvider != null ? valueProvider.value : 0;
    }
    
    public abstract class StringValueProvider : ValueProvider<string>
    {
        public static implicit operator string(StringValueProvider valueProvider) => valueProvider != null ? valueProvider.value : null;
    }
}