using UnityEngine;

namespace Hp
{
    public interface IHpInputProvider 
    {
        bool OnInput();
        Vector3 InputPos();
    }
}


