using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gehtsoft.BallisticCalculator.UI
{
    static class BulletList
    {
        #region List
        private static string[] mBullets = new string[] {
 "AP",
 "L",
 "LRN",
 "FMJ",
 "FMJBT",
 "FMJ-MK",
 "SP",
 "HP",
 "BTHP",
 "HP-MK",
 "EMFJ",
 "FPJ",
 "FRAN",
 "WC",
 "HBWC",
 "NBT",
 "A-Max",
 "V-Max",
 "Slug",
 "Brenneke",
 "Foster",
 "Sabot",
        };
        #endregion

        static public int Count
        {
            get
            {
                return mBullets.Length;
            }
        }

        static public string Item(int index)
        {
                return mBullets[index];
        }

        static public void FillComboBox(ComboBox box)
        {
            foreach (string s in mBullets)
                box.Items.Add(s);
        }
    }
}
