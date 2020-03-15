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

        [SerializeField] private GameObject _redoButtonObj, _undoButtonObj,_paintButtonObj;

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

            //Undoボタンが押されたらFunctionステートを変更
            _paintButtonObj.OnTriggerEnterAsObservable()
                .Subscribe(_ => { _paintFunctionState = HpPaintFunctionState.Paint; })
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
                            //Paint処理ここに書く
                            paint(x);
                            break;
                    }
                })
                .AddTo(this);
        }

        private void paint(HpInputData data)
        {
            GameObject tmpObj = new GameObject("tmp");
            switch (data.InputState)
            {
                case HpInputState.InputDown:
                    //今の状態をペイント機能に変更
                    _paintFunctionState = HpPaintFunctionState.Paint;
                    //ペイントオブジェクトを生成
                    tmpObj = Instantiate(_paintTrailRendererPrefab, data.InputPosition, Quaternion.identity);
                    tmpObj.transform.parent = _paintTrailRendererParent;
                    break;
                case HpInputState.Input:
                    //ペイントオブジェクトを入力位置に追従
                    tmpObj.transform.position = data.InputPosition;
                    break;
                case HpInputState.NoInput:
                    //ペイント機能を終了
                    _paintFunctionState = HpPaintFunctionState.NoFunc;
                    //なんか書くことあれば
                    tmpObj = null;
                    break;
            }
        }
    }
}