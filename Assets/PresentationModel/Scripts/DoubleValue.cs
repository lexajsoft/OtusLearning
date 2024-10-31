namespace PresentationModel.Scripts
{
    public class DoubleValue
    {
        public UniRx.ReactiveProperty<int> ValueMax { get; private set; }
        public UniRx.ReactiveProperty<int> Value { get; private set; }

        public void Add(int value)
        {
            Value.Value += value;
        }
        public void Minus(int value)
        {
            Value.Value -= value;
        }
    }
}