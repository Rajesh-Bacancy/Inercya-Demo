using System.Data;
using System.Text;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        try {
            var baseProjectPath = string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split("\\").SkipLast(4));
            var outputXMLFilePath = baseProjectPath + CommonConstants.PathToStoreCatalogXML;
            var outputJSONFilePath = baseProjectPath + CommonConstants.PathToStoreCatalogJSON;
            List<Category> categories = new List<Category>();
            List<Product> products = new List<Product>();
            List<Catalog> catalog = new List<Catalog>();

            ReadCategoriesFromCSV(baseProjectPath, categories);
            ReadProductsFromCSV(baseProjectPath, products);
            BuildCatalogList(categories, products, catalog);

            //writing data in json file
            string json = JsonSerializer.Serialize(catalog);
            File.WriteAllText(outputJSONFilePath, json);

            //writing data in xml file
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(catalog.GetType());
            using (StreamWriter writer = new StreamWriter(outputXMLFilePath))
            {
                x.Serialize(writer, catalog);
            }
        } 
        catch (Exception e){
            Console.WriteLine("Something went wrong !",e);
        }
    }

    private static void BuildCatalogList(List<Category> categories, List<Product> products, List<Catalog> catalog)
    {
        foreach (var category in categories)
        {
            catalog.Add(new Catalog
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Products = products.Where(p => p.CategoryId == category.Id).ToList()
            });
        }
    }

    private static void ReadProductsFromCSV(string? baseProjectPath,List<Product> products)
    {
        var filePath = baseProjectPath + @"\Data\Products.csv";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Console.WriteLine("fetching products data from csv has been started");

        using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Windows-1252")))
        {
            string line;
            reader.ReadLine(); // To skip the header from the csv
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                products.Add(new Product { Id = fields[0], CategoryId = fields[1], Name = fields[2], Price = fields[3] });
            }
        }

        Console.WriteLine("Products data has been fetched from csv");
    }

    private static void ReadCategoriesFromCSV(string? baseProjectPath, List<Category> categories)
    {
        var filePath = baseProjectPath + @"\Data\Categories.csv";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Console.WriteLine("fetching categories data from csv has been started");
        using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Windows-1252")))
        {
            string line;
            reader.ReadLine(); // To skip the header from the csv
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                categories.Add(new Category { Id = fields[0], Name = fields[1], Description = fields[2] });
            }
        }
        Console.WriteLine("Categories data has been fetched from csv");
    }
}

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Product
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string Price { get; set; }
    public string Name { get; set; }
}

public class Catalog
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; }
}

public static class CommonConstants
{
    public const string PathToStoreCatalogJSON = @"\Output\Catalog.json";
    public const string PathToStoreCatalogXML = @"\Output\Catalog.xml";
}