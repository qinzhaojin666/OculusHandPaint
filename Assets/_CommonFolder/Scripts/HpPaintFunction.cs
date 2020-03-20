using UnityEngine;
using Zenject;
using UniRx;
using System.Collections.Generic;
using OculusSampleFramework;

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

        [SerializeField] private ButtonController _redoButtonObj, _undoButtonObj, _paintButtonObj;

        [Inject] private IHpInputModule _inputModule;

        private HpPaintFunctionState _paintFunctionState;

        private GameObject _tmpObj;

        private void Start()
        {
            //Redoボタンが押されたらFunctionステートを変更
            _redoButtonObj.InteractableStateChanged.AddListener(modeChangeToRedo);
            
            //Undoボタンが押されたらFunctionステートを変更
            _undoButtonObj.InteractableStateChanged.AddListener(modeChangeToUndo);
            
            //Paintボタンが押されたらFunctionステートを変更
            _paintButtonObj.InteractableStateChanged.AddListener(modeChangeToPaint);
            
            //機能のステートに応じて処理を行う
            _inputModule.InputDataObservable
                .Subscribe(x =>
                {
                    switch (_paintFunctionState)
                    {
                        case HpPaintFunctionState.Redo:
                            //Redo処理ここに書く
                            redo(x);
                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Undo:
                            //Undo処理ここに書く
                            undo(x);
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


        /// <summary>
        /// Redoモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToRedo(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Redo;
            }
        }
        
        /// <summary>
        /// Undoモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToUndo(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Undo;
            }
        }
        
        /// <summary>
        /// Paintモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToPaint(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Paint;
            }
        }
        
        /// <summary>
        /// Paint機能
        /// </summary>
        /// <param name="data">発行されたメッセージの値である構造体</param>
        private void paint(HpInputData data)
        {
            switch (data.InputState)
            {
                //入力した瞬間
                case HpInputState.InputDown:
                    //書き始めたらもうUndoできなくする
                    foreach (Transform child in _paintTrailRendererParent)
                    {
                        if (child.gameObject.activeInHierarchy == false)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    Debug.Log("PaintStart");
                    //ペイントオブジェクトを生成
                    _tmpObj = Instantiate(_paintTrailRendererPrefab, data.InputPosition, Quaternion.identity);
                    _tmpObj.transform.parent = _paintTrailRendererParent;
                    break;

                //入力中
                case HpInputState.Input:
                    Debug.Log("Painting");
                    //ペイントオブジェクトを入力位置に追従
                    _tmpObj.transform.position = data.InputPosition;
                    break;

                //入力終了
                case HpInputState.NoInput:
                    //なんか書くことあれば
                    _tmpObj = null;
                    break;
            }
        }

        /// <summary>
        /// Redo機能
        /// </summary>
        /// <param name="data">発行されたメッセージの値である構造体</param>
        private void redo(HpInputData data)
        {
            List<Transform> tmpList = new List<Transform>();

            foreach (Transform child in _paintTrailRendererParent)
            {
                tmpList.Add(child);
            }
            
            //Listを反転させる
            tmpList.Reverse();
            
            foreach (Transform child in tmpList)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    child.gameObject.SetActive(false);
                    return;
                }
            }
        }

        /// <summary>
        /// Undo機能
        /// </summary>
        /// <param name="data">発行されたメッセージの値である構造体</param>
        private void undo(HpInputData data)
        {
            List<Transform> tmpList = new List<Transform>();

            foreach (Transform child in _paintTrailRendererParent)
            {
                tmpList.Add(child);
            }
            
            foreach (Transform child in tmpList)
            {
                if (child.gameObject.activeInHierarchy == false)
                {
                    child.gameObject.SetActive(true);
                    return;
                }
            }
        }

        /// <summary>
        /// 全部消す
        /// </summary>
        private void delete()
        {
            foreach (Transform child in _paintTrailRendererParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}