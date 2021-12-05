using Scripts.Wallet;
using UnityEngine;
using Zenject;

namespace Scripts.MonoInstallers
{
    public sealed class MoneySystemInstaller : MonoInstaller
    {

        [SerializeField]
        private int _amountOfMoney;

        private readonly MoneySystem _moneySystem = null;

        private MoneySystemInstaller()
        {
            _moneySystem = new MoneySystem();
        }

        public override void InstallBindings()
        {
            _moneySystem.SetAmountOfMoney(_amountOfMoney);

            Container
                .Bind<MoneySystem>()
                .FromInstance(_moneySystem)
                .AsSingle().NonLazy();
        }
    }

}
