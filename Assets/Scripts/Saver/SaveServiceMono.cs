using System;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Saver
{
    
    public class SaveServiceMono : MonoBehaviour
    {
        [Inject] private ISaving _savingService;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Load();
            }
        }

        [Button]
        private void Save()
        {
            _savingService.Save();
        }

        [Button]
        private void Load()
        {
            _savingService.Load();
        }
    }
}