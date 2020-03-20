using System;
using UnityEngine;
using UniRx;

namespace Hp
{
    /// <summary>
    /// ハンドトラッキング時の入力モジュール
    /// </summary>
    public class HpHandInputProvider : MonoBehaviour, IHpInputModule
    {
        [SerializeField] private OVRHand _ovrHand;

        [SerializeField] private OVRSkeleton _ovrSkeleton;

        [SerializeField] private OVRHand.HandFinger _handFingerType;

        [SerializeField] private OVRSkeleton.BoneId _boneId;

        /// <summary>
        /// 利用側で入力データを監視可能にする
        /// </summary>
        public IObservable<HpInputData> InputDataObservable => _inputDataSubject;

        private Subject<HpInputData> _inputDataSubject = new Subject<HpInputData>();

        private HpInputData _inputData = new HpInputData();

        private float _minPinchStrengthValue = 0.8f;

        private bool _isInput;

        private void Update()
        {
            //入力なし
            _inputData.InputState = HpInputState.NoInput;

            //指定した手のした座標を構造体にぶち込む
            _inputData.InputPosition = _ovrSkeleton.Bones[(int)_boneId].Transform.position;
            Debug.Log(_inputData.InputPosition);

            //PinchPoseできてるかどうか
            float currentPinchStrength = _ovrHand.GetFingerPinchStrength(_handFingerType);
            bool isPinching = _ovrHand.GetFingerIsPinching(_handFingerType);
            bool isPinchPose = isPinching && currentPinchStrength >= _minPinchStrengthValue;

            //入力中
            if (isPinchPose && _isInput)
            {
                Debug.Log("入力中");
                _isInput = true;
                _inputData.InputState = HpInputState.Input;
            }

            //入力した瞬間
            if (isPinchPose && _isInput == false)
            {
                Debug.Log("入力した瞬間");
                _isInput = true;
                _inputData.InputState = HpInputState.InputDown;
            }

            //ピンチポーズしてない
            if (isPinchPose == false)
            {
                _isInput = false;
            }

            //構造体をのせてメッセージを発行
            _inputDataSubject.OnNext(_inputData);
        }
    }
}