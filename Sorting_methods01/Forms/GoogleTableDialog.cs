using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace Lab4Sorting {
  public class GoogleTableDialog : Form {
    private TextBox txtUrl;
    private Button btnLoad;
    private Button btnCancel;

    public List<double> LoadedData { get; private set; } = new List<double>();

    public GoogleTableDialog() {
      Text = "Загрузка из Google Table";
      Size = new System.Drawing.Size(500, 180);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      StartPosition = FormStartPosition.CenterParent;
      MaximizeBox = false;
      MinimizeBox = false;

      var lbl = new Label {
        Text = "Ссылка на опубликованную таблицу (CSV):",
        Location = new System.Drawing.Point(15, 15),
        AutoSize = true
      };
      Controls.Add(lbl);

      txtUrl = new TextBox {
        Location = new System.Drawing.Point(15, 40),
        Width = 450,
        Text = "https://docs.google.com/spreadsheets/d/e/.../pub?output=csv"
      };
      Controls.Add(txtUrl);

      btnLoad = new Button {
        Text = "Загрузить",
        Location = new System.Drawing.Point(150, 90),
        Width = 100
      };

      btnLoad.Click += BtnLoad_Click;
      Controls.Add(btnLoad);

      btnCancel = new Button {
        Text = "Отмена",
        Location = new System.Drawing.Point(270, 90),
        Width = 100,
         DialogResult = DialogResult.Cancel
      };
      Controls.Add(btnCancel);

      CancelButton = btnCancel;
    }

    private async void BtnLoad_Click(object sender, EventArgs e) {
      string url = txtUrl.Text.Trim();
      if (string.IsNullOrWhiteSpace(url)) {
        MessageBox.Show("Введите ссылку.", "Ошибка",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      } try {
          using (var client = new HttpClient()) {
            string csv = await client.GetStringAsync(url);
            LoadedData = ParseCsv(csv);

           if (LoadedData.Count == 0) {
              MessageBox.Show("В таблице не найдено числовых данных.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
            }

            DialogResult = DialogResult.OK;
            Close();
          }
      } catch (Exception ex) {
        MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private List<double> ParseCsv(string csv) {
      var result = new List<double>();
      var lines = csv.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (var line in lines) {
        var cells = line.Split(new[] { ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var cell in cells) {
          // Убираем кавычки, пробелы и заменяем запятую на точку
          string cleaned = cell.Trim().Trim('"').Replace(',', '.');

          if (double.TryParse(cleaned,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out double value))
          {
            result.Add(value);
          }
        }
      }
      return result;
    }
  }
}
