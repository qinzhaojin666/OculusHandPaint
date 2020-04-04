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
        /// お絵描きモード
        /// </summary>
        Paint,
        
        /// <summary>
        /// 元に戻すモード
        /// </summary>
        Redo,

        /// <summary>
        /// 元に戻したものを元に戻すモード
        /// </summary>
        Undo,
        
        /// <summary>
        /// 消去モード
        /// </summary>
        Delete,
        
        /// <summary>
        /// 色選択モード
        /// </summary>
        ColorSelect,
        
        /// <summary>
        /// 保存モード
        /// </summary>
        Save,
        
        /// <summary>
        /// 読み込みモード
        /// </summary>
        Load,
        
        /// <summary>
        /// 非表示モード
        /// </summary>
        Close,
    }
}