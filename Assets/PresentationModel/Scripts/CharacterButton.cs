using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class CharacterButton : MonoBehaviour
    {
        [SerializeField] private Image _characterIcon;
        [SerializeField] private Button _button;
        [SerializeField] private TMPro.TextMeshProUGUI _characterNameText;
        
        public UnityAction<CharacterPresetScriptableObject> OnCharacterClicked;

        private CharacterPresetScriptableObject _data;
        
        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetData(CharacterPresetScriptableObject characterPresetScriptableObject)
        {
            _data = characterPresetScriptableObject;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            _characterIcon.sprite = _data.Icon;
            _characterNameText.text = _data.CharacterName;
        }

        private void OnClick()
        {
            OnCharacterClicked?.Invoke(_data);
        }
    }
}