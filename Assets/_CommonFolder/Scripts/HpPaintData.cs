using System;
using UnityEngine;

namespace Hp
{
    /// <summary>
    /// ペイントデータを入れる構造体
    /// </summary>
    [Serializable]
    public struct HpPaintData
    {
        /// <summary>
        /// TrailRendererの頂点用配列
        /// </summary>
        public Vector3[] PaintVertices;

        /// <summary>
        /// Paintオブジェクト生成された位置
        /// </summary>
        public Vector3 PaintObjectPosition;
        
        /// <summary>
        /// Paintの色情報用
        /// </summary>
        public Color PaintColor;
    }
}