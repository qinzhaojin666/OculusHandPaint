using UnityEngine;

namespace Hp
{
    /// <summary>
    /// 入力データ
    /// </summary>
    public struct HpInputData
    {
        /// <summary>
        /// 入力時の座標
        /// </summary>
        public Vector3 InputPosition;

        /// <summary>
        /// 入力の状態
        /// </summary>
        public HpInputState InputState;
    }
}
