using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APBD_Cw3.DAL;
using APBD_Cw3.Models; //!!!
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Data;

//test


namespace APBD_Cw3.Controllers
{

    [ApiController]
    [Route("api/students")]
    //https://localhost:44362/api/students/

        //test commentaa
    public class StudentsController : ControllerBase
    {


        //ZAD 1 
        string message = "";
        [HttpGet("{id}")]
        //https://localhost:44362/api/students/1059
        //https://localhost:44362/api/students/1059;DROP%20TABLE%20STUDENT
   

        public IActionResult GetStudents(string id)
        {

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "Select Enrollment.IdEnrollment, Semester, IndexNumber, Studies.Name From Enrollment, Student, Studies WHERE Enrollment.IdEnrollment = Student.IdEnrollment AND Enrollment.IdStudy = Studies.IdStudy AND IndexNumber =@id";
                com.Parameters.AddWithValue("id", id);
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    message = string.Concat(message, '\n', dr["IdEnrollment"].ToString(), ", ", dr["Semester"].ToString(), ", ", dr["IndexNumber"].ToString(), ", ", dr["Name"].ToString());
                }
            }
            return Ok(message);
        }

    }

         



}

            