using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using HastaneAPI.Models;

namespace HastaneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoktorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DoktorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select DoktorId, DoktorName from dbo.Doktor";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HastaAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);

        }
        [HttpPost]
        public JsonResult Post(Doktor dok)
        {
            string query = @"
                            insert into dbo.Doktor values
                                ('" + dok.DoktorName + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HastaAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Ekleme Başarılı");
        }
        [HttpPut]
        public JsonResult Put(Doktor dok)
        {
            string query = @"
                            update dbo.Doktor set
                               DoktorName= '" + dok.DoktorName + @"'
                               where DoktorId=" + dok.DoktorId + @"
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HastaAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Güncelleme Başarılı");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id )
        {
            string query = @"
                            delete from dbo.Doktor
                          
                               where DoktorId=" + id+ @"
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HastaAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Silme Başarılı");
        }
    }
}
