using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab2
{    
    [Serializable]
    public class V3DataList : V3Data, ISerializable
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

        public override IEnumerator<DataItem> GetEnumerator()
        {
            return list_data.GetEnumerator();
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("str", str);
            info.AddValue("date", date);
            info.AddValue("Count", Count);
            info.AddValue("MaxDistance", MaxDistance);
            info.AddValue("data", list_data);
            int i = 0;
            foreach(var item in this)
            {
                info.AddValue($"item_{i}", item, typeof(DataItem));
                info.AddValue($"itemX_{i}", item.component.X, typeof(double));
                info.AddValue($"itemY_{i}", item.component.Y, typeof(double));
                i++;
            }
        }

        public bool SaveAsText(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.Create(filename).Close();
                }
                StreamWriter sw = new StreamWriter(filename, false);
                sw.WriteLine(str);
                sw.WriteLine(date.ToString());
                sw.WriteLine(Count);
                sw.WriteLine(MaxDistance);
                foreach (var elem in this)
                {
                    sw.WriteLine(elem.x);
                    sw.WriteLine(elem.y);
                    sw.WriteLine(elem.component.X.ToString());
                    sw.WriteLine(elem.component.Y.ToString());
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in text saving: " + e.Message);
                return false;
            }
            return true;
        }

        public static bool LoadAsText(string filename, ref V3DataList v3)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    return false;
                }
                StreamReader sr = new StreamReader(filename);
                string str = sr.ReadLine();
                DateTime date = DateTime.ParseExact(sr.ReadLine(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                //v3 = new V3DataList(str, date);
                v3.str = str;
                v3.date = date;

                int count = Int32.Parse(sr.ReadLine());
                sr.ReadLine();

                double x, y;
                float comp_x, comp_y;
                DataItem data_new = new DataItem();
                while (count > 0)
                {
                    try
                    {
                        x = double.Parse(sr.ReadLine());
                    }
                    catch (EndOfStreamException)
                    {
                        return false;
                    }
                    try
                    {
                        y = double.Parse(sr.ReadLine());
                    }
                    catch (EndOfStreamException)
                    {
                        return false;
                    }
                    try
                    {
                        comp_x = float.Parse(sr.ReadLine());
                    }
                    catch (EndOfStreamException)
                    {
                        return false;
                    }
                    try
                    {
                        comp_y = float.Parse(sr.ReadLine());
                    }
                    catch (EndOfStreamException)
                    {
                        return false;
                    }
                    data_new.x = x;
                    data_new.y = y;
                    data_new.component = new Vector2(comp_x, comp_y);
                    v3.Add(data_new);
                    count--;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in text loading: " + e.Message);
                return false;
            }

            return true;
        }
    }
}
