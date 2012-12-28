using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.BusinessLogic.Common
{
    [Serializable()]
    public class NameObjectCollection : System.Collections.Specialized.NameObjectCollectionBase
    {
        public NameObjectCollection()
        {
        }
        public void Add(string tsName, object toValue)
        {
            this.BaseAdd(tsName, toValue);
        }
        public void Clear()
        {
            this.BaseClear();
        }
        public void Remove(string tsName)
        {
            this.BaseRemove(tsName);
        }
        public object this[int tnIndex]
        {
            get
            {
                return this.BaseGet(tnIndex);
            }
        }
        public object this[string tsName]
        {
            get
            {
                return this.BaseGet(tsName);
            }
            set
            {
                this.BaseSet(tsName, value);
            }
        }


    }
}

