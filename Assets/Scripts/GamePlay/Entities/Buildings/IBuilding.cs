using GamePlay.Interfaces;

namespace GamePlay.Buildings
{
    public interface IBuilding : IPlayer, IHealth
    {
        
    }
    
    public class BuildingData : EntityData, IBuilding
    {
        public int PlayerId;
        public int CurrentHealth;
        public int MaxHealth;
        
        public int GetMaxHealth()
        {
            return MaxHealth;
        }

        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }

        public int GetPlayerId()
        {
            return PlayerId;
        }
    }
}