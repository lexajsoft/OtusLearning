using System.Collections.Generic;
using Inventory.Components;
using UnityEngine;
using Zenject;

namespace Configs
{
    [CreateAssetMenu(menuName = "Create DefaultCharacteristicsConfig", fileName = "DefaultCharacteristicsConfig", order = 0)]
    public class DefaultCharacteristicsConfig : ScriptableObjectInstaller<DefaultCharacteristicsConfig>
    {
        [field: SerializeField] public List<Stat> Characteristics { get; private set; } = new List<Stat>();

        public DefaultCharacteristicsConfig GetCopy()
        {
            var obj = Instantiate(this);
            return obj;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<DefaultCharacteristicsConfig>().FromInstance(GetCopy());
        }
    }
}