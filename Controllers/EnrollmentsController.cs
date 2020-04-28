using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using APBD_Cw3.Models; //!!!!
using APBD_Cw3.DTOs.Requests; //!!!!
using APBD_Cw3.DTOs.Responses; //!!!!
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;


namespace APBD_Cw3.Controllers {

    //https://localhost:44362/api/students
    //http://localhost:51042/api/enrollment

    [Microsoft.AspNetCore.Mvc.Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request) //EnrollStudentRequest request
        {
            var st = new Student();
            st.FirstName = request.FirstName;

            var response = new EnrollStudentResponse();
            response.LastName = st.LastName;

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
                    com.Transaction = tran; //musi byc przed ExecuteReader
                    var dr = com.ExecuteReader(); //odczytujemy efekt zapytania
                    if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                    {
                        tran.Rollback(); //wycofujemy transakcje
                        return BadRequest("Studia nie istnieja.");  //musimy zwrocic blad
                    }
                    int idStudies = (int)dr["IdStudy"]; //bierzemy number id studiow...
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

                    //3. Spr nr IdEnrollment zgodny z nr studiow i najwyzszy
                    com.CommandText = "Select IdEnrollment FROM Enrollment WHERE IdEnrollment = (Select MAX(IdEnrollment) FROM Enrollment)"; //WHERE Semester = 1 AND IdStudy =" + idStudies;
                    //com.CommandText = "select IdEnrollment FROM Enrollment WHERE IdEnrollment = SELECT MAX(IdEnrollment) FROM Enrollment WHERE Semester = 1 AND IdStudy =" + idStudies;
                    dr = com.ExecuteReader(); //odczytujemy efekt zapytania
                    if (dr.Read()) //jesli zapytanie cos zwrocilo...
                    {
                        //IdEnrollment = ((int) dr["IdEnrollment"]); //!!!!!!!!!!!!!!!!!!!!!!!
                        
                        IdEnrollment = ((int) dr["IdEnrollment"])+1;
                        dr.Close();
                        //com.CommandText = "INSERT INTO ENROLLMENT (IDENROLLMENT,SEMESTER,IDSTUDY,STARTDATE) VALUES (" + IdEnrollment.ToString() + ",1," + idStudies.ToString() + "," + DateTime.Now.ToString("yyyy-mm-dd").ToString() + ")";
                        com.CommandText = "INSERT INTO ENROLLMENT (IDENROLLMENT,SEMESTER,IDSTUDY,STARTDATE) VALUES ("+IdEnrollment+ ",1,10, '2021-09-10')";
                        //CONVERT(date,'" + DateTime.Now.ToString("yyyy-mm-dd") + "',0)
                        com.ExecuteNonQuery();

                    }
                    else if (!dr.Read())
                    {
                        IdEnrollment = 1;
                        dr.Close();
                        com.CommandText = "INSERT INTO ENROLLMENT (IDENROLLMENT,SEMESTER,IDSTUDY,STARTDATE) VALUES ("+IdEnrollment+",1," + idStudies + ", '2021-09-10')";
                        //DateTime.Now.ToString("yyyy-mm-dd")
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        return BadRequest("ERROR!");
                    };



                    
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName,LastName,BirthDate,IdEnrollment) VALUES (@index2,@firstName,@lastName,@birthDate,@idEnrollment)"; //nowe zapytanie, a raczej wypchniecie danych do tabeli
                    com.Parameters.AddWithValue("index2", request.IndexNumber); //przypisanie wartosci do powyzszych paramterow
                    com.Parameters.AddWithValue("firstName", request.FirstName);    //j.w.
                    com.Parameters.AddWithValue("lastName", request.LastName);
                    com.Parameters.AddWithValue("birthDate", "1993-09-11");
                    com.Parameters.AddWithValue("idEnrollment", IdEnrollment);
                    
                    //com.CommandText = "INSERT INTO Student(IndexNumber, FirstName,LastName,BirthDate,IdEnrollment) VALUES (111,'Agaton','Kuchniarz','2000-09-10',10)"; //!!!!!!!!!!!!!
                    com.ExecuteNonQuery(); //wypychamy zapytanie
                    tran.Commit(); //potwierdzamy transakcje

                    dr.Close();
                    com.CommandText = "Select * From Enrollment WHERE IdEnrollment = " + IdEnrollment;
                    dr = com.ExecuteReader();
                    string message = "";
                    while (dr.Read())
                    {
                        message = string.Concat(message, '\n', "Enrollment ID:" , dr["IdEnrollment"].ToString(), ", Semester: ", dr["Semester"].ToString());
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
    }



