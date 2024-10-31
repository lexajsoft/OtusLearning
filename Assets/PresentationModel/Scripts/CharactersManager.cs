using System;
using System.Collections.Generic;
using UnityEngine;

namespace PresentationModel.Scripts
{
    public class CharactersManager : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private CharacterButton _characterButtonPrefab;
        [SerializeField] private List<CharacterPresetScriptableObject> _characterPresetScriptableObjects;
        [SerializeField] private CharacterInfoPopup _characterInfoPopup;
        
        private void Start()
        {
            for (int i = 0; i < _characterPresetScriptableObjects.Count; i++)
            {
                var characterButton = Instantiate(_characterButtonPrefab, _container);
                characterButton.SetData(_characterPresetScriptableObjects[i]);
                characterButton.OnCharacterClicked += OnCharacterClicked;
            }    
        }

        private void OnCharacterClicked(CharacterPresetScriptableObject data)
        {
            _characterInfoPopup.SetPresenter(new CharacterInfoPresenter(data));
            _characterInfoPopup.Show();
        }
    }
}