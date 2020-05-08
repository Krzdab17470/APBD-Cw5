using APBD_Cw3.DTOs.Promotion;
using APBD_Cw3.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Cw3.Controllers
{ 
    [Microsoft.AspNetCore.Mvc.Route("api/enrollments/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private IStudentsDbService _service;


        public PromotionController(IStudentsDbService service)
        {
            _service = service;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult PromotionStudents(PromoteStudents request)
        {
            return Ok(_service.PromoteStudents(request.Semester, request.Studies));
        }
    }
}
