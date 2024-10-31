using System;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterStat
    {
        public IReadOnlyReactiveProperty<string> Name => _name;
        [SerializeField] private ReactiveProperty<string> _name;
        public IReadOnlyReactiveProperty<int> Value => _value;
        [SerializeField] private ReactiveProperty<int> _value;

        

        public CharacterStat()
        {
            _name = new ReactiveProperty<string>();
            _value = new ReactiveProperty<int>();
        }

        public void ChangeValue(int value)
        {
            _value.Value = value;
        }
    }
}