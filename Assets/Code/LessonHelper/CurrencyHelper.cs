using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class CurrencyHelper : MonoBehaviour
    {
        [SerializeField] private long _current;
        private MoneyStorage _moneyStorage;
        private GemStorage _gemStorage;

        [Inject]
        private void Construct(MoneyStorage moneyStorage, GemStorage gemStorage)
        {
            _gemStorage = gemStorage;
            _moneyStorage = moneyStorage;
        }

        public void AddMoney()
        {
            _moneyStorage.AddMoney(_current);
        }

        public void SpendMoney()
        {
            _moneyStorage.SpendMoney(_current);
        }

        public void AddGem()
        {
            _gemStorage.AddGem(_current);
        }

        public void SpendGem()
        {
            _gemStorage.SpendGem(_current);
        }
    }
}
