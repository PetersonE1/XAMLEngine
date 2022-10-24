using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace XAMLEngine
{
    public static class Extensions
    {
        public static void RemoveRange<T>(this List<T> list, T[] range)
        {
            foreach (T item in range)
                list.Remove(item);
        }

        public static void RemoveRange<T>(this List<T> list, List<T> range)
        {
            foreach (T item in range)
                list.Remove(item);
        }

        public static void RemoveRange(this UIElementCollection list, UIElement[] range)
        {
            foreach (UIElement item in range)
                list.Remove(item);
        }

        public static void AddRange(this UIElementCollection list, UIElement[] range)
        {
            foreach (UIElement item in range)
                list.Add(item);
        }
    }
}
