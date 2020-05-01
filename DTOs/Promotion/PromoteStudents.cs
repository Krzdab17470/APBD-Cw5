using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Cw3.DTOs.Promotion
{
    public class PromoteStudents
    {

		[Required]
		public string Studies { get; set; }

		[Required]
		public string Semester { get; set; }
	}
}
