using UnityEngine;

namespace Hp
{
    /// <summary>
    /// Inputのモジュール　ハンドトラッキングの場合
    /// </summary>
    public class HpHandInputProvider : MonoBehaviour, IHpInputProvider
    {
        [SerializeField] private OVRHand _ovrHand;

        [SerializeField] private OVRSkeleton _ovrSkeleton;

        [SerializeField] private OVRHand.HandFinger _handFingerType;

        [SerializeField] private OVRSkeleton.BoneId _boneId;

        private float _minPinchStrengthValue = 0.6f;

        /// <summary>
        /// 入力中のポジションを返す
        /// </summary>
        /// <returns></returns>
        public Vector3 InputPos()
        {
            return _ovrSkeleton.Bones[(int)_boneId].Transform.position;
        }

        /// <summary>
        /// 任意のポーズでインプット中かどうかBool値を返す
        /// </summary>
        /// <returns></returns>
        public bool OnInput()
        {
            //曲げてるかどうかと曲げ方の度合い
            return _ovrHand.GetFingerIsPinching(_handFingerType) && _ovrHand.GetFingerPinchStrength(_handFingerType) >= _minPinchStrengthValue;
        }
    }
}