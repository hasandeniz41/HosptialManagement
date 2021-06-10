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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HastaneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HastaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public HastaController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select HastaId, HastaName,Doktor,Bolum,
                            Information from dbo.Hasta";
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
        public JsonResult Post(Hasta hasta)
        {
            string query = @"
                            insert into dbo.Hasta (HastaName,Doktor,Bolum,Information)
                                values
                                (
                                 '" + hasta.HastaName + @"'
                                  ,'" + hasta.Doktor + @"'
                                  ,'" + hasta.Bolum + @"'
                                     ,'" + hasta.Information + @"'
                                  )
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
            return new JsonResult("Ekleme Başarılı");
        }
        [HttpPut]
        public JsonResult Put(Hasta hasta)
        {
            string query = @"
                            update dbo.Hasta set
                               HastaName= '" + hasta.HastaName + @"',
                               Doktor='" + hasta.Doktor+ @"'
                                Bolum='"+ hasta.Bolum+ @"',
                                Information='" +hasta.Information+@"',
                                where HastaId= " + hasta.HastaId+@"
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
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Hasta
                          
                               where HastaId=" + id + @"
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
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using(var stream= new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
                 throw;
            }
        }
        [Route("api/Hasta/GetAllDoktorNames")]
        public JsonResult GetAllDoktorNames()
        {
            string query = @"
                            select DoktorName from dbo.Doktor";
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
    }
}
