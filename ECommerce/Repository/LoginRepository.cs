using ECommerce.Models;
using ECommerce.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private IConfiguration _configuration;
        private SqlConnection con;
        //To Handle connection related activities    
       
        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private void connection()
        {
            string constr = _configuration["ConnectionStrings:DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }
        public JsonResponse<string> Login(User user)
        {
            JsonResponse<string> jsonResponse = new JsonResponse<string>();
            jsonResponse.data = "Login OK";
            jsonResponse.status = new ServiceStatus(200);
            return jsonResponse;
        }

        public JsonResponse<List<User>> GetAllEmployees()
        {
            JsonResponse<List<User>> jsonResponse = new JsonResponse<List<User>>();
            connection();
            List<User> EmpList = new List<User>();
            try
            {
                SqlCommand com = new SqlCommand("PaginationWith2012", con);
                com.Parameters.AddWithValue("@start", 0);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind EmpModel generic list using dataRow     
                foreach (DataRow dr in dt.Rows)
                {

                    EmpList.Add(
                        new User
                        {
                            DummyID = Convert.ToInt32(dr["DummyID"]),
                            Name = Convert.ToString(dr["Name"]),
                            Details = Convert.ToString(dr["Details"])
                        }
                        );
                }
                jsonResponse.data = EmpList;
            }
            catch (Exception ex)
            {
            }
            
            return jsonResponse;
        }
        public JsonResponse<List<ProductResponse>> GetAllProduct()
        {
            JsonResponse<List<ProductResponse>> jsonResponse = new JsonResponse<List<ProductResponse>>();
            connection();
            List<User> userList = new List<User>();
            try
            {
                SqlCommand com = new SqlCommand("PaginationWith2012", con);
                com.Parameters.AddWithValue("@start", 0);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind EmpModel generic list using dataRow     
                foreach (DataRow dr in dt.Rows)
                {

                    userList.Add(
                        new User
                        {
                            DummyID = Convert.ToInt32(dr["DummyID"]),
                            Name = Convert.ToString(dr["Name"]),
                            Details = Convert.ToString(dr["Details"])
                        }
                        );
                }
                //jsonResponse.data = userList;
            }
            catch (Exception ex)
            {
            }

            return jsonResponse;
        }
    }
}
