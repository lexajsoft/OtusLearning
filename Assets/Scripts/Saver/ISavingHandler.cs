using System;

namespace Saver
{
    public interface ISavingHandler : ISaveable, ILoadable
    {
        public string SaveKey { get; }
        public Type SaveType { get; }
    }
}