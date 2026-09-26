using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExcelDataReader;

namespace Lab4Sorting {
  public partial class MainForm : Form {
    private double[] currentData = new double[0];

    public MainForm() {
      InitializeComponent();
      SetupDataGridView();
    }

    private void SetupDataGridView() {
      dataGridViewInput.Columns.Clear();
      dataGridViewInput.Columns.Add("colIndex", "№");
      dataGridViewInput.Columns.Add("colValue", "Значение");
      dataGridViewInput.Columns[0].ReadOnly = true;
      dataGridViewInput.AllowUserToAddRows = false;
      dataGridViewInput.AllowUserToDeleteRows = true;
    }

    // ---------- Генерация данных ----------
    private void generateItem_Click(object sender, EventArgs e) {
      using (var dialog = new GenerateDialog()) {
        if (dialog.ShowDialog() == DialogResult.OK) {
          currentData = dialog.GeneratedData;
          FillGrid(currentData);
          rtbResults.Clear();
          panelVisualization.Controls.Clear();
        }
      }
    }

    private void FillGrid(double[] data) {
      dataGridViewInput.Rows.Clear();
       for (int i = 0; i < data.Length; ++i) {
         dataGridViewInput.Rows.Add(i + 1, data[i].ToString("0.###"));
       }
     }

