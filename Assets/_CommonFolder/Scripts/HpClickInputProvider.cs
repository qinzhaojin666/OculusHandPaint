using UnityEngine;

namespace Hp
{
    public class HpClickInputProvider : IHpInputProvider
    {
        public Vector3 InputPos()
        {
            Vector3 screenClickPos = Input.mousePosition;
            Vector3 tmpPos = new Vector3(screenClickPos.x, screenClickPos.y, Camera.main.transform.forward.z);
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(tmpPos);
            return clickPos;
        }

        public bool OnInput()
        {
            return Input.GetMouseButton(0);
        }
    }
}

