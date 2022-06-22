using Microsoft.AspNetCore.Mvc;
using NFLDepthChart.Business.Interface;
using NFLDepthChart.Map;

namespace NFLDepthChart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepthChartController : ControllerBase
    {
        private readonly ILogger<DepthChartController> _logger;
        private readonly IDepthChartAction _depthChartAction;

        public DepthChartController(ILogger<DepthChartController> logger, IDepthChartAction depthChartAction)
        {
            _logger = logger;
            _depthChartAction = depthChartAction;
        }

        [HttpGet]
        public Dictionary<string, List<PlayerDetail>> Get()
        {
            return _depthChartAction.GetFullChart().Map();            
        }

        [HttpPost("AddPlayer")]
        public IEnumerable<PlayerDetail> AddPlayer([FromBody] PlayerDetail playerDetails)
        {
            _depthChartAction.AddPlayer(playerDetails.Map());

            return new List<PlayerDetail>();
        }

        [HttpPost("RemovePlayer")]
        public void RemovePlayer([FromBody] PlayerDetail playerDetails)
        {
            _depthChartAction.RemovePlayer(playerDetails.Map());
        }

        [HttpPost("GetBackups")]
        public List<PlayerDetail> GetBackups([FromBody] PlayerDetail playerDetails)
        {
            var returnList = _depthChartAction.GetBackups(playerDetails.Map()).Map();

            return returnList;
        }
    }
}