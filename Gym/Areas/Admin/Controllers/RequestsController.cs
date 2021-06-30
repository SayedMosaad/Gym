using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestsController : Controller
    {
        private readonly IApplicationRepository<Request> repository;

        public RequestsController(IApplicationRepository<Request> repository)
        {
            this.repository = repository;
        }
        public ActionResult Index(int PageNumber = 1)
        {
            int PageSize = 10;
            int excludeRecord = (PageSize * PageNumber) - PageSize;
            var requests = repository.List().Skip(excludeRecord).Take(PageSize);

            var r = repository.List();
            var result = new PagedResult<Request>
            {
                Data = requests.ToList(),
                TotalItems = repository.List().Count(),
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            return View(result);
        }
    }
}
