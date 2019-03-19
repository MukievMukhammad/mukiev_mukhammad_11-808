using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntroSortOnArray
{
    public static class HeapSort
    {
        public static int Iteration;
        public static void Sort(int[] array, int iteration)
        {
            Iteration = iteration
            Heap_sort(array, array.Length);
        }

        public static void Heapify(int[] arr, int pos, int size)
        {
            int temp;
            while (2 * pos + 1 < size)
            {
                int t = 2 * pos + 1;
                if (2 * pos + 2 < size && arr[2 * pos + 2] >= arr[t])
                {
                    t = 2 * pos + 2;
                }
                if (arr[pos] < arr[t])
                {
                    temp = arr[pos];
                    arr[pos] = arr[t];
                    arr[t] = temp;
                    pos = t;
                }
                else break;
            }
        }

        public static void HeapMake(int[] arr, int arrLength)
        {
            for (int i = arrLength - 1; i >= 0; i--)
            {
                Heapify(arr, i, arrLength);
            }
        }

        public static void Heap_sort(int[] arr, int arrLength)
        {
            int temp;
            HeapMake(arr, arrLength);
            while (arrLength > 0)
            {
                temp = arr[0];
                arr[0] = arr[arrLength - 1];
                arr[arrLength - 1] = temp;
                arrLength--;
                Heapify(arr, 0, arrLength);
            }
        }
    }

    public static class IntroSort
    {
        public static int Iteration;

        public static void Sort(int[] array)
        {
            var dipthLimit = 2 * Math.Log(array.Length);
            QuickSort(array, 0, array.Length - 1, (int)dipthLimit);
        }

        static int[] QuickSort(int[] array, int left, int right, int dipthLimit)
        {
            Iteration++;
            if(dipthLimit == 0)
            {
                HeapSort.Sort(array);
                return array;
            }

            if (left < right)
            {
                var p = QuickSortPatrition(array, left, right);
                QuickSort(array, left, p, dipthLimit - 1);
                QuickSort(array, p + 1, right, dipthLimit - 1);
            }
            return array;
        }

        static int QuickSortPatrition(int[] array, int left, int right)
        {
            var pivot = array[left];
            var i = left - 1;
            var j = right + 1;
            while (true)
            {
                do
                    j--;
                while (array[j] > pivot);
                do
                    i++;
                while (array[i] < pivot);

                if (i < j)
                {
                    var t = array[i];
                    array[i] = array[j];
                    array[j] = t;
                }
                else return j;
            }
        }
    }

}
