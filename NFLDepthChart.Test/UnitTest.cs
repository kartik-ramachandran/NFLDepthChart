using Microsoft.Extensions.DependencyInjection;
using NFLDepthChart.Business;
using NFLDepthChart.Business.Interface;

namespace NFLDepthChart.Test
{
    public class Tests
    {
        private string JsonFile = string.Empty;
        private ServiceProvider serviceProvider;
        private IDepthChartAction _depthChartService;
        [SetUp]
        public void Setup()
        {
            JsonFile = @"D:\DepthChart\NFLDepthChart\NFLDepthChart.Test\Data\PlayerModel.json";

            var services = new ServiceCollection();

            services.AddTransient<IDepthChartAction, DepthChartAction>();

            serviceProvider = services.BuildServiceProvider();

            _depthChartService = serviceProvider.GetService<IDepthChartAction>();

            _depthChartService?.SetJsonFile(JsonFile);
        }

        [Test]
        public void Check_FullChart_Success()
        {
            Dictionary<string, List<DepthChartModel>> returnValue =
                _depthChartService.GetFullChart();
            Assert.That(returnValue.Keys.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Check_AddPlayer_Success()
        {
            var depthChartModel = new DepthChartModel
            {
                Name = "Test123",
                Number = "123",
                Position = "RB",
                PositionDepth = "1"
            };

            _depthChartService.AddPlayer(depthChartModel);

            var allPlayersDetails = _depthChartService.LoadPlayerDepthChart();

            var hasPlayerBeenAdded = allPlayersDetails.Any(player => player.Name == depthChartModel.Name);

            Assert.That(hasPlayerBeenAdded, Is.True);
        }

        [Test]
        public void Check_GetBackup_Success()
        {
            var depthChartModel1 = new DepthChartModel
            {
                Name = "Test124",
                Number = "124",
                Position = "RB",
                PositionDepth = "2"
            };
            _depthChartService.AddPlayer(depthChartModel1);

            var depthChartModel2 = new DepthChartModel
            {
                Name = "Test125",
                Number = "125",
                Position = "RB",
                PositionDepth = "3"
            };
            _depthChartService.AddPlayer(depthChartModel2);

            var depthChartModel3 = new DepthChartModel
            {
                Name = "Test126",
                Number = "126",
                Position = "RB",
                PositionDepth = "4"
            };

            _depthChartService.AddPlayer(depthChartModel3);

            var returnedValue = _depthChartService.GetBackups(depthChartModel1);

            Assert.That(returnedValue.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Check_AddPlayerToMiddle_Success()
        {
            var depthChartModel = new DepthChartModel
            {
                Name = "Test129",
                Number = "129",
                Position = "RB",
                PositionDepth = "2"
            };

            _depthChartService.AddPlayer(depthChartModel);

            var allPlayersDetails = _depthChartService.LoadPlayerDepthChart();

            var hasPlayerBeenAdded = allPlayersDetails.Any(player => player.Name == depthChartModel.Name);

            Assert.That(hasPlayerBeenAdded, Is.True);
        }

        [Test]
        public void Check_RemovePlayerFromEnd_Success()
        {
            var depthChartModel = new DepthChartModel
            {
                Name = "Test127",
                Number = "127",
                Position = "TE",
                PositionDepth = "1"
            };
            _depthChartService.AddPlayer(depthChartModel);

            _depthChartService.RemovePlayer(depthChartModel);

            var allPlayersDetails = _depthChartService.LoadPlayerDepthChart();

            var hasPlayerBeenRemoved = allPlayersDetails.Any(player => player.Name == depthChartModel.Name);

            Assert.That(hasPlayerBeenRemoved, Is.False);
        }

        [Test]
        public void Check_RemovePlayerFromMiddle_Success()
        {
            var depthChartModel = new DepthChartModel
            {
                Name = "Test129",
                Number = "129",
                Position = "RB",
                PositionDepth = "2"
            };

            _depthChartService.RemovePlayer(depthChartModel);

            var allPlayersDetails = _depthChartService.LoadPlayerDepthChart();

            var hasPlayerBeenRemoved = allPlayersDetails.Any(player => player.Name == depthChartModel.Name);

            Assert.That(hasPlayerBeenRemoved, Is.False);
        }
    }
}