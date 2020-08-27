using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.Core
{
    public class PropertyMap : IDictionary<string, string>
    {
        private readonly Dictionary<string, string> _properties = new Dictionary<string, string>();

        public string this[string key] { get => _properties[key]; set => _properties[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, string>)_properties).Keys;

        public ICollection<string> Values => ((IDictionary<string, string>)_properties).Values;

        public int Count => _properties.Count;

        public bool IsReadOnly => ((IDictionary<string, string>)_properties).IsReadOnly;

        public void Add(string key, string value)
        {
            _properties.Add(key, value);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            ((IDictionary<string, string>)_properties).Add(item);
        }

        public void Clear()
        {
            _properties.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)_properties).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _properties.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((IDictionary<string, string>)_properties).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IDictionary<string, string>)_properties).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _properties.Remove(key);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)_properties).Remove(item);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _properties.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, string>)_properties).GetEnumerator();
        }
        
        public int GetInt(string key)
        {
            return Convert.ToInt32(_properties[key]);
        }

        public bool GetBoolean(string key)
        {
            return Convert.ToBoolean(_properties[key]);
        }

        public double GetDouble(string key)
        {
            return Convert.ToDouble(_properties[key]);
        }

        public float GetFloat(string key)
        {
            return Convert.ToSingle(_properties[key]);
        }

    }
}
