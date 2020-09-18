using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly DaisyContext _context;

        public ReportController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public ActionResult<IEnumerable<Report>> GetReport()
        {
            return _context.Report.ToList();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Report>> GetReport(int id)
        {
            var query = from report in _context.Set<Report>()
                        where
                        report.ReportId == id
                        select report;

            if (query == null)
            {
                return NotFound();
            }

            return query.ToList();
        }

        // PUT: api/Reports/5?status=
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutReport(int id, string status){
            var report = _context.Report.Find(id);
            if(report == null) {
                return BadRequest();
            }
            if(status!= "successful" && status != "failed"){
                return BadRequest();
            }

           report.DealStatus = status;

            try{
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException){
                    throw;
            }

            return NoContent();
        }

        // POST: api/Reports
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Report> PostReport(Report report)
        {
            _context.Report.Add(report);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReportExists(report.ReportId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetReport", new { id = report.ReportId }, report);
        }


        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.ReportId == id);
        }
    }
}
