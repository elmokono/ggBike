using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BikeGoGreen.Controllers
{
    public class StatisticsController : ApiController
    {
        [Route("api/Statistics/CurrentSpeed/{id}/{interval}")]
        [HttpGet]
        public double CurrentSpeed(string id, int interval)
        {
            using (var db = new Model.DatabaseModelDataContext())
            {
                var q = db.GetCurrent(id, interval).ToList();
                if (q.Any())
                {
                    return Convert.ToDouble(q.First().kmh ?? 0);
                }
                return 0;
            }
        }

        [Route("api/Statistics/Runner/{id}")]
        [HttpGet]
        public Model.GetRunnerResult Runner(int id)
        {
            using (var db = new Model.DatabaseModelDataContext())
            {
                var r = db.GetRunner(id).ToList().FirstOrDefault();
                if (r == null)
                {
                    return new Model.GetRunnerResult
                    {
                        hs = 0,
                        runnerId = id,
                        kmh = 0,
                        meters = 0,
                        ms = 0,
                        rpm = 0,
                        spins = 0,
                    };
                }
                return r;
            }
        }

        [Route("api/Statistics/TopTen")]
        [HttpGet]
        public IEnumerable<Model.GetRunnerResult> TopTen()
        {
            using (var db = new Model.DatabaseModelDataContext())
            {
                return db.GetRunner(-1)
                    .OrderByDescending(x => x.meters)
                    .Take(10)
                    .ToList();
            }
        }

        [Route("api/Statistics/Runners")]
        [HttpGet]
        public IEnumerable<Models.StatisticsRunner> Runners()
        {
            using (var db = new Model.DatabaseModelDataContext())
            {
                var r = db.Runners.OrderBy(x => x.FullName)
                    .Select(x => new Models.StatisticsRunner
                    {
                        Email = x.EMail,
                        FullName = x.FullName,
                        Id = x.Id,
                    })
                    .ToList();

                return r;
            }
        }
        
        [Route("api/Statistics/StopTrial/{id}")]
        [HttpPost]
        public bool StopTrial(string id)
        {
            using (var db = new Model.DatabaseModelDataContext())
            {
                db.StopTrial(id);
                db.SubmitChanges();
                return true;
            }
        }

        [Route("api/Statistics/StartTrial/{id}/{runnerId}")]
        [HttpPost]
        public bool StartTrial(string id, int runnerId)
        {
            if (runnerId == -2) { return false; }

            using (var db = new Model.DatabaseModelDataContext())
            {
                db.StartTrial(id, runnerId);
                db.SubmitChanges();
                return true;
            }
        }
    }
}