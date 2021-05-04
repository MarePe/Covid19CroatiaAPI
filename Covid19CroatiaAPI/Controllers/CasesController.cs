using Covid19CroatiaAPI.Data;
using Covid19CroatiaAPI.Entities;
using Covid19CroatiaAPI.Helpers;
using Covid19CroatiaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Controllers
{
    [ApiController]
    [Route("api/cases")]
    public class CasesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CasesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("latest")]
        public ActionResult<DailyCovidResult> GetLatest()
        {
            var entityCase = _context.DailyCovidOverviews.OrderByDescending(dailyData => dailyData.Date).FirstOrDefault();
            return DailyCovidDataMapper.Map(entityCase);

        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<DailyCovidResult>> GetAll()
        {
            var entityCases = _context.DailyCovidOverviews.OrderByDescending(dailyData => dailyData.Date);
            return DailyCovidDataMapper.Map(entityCases);
        }

        [HttpGet]
        [Route("filter")]
        public ActionResult<IEnumerable<DailyCovidResult>> GetCases([FromQuery]QueryModel queryModel)
        {            
            
            var entityCases = _context.DailyCovidOverviews
                .Where(dailyData => (queryModel.From == null || dailyData.Date >= queryModel.From)
                    && (queryModel.To == null || dailyData.Date <= queryModel.To)
                    && (queryModel.MinConfirmed == null || dailyData.DailyNewConfirmed >= queryModel.MinConfirmed)
                    && (queryModel.MaxConfirmed == null || dailyData.DailyNewConfirmed <= queryModel.MaxConfirmed))
                .OrderBy(dailyData => dailyData.Date);
            

            return DailyCovidDataMapper.Map(entityCases);


        }
    }
}
