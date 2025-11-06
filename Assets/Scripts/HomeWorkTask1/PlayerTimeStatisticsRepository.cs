namespace HomeWorkTask1
{
    public class PlayerTimeStatisticsRepository : Repository<PlayerTimeStatisticsData>
    {
        protected override void AfterLoad()
        {
            _data.CheckAfterLoad();
        }

        protected override PlayerTimeStatisticsData GetDefault()
        {
            return new PlayerTimeStatisticsData();
        }

        protected override string FileName()
        {
            return "PlayerTimeStatistics.json";
        }
    }
}