using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hp
{
    [RequireComponent(typeof(LineRenderer))]
    public class HpHandBeam : MonoBehaviour
    {
        [SerializeField] private OVRSkeleton _ovrSkeleton;

        [SerializeField] private GameObject _cursor;

        private LineRenderer _beam;

        private OVRPointerEventData _ovrPointerData;

        private EventSystem _eventSystem;

        List<RaycastResult> _raycastResultCache = new List<RaycastResult>();

        private void OnEnable()
        {
            _eventSystem = GetComponent<EventSystem>();
        }

        private void Start()
        {
            _beam = this.gameObject.GetComponent<LineRenderer>();
            _ovrPointerData = new OVRPointerEventData(_eventSystem);
        }

        private RaycastResult findFirstRaycast(List<RaycastResult> candidates)
        {
            for (var i = 0; i < candidates.Count; ++i)
            {
                if (candidates[i].gameObject == null)
                    continue;

                return candidates[i];
            }

            return new RaycastResult();
        }

        private void Update()
        {
            _ovrPointerData.Reset();

            Vector3 handStartPos = _ovrSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Start].Transform.position;
            Vector3 handMiddleFingerStartPos =
                _ovrSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Middle1].Transform.position;

            _ovrPointerData.worldSpaceRay = new Ray(handMiddleFingerStartPos, handMiddleFingerStartPos - handStartPos);
            _beam.SetPosition(0, handMiddleFingerStartPos);

            _ovrPointerData.button = PointerEventData.InputButton.Left;

            _eventSystem.RaycastAll(_ovrPointerData, _raycastResultCache);
            var raycast = findFirstRaycast(_raycastResultCache);
            _ovrPointerData.pointerCurrentRaycast = raycast;
            _raycastResultCache.Clear();

            //Todo　カーソルの位置

            OVRRaycaster ovrRaycaster = raycast.module as OVRRaycaster;

            if (ovrRaycaster)
            {
                _ovrPointerData.position = ovrRaycaster.GetScreenPosition(raycast);

                //HitしたUIのオブジェクトからRectTransformを取得
                RectTransform graphicRect = raycast.gameObject.GetComponent<RectTransform>();

                if (graphicRect != null)
                {
                    //BeamをOn
                    _beam.enabled = true;

                    Vector3 worldPos = raycast.worldPosition;
                    //Todo　カーソルの位置
                    _beam.SetPosition(1, worldPos);

                    //カーソルの位置
                    _cursor.transform.position = worldPos;
                    _cursor.transform.LookAt(-graphicRect.transform.forward);
                    Debug.Log(graphicRect.gameObject.name);
                }
            }
            else
            {
                //BeamをOff
                _beam.enabled = false;
            }
        }
    }
}