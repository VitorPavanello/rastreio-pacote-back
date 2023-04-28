using Microsoft.AspNetCore.Mvc;

namespace RastreioPacote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RastreioPacoteController : ControllerBase
    {
        private readonly ILogger<RastreioPacoteController> _logger;

        public RastreioPacoteController(ILogger<RastreioPacoteController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "RastrearPacote")]
        public List<RastreioPacoteReturn> Post([FromQuery] string numero)
        {
            try
            {
                var result = new Correios.NET.CorreiosService().GetPackageTracking(numero);

                if (result is null)
                {
                    return new List<RastreioPacoteReturn>();
                }

                var returnList = new List<RastreioPacoteReturn>();

                foreach (var track in result.TrackingHistory)
                {
                    var historico = new RastreioPacoteReturn();

                    historico.Date = track.Date.ToString("dd/MM/yyyy hh:mm tt");
                    historico.Source = track.Source;
                    historico.Status = track.Status;

                    returnList.Add(historico);
                }

                return returnList;
            }
            catch(Exception ex)
            {
                return new List<RastreioPacoteReturn>();
            }
            
        }
    }
}