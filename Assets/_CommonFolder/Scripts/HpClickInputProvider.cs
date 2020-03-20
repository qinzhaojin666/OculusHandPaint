using System;
using UniRx;
using UnityEngine;

namespace Hp
{
    /// <summary>
    /// クリック時の入力モジュール
    /// </summary>
    public class HpClickInputProvider : MonoBehaviour, IHpInputModule
    {
        /// <summary>
        /// 利用側で入力データを監視可能にする
        /// </summary>
        public IObservable<HpInputData> InputDataObservable => _inputDataSubject;

        private Subject<HpInputData> _inputDataSubject = new Subject<HpInputData>();

        private HpInputData _inputData = new HpInputData();

        private void Update()
        {
            //入力なし
            _inputData.InputState = HpInputState.NoInput;

            //クリックした座標を構造体にぶち込む
            Vector3 screenClickPos = Input.mousePosition;
            Vector3 tmpPos = new Vector3(screenClickPos.x, screenClickPos.y, Camera.main.transform.forward.z);
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(tmpPos);
            _inputData.InputPosition = clickPos;

            //入力中
            if (Input.GetMouseButton(0))
            {
                _inputData.InputState = HpInputState.Input;
            }

            //入力した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                _inputData.InputState = HpInputState.InputDown;
            }

            //構造体をのせてメッセージを発行
            _inputDataSubject.OnNext(_inputData);
        }
    }
}

