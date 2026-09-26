using Lab4Sorting;

namespace Lab4Sorting {
  partial class MainForm {
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.DataGridView dataGridViewInput;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem dataMenu;
    private System.Windows.Forms.ToolStripMenuItem actionsMenu;
    private System.Windows.Forms.ToolStripMenuItem loadExcelItem;
    private System.Windows.Forms.ToolStripMenuItem loadGoogleItem;
    private System.Windows.Forms.ToolStripMenuItem generateItem;
    private System.Windows.Forms.ToolStripMenuItem clearItem;
    private System.Windows.Forms.ToolStripMenuItem calculateItem;
    private System.Windows.Forms.ToolStripMenuItem exitItem;

    private System.Windows.Forms.CheckBox chkBubble;
    private System.Windows.Forms.CheckBox chkInsertion;
    private System.Windows.Forms.CheckBox chkShaker;
    private System.Windows.Forms.CheckBox chkQuick;
    private System.Windows.Forms.CheckBox chkBogo;

    private System.Windows.Forms.RadioButton rbAscending;
    private System.Windows.Forms.RadioButton rbDescending;

    private System.Windows.Forms.Panel panelVisualization;
    private System.Windows.Forms.RichTextBox rtbResults;

    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      } base.Dispose(disposing); 
    }

    private void InitializeComponent() {
      this.dataGridViewInput = new System.Windows.Forms.DataGridView();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.dataMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.actionsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.loadExcelItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadGoogleItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generateItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearItem = new System.Windows.Forms.ToolStripMenuItem();
      this.calculateItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitItem = new System.Windows.Forms.ToolStripMenuItem();

      this.chkBubble = new System.Windows.Forms.CheckBox();
      this.chkInsertion = new System.Windows.Forms.CheckBox();
      this.chkShaker = new System.Windows.Forms.CheckBox();
      this.chkQuick = new System.Windows.Forms.CheckBox();
      this.chkBogo = new System.Windows.Forms.CheckBox();

      this.rbAscending = new System.Windows.Forms.RadioButton();
      this.rbDescending = new System.Windows.Forms.RadioButton();

      this.panelVisualization = new System.Windows.Forms.Panel();
      this.rtbResults = new System.Windows.Forms.RichTextBox();

      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();

      // dataGridViewInput
      this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewInput.Location = new System.Drawing.Point(12, 30);
      this.dataGridViewInput.Name = "dataGridViewInput";
      this.dataGridViewInput.Size = new System.Drawing.Size(300, 400);
      this.dataGridViewInput.TabIndex = 0;

       // menuStrip
       this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
       this.dataMenu,
       this.actionsMenu});
         this.menuStrip.Location = new System.Drawing.Point(0, 0);
         this.menuStrip.Name = "menuStrip";
         this.menuStrip.Size = new System.Drawing.Size(1000, 24);
         this.menuStrip.TabIndex = 1;

       // ---- Меню "Данные" ----
       this.dataMenu.Text = "Данные";
       this.dataMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
         this.generateItem,
         this.loadExcelItem,
         this.loadGoogleItem});

       // ---- Меню "Действия" ----
       this.actionsMenu.Text = "Действия";
       this.actionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
         this.calculateItem,
         this.clearItem,
         this.exitItem});

      // ---- Пункты меню "Данные" ----
      this.generateItem.Text = "Сгенерировать данные (20-50)";
      this.generateItem.Click += new System.EventHandler(this.generateItem_Click);

      this.loadExcelItem.Text = "Загрузить из Excel";
      this.loadExcelItem.Click += new System.EventHandler(this.loadExcelItem_Click);
      this.loadGoogleItem.Text = "Загрузить из Google Table";
      this.loadGoogleItem.Click += new System.EventHandler(this.loadGoogleItem_Click);

      // ---- Пункты меню "Действия" ----
       this.calculateItem.Text = "Рассчитать";
      this.calculateItem.Click += new System.EventHandler(this.calculateItem_Click);

      this.clearItem.Text = "Очистить";
      this.clearItem.Click += new System.EventHandler(this.clearItem_Click);

      this.exitItem.Text = "Выход";
      this.exitItem.Click += new System.EventHandler(this.exitItem_Click);

       // CheckBoxes
      this.chkBubble.Text = "Пузырьковая";
      this.chkBubble.Location = new System.Drawing.Point(330, 40);
      this.chkBubble.Size = new System.Drawing.Size(150, 24);
      this.chkBubble.Checked = true;

      this.chkInsertion.Text = "Вставками";
      this.chkInsertion.Location = new System.Drawing.Point(330, 70);
      this.chkInsertion.Size = new System.Drawing.Size(150, 24);

      this.chkShaker.Text = "Шейкерная";
      this.chkShaker.Location = new System.Drawing.Point(330, 100);
      this.chkShaker.Size = new System.Drawing.Size(150, 24);

      this.chkQuick.Text = "Быстрая";
      this.chkQuick.Location = new System.Drawing.Point(330, 130);
      this.chkQuick.Size = new System.Drawing.Size(150, 24);

      this.chkBogo.Text = "BOGO";
      this.chkBogo.Location = new System.Drawing.Point(330, 160);
      this.chkBogo.Size = new System.Drawing.Size(150, 24);

      // RadioButtons
      this.rbAscending.Text = "По возрастанию";
      this.rbAscending.Location = new System.Drawing.Point(330, 200);
      this.rbAscending.Size = new System.Drawing.Size(150, 24);
      this.rbAscending.Checked = true;

      this.rbDescending.Text = "По убыванию";
      this.rbDescending.Location = new System.Drawing.Point(330, 230);
      this.rbDescending.Size = new System.Drawing.Size(150, 24);

      // panelVisualization
      this.panelVisualization.Location = new System.Drawing.Point(500, 30);
      this.panelVisualization.Name = "panelVisualization";
      this.panelVisualization.Size = new System.Drawing.Size(480, 300);
      this.panelVisualization.BackColor = System.Drawing.Color.White;
      this.panelVisualization.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panelVisualization.AutoScroll = true;

      // rtbResults
      this.rtbResults.Location = new System.Drawing.Point(500, 340);
      this.rtbResults.Name = "rtbResults";
      this.rtbResults.Size = new System.Drawing.Size(480, 300);
      this.rtbResults.ReadOnly = true;
      this.rtbResults.Font = new System.Drawing.Font("Consolas", 9F);

      // MainForm
      this.ClientSize = new System.Drawing.Size(1000, 660);
      this.Controls.Add(this.rtbResults);
      this.Controls.Add(this.panelVisualization);
      this.Controls.Add(this.rbDescending);
      this.Controls.Add(this.rbAscending);
      this.Controls.Add(this.chkBogo);
      this.Controls.Add(this.chkQuick);
      this.Controls.Add(this.chkShaker);
      this.Controls.Add(this.chkInsertion);
      this.Controls.Add(this.chkBubble);
      this.Controls.Add(this.dataGridViewInput);
      this.Controls.Add(this.menuStrip);
      this.MainMenuStrip = this.menuStrip;
      this.Name = "MainForm";
      this.Text = "Лабораторная работа №4 — Олимпиадные сортировки";

      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
