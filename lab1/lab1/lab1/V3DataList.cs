using System;
using System.Collections;
using System.Collections.Generic;

namespace lab1
{    

    public class V3DataList : V3Data
    {
        public List<DataItem> list_data { get; }

        public V3DataList(string str, DateTime date) : base(str, date)
        {
            list_data = new List<DataItem>();
        }

        public bool Add(DataItem newItem)
        {
            bool check = false;
            foreach (DataItem item in list_data)
            {
                if (item.x == newItem.x && item.y == newItem.y)
                {
                    check = true;
                    break;
                }
            }
            if (check)
            {
                return false;
            }
            else
            {
                list_data.Add(newItem);
                return true;
            }
            
        }

        public int AddDefaults(int nItmes, FdblVector2 F)
        {
            double x_new = 1.0;
            double y_new = 1.0;
            int item_count = 0;

            for (int i = 0; i < nItmes; i++)  //Так как не опредлен точный алгоритм подбора точек нужно использовать, то
                                             //я просто выбираю точки подряд с шагом 2.0. 
            {
                DataItem item = new DataItem(x_new, y_new, F(x_new, y_new));
                if (Add(item))
                {
                    item_count++;
                }
                x_new += 2;
                y_new += 2;
            }
            return item_count;
        }

        public override int Count
        {
            get
            {
                return list_data.Count;
            }
        }

        public override double MaxDistance
        {
            get
            {
                double max = 0;
                foreach (DataItem item1 in list_data)
                {
                    foreach(DataItem item2 in list_data)
                    {
                        double res = Math.Sqrt(Math.Pow(item1.x - item2.x, 2) + Math.Pow(item1.y - item2.y, 2));
                        if(res > max)
                        {
                            max = res;
                        }
                    }
                }
                return max;
            }
        }

        public override string ToString()
        {
            return String.Format("Type: {0}, Base_data: {1}, Count: {2}", GetType().ToString(), base.ToString(), Count.ToString());
        }

        public override string ToLongString(string format)
        {
            String result = String.Format("Type: {0}, Base_data: {1}, Count: {2}", GetType().ToString(), base.ToString(), Count.ToString());
            for (int i = 0; i < Count; i++)
            {
                result += "\n";
                result += list_data[i].ToLongString(format);
            }
            return result;
        }

    }
}
