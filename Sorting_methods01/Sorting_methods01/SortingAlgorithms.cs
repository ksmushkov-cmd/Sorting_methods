using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab4Sorting {
  public static class SortingAlgorithms {
    // Пузырьковая сортировка
    public static SortResult BubbleSort(double[] input, bool ascending) {
      double[] arr = (double[])input.Clone();
      long iterations = 0;
      Stopwatch sw = Stopwatch.StartNew();

      int n = arr.Length;
      for (int i = 0; i < n - 1; ++i) {
        for (int j = 0; j < n - 1 - i; ++j) {
          ++iterations;
          bool needSwap = ascending ? arr[j] > arr[j + 1] : arr[j] < arr[j + 1];
          if (needSwap) {
            double temp = arr[j];
            arr[j] = arr[j + 1];
            arr[j + 1] = temp;
          }
        }
      }

      sw.Stop();
      return new SortResult("Пузырьковая сортировка", arr, iterations, sw.Elapsed.TotalMilliseconds);
    }

    // Сортировка вставками
    public static SortResult InsertionSort(double[] input, bool ascending) {
      double[] arr = (double[])input.Clone();
      long iterations = 0;
      Stopwatch sw = Stopwatch.StartNew();

      for (int i = 1; i < arr.Length; ++i) {
        double key = arr[i];
        int j = i - 1;

         while (j >= 0)  {
           ++iterations;
           bool needShift = ascending ? arr[j] > key : arr[j] < key;
           if (needShift) {
             arr[j + 1] = arr[j];
             --j;
           } else {
             break;
           }
         }
         arr[j + 1] = key;
      }

      sw.Stop();
      return new SortResult("Сортировка вставками", arr, iterations, sw.Elapsed.TotalMilliseconds);
    }

    // Шейкерная сортировка
    public static SortResult ShakerSort(double[] input, bool ascending) {
      double[] arr = (double[])input.Clone();
      long iterations = 0;
      Stopwatch sw = Stopwatch.StartNew();

      int left = 0;
      int right = arr.Length - 1;
      bool swapped = true;

       while (left < right && swapped) {
         swapped = false;

         // Проход слева направо
          for (int i = left; i < right; ++i) {
            ++iterations;
            bool needSwap = ascending ? arr[i] > arr[i + 1] : arr[i] < arr[i + 1];
            if (needSwap) {
              double temp = arr[i];
              arr[i] = arr[i + 1];
              arr[i + 1] = temp;
              swapped = true; 
            }
          }
          --right;

          // Проход справа налево
          for (int i = right; i > left; --i) {
            ++iterations;
            bool needSwap = ascending ? arr[i - 1] > arr[i] : arr[i - 1] < arr[i];
            if (needSwap) {
              double temp = arr[i];
              arr[i] = arr[i - 1];
              arr[i - 1] = temp;
              swapped = true;
            }
          }
          ++left;
       }

       sw.Stop();
       return new SortResult("Шейкерная сортировка", arr, iterations, sw.Elapsed.TotalMilliseconds);
    }

    // Быстрая сортировка
    public static SortResult QuickSort(double[] input, bool ascending) {
      double[] arr = (double[])input.Clone();
      long iterations = 0;
      Stopwatch sw = Stopwatch.StartNew();

      QuickSortRecursive(arr, 0, arr.Length - 1, ascending, ref iterations);

      sw.Stop();
      return new SortResult("Быстрая сортировка", arr, iterations, sw.Elapsed.TotalMilliseconds);
    }

    private static void QuickSortRecursive(double[] arr, int low, int high, bool ascending, ref long iterations) {
      if (low < high) {
        int pi = Partition(arr, low, high, ascending, ref iterations);
        QuickSortRecursive(arr, low, pi - 1, ascending, ref iterations);
        QuickSortRecursive(arr, pi + 1, high, ascending, ref iterations);
      }
    }

    private static int Partition(double[] arr, int low, int high, bool ascending, ref long iterations) {
      double pivot = arr[high];
      int i = low - 1;

      for (int j = low; j < high; ++j) {
        ++iterations;
        bool needSwap = ascending ? arr[j] <= pivot : arr[j] >= pivot;
        if (needSwap) {
          ++i;
          double temp = arr[i];
          arr[i] = arr[j];
          arr[j] = temp;
        }
      }

      double temp2 = arr[i + 1];
      arr[i + 1] = arr[high];
      arr[high] = temp2;

      return i + 1;
    }

    // BOGO сортировка (ограничена 10 элементами для предотвращения зависания)
    public static SortResult BogoSort(double[] input, bool ascending) {
      double[] arr = (double[])input.Clone();
      long iterations = 0;
      Stopwatch sw = Stopwatch.StartNew();

      // Ограничение по времени: 5 секунд максимум
      TimeSpan limit = TimeSpan.FromSeconds(5);

      while (!IsSorted(arr, ascending) && sw.Elapsed < limit) {
        Shuffle(arr);
        ++iterations;
      }

      sw.Stop();
      return new SortResult("BOGO сортировка", arr, iterations, sw.Elapsed.TotalMilliseconds);
    }

    private static bool IsSorted(double[] arr, bool ascending) {
      for (int i = 1; i < arr.Length; ++i) {
        if (ascending) {
          if (arr[i - 1] > arr[i]) return false;
        } else {
           if (arr[i - 1] < arr[i]) return false;
        }
      }
      return true;
    }

    private static void Shuffle(double[] arr) {
      Random rnd = new Random(Guid.NewGuid().GetHashCode());
      for (int i = arr.Length - 1; i > 0; --i) {
        int j = rnd.Next(i + 1);
        double temp = arr[i];
        arr[i] = arr[j];
         arr[j] = temp;
      }
    }
  }
}
