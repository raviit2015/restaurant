using ECommerce.Models;
using ECommerce.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private IConfiguration _configuration;
        string connectionString = "";
        SqlConnection connection = null;

        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionStrings:DefaultConnection"];
        }
        public JsonResponse<string> Login(User user)
        {
            JsonResponse<string> jsonResponse = new JsonResponse<string>();
            return jsonResponse;
        }

        public JsonResponse<User> UserLogin(User user)
        {
            JWTTokenGenerator jWTTokenGenerator = new JWTTokenGenerator();
            JsonResponse<User> jsonResponse = new JsonResponse<User>();
            User userDetails = new User();
            DataTable dataTable = new DataTable();

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_login", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dataTable);
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    if (sqlReader.HasRows)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            userDetails.User_id = Convert.ToInt32(dataTable.Rows[i]["user_id"]);
                            userDetails.Email = dataTable.Rows[i]["email"].ToString();
                            userDetails.Name = dataTable.Rows[i]["name"].ToString();
                            userDetails.isAdmin = Convert.ToBoolean(dataTable.Rows[i]["is_admin"]);
                        }
                        userDetails.Token = jWTTokenGenerator.GetJWTToken(userDetails).ToString();
                        jsonResponse.data = userDetails;
                        jsonResponse.status = new ServiceStatus(200);
                    }
                    else
                    {
                        jsonResponse.status = new ServiceStatus(401);
                    }
                }
            }
            catch (Exception e)
            {
                jsonResponse.status = new ServiceStatus(500, e.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return jsonResponse;
        }

        public JsonResponse<int> UserSignUp(User user)
        {
            JsonResponse<int> jsonResponse = new JsonResponse<int>();
            int recordAffected = 0;
            try
            {
                if (!IfUserExists(user.Email))
                {
                    using (connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("sp_signup", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email",user.Email);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@name", user.Name);
                        cmd.Parameters.AddWithValue("@is_admin",false);
                        recordAffected = cmd.ExecuteNonQuery();
                    }
                    if (recordAffected > 0)
                    {
                        jsonResponse.data = recordAffected;
                        jsonResponse.status = new ServiceStatus(200);
                    }
                }
                else
                {
                    jsonResponse.status = new ServiceStatus(409, "User already exists with the email " + user.Email);
                }
            }
            catch (Exception ex)
            {
                jsonResponse.status = new ServiceStatus(200, ex.Message.ToString());
            }
           
            return jsonResponse;
        }


        public JsonResponse<User> GetUserByUserID(string userID)
        {
            JsonResponse<User> jsonResponse = new JsonResponse<User>();
            User userDetails = new User();
            DataTable dataTable = new DataTable();

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_get_user_by_userID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userID));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dataTable);
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    if (sqlReader.HasRows)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            userDetails.User_id = Convert.ToInt32(dataTable.Rows[i]["user_id"]);
                            userDetails.Email = dataTable.Rows[i]["email"].ToString();
                            userDetails.Name = dataTable.Rows[i]["name"].ToString();
                            userDetails.isAdmin = Convert.ToBoolean(dataTable.Rows[i]["is_admin"]);
                        }
                        //userDetails.Token = GetJWTToken(userDetails).ToString();
                        jsonResponse.data = userDetails;
                        jsonResponse.status = new ServiceStatus(200);
                    }
                    else
                    {
                        jsonResponse.status = new ServiceStatus(401);
                    }
                }

            }
            catch (Exception e)
            {
                jsonResponse.status = new ServiceStatus(500, e.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return jsonResponse;
        }


        public bool IfUserExists(string email)
        {
            bool IsUserExists = false;
            DataTable dataTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_if_user_exists", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dataTable);
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    if (sqlReader.HasRows)
                    {
                        IsUserExists = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }
            return IsUserExists;
        }

     

        public JsonResponse<List<User>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        //public JsonResponse<List<User>> GetAllEmployees()
        //{
        //    JsonResponse<List<User>> jsonResponse = new JsonResponse<List<User>>();
        //    connection();
        //    List<User> EmpList = new List<User>();
        //    try
        //    {
        //        SqlCommand com = new SqlCommand("PaginationWith2012", con);
        //        com.Parameters.AddWithValue("@start", 0);
        //        com.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(com);
        //        DataTable dt = new DataTable();

        //        con.Open();
        //        da.Fill(dt);
        //        con.Close();
        //        //Bind EmpModel generic list using dataRow     
        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            //EmpList.Add(
        //            //    new User
        //            //    {
        //            //        DummyID = Convert.ToInt32(dr["DummyID"]),
        //            //        Name = Convert.ToString(dr["Name"]),
        //            //        Details = Convert.ToString(dr["Details"])
        //            //    }
        //            //    );
        //        }
        //        jsonResponse.data = EmpList;
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return jsonResponse;
        //}
        //public JsonResponse<List<ProductResponse>> GetAllProduct()
        //{
        //    JsonResponse<List<ProductResponse>> jsonResponse = new JsonResponse<List<ProductResponse>>();
        //    connection();
        //    List<User> userList = new List<User>();
        //    try
        //    {
        //        SqlCommand com = new SqlCommand("PaginationWith2012", con);
        //        com.Parameters.AddWithValue("@start", 0);
        //        com.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(com);
        //        DataTable dt = new DataTable();

        //        con.Open();
        //        da.Fill(dt);
        //        con.Close();
        //        //Bind EmpModel generic list using dataRow     
        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            //userList.Add(
        //            //    new User
        //            //    {
        //            //        DummyID = Convert.ToInt32(dr["DummyID"]),
        //            //        Name = Convert.ToString(dr["Name"]),
        //            //        Details = Convert.ToString(dr["Details"])
        //            //    }
        //            //    );
        //        }
        //        //jsonResponse.data = userList;
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return jsonResponse;
        //}
    }
}
