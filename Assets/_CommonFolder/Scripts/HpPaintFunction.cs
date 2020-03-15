using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

namespace Hp
{
    /// <summary>
    /// 適当にオブジェクト作ってアタッチ
    /// </summary>
    [RequireComponent(typeof(TrailRenderer))]
    public class HpPaintFunction : MonoBehaviour
    {
        [SerializeField] private Transform _paintTrailRendererParent;

        [SerializeField] private GameObject _paintTrailRendererPrefab;

        [SerializeField] private GameObject _redoButtonObj, _undoButtonObj;

        [Inject] private IHpInputModule _inputModule;

        private HpPaintFunctionState _paintFunctionState;

        private void Start()
        {
            //Redoボタンが押されたらFunctionステートを変更
            _redoButtonObj.OnTriggerEnterAsObservable()
                .Subscribe(_ => { _paintFunctionState = HpPaintFunctionState.Redo; })
                .AddTo(this);

            //Undoボタンが押されたらFunctionステートを変更
            _undoButtonObj.OnTriggerEnterAsObservable()
                .Subscribe(_ => { _paintFunctionState = HpPaintFunctionState.Undo; })
                .AddTo(this);

            //機能のステートに応じて処理を行う
            _inputModule.InputDataObservable
                .Subscribe(x =>
                {
                    switch (_paintFunctionState)
                    {
                        case HpPaintFunctionState.Redo:
                            //Redo処理ここに書く
                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Undo:
                            //Undo処理ここに書く
                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Paint:
                            //Undo処理ここに書く
                            break;
                    }
                })
                .AddTo(this);
        }

        private void paint(HpInputData data)
        {
            switch (data.InputState)
            {
                case HpInputState.InputDown:
                    //ペイントオブジェクトを生成
                    break;
                case HpInputState.Input:
                //ペイントオブジェクトを追従
                case HpInputState.NoInput:
                    //なんか書くことあれば
                    break;
            }
        }
    }
}