using System.Data;
using System.Data.SqlClient;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
        string tableName = "Customers";
        var filePath = string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split("\\").SkipLast(4)) + @"\Data\Customers.csv";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = tableName;

                var dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Address", typeof(string));
                dataTable.Columns.Add("City", typeof(string));
                dataTable.Columns.Add("Country", typeof(string));
                dataTable.Columns.Add("PostalCode", typeof(string));
                dataTable.Columns.Add("Phone", typeof(string));

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
                bulkCopy.WriteToServer(dataTable);
            }
        }
    }
}
