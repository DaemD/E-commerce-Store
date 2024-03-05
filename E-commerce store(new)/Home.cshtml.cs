using E_commerce_store_new_.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace E_commerce_store_new_
{
    public class HomeModel : PageModel
    {
        public List<productInfo> products = new List<productInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sqlQ = "SELECT * FROM Product";

                    using (SqlCommand command = new SqlCommand(sqlQ, sqlConnection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productInfo product = new productInfo();

                                product.pid = reader.GetInt32(0);
                                product.name = reader.GetString(1);
                                product.description = reader.GetString(2);
                                product.imageURL = reader.GetString(3);
                                product.cat = reader.GetInt32(4);
                                product.price = reader.GetDecimal(5);
                                product.stock = reader.GetInt32(6);

                                products.Add(product);
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :" + ex.ToString());
            }
        }
    }
    public class productInfo
    {
        public int pid;
        public string name;
        public string description;
        public string imageURL;
        public int cat;
        public decimal price;
        public int stock;
    }
}
