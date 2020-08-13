using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WPF = System.Windows.Controls;
using System.Windows.Xps.Packaging;
using GemBox.Spreadsheet;


namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class PrintPreviewForm : Form
    {
        WPF.DocumentViewer mViewer;

        public PrintPreviewForm(Spreadsheet spreadsheet)
        {
            InitializeComponent();
            mViewer = new WPF.DocumentViewer();
            elementHost.Child = mViewer;
            XpsDocument xps = spreadsheet.Raw.ConvertToXpsDocument(XlsSaveOptions.XpsDefault);
            mViewer.Tag = xps;
            mViewer.Document = xps.GetFixedDocumentSequence();
        }
    }
}
