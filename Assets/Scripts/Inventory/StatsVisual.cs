using Extensions;
using UnityEngine;

namespace Inventory
{
    public class StatsVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private MenuDoubleText _menuDoubleText;
    
        [SerializeField] private Player _player;
        public void SetPlayer(Player player)
        {
            DeActivateListener();
            _player = player;
            ActivateListener();
            Rebuild();
        }

        private void ActivateListener()
        {
            _player.OnCurrentCharacteristicsChanged += OnCurrentCharacteristicsChanged;
        }

        private void DeActivateListener()
        {
            _player.OnCurrentCharacteristicsChanged -= OnCurrentCharacteristicsChanged;
        }
    
        private void OnCurrentCharacteristicsChanged()
        {
            Rebuild();
        }


        public void Rebuild()
        {
            _container.transform.DestroyAll();
            foreach (var item in _player.CurrentCharacteristics)
            {
                CreateStat(item.Key.ToString(), item.Value.ToString());
            }        
        }

        private void CreateStat(string name, string value)
        {
            var menuDoubleText = Instantiate(_menuDoubleText, _container.transform);
            menuDoubleText.SetText1(name);
            menuDoubleText.SetText2(value);
        }
    }
}
