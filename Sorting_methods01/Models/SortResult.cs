using System;

namespace Lab4Sorting {
  public class SortResult {
    public string AlgorithmName { get; set; }
    public double[] SortedArray { get; set; }
    public long Iterations { get; set; }
    public double ElapsedMilliseconds { get; set; }

    public SortResult(string name, double[] sorted, long iterations, double ms) {
      AlgorithmName = name;
      SortedArray = sorted;
      Iterations = iterations;
      ElapsedMilliseconds = ms;
    }
  }
}
