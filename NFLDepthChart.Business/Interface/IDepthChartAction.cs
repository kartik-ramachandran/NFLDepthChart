namespace NFLDepthChart.Business.Interface
{
    public interface IDepthChartAction
    {
        void AddPlayer(DepthChartModel playerDetails);
        bool RemovePlayer(DepthChartModel playerDetails);
        List<DepthChartModel> GetBackups(DepthChartModel playerDetails);
        Dictionary<string, List<DepthChartModel>> GetFullChart();
        void SetJsonFile(string filePath);
        List<DepthChartModel> LoadPlayerDepthChart();
    }
}
