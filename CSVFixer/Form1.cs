using DataAccess;
using Jitbit.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVFixer
{
    public partial class Form1 : Form
    {
        private readonly System.Collections.ArrayList defaultCoumns = new System.Collections.ArrayList() {
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
        };

        private System.Collections.ArrayList columnsToAdd = new System.Collections.ArrayList()
        {
            "Merchant Currency",
            "Currency Conversion Rate",
            "Amount (Merchant Currency)"
        };

        private string merchantCurrency = "PLN";

        private readonly string fixerIoApiKey = "88b9bb8fb42b5a735fe7f7fd8f90345f";

        private FixerIo fixerIo;

        public Form1()
        {
            InitializeComponent();
            fixerIo = new FixerIo(fixerIoApiKey);
            foreach(var col in this.columnsToAdd)
            {
                checkedListBox_columnsToAdd.Items.Add(col, true);
            }
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
                if (defaultCoumns.Contains(col))
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                }
            }
        }

        private void TransformAndSave()
        {
            var fixedFileNames = new LinkedList<string>();
            foreach (string file in listBox1.Items)
            {
                DataTable dt = DataTable.New.ReadCsv(file);
                var csvExport = new CsvExport(";", false);
                foreach(var row in dt.Rows)
                {
                    if (checkBox_removeCanceled.Checked && row["Financial Status"].ToLower().Trim() == "canceled")
                    {
                        continue;
                    }
                    if (checkBox_removePaymentDeclined.Checked && row["Financial Status"].ToLower().Trim() == "payment_declined")
                    {
                        continue;
                    }
                    if (checkBox_keepChargedOnly.Checked && row["Financial Status"].ToLower().Trim() != "charged")
                    {
                        continue;
                    }

                    var ocd = row["Order Creation Date"].Trim();
                    var dateTime = DateTime.ParseExact(ocd, "M/d/yy h:m tt", System.Globalization.CultureInfo.InvariantCulture);
                    var transactionCurrency = row["Currency of Transaction"];
                    var transactionValue = float.Parse(row["Amount Charged"]);

                    csvExport.AddRow();
                    foreach(var col in checkedListBox1.CheckedItems)
                    {
                        var colS = col.ToString();
                        csvExport[colS] = row[colS];
                    }
                    foreach(var col in checkedListBox_columnsToAdd.CheckedItems)
                    {
                        var colS = col.ToString();
                        switch(colS)
                        {
                            case "Merchant Currency":
                                csvExport[colS] = this.merchantCurrency;
                                break;
                            case "Currency Conversion Rate":
                                var rate = fixerIo.GetCurrencyConversionRate(dateTime, transactionCurrency, this.merchantCurrency);
                                csvExport[colS] = rate;
                                break;
                            case "Amount (Merchant Currency)":
                                var value = this.fixerIo.Convert(dateTime, transactionCurrency, this.merchantCurrency, transactionValue);
                                csvExport[colS] = value;
                                break;
                            default:
                                csvExport[colS] = "";
                                break;
                        }
                    }
                }
                var newFileName = file + " FIXED.csv";
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

    }
}
