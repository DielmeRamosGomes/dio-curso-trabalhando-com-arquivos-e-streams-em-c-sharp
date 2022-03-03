
namespace dio_curso_arquivos_streams.src.Model
{
    public class ObjArquive
    {
        public static void CreateArquive()
        {
            Console.WriteLine("Digite o nome do arquivo: ");
            var nameArquive = Console.ReadLine();

            if (nameArquive != null)
            {
                nameArquive = CleanName(nameArquive);
                var path = CreatePath(nameArquive);
                WriteArquive(path);
            }
            else
            {
                Console.WriteLine("Erro, nome do arquivo nao pode ser nulo!");
                return;
            }
        }

        static string CleanName(string nameArquive)
        {
            foreach (var caractere in Path.GetInvalidFileNameChars())
            {
                nameArquive = nameArquive.Replace(caractere, '-');
            }
            return nameArquive;
        }

        static void WriteArquive(string path)
        {
            try
            {
                var streamWriter = File.CreateText(path);
                streamWriter.WriteLine("Primeira linha do texto");
                streamWriter.WriteLine("Segunda linha do texto");
                streamWriter.WriteLine("Terceira linha do texto");
                streamWriter.Flush();
            }
            catch (Exception)
            {
                Console.WriteLine("Erro, nome do arquivo inválido!");
            }
        }

        public static string CreatePath(string nameArquive)
        {
            var path = Path.Combine(Environment.CurrentDirectory, $"{nameArquive}.txt");
            return path;
        }

        public static string CreatePathDirectory(string nameArquive)
        {
            var path = Path.Combine(Environment.CurrentDirectory, $"{nameArquive}");
            return path;
        }

        public static void MoveArquive(string origemPath, string destinyPath)
        {
            File.Move(origemPath, destinyPath);
        }

        public static void CopyArquive(string origemPath, string destinyPath)
        {
            File.Copy(origemPath, destinyPath);
        }

        public static DirectoryInfo? CreateDirectorys()
        {         
            Console.WriteLine("Digite o nome do diretório: ");
            var nameArquive = Console.ReadLine();
            if (nameArquive != null)
            {
                var directoryPath = CreatePathDirectory(nameArquive);
                var directory = Directory.CreateDirectory(directoryPath);
                return directory;
            }
            return null;
        }

        public static void ReadDirectory(string nameArquive)
        {
             var directoryPath = CreatePath(nameArquive);

            if (Directory.Exists(directoryPath))
            {
                var directory = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);
                foreach (var dir in directory)
                {
                    var directoryInfo = new DirectoryInfo(dir);
                    Console.WriteLine($"[Nome]:{directoryInfo.Name}");
                    Console.WriteLine($"[Raiz]:{directoryInfo.Root}");
                    if (directoryInfo.Parent != null)
                        Console.WriteLine($"[Pai]:{directoryInfo.Parent.Name}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"O {directoryPath} não existe!");
            }            
        }

        public static void ReadArquive(string nameArquive)
        {
            var filePath = CreatePath(nameArquive);

            if (File.Exists(filePath))
            {
                var arquives = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);
                foreach (var arquive in arquives)
                {
                    var fileInfo = new FileInfo(arquive);
                    Console.WriteLine($"[Nome]:{fileInfo.Name}");
                    Console.WriteLine($"[Tamanho]:{fileInfo.Length}");
                    Console.WriteLine($"[Ultimo acesso]:{fileInfo.LastAccessTime}");
                    Console.WriteLine($"[Pasta]:{fileInfo.DirectoryName}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"O {filePath} não existe!");
            }
        }

        public static void CreateSubdirectorys()
        {
            var directory = CreateDirectorys();
            Console.WriteLine("Digite quantos subdiretorios voce quer criar: ");
            if (int.TryParse(Console.ReadLine(), out int numberSubdirectorys))
            {
                for (int i = 1; i <= numberSubdirectorys; i++)
                {
                    Console.WriteLine("Digite um nome para o subdiretório: ");
                    var nameSubdirectory = Console.ReadLine();
                    if (nameSubdirectory != null)
                        if(directory != null)
                            directory.CreateSubdirectory(nameSubdirectory);
                }
            }
        }

        public static void Watcher(string nameDirectory)
        {
            var path = CreatePathDirectory(nameDirectory);
            using var fileSystemWatcher = new FileSystemWatcher(path);
            fileSystemWatcher.Created += OnCreated;
            fileSystemWatcher.Renamed += OnRenamed;
            fileSystemWatcher.Deleted += OnDeleted;
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.IncludeSubdirectories = true;
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Foi excluido o diretório {e.Name}");
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"O diretório {e.OldName} foi renomeado para {e.Name}");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Foi criado o diretório {e.Name}");
        }
    }
}
