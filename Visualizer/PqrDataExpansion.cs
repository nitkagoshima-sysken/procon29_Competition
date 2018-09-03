using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public static class PqrDataExpansion
    {
        /// <summary>
        /// PQRデータが正規かどうか判定します。
        /// </summary>
        /// <param name="pqr"></param>
        /// <returns></returns>
        public static bool IsRegular(this PqrData pqr)
        {
            bool isRegular = true;
            if (pqr.Fields == null)
            {
                MessageBox.Show("PQRファイルの読み込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (pqr.One.X < 0 || pqr.One.Y < 0)
            {
                MessageBox.Show("1人目のエージェントの位置" + pqr.One + "が不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pqr.One = new Coordinate(0, 0);
                isRegular = false;
            }
            if (pqr.Two.X < 0 || pqr.Two.Y < 0)
            {
                MessageBox.Show("2人目のエージェントの位置" + pqr.One + "が不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pqr.Two = new Coordinate(0, 0);
                isRegular = false;
            }
            return isRegular;
        }
    }
}
