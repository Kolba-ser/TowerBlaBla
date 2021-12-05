using UnityEngine;
using Zenject;

namespace Scripts.Wallet
{
    public sealed class MoneySystem 
    {
        public int Money { get; private set; }

        public delegate void ValueChangeHandler();
        public event ValueChangeHandler OnValueChangeEvent;

        private bool _isInitialized;

        public void SetAmountOfMoney(int amount)
        {
            if (_isInitialized) return;

            Money = amount > 0 ? amount : 1000;
            _isInitialized = true;
        }

        public bool TakeMoney(int quantityOfMoney)
        {
            if (Money < quantityOfMoney | quantityOfMoney < 0)
                return false;

            Money -= quantityOfMoney;
            SendEvent();
            return true;
        }
        public bool AppendMoney(int quantityOfMoney)
        {
            if (quantityOfMoney < 0)
                return false;

            Money += quantityOfMoney;
            SendEvent();

            return true;
        }
        
        private void SendEvent()
        {
            OnValueChangeEvent?.Invoke();
        }

    }
}
