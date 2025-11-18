namespace GamePlay.Units
{
    public class UnitData : EntityData, IUnit
    {
        public int PlayerId;
        public float Speed;
        public int MaxHealth;
        public int CurrentHealth;
        public float GetSpeed()
        {
            return Speed;
        }

        public int GetPlayerId()
        {
            return PlayerId;
        }

        public int GetMaxHealth()
        {
            return MaxHealth;
        }

        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }
    }
}