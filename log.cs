using System;
using System.IO;


namespace tLogger
{
    public class Log
    {

        private static Int32 maxFileSize = 10240000; // 10 MB
        private static string numberPattern = "({0})";

        public bool Write(string strLog, string logFileName, bool overwrite = false)
        {
            try
            {
                Stream fileStream = null;
                FileInfo logFileInfo = new FileInfo(logFileName);
                DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (logFileInfo.Length > maxFileSize)
                {
                    var nextFileName = NextAvailableFilename(logFileName);
                    logFileInfo = new FileInfo(logFileName);
                }

                if(logFileInfo.Exists && overwrite)
                {
                    logFileInfo.Delete();
                    fileStream =  logFileInfo.Create();
                } else
                {
                    fileStream = (!logFileInfo.Exists ? logFileInfo.Create() : new FileStream(logFileName, FileMode.Append));
                }
                
                StreamWriter log = new StreamWriter(fileStream);
                log.WriteLine(strLog);
                log.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return File.Exists(logFileName);
        }

        public static string NextAvailableFilename(string path)
        {
            if (!File.Exists(path))
                return path;

            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }
    }
}
