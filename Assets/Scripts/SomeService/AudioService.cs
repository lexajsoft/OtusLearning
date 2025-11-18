using System;
using Saver;
using UnityEngine;
using Zenject;

namespace SomeService
{
    /// <summary>
    ///  Это чисто пример подключение другого сервиса которого надо будет сохранять
    /// </summary>
    public class AudioService : MonoBehaviour, ISavingHandler
    {
        [SerializeField] private AudioServiceData _audioServiceData;
        [Inject] private ISaving _savingService;
        
        public object GetValue()
        {
            return _audioServiceData;
        }

        public void SetObjectValue(object value)
        {
            _audioServiceData = (AudioServiceData)value;
        }

        public string SaveKey => nameof(AudioServiceData);
        public Type SaveType => typeof(AudioServiceData);

        private void Start()
        {
            _savingService.Register(this);
        }

        private void OnDestroy()
        {
            _savingService.UnRegister(this);
        }
    }
}