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

        [SerializeField] private ButtonController _paintButtonObj, _redoButtonObj, _undoButtonObj, _deleteButtonObj, _colorSelectButtonObj, _saveButtonObj, _loadButtonObj, _closeButtonObj;

        [Inject] private IHpInputModule _inputModule;

        private HpPaintFunctionState _paintFunctionState;

        private GameObject _tmpObj;

        private MaterialPropertyBlock _materialPropertyBlock;

        private int _propertyID;

        private void Start()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();

            //色のプロパティIDをintで保持
            _propertyID = Shader.PropertyToID("_Color");
            
            //==============================================================================================================
            //
            //　ボタンのリスナーに”モード変更の機能”を登録
            //
            //==============================================================================================================

            _redoButtonObj.InteractableStateChanged.AddListener(modeChangeToRedo);
            _undoButtonObj.InteractableStateChanged.AddListener(modeChangeToUndo);
            _paintButtonObj.InteractableStateChanged.AddListener(modeChangeToPaint);
            _deleteButtonObj.InteractableStateChanged.AddListener(modeChangeToDelete);
            _colorSelectButtonObj.InteractableStateChanged.AddListener(modeChangeToColorSelect);
            _saveButtonObj.InteractableStateChanged.AddListener(modeChangeToSave);
            _loadButtonObj.InteractableStateChanged.AddListener(modeChangeToLoad);
            _closeButtonObj.InteractableStateChanged.AddListener(modeChangeToColorSelect);

            //機能のステートに応じて処理を行う
            _inputModule.InputDataObservable
                .Where(_ => _paintFunctionState != HpPaintFunctionState.NoFunc)
                .Subscribe(x =>
                {
                    switch (_paintFunctionState)
                    {
                        case HpPaintFunctionState.Paint:
                            paint(x);
                            break;
                        case HpPaintFunctionState.Redo:
                            redo(x);
                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Undo:
                            undo(x);
                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Delete:
                            //Delete処理ここに書く
                            delete();
                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.ColorSelect:
                            //ColorSelect処理ここに書く　一度別のUIをかます

                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Save:
                            //Save処理ここに書く 一度別のUIをかます

                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Load:
                            //Load処理ここに書く　一度別のUIをかます

                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                        case HpPaintFunctionState.Close:
                            //Close処理ここに書く　一度別のUIをかます

                            //使い終わったら機能のステート未使用に戻す
                            _paintFunctionState = HpPaintFunctionState.NoFunc;
                            break;
                    }
                })
                .AddTo(this);
        }

        //==============================================================================================================
        //
        //　ボタンの判定で各ステートに遷移　ボタンのリスナーに登録
        //
        //==============================================================================================================

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
        /// Deleteモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToDelete(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Delete;
            }
        }

        /// <summary>
        /// ColorSelectモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToColorSelect(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.ColorSelect;
            }
        }

        /// <summary>
        /// Saveモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToSave(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Save;
            }
        }

        /// <summary>
        /// Loadモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToLoad(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Load;
            }
        }

        /// <summary>
        /// Closeモードに変更
        /// </summary>
        /// <param name="obj">リスナー登録時に必要な引数</param>
        private void modeChangeToClose(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                _paintFunctionState = HpPaintFunctionState.Close;
            }
        }

        //==============================================================================================================
        //
        //　呼び出されるお絵描き機能群
        //
        //==============================================================================================================

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

        /// <summary>
        /// セーブ機能
        /// </summary>
        private void save()
        {
            //ここでTrailRendererの情報を構造体、及びリストに格納する
            HpPaintDataWrapper paintDataWrapper = new HpPaintDataWrapper();
            HpPaintData paintData = new HpPaintData();
            
            //Paintオブジェクト(TrailRenderer)のリストを作成
            List<TrailRenderer> trList = new List<TrailRenderer>();

            foreach (Transform child in _paintTrailRendererParent.transform)
            {
                TrailRenderer tr = child.GetComponent<TrailRenderer>();

                if (tr != null)
                {
                    trList.Add(tr);
                }
            }
            
            foreach (TrailRenderer element in trList)
            {
                int posCount = element.positionCount;
                Vector3[] posArray = new Vector3[posCount];

                //全ての頂点を取ってくる
                int vertCount = element.GetPositions(posArray);
                
                //構造体にTrailRendererの頂点座標の配列を格納
                paintData.PaintVertices = posArray;

                //構造体に色情報を格納
                paintData.PaintColor = _materialPropertyBlock.GetColor(_propertyID);
                
                //構造体をリストに追加
                paintDataWrapper.DataList.Add(paintData);
            }
        }
    }
}