    // ---------- Загрузка из Excel ----------
    private void loadExcelItem_Click(object sender, EventArgs e) {
      using (OpenFileDialog ofd = new OpenFileDialog()) {
        ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
        if (ofd.ShowDialog() == DialogResult.OK) {
          try {
            var data = ReadExcel(ofd.FileName);
            if (data.Count == 0) {
              MessageBox.Show("Файл не содержит числовых данных.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
            }

            currentData = data.ToArray();
            FillGrid(currentData);
          } catch (Exception ex) {
            MessageBox.Show("Ошибка чтения Excel: " + ex.Message, "Ошибка",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }

    private List<double> ReadExcel(string path) {
      var result = new List<double>();

      using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
      using (var reader = ExcelReaderFactory.CreateReader(stream)) {
        do {
           while (reader.Read()) {
             for (int i = 0; i < reader.FieldCount; ++i) {
               var val = reader.GetValue(i);
               if (val == null) continue;

               string text = val.ToString().Trim().Replace(',', '.');

               if (double.TryParse(text,
                 System.Globalization.NumberStyles.Any,
                 System.Globalization.CultureInfo.InvariantCulture,
                 out double num)) {
                 result.Add(num);
               }
             }
           }
        } while (reader.NextResult());
      }
      return result;
    }

    // ---------- Загрузка из Google Table ----------
    private void loadGoogleItem_Click(object sender, EventArgs e) {
      using (var dialog = new GoogleTableDialog()) {
        if (dialog.ShowDialog() == DialogResult.OK) {
          if (dialog.LoadedData.Count == 0) {
            MessageBox.Show("Не удалось загрузить данные из Google Table.", "Ошибка",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }
        currentData = dialog.LoadedData.ToArray();
        FillGrid(currentData);
        }
      }
    }

     // ---------- Очистка ----------
    private void clearItem_Click(object sender, EventArgs e) {
      dataGridViewInput.Rows.Clear();
      currentData = new double[0];
      rtbResults.Clear();
      panelVisualization.Controls.Clear();
    }

    // ---------- Выход ----------
    private void exitItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    // ---------- Основной расчёт ----------
    // ---------- Основной расчёт ----------
  private void calculateItem_Click(object sender, EventArgs e) {
    // 1. Считываем данные из таблицы
    if (!TryReadDataFromGrid(out double[] data, out string error)) {
      MessageBox.Show(error, "Ошибка ввода",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
      return;
    }

    if (data.Length == 0) {
      MessageBox.Show("Нет данных для сортировки.", "Ошибка",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
      return;
    }

    // 2. Проверяем выбор алгоритмов
    var selectedAlgorithms = new List<Func<double[], bool, SortResult>>();
    if (chkBubble.Checked) selectedAlgorithms.Add(SortingAlgorithms.BubbleSort);
    if (chkInsertion.Checked) selectedAlgorithms.Add(SortingAlgorithms.InsertionSort);
    if (chkShaker.Checked) selectedAlgorithms.Add(SortingAlgorithms.ShakerSort);
    if (chkQuick.Checked) selectedAlgorithms.Add(SortingAlgorithms.QuickSort);

    if (selectedAlgorithms.Count == 0 && !chkBogo.Checked) {
      MessageBox.Show("Выберите хотя бы один алгоритм сортировки.", "Ошибка",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
      return;
    }

    // 3. Направление сортировки
    bool ascending = rbAscending.Checked;

    // 4. Очищаем вывод
    rtbResults.Clear();
    panelVisualization.Controls.Clear();

    var results = new List<SortResult>();

    // 5.1. Обычные алгоритмы — на всём массиве
    foreach (var algo in selectedAlgorithms) {
      try {
        SortResult result = algo(data, ascending);
        results.Add(result);
      } catch (Exception ex) {
        rtbResults.AppendText($"Ошибка выполнения алгоритма: {ex.Message}\n");
      }
    }

    // 5.2. BOGO — на подмассиве
    if (chkBogo.Checked) {
      const int BogoSize = 6;

      if (data.Length < BogoSize) {
        rtbResults.AppendText($"BOGO: недостаточно данных (нужно {BogoSize}).\n");
      } else {
        double[] bogoSub = data.Take(BogoSize).ToArray();

        try {
          SortResult bogoResult = SortingAlgorithms.BogoSort(bogoSub, ascending);
          bogoResult.AlgorithmName = $"BOGO сортировка (первые {BogoSize} из {data.Length})";
          results.Add(bogoResult);
        } catch (Exception ex) {
          rtbResults.AppendText($"Ошибка BOGO: {ex.Message}\n");
        }
      }
    }

    // 6. Вывод результатов
    DisplayResults(results, data, ascending);
  }

    private bool TryReadDataFromGrid(out double[] data, out string error) {
      data = null;
      error = null;
      var list = new List<double>();

      foreach (DataGridViewRow row in dataGridViewInput.Rows) {
        if (row.IsNewRow) continue;

        var cell = row.Cells["colValue"].Value;
        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) {
          error = $"Пустая ячейка в строке {row.Index + 1}.";
          return false;
        }

        // Заменяем запятую на точку — на случай русской раскладки
        string text = cell.ToString().Trim().Replace(',', '.');

        if (!double.TryParse(text,
          System.Globalization.NumberStyles.Any,
          System.Globalization.CultureInfo.InvariantCulture,
          out double value))
        {
        error = $"Некорректное значение в строке {row.Index + 1}: \"{cell}\". " +
                 "Ожидается число.";
        return false;
        }

        list.Add(value);
      }

      // Проверка количества — ПОСЛЕ цикла
      if (list.Count < 20 || list.Count > 50) {
        error = $"Количество чисел должно быть от 20 до 50. Сейчас: {list.Count}.";
        return false;
      }

      data = list.ToArray();
      return true;
    }

    private void DisplayResults(List<SortResult> results, double[] original, bool ascending) {
      rtbResults.AppendText("=== РЕЗУЛЬТАТЫ СОРТИРОВКИ ===\n");
      rtbResults.AppendText($"Направление: {(ascending ? "по возрастанию" : "по убыванию")}\n");
      rtbResults.AppendText($"Исходный массив ({original.Length} эл.): " +
                          string.Join(", ", original.Select(x => x.ToString("0.###"))) + "\n\n");

      var fair = results.Where(r => !r.AlgorithmName.StartsWith("BOGO")).ToList();
      SortResult fastest = fair.OrderBy(r => r.ElapsedMilliseconds).FirstOrDefault();

      int y = 10;
      foreach (var r in results) {
        rtbResults.AppendText($"--- {r.AlgorithmName} ---\n");
        rtbResults.AppendText($"  Время:      {r.ElapsedMilliseconds:F4} мс\n");
        rtbResults.AppendText($"  Итераций:   {r.Iterations}\n");
        rtbResults.AppendText($"  Результат:  " +
                              string.Join(", ", r.SortedArray.Select(x => x.ToString("0.###"))) + "\n");

        if (r.AlgorithmName.StartsWith("BOGO"))
          rtbResults.AppendText("  BOGO сортирует только часть массива (подмассив)\n");

        if (fastest != null && ReferenceEquals(r, fastest))
          rtbResults.AppendText("  >>> САМЫЙ БЫСТРЫЙ АЛГОРИТМ <<<\n");

        rtbResults.AppendText("\n");

        DrawVisualization(r, y);
        y += 90;
      }

      if (fastest != null) {
        rtbResults.AppendText($"\nБыстрее всех (без учёта BOGO): {fastest.AlgorithmName} " +
                              $"({fastest.ElapsedMilliseconds:F4} мс)\n");
      }
    }

    private void DrawVisualization(SortResult result, int yOffset) {
      var panel = new Panel {
        Location = new Point(5, yOffset),
        Size = new Size(panelVisualization.Width - 20, 80),
        BorderStyle = BorderStyle.FixedSingle,
        BackColor = Color.WhiteSmoke
      };

      var label = new Label {
        Text = $"{result.AlgorithmName} ({result.ElapsedMilliseconds:F3} мс, {result.Iterations} итер.)",
        Location = new Point(5, 2),
        AutoSize = true,
        Font = new Font("Segoe UI", 8F, FontStyle.Bold)
      };
      panel.Controls.Add(label);

      var canvas = new Panel {
        Location = new Point(5, 22),
        Size = new Size(panel.Width - 15, 50),
        BackColor = Color.White
      };
      canvas.Paint += (s, e) => PaintBars(e.Graphics, canvas, result.SortedArray);
      panel.Controls.Add(canvas);

      panelVisualization.Controls.Add(panel);
    }

    private void PaintBars(Graphics g, Panel canvas, double[] arr) {
      if (arr == null || arr.Length == 0) return;

      double minVal = arr.Min();
      double maxVal = arr.Max();
      double range = maxVal - minVal;
      if (range == 0) range = 1;

      int n = arr.Length;
      float barWidth = (float)canvas.Width / n;

      for (int i = 0; i < n; ++i) {
        float normalized = (float)((arr[i] - minVal) / range);
        int barHeight = (int)(normalized * (canvas.Height - 5)) + 5;

        var rect = new RectangleF(
           i * barWidth + 1,
           canvas.Height - barHeight,
           Math.Max(1, barWidth - 2),
           barHeight);

           using (var brush = new SolidBrush(ColorFromValue(arr[i], minVal, maxVal))) {
             g.FillRectangle(brush, rect);
           }
      }
    }

    private Color ColorFromValue(double value, double minVal, double maxVal) {
      double range = maxVal - minVal;
      if (range == 0) range = 1;

      int t = (int)(255 * ((value - minVal) / range));
      t = Math.Max(0, Math.Min(255, t));
      return Color.FromArgb(255 - t / 2, 100 + t / 3, t);
    }
  }
}
