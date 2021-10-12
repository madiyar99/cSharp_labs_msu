using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace lab1
{
    public class V3DataArray : V3Data
    {
        public int nodes_count_x { get; }
        public int nodes_count_y { get; }
        public double step_x { get; }
        public double step_y { get; }

        public Vector2[,] array_nodes { get; } 

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
    }
}
