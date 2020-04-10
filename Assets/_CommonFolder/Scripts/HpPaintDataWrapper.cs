using System;
using System.Collections.Generic;

namespace Hp
{
    /// <summary>
    /// Jsonとして扱うデータ　リストを利用するためにクラスでラップ
    /// </summary>
    [Serializable]
    public class HpPaintDataWrapper
    {
        /// <summary>
        /// ペイントデータを入れるリスト
        /// </summary>
        public List<HpPaintData> DataList = new List<HpPaintData>();
    }
}