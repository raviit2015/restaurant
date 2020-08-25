using ECommerce.Models;
using ECommerce.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IConfiguration _configuration;
        string connectionString = "";
        SqlConnection connection = null;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionStrings:DefaultConnection"];
        }

      

        public JsonResponse<List<Item>> GetAllItems()
        {
            JsonResponse<List<Item>> jsonResponse = new JsonResponse<List<Item>>();
            try
            {
                jsonResponse.data = GetAllItems2();
                jsonResponse.status = new ServiceStatus(200);
            }
            catch (Exception ex)
            {

            }
           
            return jsonResponse;
        }



        public JsonResponse<List<Item>> GetItemCategories()
        {
            JsonResponse<List<Item>> jsonResponse = new JsonResponse<List<Item>>();
            try
            {
                jsonResponse.data = GetItemsCategories2();
                jsonResponse.status = new ServiceStatus(200);
            }
            catch (Exception ex)
            {

            }

            return jsonResponse;
        }

        private List<Item> GetAllItems2()
        {
            DataTable ResultDT = new DataTable();
            ResultDT = GetAllItems3();
            List<Item> Studentlist = new List<Item>();
            Studentlist = DataTableToList.ConvertToList<Item>(ResultDT);
            return Studentlist;
        }
        private DataTable GetAllItems3()
        {
            DataTable dt = new DataTable();
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_get_all_items", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        private List<Item> GetItemsCategories2()
        {
            DataTable ResultDT = new DataTable();
            ResultDT = GetItemsCategories3();
            List<Item> Studentlist = new List<Item>();
            Studentlist = DataTableToList.ConvertToList<Item>(ResultDT);
            return Studentlist;
        }
        private DataTable GetItemsCategories3()
        {
            DataTable dt = new DataTable();
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_get_item_categories", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

    }
}
