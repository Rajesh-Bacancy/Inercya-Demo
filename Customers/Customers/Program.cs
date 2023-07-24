using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {           
            string connectionString = ConfigurationManager.ConnectionStrings["AppConnectionString"]?.ConnectionString;
            string tableName = "Customers";
            var filePath = string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split("\\").SkipLast(4)) + @"\Data\Customers.csv";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = tableName;

                    //schema declaration
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Id", typeof(string));
                    dataTable.Columns.Add("Name", typeof(string));
                    dataTable.Columns.Add("Address", typeof(string));
                    dataTable.Columns.Add("City", typeof(string));
                    dataTable.Columns.Add("Country", typeof(string));
                    dataTable.Columns.Add("PostalCode", typeof(string));
                    dataTable.Columns.Add("Phone", typeof(string));

                    //reading data from csv and storing in dataTable
                    using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Windows-1252")))
                    {
                        string line;
                        reader.ReadLine(); // To skip the header from the csv
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] fields = line.Split(';');
                            dataTable.Rows.Add(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6]);
                        }
                    }
                    //writing data in database
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong !", e);
        }
      
    }
}
