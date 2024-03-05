using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace E_commerce_store_new_.Pages

{
    public class CheckoutModel : PageModel
    {
        public string Username { get; set; }
        public List<CartContains> UserCart = new List<CartContains>();
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




            /* var message = new MailMessage();
            message.From = new MailAddress("deluxemart666@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Order Submitted";
            message.Body = "Thank you for your order! We have received your order and will process it as soon as possible.";

            var client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("deluxemart666@gmail.com", "deluxe123mart");
            client.EnableSsl = true;
            client.Send(message);
            */

            string to = "deluxemart666@gmail.com"; //To address    
            string from = email; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("deluxemart666@gmail.com", "deluxe123mart");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
               
            }





        }

        public void OnGet()
        {

            OnPosttotalPrice();
            if (Request.Cookies != null && Request.Cookies["username"] != null)
            {
                Username = Request.Cookies["username"];
            }


            try
            {
                string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sqlQ = "SELECT CartId, p.ProductID, ProductName, ImageURL, Price, Quantity, DateAdded FROM Cart c JOIN Product p ON c.ProductId = p.ProductID " +
                     "WHERE c.Username = @username";

                    using (SqlCommand command = new SqlCommand(sqlQ, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@username", Username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CartContains c = new CartContains();

                                c.CartId = reader.GetInt32(0);
                                c.ProductId = reader.GetInt32(1);
                                c.Name = reader.GetString(2);
                                c.Image = reader.GetString(3);
                                c.Price = reader.GetDecimal(4);
                                c.Quantity = reader.GetInt32(5);

                                c.DateAdded = reader.GetDateTime(6);

                                UserCart.Add(c);
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

        public void OnPostRemoveFromCart(int productId)
        {

            if (Request.Cookies != null && Request.Cookies["username"] != null)
            {
                Username = Request.Cookies["username"];
            }

            // Insert the product into the cart database
            var connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DateTime now = DateTime.Now;

                var command = new SqlCommand("Delete from Cart where ProductId = @ProductId AND Username = @Username ", connection);

                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@Username", Username);

                command.ExecuteNonQuery();
            }
            OnPosttotalPrice();

            RedirectToPage("/Cart");

        }

        public void OnPosttotalPrice()
        {
            if (Request.Cookies != null && Request.Cookies["username"] != null)
            {
                Username = Request.Cookies["username"];
            }
            try
            {
                string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sqlQ = "SELECT SUM(price) AS total_price FROM Cart c join Product p on c.ProductId = p.ProductID " +
                   "where c.Username = @Username";

                    using (SqlCommand command = new SqlCommand(sqlQ, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Username", Username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal totalPrice = reader.GetDecimal(reader.GetOrdinal("total_price"));
                                ViewData["totalPrice"] = totalPrice.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine(ex.ToString());
                // Set a default value for the total price
                ViewData["totalPrice"] = "N/A";
            }
        }


    }

    public class CartContains
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public DateTime DateAdded { get; set; }
    }


}