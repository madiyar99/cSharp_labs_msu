using System;
using System.Numerics;
using System.Collections.Generic;

namespace lab2
{
    public delegate Vector2 FdblVector2(double x, double y);


    class Program
    {
        static void Main(string[] args)
        {
            //V3DataArray data_arr1 = new V3DataArray("data_arr1", DateTime.Now, 3, 3, 3.5, 7.5, CalculateMethodsComp.calculate_func1);
            //Console.WriteLine(data_arr1.ToLongString("F3"));
            //V3DataList data_list1 = (V3DataList)data_arr1;
            /*
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
            
                Console.WriteLine(String.Format("{0} - Count: {1}, MaxDistance: {2}\n", i, main_collection1[i].Count, main_collection1[i].MaxDistance));
            }

            foreach(var i in data_list1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in data_arr1)
            {
                Console.WriteLine(i);
            }
            

            Console.WriteLine();
            //V3MainCollection main_collection1 = new V3MainCollection();
            //main_collection1.Add(data_arr1);
            //main_collection1.Add(data_list1);
            //Console.WriteLine(main_collection1.print_aver());

            //data_list1.SaveBinary("saved1.dat", ref data_list1);
            //data_arr1.SaveBinary("saved2.dat", ref data_arr1);
            V3DataArray v_ex = new V3DataArray("objFromBinaryFile", DateTime.Now);        
            v_ex.LoadBinary("saved2.dat", ref v_ex);
            //V3DataList data_list1 = (V3DataList)v_ex;
            //data_list1.SaveAsText("saved_as_text.txt");
            foreach (var i in v_ex)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine(v_ex.date);

            V3DataList list_example = new V3DataList("objFromText", DateTime.Now);
            list_example.LoadAsText("saved_as_text.txt", ref list_example);
            foreach(var i in list_example)
            {
                Console.WriteLine(i);
            }
            
            V3DataArray data_arr1 = new V3DataArray("data_arr2", DateTime.Now, 1, 2, 0.5, 2.0, CalculateMethodsComp.calculate_func1);
            V3DataList list_example = new V3DataList("objFromText", DateTime.Now);
            list_example.LoadAsText("saved_as_text.txt", ref list_example);

            foreach (var i in data_arr1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in list_example)
            {
                Console.WriteLine(i);
            }

            V3MainCollection main_collection1 = new V3MainCollection();
            main_collection1.Add(data_arr1);
            main_collection1.Add(list_example);
            //main_collection1.linq_present();
            Console.WriteLine();
            foreach(var i in main_collection1)
            {
                Console.WriteLine(i);
            }
           
            Console.WriteLine(main_collection1.Average_val);

            foreach(var i in main_collection1.Grouping_val_by_x)
            {
                Console.WriteLine("Key = " + i.Key);
                foreach(DataItem item in i)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            */
            test_method1();
            test_methot2();


        }

        static int test_method1()
        {
            Console.WriteLine("\n---------- V3DataArray saving and loading ---------\n");
            bool check1 = false, check2 = false;
            V3DataArray data_arr1 = new V3DataArray("data_arr1", DateTime.Now, 2, 2, 3.5, 7.5, CalculateMethodsComp.calculate_func1);
            if (data_arr1.SaveBinary("data_arr1_binary.dat"))
            {
                Console.WriteLine("Объект data_arr1 успешно сохранен в файл data_arr1_binary.dat");
                check1 = true;
            }
            else
            {
                Console.WriteLine("Произошла ошибка во время сохранения объекта data_arr1");
            }
            V3DataArray data_arr2 = new V3DataArray("objFromBinary", DateTime.Now);

            if(data_arr2.LoadBinary("data_arr1_binary.dat", ref data_arr2))
            {
                check2 = true;
                Console.WriteLine("Объект data_arr2 успешно восстановлен из файлф data_arr1_binary.dat");
            }
            else
            {
                Console.WriteLine("Произошла ошибка во время восстановления объекта data_arr2");
            }

            if (check1)
            {
                Console.WriteLine("Данные объекта data_arr1");
                foreach(var i in data_arr1)
                {
                    Console.WriteLine(i);
                }
            }
            if (check2)
            {
                Console.WriteLine("Данные объекта data_arr2");
                foreach (var i in data_arr2)
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("\n---------- V3DataList saving and loading ---------\n");
            check1 = false;
            check2 = false;
            V3DataList data_list1 = new V3DataList("data_list1", DateTime.Now);
            data_list1.AddDefaults(4, CalculateMethodsComp.calculate_func2);
            if (data_list1.SaveAsText("data_list1_text.txt"))
            {
                Console.WriteLine("Объект data_list1 успешно сохранен в файл data_list1_text.txt");
                check1 = true;
            }
            else
            {
                Console.WriteLine("Произошла ошибка во время сохранения объекта data_list1");
            }
            V3DataList data_list2 = new V3DataList("objFromText", DateTime.Now);

            if (data_list2.LoadAsText("data_list1_text.txt", ref data_list2))
            {
                Console.WriteLine("Объект data_list2 успешно восстановлен из файлф data_list1_text.txt");
                check2 = true;
            }
            else
            {
                Console.WriteLine("Произошла ошибка во время восстановления объекта data_list2");
            }

            if (check1)
            {
                Console.WriteLine("Данные объекта data_list1");
                foreach (var i in data_list1)
                {
                    Console.WriteLine(i);
                }
            }
            if (check2)
            {
                Console.WriteLine("Данные объекта data_list2");
                foreach (var i in data_list2)
                {
                    Console.WriteLine(i);
                }
            }
            return 0;
        }

        static int test_methot2()
        {
            Console.WriteLine("\n---------- Collection linq test ---------\n");
            V3DataList data_list1 = new V3DataList("data_list1", DateTime.Now);   //ноль элементов DataItem
            V3DataList data_list2 = new V3DataList("data_list2", DateTime.Now);
            data_list2.AddDefaults(4, CalculateMethodsComp.calculate_func2);

            V3DataArray data_arr1 = new V3DataArray("data_arr1", DateTime.Now);   //ноль узлов
            V3DataArray data_arr2 = new V3DataArray("data_arr2", DateTime.Now, 2, 2, 3.5, 7.5, CalculateMethodsComp.calculate_func1);

            V3MainCollection collection = new V3MainCollection();
            collection.Add(data_list1);
            collection.Add(data_list2);
            collection.Add(data_arr1);
            collection.Add(data_arr2);

            for(int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine("Объект: " + collection[i].str + ". Количество элементов: " + collection[i].Count);
                foreach(var x in collection[i])
                {
                    Console.WriteLine(x);
                }
            }

            Console.WriteLine("Первый запрос linq. Находит среднее значение");
            Console.WriteLine(collection.Average_val);

            Console.WriteLine("Второй запрос linq. Перечисляет разность между максимальным и минимальным значением модуля для каждого объекта");
            foreach(var i in collection.Diference_val)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Третий запрос linq. Группирует по значениям Х");
            foreach (var i in collection.Grouping_val_by_x)
            {
                Console.WriteLine("Key = " + i.Key);
                foreach (DataItem item in i)
                {
                    Console.WriteLine(item.ToString());
                }
            }

            return 0;
        }
    }
}
