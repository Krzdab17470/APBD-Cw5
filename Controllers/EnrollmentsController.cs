using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using APBD_Cw3.Models; //!!!!
using APBD_Cw3.DTOs.Requests; //!!!!
using APBD_Cw3.DTOs.Responses; //!!!!
using APBD_Cw3.DTOs.Promotion;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;


namespace APBD_Cw3.Controllers {

    //https://localhost:44362/api/students
    //http://localhost:51042/api/enrollments

        //ZAD 2:
        /*
        [Route("api/enrollments/promotions")];
        [ApiController];
    public class EnrollmentsController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult PromotionStudents(PromoteStudents request)
        {



            return Ok();
        }

    }
    */


        //ZAD 1:
       //   /*
    [Microsoft.AspNetCore.Mvc.Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request) //EnrollStudentRequest request
        {
            var st = new Student();
            st.LastName = request.LastName;
            st.FirstName = request.FirstName;
            st.BirthDate = request.BirthDate;
            st.Studies = request.Studies;
            st.Semester = 1;

           //var response = new EnrollStudentResponse();
           //response.LastName = st.LastName;
           //response.Semester = 1;
           //response.Index = request.IndexNumber;

            var enrollment = new Enrollment();
            enrollment.Semester = 1;

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
              
                com.Connection = con;
                con.Open(); //otiweramy polaczenie
                var tran = con.BeginTransaction(); //otwieramy nowa transakcje

                try
                {
                    //1. Spr czy istnieja studia
                    com.CommandText = "SELECT IdStudy FROM Studies WHERE name=@name"; //SQL command
                    com.Parameters.AddWithValue("name", request.Studies);  //set value to @name parameter
                    //response.StudiesName = request.Studies;
                    com.Transaction = tran; //musi byc przed ExecuteReader
                    var dr = com.ExecuteReader(); //odczytujemy efekt zapytania
                    if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                    {
                        dr.Close();
                        tran.Rollback(); //wycofujemy transakcje
                        return BadRequest("Studia nie istnieja.");  //musimy zwrocic blad
                    }
                    int idStudies = (int)dr["IdStudy"]; //bierzemy number id studiow, przyda sie pozniej...
                    //response.IdStudies = idStudies;
                    enrollment.IdStudies = idStudies;
                    dr.Close();

                    //2. Spr czy nr indexu studenta jest unikalny
                    com.CommandText = "SELECT INDEXNUMBER FROM STUDENT WHERE INDEXNUMBER = @Index";
                    com.Parameters.AddWithValue("Index", request.IndexNumber);
                    dr = com.ExecuteReader(); //odczytujemy efekt zapytania

                    if (dr.Read()) //jesli zapytanie cos zwrocilo...
                    {
                        dr.Close();
                        tran.Rollback(); //wycofujemy transakcje
                        return BadRequest("W bazie istnieje juz ten number indeksu.");  //musimy zwrocic blad
                    }

                    dr.Close();
                  
                    int IdEnrollment;

                    //3. odnajdujemy najnowszy wpis w tabeli Enrollments zgodny ze studiami studenta i wartoscia Semester = 1
                    com.CommandText = "Select IdEnrollment FROM Enrollment WHERE Semester = 1 AND IdStudy =" + idStudies;
                    dr = com.ExecuteReader(); //odczytujemy efekt zapytania
                    if (dr.Read()) //jesli zapytanie cos zwrocilo...
                    {
  
                        IdEnrollment = ((int) dr["IdEnrollment"]);
                        dr.Close();

                    }
                    else if (!dr.Read())
                    {
                        dr.Close();
                        com.CommandText = "Select IdEnrollment FROM Enrollment WHERE IdEnrollment = (Select MAX(IdEnrollment) FROM Enrollment)";
                        dr = com.ExecuteReader(); //Dodane
                        dr.Read();
                        IdEnrollment = ((int)dr["IdEnrollment"])+1;
                        dr.Close();
                        com.CommandText = "INSERT INTO ENROLLMENT (IDENROLLMENT,SEMESTER,IDSTUDY,STARTDATE) VALUES ("+IdEnrollment+",1," + idStudies + ", '2021-09-10')";

                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        return BadRequest("ERROR!");
                    };


                    //response.IdEnrollment = IdEnrollment;
                    enrollment.IdEnrollment = IdEnrollment;
                    
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName,LastName,BirthDate,IdEnrollment) VALUES (@index2,@firstName,@lastName,@birthDate,@idEnrollment)"; //nowe zapytanie, a raczej wypchniecie danych do tabeli
                    com.Parameters.AddWithValue("index2", request.IndexNumber); //przypisanie wartosci do powyzszych paramterow
                    com.Parameters.AddWithValue("firstName", request.FirstName);    //j.w.
                    com.Parameters.AddWithValue("lastName", request.LastName);
                    com.Parameters.AddWithValue("birthDate", "1993-09-11");
                    com.Parameters.AddWithValue("idEnrollment", IdEnrollment);
                    
                    com.ExecuteNonQuery(); //wypychamy zapytanie
                    tran.Commit(); //potwierdzamy transakcje

                    dr.Close();
                    com.CommandText = "Select * From Enrollment WHERE IdEnrollment = " + IdEnrollment;
                    dr = com.ExecuteReader();
                    string message = "";
                    while (dr.Read())
                    {
                        //message = string.Concat(message, '\n', "Enrollment ID:" , dr["IdEnrollment"].ToString(), ", Semester: ", dr["Semester"].ToString());
                        message = string.Concat(message, '\n', "Enrollment ID:", enrollment.IdEnrollment.ToString(), ", Semester: ", enrollment.Semester.ToString(), ", ID Studies: :", enrollment.IdStudies.ToString());
                    }


                    return Ok(message);

                }
                catch (SqlException exc) //wylapujemy ewentualny blad...
                {
                    tran.Rollback(); //...i w razie czego robimy rollback
                   
                    return BadRequest(exc.ToString());
                }

            }



            }

        }
        //*/
    }



