namespace NFLDepthChart.Map
{
    public static class DomainToModel
    {
        public static List<PlayerDetail> Map(this List<DepthChartModel> playerDetail)
        {
            var returnModel = new List<PlayerDetail>();

            playerDetail.ForEach(p =>
            {
                returnModel.Add(new PlayerDetail
                {
                    Number = p.Number,
                    Name = p.Name,
                    PositionDepth = p.PositionDepth,
                    Position = p.Position
                });
            });

            return returnModel;
        }

        public static Dictionary<string, List<PlayerDetail>> Map(this Dictionary<string, List<DepthChartModel>> playerDetailsDictionary)
        {
            var returnModel = new Dictionary<string, List<PlayerDetail>>();

            var allKeys = playerDetailsDictionary.Keys.ToList();

            allKeys.ForEach(key =>
            {
                var keyValue = playerDetailsDictionary[key];

                if (keyValue != null)
                {
                    var mappedList = new List<PlayerDetail>();
                    keyValue.ForEach(value =>
                    {
                        mappedList.Add(new PlayerDetail
                        {
                            Number = value.Number,
                            Name = value.Name,
                            PositionDepth = value.PositionDepth,
                            Position = value.Position
                        });
                    });

                    returnModel.Add(key, mappedList.OrderBy(m => m.PositionDepth).ToList());
                }                
            });
            return returnModel;
        }
    }
}
