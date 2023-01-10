namespace ConsoleNote
{

    public static class StorageHandler
    {
        public static string ReadFile(string filePath)
        {
            using var stream = new StreamReader(filePath);
            return stream.ReadToEnd();
        }

        public static void WriteFile(string filePath, string content)
        {
            using var fileSave = new StreamWriter(File.Create($"{filePath}"));
            fileSave.WriteLine(content);
            fileSave.Close();
        }

        public static FileStream CreateFile(string filename) // Creates files
        {
            return File.Create(filename);
        }

        public static bool Delete(string filename)
        {
            try
            {
                File.Delete(filename);
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public static string GetValidFilename(string fileName) // Checks to see if filename has ".txt"
        {
            string filePath;
            if (fileName.Contains(".txt", StringComparison.CurrentCultureIgnoreCase))
            {
                filePath = fileName;
            }
            else
            {
                filePath = fileName + ".txt";
            }

            return filePath;
        }
    }
}