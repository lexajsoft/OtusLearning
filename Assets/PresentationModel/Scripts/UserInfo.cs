using System;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo
    {
        public IReadOnlyReactiveProperty<string> Name => _name;
        private ReactiveProperty<string> _name = new ReactiveProperty<string>();

        public IReadOnlyReactiveProperty<string> Description => _description;
        private ReactiveProperty<string> _description = new ReactiveProperty<string>();
        
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;
        private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();

        public void ChangeName(string name)
        {
            _name.Value = name;
        }

        public void ChangeDescription(string description)
        {
            _description.Value = description;
        }

        public void ChangeIcon(Sprite icon)
        {
            _icon.Value = icon;
        }
    }
}