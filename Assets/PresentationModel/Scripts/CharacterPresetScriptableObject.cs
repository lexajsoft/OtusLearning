using Lessons.Architecture.PM;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace PresentationModel.Scripts
{
    
    
    [CreateAssetMenu(menuName = "Create CharacterPresetScriptableObject", fileName = "CharacterPresetScriptableObject", order = 0)]
    public class CharacterPresetScriptableObject : ScriptableObject
    {
        public string CharacterName;
        public Sprite Icon;
        public string Description;
        
        public int Level;
        public int ExperienceCurrent;
        
        public CharacterStat[] Stats;
        
    }
}