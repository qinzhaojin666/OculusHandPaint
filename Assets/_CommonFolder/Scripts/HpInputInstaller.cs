using Zenject;
using UnityEngine;
using UnityEngine.XR;

namespace Hp
{
    /// <summary>
    /// モジュールのインストーラー
    /// </summary>
    public class HpInputInstaller : MonoInstaller<HpInputInstaller>
    {
        [SerializeField] private GameObject handInput;
        [SerializeField] private GameObject clickInput;

        public override void InstallBindings()
        {
            //実機上
            Container
                .Bind<IHpInputModule>()
                .To<HpHandInputProvider>()
                .FromComponentOn(handInput)
                .AsCached()
                .When(_ => isOnDevice());

            //実機無い時
            Container
                .Bind<IHpInputModule>()
                .To<HpClickInputProvider>()
                .FromComponentOn(clickInput)
                .AsCached()
                .When(_ => !isOnDevice());
        }

        //デバイス上でのプレイかどうか
        private bool isOnDevice()
        {
            if (XRDevice.isPresent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
