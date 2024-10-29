using UniRx;

namespace Code
{
    public sealed class MoneyStorage
    {
        public IReadOnlyReactiveProperty<long> Money => _money;
        
        private readonly ReactiveProperty<long> _money;
        public MoneyStorage(long money)
        {
            _money = new LongReactiveProperty(money);
        }
        
        public void AddMoney(long money)
        {
            _money.Value += money;
        }
        
        public void SpendMoney(long money)
        {
            _money.SetValueAndForceNotify(_money.Value - money);
        }
    }
}
