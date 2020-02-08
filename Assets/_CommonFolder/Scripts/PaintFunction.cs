using UnityEngine;
/// <summary>
/// 適当にオブジェクト作ってアタッチ
/// </summary>
[RequireComponent(typeof(TrailRenderer))]
public class PaintFunction : MonoBehaviour
{
    [SerializeField]
    OVRHand m_oVRHand;

    [SerializeField]
    OVRSkeleton m_ovrSkeleton;

    [SerializeField]
    OVRHand.HandFinger m_handFingerType;

    TrailRenderer m_tr;

    void Reset()
    {
        m_tr = this.gameObject.GetComponent<TrailRenderer>();
        m_tr.time = Mathf.Infinity;
        m_tr.widthMultiplier = 0.01f;
        m_tr.minVertexDistance = 0.01f;
    }

    void Start()
    {
        m_tr = this.gameObject.GetComponent<TrailRenderer>();
    }

    void Update()
    {
        Vector3 indexTipPos = m_ovrSkeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        this.gameObject.transform.position = indexTipPos;

        if (m_oVRHand.GetFingerPinchStrength(m_handFingerType) == 0)
        {
            m_tr.emitting = true;
        }
        else
        {
            m_tr.emitting = false;
        }
    }
}