namespace Scripts.Wallet
{
    public sealed class Money
    {
        public int Amount { get; private set; }

        public Money(int amountOfmoney)
        {
            Amount = amountOfmoney;
        }
    }
}
