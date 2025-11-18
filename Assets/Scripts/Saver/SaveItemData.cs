using System;

namespace Saver
{
    [Serializable]
    public class SaveItemData
    {
        public Type Type;
        public object Savable;
    }
}