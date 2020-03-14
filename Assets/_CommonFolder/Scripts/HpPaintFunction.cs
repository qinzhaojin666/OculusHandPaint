using UnityEngine;
using Zenject;

namespace Hp
{
    /// <summary>
    /// 適当にオブジェクト作ってアタッチ
    /// </summary>
    [RequireComponent(typeof(TrailRenderer))]
    public class HpPaintFunction : MonoBehaviour
    {
        [Inject] private IHpInputProvider inputProvider;

        private TrailRenderer m_tr;

        private void Reset()
        {
            m_tr = this.gameObject.GetComponent<TrailRenderer>();
            m_tr.time = Mathf.Infinity;
            m_tr.widthMultiplier = 0.01f;
            m_tr.minVertexDistance = 0.01f;
        }

        private void Start()
        {
            m_tr = this.gameObject.GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            if (inputProvider.OnInput())
            {
                this.gameObject.transform.position = inputProvider.InputPos();
                Debug.Log("OnInput");
                m_tr.emitting = true;
            }
            else
            {
                m_tr.emitting = false;
                Debug.Log("NoInput");
            }
        }
    }
}