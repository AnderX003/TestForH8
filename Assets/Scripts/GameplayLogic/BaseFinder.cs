using System.Collections.Generic;

namespace GameplayLogic
{
    public class BaseFinder<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> dictionary = new();

        public void Register(TValue value, TKey key)
        {
            dictionary.Add(key, value);
        }

        public void Unregister(TValue value, TKey key)
        {
            dictionary.Remove(key);
        }

        protected bool GetByKey(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }
    }
}
