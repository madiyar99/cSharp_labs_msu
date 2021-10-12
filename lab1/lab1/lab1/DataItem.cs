using System;
using System.Numerics;

namespace lab1
{
    public struct DataItem
    {
        public double x { set; get; }
        public double y { set; get; }
        public Vector2 component { set; get; }

        public DataItem(double x, double y, Vector2 component)
        {
            this.x = x;
            this.y = y;
            this.component = component;
        }

        public string ToLongString(string format)
        {
            return String.Format("X: {0}, Y: {1}, comp_value: {2}, comp_length: {3}", x.ToString(format),
                y.ToString(format), component.ToString(format), component.Length().ToString(format));
        }

        public override string ToString()
        {
            return String.Format("x: {0}, y: {1}, comp: {2}", x.ToString(), y.ToString(), component.ToString());
        }
    }

}
