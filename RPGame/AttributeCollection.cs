using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame
{
    public class AttributeCollection
    {
        Dictionary<string, object> attributes = new Dictionary<string, object>();

        public void New<T>(string name)
        {
            New<T>(name, default(T));
        }

        public void New<T>(string name, T value)
        {
            string nameToUpper = name.ToUpper();

            object currentValue;
            if (!this.attributes.TryGetValue(nameToUpper, out currentValue))
            {
                this.attributes.Add(nameToUpper, value);
            }
            else
            {
                if (currentValue.GetType() != typeof(T))
                {
                    throw new InvalidOperationException(string.Format("Attribut {0} ist vom Typ {1} und nicht {2}, du Depp!", nameToUpper, currentValue.GetType(), typeof(T)));
                }
            }
        }

        public T Get<T>(string name)
        {
            object value;
            if (this.attributes.TryGetValue(name.ToUpper(), out value))
            {
                return (T)value;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Attribut {0} existiert nicht", name.ToUpper()));
            }
        }

        public void Set<T>(string name, T value)
        {
            string nameToUpper = name.ToUpper();
            if (this.attributes.ContainsKey(nameToUpper))
            {
                this.attributes[nameToUpper] = value;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Attribut {0} existiert nicht", nameToUpper));
            }
        }
    }
}
