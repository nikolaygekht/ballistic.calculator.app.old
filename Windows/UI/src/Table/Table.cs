using System;
using System.Collections.Generic;
using System.Text;
using GemBox.Spreadsheet;

namespace Gehtsoft.BallisticCalculator.UI
{
    public class Spreadsheet 
    {
        private ExcelFile mWb;
        
        public ExcelFile Raw
        {
            get
            {
                return mWb;
            }
        }

        static Spreadsheet()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY"); 
        }

        public Spreadsheet()
        {
            mWb = new ExcelFile();
            mWb.Worksheets.Add("Sheet 1");
        }

        public Spreadsheet(string filename)
        {
            mWb = ExcelFile.Load(filename);
        }
    }
}
