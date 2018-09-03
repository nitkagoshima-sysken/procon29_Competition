using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// PQRファイルの読み込みでサイズの個数が2
    /// </summary>
    public class NumberOfIncorrectSizeInPqrExpresion : Exception
    {
        /// <summary>
        /// SizeInPqrExpresion クラスの新しいインスタンスを初期化します。
        /// </summary>
        public NumberOfIncorrectSizeInPqrExpresion() : base()
        {

        }

        /// <summary>
        /// 指定したエラーメッセージを使用して、SizeInPqrExpresion クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">エラーを説明するメッセージ。</param>
        public NumberOfIncorrectSizeInPqrExpresion(string message) : base(message)
        {

        }

        /// <summary>
        /// 指定したエラーメッセージおよびこの例外の原因となった内部例外への参照を使用して、SizeInPqrExpresion クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">エラーを説明するメッセージ。</param>
        /// <param name="inner">現在の例外の原因である例外。</param>
        public NumberOfIncorrectSizeInPqrExpresion(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
