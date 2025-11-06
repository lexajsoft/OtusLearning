namespace HomeWorkTask2
{
    public static class GeneratorChestItems
    {
        public static Chest GetChestGold()
        {
            ChestRandomItems chestRandomItems = new ChestRandomItems();

            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Coins", IconName = "Coins", MinCount = 250, MaxCount = 500, Chance = 1f, ItemType = ItemType.CurrencySoft});
            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Hard", IconName = "Hard", MinCount = 20, MaxCount = 100, Chance = 0.55f,ItemType = ItemType.CurrencyHard});
            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Armor", IconName = "Armor", MinCount = 1, MaxCount = 1, Chance = 0.25f,ItemType = ItemType.ItemArmor});
            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Weapon", IconName = "Weapon", MinCount = 1, MaxCount = 1, Chance = 0.2f,ItemType = ItemType.ItemWeapon});
            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Eat", IconName = "Eat", MinCount = 1, MaxCount = 3, Chance = 0.75f, ItemType = ItemType.ItemEat});
            
            return new Chest()
            {
                NameChest = "Chest Gold",
                DelayTime = 3620f,
                Timer = new Timer(),
                chestRandomItems = chestRandomItems,
                ChestType =  ChestType.ChestGold
            };
        }
        
        public static Chest GetChestWood()
        {
            ChestRandomItems chestRandomItems = new ChestRandomItems();

            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Coins", IconName = "Coins", MinCount = 1, MaxCount = 20, Chance = 1f, ItemType = ItemType.CurrencySoft});
            
            return new Chest()
            {
                NameChest = "Chest Wood",
                DelayTime = 10f,
                Timer = new Timer(),
                chestRandomItems = chestRandomItems,
                ChestType =  ChestType.ChestWood
            };
        }
        public static Chest GetChestSilver()
        {
            ChestRandomItems chestRandomItems = new ChestRandomItems();

            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Coins", IconName = "Coins", MinCount = 100, MaxCount = 500, Chance = 1f, ItemType = ItemType.CurrencySoft});
            chestRandomItems.Items.Add(new ItemCountRandom() {ID = ItemType.CurrencySoft.ToString(), Name = "Hard", IconName = "Hard", MinCount = 5, MaxCount = 20, Chance = 0.45f,ItemType = ItemType.CurrencyHard});
            
            return new Chest()
            {
                NameChest = "Chest Silver",
                DelayTime = 1800f,
                Timer = new Timer(),
                chestRandomItems = chestRandomItems,
                ChestType =  ChestType.ChestSilver
            };
        }
    }
}