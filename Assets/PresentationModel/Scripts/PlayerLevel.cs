using System;
using NaughtyAttributes;
using UniRx;

namespace PresentationModel.Scripts
{
    public sealed class PlayerLevel
    {
        // public event Action OnLevelUp;
        // public event Action<int> OnExperienceChanged;

        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        private readonly ReactiveProperty<int> _currentLevel;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;
        private readonly ReactiveProperty<int> _currentExperience;
        
        // public int CurrentExperience { get; private set; }
        // public int CurrentLevel { get; private set; } = 1;

        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        private readonly ReactiveProperty<bool> _canLevelUp;
        

        public int RequiredExperience
        {
            get { return 100 * (_currentLevel.Value + 1); }
        }

        public PlayerLevel(int currentLevel, int currentExperience)
        {
            _currentLevel = new ReactiveProperty<int>();
            _currentLevel.Value = currentLevel;

            _currentExperience = new ReactiveProperty<int>();
            _currentExperience.Value = currentExperience;

            _canLevelUp = new ReactiveProperty<bool>();
            
            _currentExperience.Subscribe((val)=>
            {
                UpdateCanLevelUp();
            });
        }

        [Button]
        public void AddExperience(int range)
        {
            var exp = Math.Min(_currentExperience.Value + range, RequiredExperience);
            _currentExperience.Value = (exp);
        }
        
        [Button]
        public void LevelUp()
        {
            if (_canLevelUp.Value)
            {
                int exp = 0;
                if (_currentExperience.Value > RequiredExperience)
                {
                    exp = _currentExperience.Value - RequiredExperience;
                }
                else
                {
                    exp = 0;
                }
                _currentExperience.SetValueAndForceNotify(exp);
                _currentLevel.SetValueAndForceNotify(_currentLevel.Value+1);
                
                UpdateCanLevelUp();
            }
        }

        
        
        public void UpdateCanLevelUp()
        {
            _canLevelUp.Value = _currentExperience.Value >= RequiredExperience;
        }
    }
}