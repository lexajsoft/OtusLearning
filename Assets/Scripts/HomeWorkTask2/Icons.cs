using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWorkTask2
{
    [Serializable]
    public class Icons
    {
        [Serializable]
        public class IconData
        {
            public string Key;
            public Sprite Sprite;
        }
        
        [SerializeField] private List<IconData> _icons;
        private Dictionary<string, Sprite> _sprites;

        public void Init()
        {
            _sprites = new Dictionary<string, Sprite>();
            for (int i = 0; i < _icons.Count; i++)
            {
                _sprites[_icons[i].Key] = _icons[i].Sprite;
            }
        }

        public Sprite GetSprite(string key)
        {
            if (_sprites.TryGetValue(key, out var sprite))
                return sprite;
            return null;
        }
    }
}