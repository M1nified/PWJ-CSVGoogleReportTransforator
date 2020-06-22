using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVFixer
{
    class Settings
    {
        static char Separator = '|';

        public ArrayList InputColumnsToInclude { get; set; }
        public ArrayList ColumnsToAdd { get; set; }
        public string OutputDateTimeFormat { get; set; }
        public string MerchantCurrency { get; set; }
        public bool NameWithSingleMonth { get; set; } = false;

        public Settings() { }

        public Settings(Settings defaultSettings)
        {
            InputColumnsToInclude = defaultSettings.InputColumnsToInclude;
            ColumnsToAdd = defaultSettings.ColumnsToAdd;
            OutputDateTimeFormat = defaultSettings.OutputDateTimeFormat;
            MerchantCurrency = defaultSettings.MerchantCurrency;
        }

        public void Load()
        {
            if (!Properties.Settings.Default.SettingAlreadySaved)
                return;

            var inputCollumnsToIncludeStrings = Properties.Settings.Default
                .InputColumnsToInclude.Split(Separator);
            InputColumnsToInclude = new ArrayList(inputCollumnsToIncludeStrings);

            var columnsToAdd = Properties.Settings.Default
                .ColumnsToAdd.Split(Separator);
            ColumnsToAdd = new ArrayList(columnsToAdd);

            OutputDateTimeFormat = Properties.Settings.Default.OutputDateTimeFormat;

            MerchantCurrency = Properties.Settings.Default.MerchantCurrency;

            NameWithSingleMonth = Properties.Settings.Default.NameWithSingleMonth;
        }

        public void Save()
        {
            Properties.Settings.Default.InputColumnsToInclude = string
                .Join(Separator.ToString(), InputColumnsToInclude.ToArray());
            Properties.Settings.Default.ColumnsToAdd = string
                .Join(Separator.ToString(), ColumnsToAdd.ToArray());
            Properties.Settings.Default.OutputDateTimeFormat = OutputDateTimeFormat;
            Properties.Settings.Default.MerchantCurrency = MerchantCurrency;
            Properties.Settings.Default.NameWithSingleMonth = NameWithSingleMonth;
            Properties.Settings.Default.SettingAlreadySaved = true;
            Properties.Settings.Default.Save();
        }
    }
}
