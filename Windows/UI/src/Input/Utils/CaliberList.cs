using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gehtsoft.BallisticCalculator.UI
{
    static class CaliberList
    {

        #region List
        private static string[] mCalibers = new string[] {
".17 Mach 2",
".17 HMR",
".17 PMC/Aguila",
".17 Winchester Super Magnum",
".22 Short",
".22 Long",
".22 Long Rifle",
".22 Remington Special",
".22 WRF",
"5.45x18mm",
"5.7x28mm",
"5.8x21mm",
".204 Ruger",
"5.45x39mm",
".22-250",
".22 Hornet",
".223 WSSM",
".222 Remington",
".222 Remington Magnum",
"5.56X45 (NATO)",
".223 Remington",
".25 ACP",
".243 Winchester",
".243 WSSM",
".240 Weatherby Magnum",
".25-06 Remington",
"6.5 x 50 SR Arisaka",
"6.5x53mmR",
"6.5x55mm Swedish",
".260 Remington",
"6.5 mm Creedmoor",
"6.5 mm Grendel",
".264 Win Magnum",
"6.5x52 Mannlicher-Carcano",
"7.65x25mm Borchardt",
"7.62x25mm Tokarev",
"7.63x25mm Mauser",
"7.65x21mm Parabellum",
".32 ACP",
"7.62x38mmR",
".32 S&W",
".327 Federal Magnum",
"6.8mm Remington SPC",
".270 Winchester",
".270 WSM",
".270 Weatherby Magnum",
".284 Winchester",
"7mm WSM",
".280 British",
".280/30 British",
"7mm-08 Remington",
"7x55mm Swiss",
"7x57mm Mauser",
"7x64 Brenneke",
".280 Remington",
"7mm Weatherby Magnum",
".30 Carbine",
"7.62x40 WT",
"7.62x51mm (NATO)",
".308 Win",
".300 WSM",
"7.35x51mm Carcano",
".30-03",
".300 Winchester Magnum",
".30-30 Winchester",
".300 Weatherby Magnum",
"7.62x54mmR",
"7.62x39mm M43",
".303 British",
"7.7x58mm Arisaka",
".300 Lapua Magnum",
"300 Whisper",
"300 BLK",
".32 S&W",
".327 Federal Magnum",
"7.92x33mm Kurz",
"8x50mmR Mannlicher",
".32 Winchester Special",
".325 WSM",
"8x56mmR Mannlicher",
"8x58mmR",
".338 Whisper",
".338 Federal",
".338 Winchester Magnum",
".338 Lapua Magnum",
".380 ACP",
"9x19mm Parabellum",
".357 SIG",
"9x21mm",
"9x21mm Gyurza",
"9x23mm Largo",
".38 ACP",
".38 Super",
"9x18mm Makarov",
".38 S&W",
".38 S&W Special",
".357 S&W Magnum",
".41 Long Colt",
"9x39mm",
"9x57mm Mauser",
".375 Ruger",
".375 Weatherby Magnum",
".375 Winchester",
".375 Chey Tac",
".40 S&W",
"10mm Auto",
".400 Corbon",
".41 Action Express",
".408 Chey Tac",
".416 Barrett",
".416 Remington Magnum",
".416 Weatherby Magnum",
".405 Winchester",
".45 GAP",
".45 ACP",
".450 Nitro Express",
".458 Winchester Magnum",
".458 SOCOM",
".460 Weatherby Magnum",
".50 Action Express",
".480 Ruger",
".500 S&W Magnum",
".50 Beowulf",
".50 BMG",
"12.7x99 mm NATO",
"12.7x108mm",
"410 GA",
"20 GA",
"12 GA",
        };
        #endregion

        static public int Count
        {
            get
            {
                return mCalibers.Length;
            }
        }

        static public string Item(int index)
        {
                return mCalibers[index];
        }

        static public void FillComboBox(ComboBox box)
        {
            foreach (string s in mCalibers)
                box.Items.Add(s);
        }
    }


}


