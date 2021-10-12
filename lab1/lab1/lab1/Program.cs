using System;
using System.Numerics;
using System.Collections.Generic;

namespace lab1
{
    public delegate Vector2 FdblVector2(double x, double y);


    class Program
    {
        static void Main(string[] args)
        {
            V3DataArray data_arr1 = new V3DataArray("data_arr1", DateTime.Now, 10, 15, 3.5, 7.5, CalculateMethodsComp.calculate_func1);
            Console.WriteLine(data_arr1.ToLongString("F3"));
            V3DataList data_list1 = (V3DataList)data_arr1;
            Console.WriteLine(data_list1.ToLongString("F4"));
            Console.WriteLine(String.Format("Count_arr: {0} MaxDistance_arr: {1}", data_arr1.Count, data_arr1.MaxDistance));
            Console.WriteLine(String.Format("Count_list: {0} MaxDistance_list: {1}", data_list1.Count, data_list1.MaxDistance));

            V3MainCollection main_collection1 = new V3MainCollection();
            V3DataArray data_arr2 = new V3DataArray("data_arr2", DateTime.Now, 5, 10, 2.5, 6.0, CalculateMethodsComp.calculate_func1);
            V3DataList data_list2 = (V3DataList)data_arr2;
            main_collection1.Add(data_arr1);
            main_collection1.Add(data_arr2);
            main_collection1.Add(data_list1);
            main_collection1.Add(data_list2);
            main_collection1.ToLongString("F4");

            for (int i = 0; i < main_collection1.Count; i++)
            {
                Console.WriteLine(String.Format("{0} - Count: {1}, MaxDistance: {2}\n", i, main_collection1[i].Count, main_collection1[i].MaxDistance));
            }
        }
    }
}
