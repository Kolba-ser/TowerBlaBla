using Scripts.Wallet;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.MonoB.Presenters
{
    public sealed class MoneyPresenter : MonoBehaviour
    {
        [SerializeField]
        private Text _moneyText = null;

        private MoneySystem _wallet = null;

        [Inject]
        private void Construct(MoneySystem service)
        {
            _wallet = service;
            
        }

        private void Start()
        {
            SetTextMoney();
        }

        public void OnEnable()
        {
            _wallet.OnValueChangeEvent += OnValueChange;
        }
        public void OnDisable()
        {
            _wallet.OnValueChangeEvent -= OnValueChange;
        }

        private void OnValueChange()
        {
            SetTextMoney();
        }

        private void SetTextMoney()
        { 
            _moneyText.text = _wallet.Money.ToString();
        }
    }
}
