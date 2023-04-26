using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebApp.Pages
{
    public class TestAzSQLDBModel : PageModel
    {
        public IList<City>? Cities { get; set; }

        public void OnGet()
        {
            Cities = GetData();
        }

        public List<City> GetData()
        {
            List<City> cities = new List<City>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "akslabsqlsrv.database.windows.net";
                builder.UserID = "app-user";
                builder.Password = "SomeVery_STRONG_Passw0rd!";
              builder.InitialCatalog = "aksDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    string sql = "SELECT name, callcode, trafficcode FROM dbo.cities";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var cname = reader["name"].ToString();
                                int ccode, tcode;
                                int.TryParse(reader["callcode"].ToString(), out ccode);
                                int.TryParse(reader["trafficcode"].ToString(), out tcode);
                                cities.Add(new City(cname, ccode, tcode));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return null;
            }

            return cities;
        }

    }
}
