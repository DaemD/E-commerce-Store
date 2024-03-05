using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace E_commerce_store_new_.Pages
{


    public class ReviewsModel : PageModel
    {

        public List<Review> Revs = new List<Review>();
  
        /*public void OnGet()
        {

            try
            {
                string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sqlQ = "SELECT * FROM Review";

                    using (SqlCommand command = new SqlCommand(sqlQ, sqlConnection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Review r = new Review();

                                r.rid = reader.GetInt32(0);
                                r.cid = reader.GetInt32(1);
                                r.pid = reader.GetInt32(2);
                                r.rat = reader.GetInt32(3);
                                r.cmnt = reader.GetString(4);
                                r.DateAdded = reader.GetDateTime(5);
                                r.imageURL = reader.GetString(6);


                                Revs.Add(r);
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
        */
        public void OnGet()
        {
            // OnGetMostSelling();

            try
            {
                string connectionString = "Data Source=DELL3501SSD\\SQLEXPRESS;Initial Catalog=store(ecommerce);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sqlQ = "SELECT * FROM Review";

                    using (SqlCommand command = new SqlCommand(sqlQ, sqlConnection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Review r = new Review();

                                r.rid = reader.GetInt32(0);
                                r.cid = reader.GetInt32(1);
                                r.pid = reader.GetInt32(2);
                                r.rat = reader.GetInt32(3);
                                r.cmnt = reader.GetString(4);
                                r.DateAdded = reader.GetDateTime(5);
                                r.imageURL = reader.GetString(6);


                                Revs.Add(r);
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



    public class Review
    {
        public int rid { get; set; }
        public int cid { get; set; }
        public int pid { get; set; }
        public int rat { get; set; }

        public string imageURL { get; set; }
        public DateTime DateAdded { get; set; }
        public string cmnt { get; set; }
       

    }
}
