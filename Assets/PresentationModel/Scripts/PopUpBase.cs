using Code;

namespace PresentationModel.Scripts
{
    public abstract class PopUpBase<T> : ViewBase where T : IPresenter
    {
        public abstract void SetPresenter(T presenter);
    }
}