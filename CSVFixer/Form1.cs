using DataAccess;
using Jitbit.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVFixer
{
    public partial class Form1 : Form
    {
        private Settings Settings;
        private Settings DefaultSettings = new Settings()
        {
            InputColumnsToInclude = new System.Collections.ArrayList()
                    {
                        "Order Creation Date",
                        "Currency of Transaction",
                        "Order Amount",
                        "Amount Charged",
                        "Financial Status",
                        "Total Tax",
                        "Buyer State",
                        "Buyer Postal Code",
                        "Buyer Country",
                        "Item 1 Name",
                        "Item 1 Price",
                        "Item 1 Quantity"
                    },
                ColumnsToAdd = new System.Collections.ArrayList()
                    {
                        "Merchant Currency",
                        "Currency Conversion Rate",
                        "Amount (Merchant Currency)"
                    },
                OutputDateTimeFormat = "yyyy-MM-dd HH:mm",
                MerchantCurrency = "PLN",
            };

        private readonly string fixerIoApiKey = "88b9bb8fb42b5a735fe7f7fd8f90345f";

        private FixerIo fixerIo;

        public Form1()
        {
            InitializeComponent();

            Settings = new Settings(DefaultSettings);
            Settings.Load();

            fixerIo = new FixerIo(fixerIoApiKey);
            foreach(var col in Settings.ColumnsToAdd)
            {
                checkedListBox_columnsToAdd.Items.Add(col, true);
            }
            textBox_outputDateTimeFormat.Text = Settings.OutputDateTimeFormat;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("DragEnter!");
            e.Effect = DragDropEffects.Copy;
        }

        private void FileList_OnDrop(object sender, DragEventArgs e)
        {
            var dropped = ((string[])e.Data.GetData(DataFormats.FileDrop));
            var files = dropped.ToList();

            if (!files.Any())
                return;

            foreach (string drop in dropped)
                if (System.IO.Directory.Exists(drop))
                    files.AddRange(Directory.GetFiles(drop, "*.csv", System.IO.SearchOption.AllDirectories));

            foreach (string file in files)
            {
                if (!listBox1.Items.Contains(file) && file.ToLower().EndsWith(".csv"))
                    listBox1.Items.Add(file);
            }
            AnalyzeFiles();
            AutoCheckColumns();
        }

        private void AnalyzeFiles()
        {
            foreach (string file in listBox1.Items)
            {
                DataTable dt = DataTable.New.ReadCsv(file);
                foreach (var columnName in dt.ColumnNames)
                {
                    if (!checkedListBox1.Items.Contains(columnName))
                    {
                        checkedListBox1.Items.Add(columnName);
                    }

                }
            }
        }

        private void AutoCheckColumns()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                var col = checkedListBox1.Items[i];
                if (Settings.InputColumnsToInclude.Contains(col))
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                }
            }
        }

        private DateTime ParseGoogleDateTime(string dateString)
        {
            var dateTime = DateTime.ParseExact(dateString, "M/d/yy h:m tt", System.Globalization.CultureInfo.InvariantCulture);
            return dateTime;
        }

        private void TransformAndSave()
        {
            var fixedFileNames = new LinkedList<string>();
            var stringNumberExtractor = new StringNumberExtractor();
            foreach (string file in listBox1.Items)
            {
                DataTable dt = DataTable.New.ReadCsv(file);
                var csvExport = new CsvExport(",", false);
                foreach(var row in dt.Rows)
                {
                    if (checkBox_removeCanceled.Checked && row["Financial Status"].ToLower().Trim() == "canceled")
                    {
                        continue;
                    }
                    if (checkBox_removePaymentDeclined.Checked && row["Financial Status"].ToLower().Trim() == "payment declined")
                    {
                        continue;
                    }
                    if (checkBox_keepChargedOnly.Checked && row["Financial Status"].ToLower().Trim() != "charged")
                    {
                        continue;
                    }

                    var ocd = row["Order Creation Date"].Trim();
                    var dateTime = ParseGoogleDateTime(ocd);
                    var transactionCurrency = row["Currency of Transaction"];
                    var transactionValue = float.Parse(stringNumberExtractor.Extract(row["Amount Charged"]));

                    csvExport.AddRow();
                    foreach(var col in checkedListBox1.CheckedItems)
                    {
                        var colS = col.ToString();
                        var data = row[colS];
                        switch (colS)
                        {
                            case "Order Creation Date":
                                data = dateTime.ToString(textBox_outputDateTimeFormat.Text);
                                break;
                            default:
                                float number;
                                if(float.TryParse(data, out number))
                                {
                                    data = number.ToString(CultureInfo.CurrentCulture);
                                }
                                break;
                        }
                        csvExport[colS] = data;
                    }
                    foreach(var col in checkedListBox_columnsToAdd.CheckedItems)
                    {
                        var colS = col.ToString();
                        switch(colS)
                        {
                            case "Merchant Currency":
                                csvExport[colS] = Settings.MerchantCurrency;
                                break;
                            case "Currency Conversion Rate":
                                var rate = fixerIo.GetCurrencyConversionRate(dateTime, transactionCurrency, Settings.MerchantCurrency);
                                csvExport[colS] = rate;
                                break;
                            case "Amount (Merchant Currency)":
                                var value = this.fixerIo.Convert(dateTime, transactionCurrency, Settings.MerchantCurrency, transactionValue);
                                csvExport[colS] = value;
                                break;
                            default:
                                csvExport[colS] = "";
                                break;
                        }
                    }
                }
                var newFileName = file + " FIXED";
                if(checkBox_nameFileWithMonth.Checked)
                {
                    var fstRow = dt.Rows.FirstOrDefault();
                    if(fstRow != null)
                    {
                        var dateTime = ParseGoogleDateTime(fstRow["Order Creation Date"]);
                        newFileName = Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + dateTime.ToString("yyyy MM");
                    }
                }
                newFileName += ".csv";
                csvExport.ExportToFile(newFileName);
                fixedFileNames.AddLast(newFileName);
            }
            MessageBox.Show("Saved files as:\n" + fixedFileNames.Aggregate<string>((list, item) => item + "\n"));
        }

        private void button_clearfilelist_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            checkedListBox1.Items.Clear();
        }

        private void button_doit_Click(object sender, EventArgs e)
        {
            TransformAndSave();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox_nameFileWithMonth.Checked = Settings.NameWithSingleMonth;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
        }

        private void checkBox_nameFileWithMonth_CheckedChanged(object sender, EventArgs e)
        {
            Settings.NameWithSingleMonth = checkBox_nameFileWithMonth.Checked;
        }
    }
}
