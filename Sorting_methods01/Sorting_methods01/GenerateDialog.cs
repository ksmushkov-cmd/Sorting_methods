using System;
using System.Windows.Forms;

namespace Lab4Sorting {
  public class GenerateDialog : Form {
    private NumericUpDown nudCount;
    private Button btnOk;
    private Button btnCancel;

    public double[] GeneratedData { get; private set; }

    public GenerateDialog() {
      Text = "Генерация данных (количество 20–50)";
      Size = new System.Drawing.Size(300, 160);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      StartPosition = FormStartPosition.CenterParent;
      MaximizeBox = false;
      MinimizeBox = false;

      var lbl = new Label {
        Text = "Количество элементов:",
        Location = new System.Drawing.Point(15, 20),
        AutoSize = true
      };
      Controls.Add(lbl);

      nudCount = new NumericUpDown {
        Location = new System.Drawing.Point(160, 18),
        Width = 100,
        Minimum = 20,     
        Maximum = 50,      
        Value = 20       
      };
      Controls.Add(nudCount);

      btnOk = new Button {
        Text = "OK",
        Location = new System.Drawing.Point(50, 70),
        Width = 80,
        DialogResult = DialogResult.OK
      };
      btnOk.Click += (s, e) => Generate();
      Controls.Add(btnOk);

      btnCancel = new Button {
        Text = "Отмена",
        Location = new System.Drawing.Point(150, 70),
        Width = 80,
        DialogResult = DialogResult.Cancel
      };
      Controls.Add(btnCancel);

      AcceptButton = btnOk;
      CancelButton = btnCancel;
    }

    private void Generate() {
      int count = (int)nudCount.Value;   // теперь всегда 20..50
      var rnd = new Random();
      GeneratedData = new double[count]; // ← было int[count]
      for (int i = 0; i < count; ++i) {
        // Дробные и отрицательные значения в диапазоне [-100; 100)
        double value = rnd.NextDouble() * 200.0 - 100.0;
        value = Math.Round(value, 2);  // округление до 2 знаков
        GeneratedData[i] = value;
      }
    }
  }
}