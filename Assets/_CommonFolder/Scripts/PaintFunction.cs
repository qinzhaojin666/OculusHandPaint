using UnityEngine;
using Zenject;
/// <summary>
/// 適当にオブジェクト作ってアタッチ
/// </summary>
[RequireComponent(typeof(TrailRenderer))]
public class PaintFunction : MonoBehaviour
{
    [Inject]
    IInputProvider inputProvider;

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
       

        if (inputProvider.OnInput())
        {
            this.gameObject.transform.position = inputProvider.InputPos();
            Debug.Log("ClickInput");
            m_tr.emitting = true;
        }
        else
        {
            m_tr.emitting = false;
        }
    }
}