using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.Write("Введите размерность массива: ");
        int size = int.Parse(Console.ReadLine());

        SortAndMeasure(size, BubbleSort, "Пузырьковая сортировка");
        SortAndMeasure(size, InsertionSort, "Сортировка вставками");
        SortAndMeasure(size, SelectionSort, "Сортировка выбором");
        SortAndMeasure(size, QuickSort, "Быстрая сортировка");
        SortAndMeasure(size, MergeSort, "Сортировка слиянием");
        SortAndMeasure(size, ShakerSort, "Шейкерная сортировка");

        Console.WriteLine("Нажмите Enter, чтобы закрыть консоль");
        Console.ReadLine();
    }

    static int[] GenerateArray(int size, int seed)
    {
        Random random = new Random(seed);
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(0, 100);
        }
        return array;
    }

    static void SortAndMeasure(int size, Func<int[], long> sortMethod, string sortName)
    {
        int[] array = GenerateArray(size, Environment.TickCount + sortName.GetHashCode());
        Stopwatch stopwatch = Stopwatch.StartNew();
        long swaps = sortMethod(array);
        stopwatch.Stop();
        Console.WriteLine($"{sortName}: {stopwatch.Elapsed.TotalSeconds:F3} секунд, Перестановок: {swaps}");
    }

    static long BubbleSort(int[] array)
    {
        long countSwaps = 0;
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    countSwaps++;
                }
            }
        }
        return countSwaps;
    }

    static long InsertionSort(int[] array)
    {
        long countSwaps = 0;
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
                countSwaps++;
            }
            array[j + 1] = key;
        }
        return countSwaps;
    }

    static long SelectionSort(int[] array)
    {
        long countSwaps = 0;
        for (int i = 0; i < array.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[minIndex]) minIndex = j;
            }
            if (minIndex != i)
            {
                (array[i], array[minIndex]) = (array[minIndex], array[i]);
                countSwaps++;
            }
        }
        return countSwaps;
    }

    static long QuickSort(int[] array) => QuickSortHelper(array, 0, array.Length - 1);

    static long QuickSortHelper(int[] array, int low, int high)
    {
        long countSwaps = 0;
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high, ref countSwaps);
            countSwaps += QuickSortHelper(array, low, pivotIndex - 1);
            countSwaps += QuickSortHelper(array, pivotIndex + 1, high);
        }
        return countSwaps;
    }

    static int Partition(int[] array, int low, int high, ref long countSwaps)
    {
        int pivot = array[high];
        int i = low;
        for (int j = low; j < high; j++)
        {
            if (array[j] <= pivot)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                countSwaps++;
            }
        }
        (array[i], array[high]) = (array[high], array[i]);
        countSwaps++;
        return i;
    }

    static long MergeSort(int[] array) => MergeSortHelper(array, 0, array.Length - 1);

    static long MergeSortHelper(int[] array, int left, int right)
    {
        long countSwaps = 0;
        if (left < right)
        {
            int mid = left + (right - left) / 2;
            countSwaps += MergeSortHelper(array, left, mid);
            countSwaps += MergeSortHelper(array, mid + 1, right);
            countSwaps += Merge(array, left, mid, right);
        }
        return countSwaps;
    }

    static long Merge(int[] array, int left, int mid, int right)
    {
        long countSwaps = 0;
        int[] leftArray = new int[mid - left + 1];
        int[] rightArray = new int[right - mid];
        Array.Copy(array, left, leftArray, 0, leftArray.Length);
        Array.Copy(array, mid + 1, rightArray, 0, rightArray.Length);
        int i = 0, j = 0, k = left;
        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (leftArray[i] <= rightArray[j])
                array[k++] = leftArray[i++];
            else
            {
                array[k++] = rightArray[j++];
                countSwaps++;
            }
        }
        while (i < leftArray.Length) array[k++] = leftArray[i++];
        while (j < rightArray.Length) array[k++] = rightArray[j++];
        return countSwaps;
    }

    static long ShakerSort(int[] array)
    {
        long countSwaps = 0;
        int left = 0, right = array.Length - 1;
        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (array[i] > array[i + 1])
                {
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    countSwaps++;
                }
            }
            right--;
            for (int i = right; i > left; i--)
            {
                if (array[i] < array[i - 1])
                {
                    (array[i], array[i - 1]) = (array[i - 1], array[i]);
                    countSwaps++;
                }
            }
            left++;
        }
        return countSwaps;
    }
}
