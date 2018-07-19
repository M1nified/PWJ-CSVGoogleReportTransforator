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
        private System.Collections.ArrayList defaultCoumns = new System.Collections.ArrayList() {
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

        public Form1()
        {
            InitializeComponent();
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
                    if(checkBox_removeCanceled.Checked && row["Financial Status"].ToLower().Trim() == "canceled")
                    {
                        continue;
                    }

                    csvExport.AddRow();
                    foreach(var col in checkedListBox1.CheckedItems)
                    {
                        var colS = col.ToString();
                        csvExport[colS] = row[colS];
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
