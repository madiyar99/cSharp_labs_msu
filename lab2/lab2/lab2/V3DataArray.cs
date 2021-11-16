using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Globalization;

namespace lab2
{
    [Serializable]
    public class V3DataArray : V3Data
    {
        public int nodes_count_x { get; private set;  }
        public int nodes_count_y { get; private set; }
        public double step_x { get; private set; }
        public double step_y { get; private set; }


        public Vector2[,] array_nodes { get; private set; }


        public V3DataArray(string str, DateTime date) : base(str, date)
        {
            array_nodes = new Vector2[0, 0];
        }

        public V3DataArray(string str, DateTime date, int nodes_count_x, int nodes_count_y,
            double step_x, double step_y, FdblVector2 F) : base(str, date)
        {
            this.nodes_count_x = nodes_count_x;
            this.nodes_count_y = nodes_count_y;
            this.step_x = step_x;
            this.step_y = step_y;
            array_nodes = new Vector2[nodes_count_x, nodes_count_y];

            for(int i = 0; i < nodes_count_x; i++)
            {
                for(int j = 0; j < nodes_count_y; j++)
                {
                    array_nodes[i, j] = F(i * step_x, j * step_y);
                }
            }
        }

        public override int Count
        {
            get
            {
                return nodes_count_x * nodes_count_y;
            }
        }

        public override double MaxDistance
        {
            get
            {
                if(Count == 0)
                {
                    return 0;
                }
                double max = Math.Sqrt(Math.Pow((nodes_count_x - 1) * step_x, 2) + Math.Pow((nodes_count_y - 1) * step_y, 2));
                return max;
            }
        }

        public override string ToString()
        {
            return String.Format("Type: {0}, Base_data: {1}, nodes_count_x: {2}, nodes_count_y: {3}, " +
                "step_x: {4}, step_y: {5}", GetType().ToString(), base.ToString(), nodes_count_x.ToString(),
                nodes_count_y.ToString(), step_x.ToString(), step_y.ToString());
        }

        public override string ToLongString(string format)
        {
            String result = String.Format("Type: {0}, Base_data: {1}, nodes_count_x: {2}, nodes_count_y: {3}, " +
                "step_x: {4}, step_y: {5}", GetType().ToString(), base.ToString(), nodes_count_x.ToString(),
                nodes_count_y.ToString(), step_x.ToString(), step_y.ToString());

            for(int i = 0; i < nodes_count_x; i++)
            {
                for(int j = 0; j < nodes_count_y; j++)
                {
                    result += "\n";
                    result += String.Format("x: {0}, y: {1}, comp_value: {2}, comp_length: {3}", (i * step_x).ToString(format),
                        (j * step_y).ToString(format), array_nodes[i, j].ToString(format), array_nodes[i, j].Length().ToString(format));
                }
            }
            return result;
        }

        public static explicit operator V3DataList(V3DataArray arr)
        {
            V3DataList DataList_item = new V3DataList(arr.str, arr.date);
            for (int i = 0; i < arr.nodes_count_x; i++)
            {
                for (int j = 0; j < arr.nodes_count_y; j++)
                {
                    DataItem item = new DataItem(i * arr.step_x, j * arr.step_y, arr.array_nodes[i, j]);
                    DataList_item.Add(item);
                }
            }
            return DataList_item;
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            V3DataList ee = (V3DataList)this;
            return ee.list_data.GetEnumerator();
        }

        public bool SaveBinary(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(filename, FileMode.OpenOrCreate);
                BinaryWriter writer = new BinaryWriter(fs);
                writer.Write(str);
                writer.Write(date.ToString());
                writer.Write(Count);
                writer.Write(MaxDistance);
                writer.Write(nodes_count_x);
                writer.Write(nodes_count_y);
                writer.Write(step_x);
                writer.Write(step_y);
                for (int i = 0; i < nodes_count_x; i++)
                {
                    for (int j = 0; j < nodes_count_y; j++)
                    {
                        writer.Write(array_nodes[i, j].X.ToString());
                        writer.Write(array_nodes[i, j].Y.ToString());
                    }
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in binary saving: " + e.Message);
                return false;
            }
            finally
            {
                if(fs != null)
                {
                    fs.Close();
                }
            }
            return true;
        }

        public bool LoadBinary(string filename, ref V3DataArray v3)
        {
            FileStream fs = null;
            try
            {
                if (!File.Exists(filename))
                {
                    return false;
                }
                fs = File.Open(filename, FileMode.Open);
                BinaryReader read = new BinaryReader(fs);

                string str = read.ReadString();
                DateTime date = DateTime.ParseExact(read.ReadString(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                int Count = read.ReadInt32();
                double MaxDistance = read.ReadDouble();
                int nodes_count_x = read.ReadInt32();
                int nodes_count_y = read.ReadInt32();
                double step_x = read.ReadDouble();
                double step_y = read.ReadDouble();
                Vector2[,] array_nodes = new Vector2[nodes_count_x, nodes_count_y];

                for (int i = 0; i < nodes_count_x; i++)
                {
                    for (int j = 0; j < nodes_count_y; j++)
                    {
                        float x = float.Parse(read.ReadString());
                        float y = float.Parse(read.ReadString());
                        Vector2 vec2 = new Vector2(x, y);
                        array_nodes[i, j] = vec2;
                    }
                }

                //v3 = new V3DataArray(str, date);
                v3.str = str;
                v3.date = date;
                v3.nodes_count_x = nodes_count_x;
                v3.nodes_count_y = nodes_count_y;
                v3.step_x = step_x;
                v3.step_y = step_y;
                v3.array_nodes = array_nodes;
                read.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in binary loading: " + e.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return true;
        }

    }
}
