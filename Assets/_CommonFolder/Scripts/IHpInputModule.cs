using System;

namespace Hp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHpInputModule 
    {
        IObservable<HpInputData> InputDataObservable { get; }
    }
}


