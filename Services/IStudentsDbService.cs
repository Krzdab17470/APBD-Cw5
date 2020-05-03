using APBD_Cw3.DTOs.Requests;
using APBD_Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Cw3.Services
{
    public interface IStudentsDbService
    {
        Enrollment EnrollStudent(EnrollStudentRequest request);

        Enrollment PromoteStudents(int semester, string studies);
    }
}
