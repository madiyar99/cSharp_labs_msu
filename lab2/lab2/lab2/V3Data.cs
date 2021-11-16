using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace lab2
{
    [Serializable]
    public abstract class V3Data : IEnumerable<DataItem>
    {
        public string str { get; protected set; }
        public DateTime date { get; protected set;  }
        public abstract int Count { get; }
        public abstract double MaxDistance { get; }
 

        public V3Data(string str, DateTime date)
        {
            this.str = str;
            this.date = date;
        }

        public abstract string ToLongString(string format);
        public abstract IEnumerator<DataItem> GetEnumerator();

        public override String ToString()
        {
            return String.Format("str: {0}, Count: {1}, Max distance: {2}, Date: {3}", str, Count.ToString(), MaxDistance.ToString(), date.ToString());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
