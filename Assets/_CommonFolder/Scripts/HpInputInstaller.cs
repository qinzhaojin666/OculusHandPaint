using Zenject;
using UnityEngine;
using UnityEngine.XR;

namespace Hp
{
    public class HpInputInstaller : MonoInstaller<HpInputInstaller>
    {
        [SerializeField]
        GameObject handImput;

        public override void InstallBindings()
        {
            //実機上
            Container
                .Bind<IHpInputProvider>()
                .To<HpHandInputProvider>()
                .FromComponentOn(handImput)
                .AsCached()
                .When(_ => isOnDevice());
          
            //実機無い時
            Container
                .Bind<IHpInputProvider>()
                .To<HpClickInputProvider>()
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
