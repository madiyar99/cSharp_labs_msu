using System;
using System.Numerics;
using System.Collections.Generic;

namespace lab1
{
    static class CalculateMethodsComp
    {
        public static Vector2 calculate_func1(double x, double y)
        {
            return new Vector2((float)(x - 5), (float)(y + 5));    
        }
        public static Vector2 calculate_func2(double x, double y)
        {
            return new Vector2((float)(x * 2), (float)(y * 3));    
        }
    }
}
