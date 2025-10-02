using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Create ColorConfig", fileName = "ColorConfig", order = 0)]
    public class LevelRareColorConfig : Zenject.ScriptableObjectInstaller<LevelRareColorConfig>
    {
        [SerializeField]private List<Color> _colorsList;
        private Dictionary<LevelRare, Color> _colors;

        private void Init()
        {
            _colors = new Dictionary<LevelRare, Color>();
            for (int i = 0; i < 5; i++)
            {
                if(_colorsList.Count > i)
                    _colors[ (LevelRare)i] = _colorsList[i];
                else
                    _colors[ (LevelRare)i] = Color.black;
            }
        
        }

        public Color GetColorByLevelRare(LevelRare levelRare)
        {
            return _colors[levelRare];
        }

        public LevelRareColorConfig GetCopy()
        {
            var obj = Instantiate(this);
            obj.Init();
            return obj;
        }
        
        public override void InstallBindings()
        {
        
            Container.Bind<LevelRareColorConfig>().FromInstance(GetCopy());
        }
    }
}