using System;
using DG.Tweening;
using Zenject;

namespace Code
{
    public sealed class GemPanelAdapter : IInitializable, IDisposable
    {
        private readonly CurrencyView _currencyView;
        private readonly GemStorage _gemStorage;

        public GemPanelAdapter(CurrencyView currencyView, GemStorage gemStorage)
        {
            _currencyView = currencyView;
            _gemStorage = gemStorage;
        }

        public void Initialize()
        {
            _gemStorage.OnGemChanged += OnGemChanged;
            Setter(_gemStorage.Gem);
        }
        
        private long _lastCurrency;
        private Sequence _sequence;
        private void OnGemChanged(long gem)
        {
            _sequence?.Kill();
            var tweenerCore = DOTween.To(() => _lastCurrency, Setter, gem, _currencyView.Duration);
            _sequence = DOTween.Sequence();
            _sequence.Append(_currencyView.AnimateStartText());
            _sequence.Append(tweenerCore);
            _sequence.Append(_currencyView.AnimateEndText());
        }
        
        private void Setter(long value)
        { 
            _currencyView.UpdateCurrency(value.ToString());
            _lastCurrency = value;
        }

        public void Dispose()
        {
            _gemStorage.OnGemChanged -= OnGemChanged;
        }
    }
}
