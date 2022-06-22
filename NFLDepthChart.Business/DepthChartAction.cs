using NFLDepthChart.Business.Interface;
using System.Reflection;
using System.Text.Json;

namespace NFLDepthChart.Business
{
    public class DepthChartAction : IDepthChartAction
    {
        private string JsonFile = @"D:\DepthChart\NFLDepthChart\NFLDepthChart.Business\Data\PlayerModel.json";
               
        public void SetJsonFile(string filePath)
        {
            JsonFile = filePath;
        }

        public void AddPlayer(DepthChartModel playerDetails)
        {
            WriteToJson(playerDetails);
        }

        public bool RemovePlayer(DepthChartModel playerDetails)
        {
            var valuesInJson = LoadPlayerDepthChart();

            var itemToRemove = valuesInJson.Find(v => v.Position == playerDetails.Position && v.Name.ToLower().IndexOf(playerDetails.Name.ToLower()) != -1); 

            if (itemToRemove != null)
            {
                //move other players depth
                var otherPlayersInSamePosition = valuesInJson.FindAll(v => v.Position == playerDetails.Position && Convert.ToInt32(v.PositionDepth) > Convert.ToInt32(itemToRemove.PositionDepth));

                var hasValueBeenRemoved = valuesInJson.Remove(itemToRemove);

                otherPlayersInSamePosition.ForEach(o =>
                {
                    o.PositionDepth = (Convert.ToInt32(o.PositionDepth) - 1).ToString();
                });               

                if (hasValueBeenRemoved)
                {
                    WriteListToJson(valuesInJson);
                }

                return hasValueBeenRemoved;
            }

            return false;           
        }

        public List<DepthChartModel> GetBackups(DepthChartModel playerDetails)
        {
            var valuesInJson = LoadPlayerDepthChart();

            var filteredPlayersForPosition = valuesInJson.FindAll(v => v.Position == playerDetails.Position);

            var enteredPlayerDepth = filteredPlayersForPosition.Find(fp => fp.Name.ToLower().Equals(playerDetails.Name.ToLower()));

            return filteredPlayersForPosition.FindAll(fp => Convert.ToInt32(fp.PositionDepth) > Convert.ToInt32(enteredPlayerDepth?.PositionDepth));
        }

        public Dictionary<string, List<DepthChartModel>> GetFullChart()
        {
            var playerDetailsList = LoadPlayerDepthChart();

            return playerDetailsList.GroupBy(p => p.Position).ToDictionary(p => p.Key, p => p.ToList());
        }

        public List<DepthChartModel> LoadPlayerDepthChart()
        {
            using (StreamReader r = new StreamReader(JsonFile))
            {
                string json = r.ReadToEnd();
                return JsonSerializer.Deserialize<List<DepthChartModel>>(json);
            }
        }

        private void WriteToJson(DepthChartModel playerDetails)
        {
            var valueInJson = LoadPlayerDepthChart();

            if (CheckDuplicateRecord(playerDetails, valueInJson))
            {
                return;
            }

            GetPositionDepth(playerDetails, valueInJson);

            valueInJson.Add(playerDetails);

            WriteListToJson(valueInJson);
        }

        private void WriteListToJson(List<DepthChartModel> valueInJson)
        {
            using (StreamWriter r = new StreamWriter(JsonFile))
            {
                var serializedPlayerDetails = JsonSerializer.Serialize<List<DepthChartModel>>(valueInJson);
                r.Write(serializedPlayerDetails);
            }
        }

        private bool CheckIfPlayerExists(DepthChartModel playerDetails, List<DepthChartModel> playerDetailsList)
        {
            return playerDetailsList.Any(v => v.Number == playerDetails.Number && v.Name.ToLower().IndexOf(playerDetails.Name.ToLower()) != -1);
        }

        private bool CheckDuplicateRecord(DepthChartModel playerDetails, List<DepthChartModel> playerDetailsList)
        {
            return playerDetailsList.Any(v => v.Number == playerDetails.Number && v.Name.ToLower().IndexOf(playerDetails.Name.ToLower()) != -1 && v.Position == playerDetails.Position);
        }

        private void GetPositionDepth(DepthChartModel playerDetails, List<DepthChartModel> playerDetailsList)
        {
            //get last position depth and save it for the new player
            if (string.IsNullOrEmpty(playerDetails.PositionDepth))
            {
                var maxPositionDepth = playerDetailsList.FindAll(p => p.Position == playerDetails.Position).MaxBy(m => m.PositionDepth)?.PositionDepth;

                playerDetails.PositionDepth = (Convert.ToInt32(maxPositionDepth) + 1).ToString();
            }
            else
            {
                var playerDetailsForPosition = playerDetailsList.FindAll(p => p.Position == playerDetails.Position);

                var enteredPlayerDepth = Convert.ToInt32(playerDetails.PositionDepth);

                playerDetailsForPosition.ForEach(p => {
                    var existingPositionDepth = Convert.ToInt32(p.PositionDepth);

                    if(existingPositionDepth >= enteredPlayerDepth)
                    {
                        p.PositionDepth = (Convert.ToInt32(p.PositionDepth) + 1).ToString();
                    }
                });
            }

        }
    }
}