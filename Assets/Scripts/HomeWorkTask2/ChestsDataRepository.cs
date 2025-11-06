using UnityEngine.Serialization;

namespace HomeWorkTask2
{
    public class ChestsDataRepository : Repository<ChestsData>
    {
        protected override ChestsData GetDefault()
        {
            ChestsData chestsData = new ChestsData()
            {
                Chests =
                {
                    GeneratorChestItems.GetChestWood(),
                    GeneratorChestItems.GetChestSilver(),
                    GeneratorChestItems.GetChestGold(),
                }
            };
            return chestsData;
        }

        protected override string FileName()
        {
            return "ChestsData.json";
        }
    }


    // конкретный сундук
}