using System;
using System.Collections.Generic;
using System.Text;
using GemBox.Spreadsheet;

namespace Gehtsoft.BallisticCalculator.UI
{
    class PrintPreviewFactory
    {
        public static Spreadsheet CreatePrintPreview(BallisticModel model)
        {
            Spreadsheet sp = new Spreadsheet();
            ExcelWorksheet ws = sp.Raw.Worksheets[0];
            int i, j;
            string[] arr = null;
            string[] fmts = null;
            model.GetHeaderDataExport(ref arr);
            for (i = 0; i < arr.Length; i++)
                arr[i] = arr[i].Replace('(', '\n').Replace(')', ' ');
            model.GetFormat(ref fmts);

            ws.Rows[0].Cells[0].Value = model.BallisticInfo.Name;
            ws.Rows[0].Cells[0].Style.Font.Weight = ExcelFont.BoldWeight;

            for (i = 0; i < arr.Length; i++)
            {
                ws.Rows[1].Cells[i].Value = arr[i];
                ws.Rows[1].Cells[i].Style.FillPattern.PatternForegroundColor = SpreadsheetColor.FromArgb(200, 200, 200);
                ws.Rows[1].Cells[i].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                ws.Rows[1].Cells[i].Style.Borders.SetBorders(MultipleBorders.Outside, SpreadsheetColor.FromArgb(0, 0, 0), LineStyle.Thin);
                ws.Rows[1].Cells[i].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                ws.Rows[1].Cells[i].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
            }

            for (j = 0; j < model.BallisticInfo.Count && j < 130; j++)
            {
                model.GetOneRowDataExport(j, ref arr);
                for (i = 0; i < arr.Length; i++)
                {
                    ws.Rows[j + 2].Cells[i].Value = arr[i];
                    if (j % 2 == 1)
                    {
                        ws.Rows[j + 2].Cells[i].Style.FillPattern.PatternForegroundColor = SpreadsheetColor.FromArgb(240, 240, 240);
                        ws.Rows[j + 2].Cells[i].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                    }
                    ws.Rows[j + 2].Cells[i].Style.Borders.SetBorders(MultipleBorders.Outside, SpreadsheetColor.FromArgb(0, 0, 0), LineStyle.Thin);
                    ws.Rows[j + 2].Cells[i].Style.NumberFormat = fmts[i];
                    if (fmts[i] != null && fmts[i] != "@")
                        ws.Rows[j + 2].Cells[i].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                }
            }

            for (i = 0; i < arr.Length; i++)
                ws.Columns[i].AutoFit();

            ws.PrintOptions.FitWorksheetWidthToPages = 1;

            return sp;
        }
    }
}
