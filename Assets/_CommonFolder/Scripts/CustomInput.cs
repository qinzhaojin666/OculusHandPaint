using UnityEngine;

namespace CustomInput
{
    /// <summary>
    /// Custom InputKey
    /// </summary>
    public static class SimpleInput
    {
        /// <summary>
        /// Being touched return true
        /// </summary>
        public static bool GetTouch()
        {
            if (0 < Input.touchCount)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && touch.phase == TouchPhase.Stationary)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Touching return true only 1 frame
        /// </summary>
        public static bool GetTouchDown()
        {
            if (0 < Input.touchCount)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Releasing return true only 1 frame
        /// </summary>
        public static bool GetTouchUp()
        {
            if (0 < Input.touchCount)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return touching position only 1 frame. Not being touched return Vector.zero
        /// </summary>
        public static Vector2 GetTouchDownPos()
        {
            if (0 < Input.touchCount)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    return touch.position;
                }
            }
            return Vector3.zero;
        }
    }
}