namespace Hp
{
    /// <summary>
    /// お絵描きモードのステート
    /// </summary>
    public enum HpPaintFunctionState
    {
        /// <summary>
        /// 何の機能も使ってない
        /// </summary>
        NoFunc,

        /// <summary>
        /// 元に戻すモード
        /// </summary>
        Redo,

        /// <summary>
        /// 元に戻したものを元に戻すモード
        /// </summary>
        Undo,

        /// <summary>
        /// お絵描きモード
        /// </summary>
        Paint
    }
}