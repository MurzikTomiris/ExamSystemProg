using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //NuGet SharpZipLib
            //запускаю через start.bat
            //Task10 add "file.pdf" "123.7z"
            //и Task10 extract "123.7z" "file2.pdf"

            if (args.Length < 3)
            {
                Console.WriteLine("Usage: MyConsole add <sourceFile> <archiveFile> | extract <archiveFile> <destinationFile>");
                return;
            }

            string command = args[0];
            string sourceFile = args[1];
            string secondFile = args[2];

            try
            {
                switch (command.ToLower())
                {
                    case "add":
                        AddToArchive(sourceFile, secondFile);
                        break;
                    case "extract":
                        ExtractFromArchive(sourceFile, secondFile);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Use 'add' or 'extract'.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AddToArchive(string sourceFile, string secondFile)
        {
            using (FileStream fsOut = File.Create(secondFile))
            using (ZipOutputStream zipStream = new ZipOutputStream(fsOut))
            {
                zipStream.SetLevel(9); // 0-9, 9 being the highest compression

                byte[] buffer = new byte[4096];
                ZipEntry entry = new ZipEntry(Path.GetFileName(sourceFile))
                {
                    DateTime = DateTime.Now
                };
                zipStream.PutNextEntry(entry);

                using (FileStream fsInput = File.OpenRead(sourceFile))
                {
                    StreamUtils.Copy(fsInput, zipStream, buffer);
                }

                zipStream.CloseEntry();
                zipStream.IsStreamOwner = true;
                zipStream.Close();
            }

            Console.WriteLine($"File '{sourceFile}' has been added to archive '{secondFile}'.");
        }

        private static void ExtractFromArchive(string archiveFile, string secondFile)
        {
            using (FileStream fsInput = File.OpenRead(archiveFile))
            using (ZipInputStream zipStream = new ZipInputStream(fsInput))
            {
                ZipEntry entry = zipStream.GetNextEntry();
                if (entry != null)
                {
                    using (FileStream fsOutput = File.Create(secondFile))
                    {
                        byte[] buffer = new byte[4096];
                        StreamUtils.Copy(zipStream, fsOutput, buffer);
                    }

                    Console.WriteLine($"File '{secondFile}' has been extracted from archive '{archiveFile}'.");
                }
                else
                {
                    Console.WriteLine($"No entries found in archive '{archiveFile}'.");
                }
            }
        }
    }
}
    