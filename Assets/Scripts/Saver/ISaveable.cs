using System;

namespace Saver
{
    public interface ISaveable
    {
        Type GetType();
        object GetValue();
    }
}