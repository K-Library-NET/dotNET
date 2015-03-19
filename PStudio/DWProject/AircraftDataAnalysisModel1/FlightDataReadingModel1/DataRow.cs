using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class DataRow : IDictionary<string, object>
    {
        private readonly DataTable owner;
        private IDictionary<string, object> m_valueMapRef = null;
        private ExpandoObject m_rowObject;

        public DataRow(DataTable owner)
        {
            this.owner = owner;
            this.EnsureRowObject();
        }

        public object this[string columnName]
        {
            get
            {
                if (this.RowObject.ContainsKey(columnName))
                {
                    return this.RowObject[columnName];
                }
                return 0;
            }
            set
            {
                this.RowObject[columnName] = value;
            }
        }

        internal IDictionary<string, object> RowObject
        {
            get
            {
                this.EnsureRowObject();
                return this.m_valueMapRef;
            }
        }

        private void EnsureRowObject()
        {
            if (this.m_rowObject == null)
            {
                this.m_rowObject = new ExpandoObject();
                m_valueMapRef = this.m_rowObject as IDictionary<string, object>;
            }
        }

        public void Add(string key, object value)
        {
            this.m_valueMapRef.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return m_valueMapRef.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return m_valueMapRef.Keys; }
        }

        public bool Remove(string key)
        {
            return m_valueMapRef.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return m_valueMapRef.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return m_valueMapRef.Values; }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.m_valueMapRef.Add(item);
        }

        public void Clear()
        {
            m_valueMapRef.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return m_valueMapRef.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            m_valueMapRef.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_valueMapRef.Count; }
        }

        public bool IsReadOnly
        {
            get { return m_valueMapRef.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return m_valueMapRef.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return m_valueMapRef.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_valueMapRef.GetEnumerator();
        }
    }
}
