using APBD_Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Cw3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        
    }
}
