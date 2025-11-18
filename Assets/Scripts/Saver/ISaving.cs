namespace Saver
{
    public interface ISaving
    {
        void Save();
        void Load();
        void Register(ISavingHandler savingHandler);
        void UnRegister(ISavingHandler savingHandler);
    }
}