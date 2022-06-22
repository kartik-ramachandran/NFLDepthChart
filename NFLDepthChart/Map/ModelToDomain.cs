namespace NFLDepthChart.Map
{
    public static class ModelToDomain
    {
        public static DepthChartModel Map (this PlayerDetail playerDetail)
        {
            var returnModel = new DepthChartModel
            {
                Number = playerDetail.Number,
                Name = playerDetail.Name,
                PositionDepth = playerDetail.PositionDepth,
                Position = playerDetail.Position
            };

            return returnModel;
        }
    }
}
