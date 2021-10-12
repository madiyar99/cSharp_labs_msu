using System;
namespace lab1
{
    public abstract class V3Data
    {
        public string str { get; }
        public DateTime date { get; }
        public abstract int Count { get; }
        public abstract double MaxDistance { get; }

        public V3Data(string str, DateTime date)
        {
            this.str = str;
            this.date = date;
        }

        public abstract string ToLongString(string format);

        public override String ToString()
        {
            return String.Format("str: {0}, Count: {1}, Max distance: {2}, Date: {3}", str, Count.ToString(), MaxDistance.ToString(), date.ToString());
        }
    }
}
