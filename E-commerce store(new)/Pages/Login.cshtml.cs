using E_commerce_store_new_.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace E_commerce_store_new_
{
    public class LoginModel : PageModel
    {
        public void OnPost()
        {
            // Get the values entered in the form
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // Create a SqlConnection object
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                // Create a SqlCommand object with a parameterized query to check if a row exists with the given username and password
                string sql = "SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName AND Password = @Password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);

                // Execute the query and get the result
                int count = (int)cmd.ExecuteScalar();

                // Check if a row exists with the given username and password
                if (count > 0)
                {
                    // The user is authenticated
                    // Add a cookie to the response with the username
                    Response.Cookies.Append("username", username, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    // Redirect to the home page or some other page
                    Response.Redirect("/Products2");
                }
                else
                {
                    // The user is not authenticated
                    // Show an error message or redirect to the login page
                    Response.Redirect("/Login");
                }
            }
        }
    }
}
