using HiveMQtt;
using Microsoft.Data.SqlClient;
namespace SQL;
public class SqlRepository
{
  string connectionString; // link met database
  public SqlRepository(string connectionString)
  {
    this.connectionString = connectionString;
  }

  public List<Schema> SqlRetrieveMethodeSchema()
 {
    List<Schema> list = new List<Schema>();
    using (var connection = new SqlConnection(connectionString)) // maken van connectie
    {
        connection.Open(); // open connectie
        using (var command = connection.CreateCommand())
        {
            // SQL-query met TOP en parameter om SQL-injectie te voorkomen
            command.CommandText = "SELECT TOP 7 TijdsWaarde FROM [Schema]";
            using (var reader = command.ExecuteReader()) // uitvoeren van de query
            {
                while (reader.Read()) // zolang er data is
                {
                    Schema schema = new Schema() // data object maken
                    {
                        tijdsWaarde = reader.GetString(0)
                    };

                    list.Add(schema); // voeg object toe aan lijst
                }
            }
        }
        connection.Close(); // sluit connectie
    }
    return list;
 }
} 