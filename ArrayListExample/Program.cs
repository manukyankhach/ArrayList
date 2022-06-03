using ArrayListExample;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace _ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            MyArrayList arrayList = new MyArrayList();
            arrayList.Add(45);
            arrayList.Add("hjdgchd");
            for (int i = 0; i < arrayList.Count; i++)
            {
                Console.WriteLine(arrayList[i]);
            }
            Console.ReadLine();
        }
    }
}