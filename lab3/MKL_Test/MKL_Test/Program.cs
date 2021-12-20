using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace MKL_Test
{
    class Program
    {
        [DllImport("C:\\Users\\Asus\\Desktop\\c#_lab_win\\lab3\\ООП_2021_2022\\MKL_Test\\x64\\Debug\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double VM_log_vmdLn(int n, double[] arr, double[] results_mkl, ref int ret,
               ref double time_relations, string mode_inp);

       [DllImport("C:\\Users\\Asus\\Desktop\\c#_lab_win\\lab3\\ООП_2021_2022\\MKL_Test\\x64\\Debug\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double VM_log_vmsLn(int n, double[] arr, double[] results_mkl, ref int ret,
               ref double time_relations, string mode_inp);
        static void Main(string[] args)
        {
            Console.WriteLine("\n------------------ Первый тест --------------------\n");
            VMBenchmark bm = new();
            bm.List_Add(0, 10, 50);//50 элементов на отрезке [0, 10]
            Console.WriteLine(bm.ToString());
            bm.SaveAsText("data.txt");
   
            Console.WriteLine("\n------------------ Второй тест --------------------\n");
            VMBenchmark bm1 = new();
            bm1.List_Add(2000, 10000, 100);//100 элементов на отрезке [2000, 10000]
            Console.WriteLine(bm1.ToString());
            bm1.SaveAsText("data.txt");
           
            Console.WriteLine("\n------------------ Третий тест --------------------\n");
            VMBenchmark bm2 = new();
            bm2.List_Add(500, 50000, 1000);//1000 элементов на отрезке [500,50000]
            Console.WriteLine(bm2.ToString());
            bm2.SaveAsText("data.txt");
            Console.WriteLine("hello");
        }

        public struct VMTime
        {
            double[] arr;           //тестируемые аругменты
            double[] results_mkl;   //результаты mkl          
            public VMTime(double[] arr)
            {
                this.arr = arr;
                this.results_mkl = new double[arr.Length];
            }
            public int Arr_Length
            {
                get
                {
                    return arr.Length;
                }
            }

            public double[] Time_Relation
            {
                get
                {
                    double[] time_relations = new double[4];
                    //Для функции vmdLn и точности HA
                    int ret = -1;
                    VM_log_vmdLn(this.Arr_Length, this.arr, this.results_mkl, ref ret, ref time_relations[0], "HA");
                    //Для функции vmdLn и точности EP
                    ret = -1;
                    VM_log_vmdLn(this.Arr_Length, this.arr, this.results_mkl, ref ret, ref time_relations[1], "EP");
                    //Для функции vmsLn и точности HA
                    ret = -1;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.results_mkl, ref ret, ref time_relations[2], "HA");
                    //Для функции vmsLn и точности EP
                    ret = -1;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.results_mkl, ref ret, ref time_relations[3], "EP");
                    return time_relations;
                }
            }
        }

        public struct VMAccuracy
        {
            int left;
            int right;
            double[] arr;// элементы с отрезка
            double[] res_vmsLn_HA;
            double[] res_vmsLn_EP;
            double[] res_vmdLn_HA;
            double[] res_vmdLn_EP;
            public VMAccuracy(int left, int right, int n, double[] arr)
            {
                this.arr = arr;
                res_vmsLn_HA = new double[n];
                res_vmsLn_EP = new double[n];
                res_vmdLn_HA = new double[n];
                res_vmdLn_EP = new double[n];
                this.left = left;
                this.right = right;
            }
            public int Arr_Length
            {
                get
                {
                    return arr.Length;
                }
            }
            public double Arr_Left
            {
                get
                {
                    return this.left;
                }
            }
            public double Arr_Right
            {
                get
                {
                    return this.right;
                }
            }
            public double Max_Relation_VmsLn
            {
                get
                {
                    //Для функции vmsLn и точности HA
                    int ret = -1;
                    double time = 0;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.res_vmsLn_HA, ref ret, ref time, "HA");
                    //Для функции vmsLn и точности EP
                    ret = -1;
                    time = 0;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.res_vmsLn_EP, ref ret, ref time, "EP");

                    double max_res_VmsLn = Math.Abs(this.res_vmsLn_HA[0] - this.res_vmsLn_EP[0]) /
                            Math.Abs(this.res_vmsLn_HA[0]);
                    for (int i = 1; i < Arr_Length; i++)
                    {
                        double curent = Math.Abs(this.res_vmsLn_HA[i] - this.res_vmsLn_EP[i]) /
                            Math.Abs(this.res_vmsLn_HA[i]);
                        if (curent > max_res_VmsLn)
                        {
                            max_res_VmsLn = curent;
                        }
                    }
                    return max_res_VmsLn;
                }
            }
            public double Max_Relation_VmdLn
            {
                get
                {
                    //Для функции vmdLn и точности HA
                    int ret = -1;
                    double time = 0;
                    VM_log_vmdLn(this.Arr_Length, this.arr, this.res_vmdLn_HA, ref ret, ref time, "HA");
                    //Для функции vmdLn и точности EP
                    ret = -1;
                    time = 0;
                    VM_log_vmdLn(this.Arr_Length, this.arr, this.res_vmdLn_EP, ref ret, ref time, "EP");

                    double max_res_VmdLn = Math.Abs(this.res_vmsLn_HA[0] - this.res_vmsLn_EP[0]) /
                            Math.Abs(this.res_vmsLn_HA[0]);
                    for (int i = 1; i < Arr_Length; i++)
                    {
                        double curent = Math.Abs(this.res_vmsLn_HA[i] - this.res_vmsLn_EP[i]) /
                            Math.Abs(this.res_vmsLn_HA[i]);
                        if (curent > max_res_VmdLn)
                        {
                            max_res_VmdLn = curent;
                        }
                    }
                    return max_res_VmdLn;
                }
            }

            public double Max_Relation_VmsLn_VmdLn
            {
                get
                {
                    if (Max_Relation_VmdLn > Max_Relation_VmsLn)
                    {
                        return Max_Relation_VmdLn;
                    }
                    else
                    {
                        return Max_Relation_VmsLn;
                    }
                }
            }
            public double[] Max_diff_vmsLn
            {
                get
                {
                    //Для функции vmsLn и точности HA
                    int ret = -1;
                    double time = 0;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.res_vmsLn_HA, ref ret, ref time, "HA");
                    //Для функции vmsLn и точности EP
                    ret = -1;
                    time = 0;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.res_vmsLn_EP, ref ret, ref time, "EP");

                    double max_diff = Math.Abs(this.res_vmsLn_HA[0] - this.res_vmsLn_EP[0]);
                    int index_max = 0;
                    for (int i = 1; i < this.Arr_Length; i++)
                    {
                        double current = Math.Abs(this.res_vmsLn_HA[i] - this.res_vmsLn_EP[i]);
                        if (current > max_diff)
                        {
                            max_diff = current;
                            index_max = i;
                        }
                    }

                    double[] result = new double[3];
                    result[0] = res_vmsLn_HA[index_max];   //значение функции vmsLn в режиме HA
                    result[1] = res_vmsLn_EP[index_max];   //значение функции vmsLn в режиме EP
                    result[2] = this.arr[index_max];       //значение аргумента
                    return result;
                }
            }
            public double[] Max_diff_vmdLn
            {
                get
                {
                    //Для функции vmdLn и точности HA
                    int ret = -1;
                    double time = 0;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.res_vmdLn_HA, ref ret, ref time, "HA");
                    //Для функции vmdLn и точности EP
                    ret = -1;
                    time = 0;
                    VM_log_vmsLn(this.Arr_Length, this.arr, this.res_vmdLn_EP, ref ret, ref time, "EP");

                    double max_diff = Math.Abs(this.res_vmdLn_HA[0] - this.res_vmdLn_EP[0]);
                    int index_max = 0;
                    for (int i = 0; i < this.Arr_Length; i++)
                    {
                        double current = Math.Abs(this.res_vmdLn_HA[i] - this.res_vmdLn_EP[i]);
                        if (current > max_diff)
                        {
                            max_diff = current;
                            index_max = i;
                        }
                    }

                    double[] result = new double[3];
                    result[0] = res_vmdLn_HA[index_max];   //значение функции vmdLn в режиме HA
                    result[1] = res_vmdLn_EP[index_max];   //значение функции vmdLn в режиме EP
                    result[2] = this.arr[index_max];       //значение аргумента
                    return result;
                }
            }
        }

        public class VMBenchmark
        {
            List<VMTime> list_time;
            List<VMAccuracy> list_accuracy;
            public VMBenchmark()
            {
                list_time = new List<VMTime>();
                list_accuracy = new List<VMAccuracy>();
            }
            public void List_Add(int left, int right, int n)
            {
                double[] arr = new double[n]; // заполняем массив случайными числами
                Random x = new Random();
                for (int i = 0; i < n; i++)
                {
                    arr[i] = Convert.ToDouble(x.Next(left, right) / 10.0);
                }

                VMTime struct_time = new VMTime(arr);
                VMAccuracy struct_accuracy = new VMAccuracy(left, right, n, arr);
                this.list_time.Add(struct_time);
                this.list_accuracy.Add(struct_accuracy);
            }
            public override string ToString()
            {
                string res_str = "\n\nVMTime List: \n";
                for (int i = 0; i < this.list_time.Count; i++)
                {
                    double[] curr_time = list_time[i].Time_Relation;
                    string str1 = String.Concat("Для функции vmdLn: ",
                            "\n Количество элементов: ", list_time[i].Arr_Length.ToString(),
                            "\n Отношение времени вычисления в режиме VML_HA: ", curr_time[0].ToString(),
                            "\n Отношение времени вычисления в режиме VML_EP: ", curr_time[1].ToString());
                    res_str = String.Concat(res_str, str1);
                    string str2 = String.Concat("\nДля функции vmsLn: ",
                            "\n Количество элементов: ", list_time[i].Arr_Length.ToString(),
                            "\n Отношение времени вычисления в режиме VML_HA: ", curr_time[2].ToString(),
                            "\n Отношение времени вычисления в режиме VML_EP: ", curr_time[3].ToString());
                    res_str = String.Concat(res_str, str2);
                }
                res_str = String.Concat(res_str, "\n\nVMAccuracy List: \n");
                for (int i = 0; i < this.list_accuracy.Count; i++)
                {
                    double[] diff_vmdLn = list_accuracy[i].Max_diff_vmdLn;
                    double[] diff_vmsLn = list_accuracy[i].Max_diff_vmsLn;
                    string str1 = String.Concat("Для функции vmdLn: ",
                            "\n Количество элементов: ", list_accuracy[i].Arr_Length.ToString(),
                            "\n Максимальное значения отношения: ", list_accuracy[i].Max_Relation_VmdLn.ToString(),
                            "\n Максимально отличающие элементы(значение функции в режиме HA): ", diff_vmdLn[0].ToString(),
                            "\n Максимально отличающие элементы(значение функции в режиме EP): ", diff_vmdLn[1].ToString(),
                            "\n Максимально отличающие элементы(аргумент функции): ", diff_vmdLn[2].ToString());
                    res_str = String.Concat(res_str, str1);
                    string str2 = String.Concat("\nДля функции vmsLn: ",
                            "\n Количество элементов: ", list_accuracy[i].Arr_Length.ToString(),
                            "\n Максимальное значения отношения: ", list_accuracy[i].Max_Relation_VmsLn.ToString(),
                            "\n Максимально отличающие элементы(значение функции в режиме HA): ", diff_vmsLn[0].ToString(),
                            "\n Максимально отличающие элементы(значение функции в режиме EP): ", diff_vmsLn[1].ToString(),
                            "\n Максимально отличающие элементы(аргумент функции): ", diff_vmsLn[2].ToString());
                    res_str = String.Concat(res_str, str2);
                }
                return res_str;
            }
            public bool SaveAsText(string filename)
            {
                
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filename, FileMode.Append);
                    StreamWriter writer = new(fs);
                    writer.WriteLine(this.ToString());
                    writer.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    if (fs != null) fs.Close();
                }
                return true;
            }

        }

    }
}
