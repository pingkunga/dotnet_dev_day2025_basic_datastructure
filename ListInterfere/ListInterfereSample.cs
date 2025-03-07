using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
        
        List<int> list2 = new List<int> { 100, 101, 102 };
        
        int insertPosition = 2;
        
        list1.InsertRange(insertPosition, list2);
        
        foreach (int item in list1)
        {
            Console.Write(item + " ");
        }
    }
}