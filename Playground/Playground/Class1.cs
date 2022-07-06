using System;

namespace Playground
{
    public class Class1 {
        public static void main(String[] args) {
            
        }
        public Class1()
        {
            int[] nums = new int[] { 6, -2, 4, 5 };
            int a = 0;
            GetMin(nums, out a);
            Console.WriteLine(a);
        }

        private static void GetMin(int[] arr, out int min) {
            min = Int32.MaxValue;
            foreach (int num in arr)
                if (num < min)
                    min = num;
        }
    }
}
