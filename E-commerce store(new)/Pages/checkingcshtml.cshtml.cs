using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;

namespace E_commerce_store_new_.Pages
{
    public class checkingcshtmlModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()

        {

            //Response.Redirect("/Login");
            string firstName = Request.Form["FIRSTNAME"];
            string lastName = Request.Form["LASTNAME"];
            string email = Request.Form["EMAIL"];
            string address = Request.Form["ADDRESS"];
            string phoneNumber = Request.Form["PHONE"];
         




            try
            {
                // Set up the connection string
                string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



                // Set up the SQL connection and command objects
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create the SQL query to insert the data into the database
                    string query = "INSERT INTO Customer (First_name, Last_name, Email, Address, PhoneNumber) VALUES (@First_name, @Last_name, @Email, @Address,@PhoneNumber)";



                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameters to the command object
                        command.Parameters.AddWithValue("@First_name", firstName);
                        command.Parameters.AddWithValue("@Last_name", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);


                        // Open the connection and execute the command

                        command.ExecuteNonQuery();
                    }
                }

                // Redirect the user to the home page after sign-up


            }
            catch (Exception ex)
            {
                // Handle the exception here, e.g. log the error or display a message to the user
            }

        }


        }
    }
