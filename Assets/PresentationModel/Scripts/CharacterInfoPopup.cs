using System;
using System.Collections.Generic;
using Code;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class CharacterInfoPopup : PopUpBase<IPresenter>
    {
        [SerializeField] private TMPro.TextMeshProUGUI _userNameText;
        [SerializeField] private TMPro.TextMeshProUGUI _characterLevelText;
        [SerializeField] private TMPro.TextMeshProUGUI _characterDiscriptionText;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private ProgressBar _progressBar;
        
        [SerializeField] private Transform _containerStats;
        [FormerlySerializedAs("_statVisualPrefab")] [SerializeField] private StatView statViewPrefab;

        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Button _addExpButton;
        [SerializeField] private Button _closeButton;
        
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private ICharacterInfoPresenter _presenter;

        private void Start()
        {
            _closeButton.onClick.AddListener(() =>
            {
                Hide();
            });
        }
        
        public override void Hide()
        {
            _compositeDisposable.Dispose();
            base.Hide();
        }

        public override void SetPresenter(IPresenter presenter)
        {
            if (presenter is ICharacterInfoPresenter characterInfoPresenter)
            {
                _presenter = characterInfoPresenter;
            }
            else
            {
                return;
            }
            if(_compositeDisposable != null)
                _compositeDisposable.Dispose();
            _compositeDisposable = new CompositeDisposable(); 
                
            UpdateCharacterVisual(_presenter.UserInfo);
            UpdateCharacterExperienceVisual(_presenter.PlayerLevel);
            UpdateCharacterStatsVisual(_presenter.CharacterInfo.GetStats());
                
                
            _presenter.LevelUpCommand.BindTo(_levelUpButton).AddTo(_compositeDisposable);
            _presenter.AddExperience.BindTo(_addExpButton).AddTo(_compositeDisposable);
            
            // нужно использовать фишки unirx
            // переписать все элементы с использованием этих блоков
            // IReadOnlyReactiveProperty<int>
            _presenter.PlayerLevel.CurrentLevel.Subscribe((val) =>
            {
                _characterLevelText.text = $"Level: {val}";
            }).AddTo(_compositeDisposable);

            _presenter.PlayerLevel.CurrentExperience.Subscribe((exp) =>
            {
                _progressBar.SetValueMax(_presenter.PlayerLevel.RequiredExperience);
                _progressBar.SetValue(exp);
            }).AddTo(_compositeDisposable);
            
        }

        private void UpdateCharacterVisual(UserInfo userInfo)
        {
            // первый вариант
            // _characterIcon.sprite = userInfo.Icon.Value;
            // _userNameText.text = userInfo.Name.Value;
            // _characterDiscriptionText.text = userInfo.Description.Value;
            
            userInfo.Icon.Subscribe((sprite) =>
            {
                _characterIcon.sprite = sprite;
            }).AddTo(_compositeDisposable);
            
            userInfo.Name.Subscribe((nameText) =>
            {
                _userNameText.text = nameText;
            }).AddTo(_compositeDisposable);
            
            userInfo.Description.Subscribe((description) =>
            {
                _characterDiscriptionText.text = description;
            }).AddTo(_compositeDisposable);
        }
        
        private void UpdateCharacterExperienceVisual(PlayerLevel playerLevel)
        {
            // первая реализация
            // _characterLevelText.text = $"Level: {playerLevel.CurrentLevel}";
            
            playerLevel.CurrentLevel.Subscribe((level) =>
            {
                _characterLevelText.text = $"Level: {level}";    
            }).AddTo(_compositeDisposable);
        }

        private void UpdateCharacterStatsVisual(CharacterStat [] stats)
        {
            DestroyStatsVisual();
            foreach (var stat in stats)
            {
                var statVisual = Instantiate(statViewPrefab, _containerStats);
                statVisual.SetData(stat.Name.Value, stat.Value.Value.ToString());
            }
        }

        private void DestroyStatsVisual()
        {
            GameObject[] list = new GameObject[_containerStats.childCount]; 
            for (int i = 0; i < _containerStats.childCount; i++)
            {
                list[i] = _containerStats.GetChild(i).gameObject;
            }

            for (int i = 0; i < list.Length; i++)
            {
                Destroy(list[i]);
            }
        }
    }
    
}