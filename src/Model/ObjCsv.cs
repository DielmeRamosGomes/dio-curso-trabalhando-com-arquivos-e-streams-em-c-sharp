using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;

namespace dio_curso_arquivos_streams.src.Model
{
    public class ObjCsv
    {
        public static void ReadArquiveCsv()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "usuarios-exportacao.csv");
            if (File.Exists(path))
            {
                using var streamReader = new StreamReader(path);
                var header = streamReader.ReadLine()?.Split(',');
                while (true)
                {
                    var register = streamReader.ReadLine()?.Split(',');
                    if (register == null) break;
                    if (header?.Length != register.Length)
                    {
                        Console.WriteLine("Arquivo fora do padrão");
                        break;
                    }
                    for (int i = 0; i < register.Length; i++)
                    {
                        Console.WriteLine($"{header?[i]}: {register[i]}");
                    }
                    Console.WriteLine("--------------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"O caminho {path} não existe");
            } 
        }

        public static void WriteArquiveCsv()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Saida");
            var persons = new List<Person>()
            {
                new Person()
                {
                    Name = "Jose da Silva",
                    Email = "js@gmail.com",
                    Telephone = 12346,
                    Birth = new DateOnly(year: 1970, month: 2, day: 14)
                },
                new Person
                {
                    Name = "Pedro Paiva",
                    Email = "pp@gmail.com",
                    Telephone = 456789,
                    Birth = new DateOnly(year: 2002, month: 4, day: 20)
                },
                new Person
                {
                    Name = "Maria Antonia",
                    Email = "ma@gmail.com",
                    Telephone = 12346,
                    Birth = new DateOnly(year: 1982, month: 12, day: 4)
                },
                new Person
                {
                    Name = "Carla Moraes",
                    Email = "cms@gmail.com",
                    Telephone = 9987411,
                    Birth = new DateOnly(year: 2000, month: 7, day: 20)
                }
            };
            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
                path = Path.Combine(path, "usuarios.csv");
            }
            path = Path.Combine(path, "usuarios.csv");
            using var streamWriter = new StreamWriter(path);
            streamWriter.WriteLine("nome,email,telefone,nascimento");
            foreach (var person in persons)
            {
                var line = $"{person.Name},{person.Email},{person.Telephone},{person.Birth}";
                streamWriter.WriteLine(line);
            }
        }

        public static void ReaderArquiveFromCsvHelperDynamic()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "produtos.csv");
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException($"O caminho {path} não existe!");
            using var streamReader = new StreamReader(fileInfo.FullName);
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            using var csvReader = new CsvReader(streamReader, csvConfiguration);
            var register = csvReader.GetRecords<dynamic>();
            foreach (var record in register)
            {
                Console.WriteLine($"Produto: {record.Produto}");
                Console.WriteLine($"Marca: {record.Marca}");
                Console.WriteLine($"Preço: {record.Preco}");
                Console.WriteLine("---------------------------------");
            }
        }

        public static void ReaderArquiveFromCsvHelperClass()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "products.csv");
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException($"O caminho {path} não existe!");
            using var streamReader = new StreamReader(fileInfo.FullName);
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            using var csvReader = new CsvReader(streamReader, csvConfiguration);
            var records = csvReader.GetRecords<Products>();
            foreach (var record in records)
            {
                Console.WriteLine($"Name: {record.Name}");
                Console.WriteLine($"Brand: {record.Brand}");
                Console.WriteLine($"Price: {record.Price}");
                Console.WriteLine("---------------------------------");
            }
        }

        public static void ReaderArquiveFromCsvHelperClassWithDelimiter()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "productswithpont.csv");
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException($"O caminho {path} não existe!");
            using var streamReader = new StreamReader(fileInfo.FullName);
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            using var csvReader = new CsvReader(streamReader, csvConfiguration);
            var records = csvReader.GetRecords<Products>();
            foreach (var record in records)
            {
                Console.WriteLine($"Name: {record.Name}");
                Console.WriteLine($"Brand: {record.Brand}");
                Console.WriteLine($"Price: {record.Price}");
                Console.WriteLine("---------------------------------");
            }
        }
    }
}
