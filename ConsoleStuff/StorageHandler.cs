using System.Text;

namespace ConsoleNote
{

    public static class StorageHandler
    {
        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "\0";
            }

            using var stream = new StreamReader(filePath);
            return stream.ReadToEnd();
        }

        public static bool WriteFile(string filePath, string content, bool append)
        {
            if (!File.Exists(filePath) && append)
            {
                return false;
            }

            if (append)
            {
                File.AppendAllText(filePath, content);
            }
            else
            {
                File.WriteAllText(filePath, content);
            }

            return true;
        }

        public static bool WriteFile(string filename, List<string> content, bool append)
        {
            string filePath = GetValidFilename(filename);
            StringBuilder builder = new StringBuilder();

            Extensions.InsertNewline(content);
            Extensions.SpaceInsert(content);

            foreach (string i in content)
            {
                builder.Append(i);
            }

            return WriteFile(filePath, builder.ToString(), append);
        }

        public static FileStream CreateFile(string filename)
        {
            try
            {
                return File.Create(filename);
            } catch (Exception)
            {
                Logger.Error("Failed to create file.");
                return null;
            }
        }

        public static bool Delete(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }

            try
            {
                File.Delete(filename);
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string GetValidFilename(string fileName)
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