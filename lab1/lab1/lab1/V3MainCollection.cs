using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace lab1
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
    }
}
