using System;
using Code;
using UniRx;

namespace PresentationModel.Scripts
{
    public interface ICharacterInfoPresenter : IPresenter
    {
        UserInfo UserInfo { get; }
        CharacterInfo CharacterInfo { get; }
        PlayerLevel PlayerLevel { get; }
        
        public ReactiveCommand LevelUpCommand { get; }

        public ReactiveCommand AddExperience { get; }
    }

    public class CharacterInfoPresenter : ICharacterInfoPresenter
    {
        public UserInfo UserInfo { get; }
        public CharacterInfo CharacterInfo { get; }
        public PlayerLevel PlayerLevel { get; }
        public ReactiveCommand LevelUpCommand { get; }
        public ReactiveCommand AddExperience { get; }


        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public CharacterInfoPresenter(CharacterPresetScriptableObject characterPresetScriptableObject)
        {
            UserInfo = new UserInfo();
            UserInfo.ChangeIcon(characterPresetScriptableObject.Icon);
            UserInfo.ChangeName(characterPresetScriptableObject.CharacterName);
            UserInfo.ChangeDescription(characterPresetScriptableObject.Description);
            
            CharacterInfo = new CharacterInfo();
            for (int i = 0; i < characterPresetScriptableObject.Stats.Length; i++)
            {
                CharacterInfo.AddStat(characterPresetScriptableObject.Stats[i]);    
            }
            
            PlayerLevel = new PlayerLevel(characterPresetScriptableObject.Level, characterPresetScriptableObject.ExperienceCurrent);

            LevelUpCommand = new ReactiveCommand(PlayerLevel.CanLevelUp);
            LevelUpCommand.Subscribe(OnLevelUp).AddTo(_compositeDisposable);

            AddExperience = new ReactiveCommand();
            AddExperience.Subscribe(AddFreeExp).AddTo(_compositeDisposable);
        }

        private void AddFreeExp(Unit _)
        {
            PlayerLevel.AddExperience(50);
        }

        private void OnLevelUp(Unit _)
        {
            PlayerLevel.LevelUp();
        }

        public CharacterInfoPresenter(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            UserInfo = userInfo;
            CharacterInfo = characterInfo;
            PlayerLevel = playerLevel;
        }
    }
}