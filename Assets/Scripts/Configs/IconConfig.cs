using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Create IconConfig", fileName = "IconConfig", order = 0)]
    public class IconConfig : Zenject.ScriptableObjectInstaller<IconConfig>
    {
        public List<Sprite> Weapons;
        public List<Sprite> Heads;
        public List<Sprite> Feets;
        public List<Sprite> Arms;
        public List<Sprite> Chests;
        public List<Sprite> Items;

        public IconConfig GetCopy()
        {
            var obj = Instantiate(this);
            return obj;
        }

        public override void InstallBindings()
        {
            Container.Bind<IconConfig>().FromInstance(GetCopy());
        }
    }
}