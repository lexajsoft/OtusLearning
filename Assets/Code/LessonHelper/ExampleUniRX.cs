using UniRx;

namespace Code
{
    public sealed class ExampleUniRX
    {
        private readonly ReactiveCollection<string> _reactiveCollection = new();
        private CompositeDisposable _compositeDisposable = new();
        private void TestObserve()
        {
            _compositeDisposable?.Dispose();
            _compositeDisposable = new CompositeDisposable();
            _reactiveCollection.ObserveAdd().Subscribe(OnObserveAdd).AddTo(_compositeDisposable);
            _reactiveCollection.ObserveRemove().Subscribe(OnObserveRemove).AddTo(_compositeDisposable);
        }

        private void OnObserveAdd(CollectionAddEvent<string> collectionAddEvent)
        {
            // code here 
        }

        private void OnObserveRemove(CollectionRemoveEvent<string> collectionRemoveEvent)
        {
            // code here 
        }
        
        public class TestArgs
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private void MessageBrokerTest()
        {
            MessageBroker.Default.Receive<TestArgs>().Subscribe(x => UnityEngine.Debug.Log(x));
            
             //-------
            MessageBroker.Default.Publish(new TestArgs { Value = 1000 });
        }   
    }
}
