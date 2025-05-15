Form1.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ClosedXML.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace CreditCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSaveExcel_Click(object sender, EventArgs e)
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Кредитний план");

                // Додаємо заголовки
                worksheet.Cell(1, 1).Value = "Місяць";
                worksheet.Cell(1, 2).Value = "Платіж";

                // Додаємо дані
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? "";
                    worksheet.Cell(i + 2, 2).Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value ?? 0);
                }

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel файли (*.xlsx)|*.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);

                    // Додаємо діаграму через Interop Excel
                    AddChartToExcel(saveFileDialog.FileName);

                    MessageBox.Show("Файл успішно збережено з діаграмою!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Очищаємо таблицю
            dataGridView1.Rows.Clear();

            // Якщо немає колонок — додаємо
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Month", "Місяць");
                dataGridView1.Columns.Add("Payment", "Платіж");
            }

            try
            {
                // Зчитуємо дані
                double amount = double.Parse(txtAmount.Text);
                double rate = double.Parse(txtRate.Text) / 100 / 12; // місячна ставка
                int months = int.Parse(txtMonths.Text);

                // Формула ануїтетного платежу
                double monthlyPayment = (amount * rate) / (1 - Math.Pow(1 + rate, -months));

                // Заповнюємо таблицю
                for (int i = 1; i <= months; i++)
                {
                    dataGridView1.Rows.Add(i, monthlyPayment.ToString("F2"));
                }

                MessageBox.Show("Розрахунок завершено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка вводу даних: " + ex.Message);
            }
        }
        private void AddChartToExcel(string filePath)
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excelApp.Workbooks.Open(filePath);
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

            var charts = worksheet.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(300, 50, 400, 300);
            var chart = chartObject.Chart;

            Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Range["A1", $"B{dataGridView1.Rows.Count + 1}"];
            chart.SetSourceData(dataRange);
            chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;

            workbook.Save();
            workbook.Close();
            excelApp.Quit();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Очищаємо старі дані
            chart1.Series.Clear();

            // Створюємо новий ряд даних
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Кредит");

            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // Тип графіка: Лінія

            // Додаємо дані з таблиці (DataGridView)
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    double month = Convert.ToDouble(row.Cells[0].Value);
                    double payment = Convert.ToDouble(row.Cells[1].Value);
                    series.Points.AddXY(month, payment);
                }
            }

            // Додаємо серію на графік
            chart1.Series.Add(series);
        }
    }
}
Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreditCalculator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}





Код файлу Form1Designer.cs
namespace CreditCalculator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.labelRate = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.labelMonths = new System.Windows.Forms.Label();
            this.txtMonths = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnSaveExcel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(77, 57);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(76, 13);
            this.labelAmount.TabIndex = 0;
            this.labelAmount.Text = "Сума кредиту";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(226, 57);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 20);
            this.txtAmount.TabIndex = 1;
            // 
            // labelRate
            // 
            this.labelRate.AutoSize = true;
            this.labelRate.Location = new System.Drawing.Point(80, 116);
            this.labelRate.Name = "labelRate";
            this.labelRate.Size = new System.Drawing.Size(117, 13);
            this.labelRate.TabIndex = 2;
            this.labelRate.Text = "Процентна ставка (%)";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(226, 116);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(100, 20);
            this.txtRate.TabIndex = 3;
            // 
            // labelMonths
            // 
            this.labelMonths.AutoSize = true;
            this.labelMonths.Location = new System.Drawing.Point(80, 169);
            this.labelMonths.Name = "labelMonths";
            this.labelMonths.Size = new System.Drawing.Size(87, 13);
            this.labelMonths.TabIndex = 4;
            this.labelMonths.Text = "Термін (місяців)";
            // 
            // txtMonths
            // 
            this.txtMonths.Location = new System.Drawing.Point(226, 169);
            this.txtMonths.Name = "txtMonths";
            this.txtMonths.Size = new System.Drawing.Size(100, 20);
            this.txtMonths.TabIndex = 5;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(109, 238);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(121, 31);
            this.btnCalculate.TabIndex = 6;
            this.btnCalculate.Text = "Розрахувати";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(529, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(486, 518);
            this.dataGridView1.TabIndex = 7;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 275);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(462, 340);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // btnSaveExcel
            // 
            this.btnSaveExcel.Location = new System.Drawing.Point(665, 557);
            this.btnSaveExcel.Name = "btnSaveExcel";
            this.btnSaveExcel.Size = new System.Drawing.Size(149, 58);
            this.btnSaveExcel.TabIndex = 9;
            this.btnSaveExcel.Text = "Зберегти в Excel";
            this.btnSaveExcel.UseVisualStyleBackColor = true;
            this.btnSaveExcel.Click += new System.EventHandler(this.btnSaveExcel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 557);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 58);
            this.button1.TabIndex = 10;
            this.button1.Text = "Побудувати графік";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 656);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSaveExcel);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.txtMonths);
            this.Controls.Add(this.labelMonths);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.labelRate);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.labelAmount);
            this.Name = "Form1";
            this.Text = "Кредитний калькулятор";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label labelRate;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label labelMonths;
        private System.Windows.Forms.TextBox txtMonths;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnSaveExcel;
        private System.Windows.Forms.Button button1;
    }
}
