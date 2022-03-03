using System.Text;

namespace dio_curso_arquivos_streams.src.Model
{
    public class ObjArquiveStream
    {
        public static void WriteArquiveFromBufferStream()
        {
            string textReader = "TextReader é a classe base abstrata " + 
                "de StreamReader e StringReader, que le caracteres " + 
                "de streams e strings, respectivamente.\n\n" + 
                
                "Crie uma instancia de TextReader para abrir um arquivo de texto " + 
                "para ler um intervalo especificado de caracteres " + 
                "ou para criar um leitor baseado em um fluxo existente.\n\n" + 
                
                "Voce também pode usar uma instancia de TextReader para ler" +
                "texto de um armazenamento de suporte personalizado usando as mesmas" +
                "APIs que voce usaria para uma string ou um fluxo.\n\n";

            Console.WriteLine($"Texto original: {textReader}");
            string? line, paragraph = "";
            var stringReader = new StringReader(textReader);
            while (true)
            {
                line = stringReader.ReadLine();
                if (line != null) 
                    paragraph += line + " ";
                else
                {
                    paragraph += '\n';
                    break;
                }
            }
            Console.WriteLine($"Texto modificado: {paragraph}");
            int readCaractere;
            char convertCaractere;
            var stringWriter = new StringWriter();
            stringReader = new StringReader(paragraph);

            while (true)
            {
                readCaractere = stringReader.Read();
                if (readCaractere != -1) break;
                convertCaractere = Convert.ToChar(readCaractere);
                if(convertCaractere == '.')
                {
                    stringWriter.Write("\n\n");
                    stringReader.Read();
                    stringReader.Read();
                }
                else
                {
                    stringWriter.Write(convertCaractere);
                }
            }
            Console.WriteLine($"Texto armazenado no stringWriter: {stringWriter}");
        }

        public static void ReadArquiveFromBufferStream()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Caracteres na primeira linha para ler");
            stringBuilder.AppendLine("e a segunda linha");
            stringBuilder.AppendLine("e o fim");
            using var stringReader = new StringReader(stringBuilder.ToString());
            var buffer = new char[10];
            var size = 0;
            do
            {
                buffer = new char[10];
                size = stringReader.Read(buffer);
                Console.Write(string.Join("", buffer));
            } while (size >= buffer.Length);
        }
    }
}
