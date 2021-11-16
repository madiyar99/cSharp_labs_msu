using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace lab2
{
    public class V3MainCollection
    {
        private List<V3Data> list_v3data;
        private int count;
        public int Count
        {
            get
            {
                return count;
            }
        }

        public V3MainCollection()
        {
            count = 0;
            list_v3data = new List<V3Data>();
        }

        public V3Data this[int index]
        {
            get
            {
                return list_v3data[index];
            }
        }

        public bool Contains(String ID)
        {
            bool check = false;

            foreach (V3Data item in list_v3data)
            {
                if(item.str == ID)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        public bool Add(V3Data v3Data)
        {
            if (Contains(v3Data.str) == false)
            {
                list_v3data.Add(v3Data);
                count++;
                return true;
            }
            return false;
        }

        public string ToLongString(String format)
        {
            String result = "";
            for (int i = 0; i < count; i++)
            {
                result += list_v3data[i].ToLongString(format) + "\n";
            }   
            return result;
        }

        public override string ToString()
        {
            String result = "";
            for (int i = 0; i < count; i++)
            {
                result += list_v3data[i].ToString() + "\n";
            }
            return result;
        }


        public double Average_val
        {
            get
            {
                if(Count == 0)
                {
                    return double.NaN;
                }
                var select1 = from t in list_v3data
                             from new_item in t
                             select (Math.Sqrt(new_item.x * new_item.x + new_item.y * new_item.y));

                return select1.Average();                                                      
            }
        }

        public IEnumerable<float> Diference_val
        {
            get
            {
                if (Count == 0)
                    return null;
                var select1 = from new_item in list_v3data
                              where new_item.Count > 0
                              select new_item.Max(comp => comp.component.Length()) - new_item.Min(comp => comp.component.Length());
                return select1;
            }
        }

        public IEnumerable<IGrouping<double, DataItem>> Grouping_val_by_x
        {
            get
            {
                if (Count == 0)
                    return null;
                var select1 = from t in list_v3data
                              from DataItem new_item in t
                              group new_item by new_item.x into group_by_x
                              select group_by_x;
                return select1;
            }
        }
    }
}
