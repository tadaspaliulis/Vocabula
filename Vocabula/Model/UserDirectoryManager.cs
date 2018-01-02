using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model
{
    public enum VocabulaDirectories
    {
        VocabulaUserDocuments
    }

    public enum VocabulaFiles
    {
        StatisticsFile,
        WordsFile,
    }

    /// <summary>
    /// Class for dealing with user directories
    /// </summary>
    public static class UserDirectoryManager
    {
        public static string CreateDirectory(VocabulaDirectories directory)
        {
            var path = GetDirectory(directory);
            //Create directories if they don't already exist
            Directory.CreateDirectory(path);
            return path;
        }

        public static string GetDirectory(VocabulaDirectories directory)
        {
            switch (directory)
            {
                case VocabulaDirectories.VocabulaUserDocuments:
                    string userDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    return Path.Combine(userDocumentsPath, _vocabulaSubfolder);
                default:
                    throw new NotSupportedException(directory + " is not a supported directory");
            }
        }

        public static string GetFilePath(VocabulaFiles file)
        {
            var userDocPath = GetDirectory(VocabulaDirectories.VocabulaUserDocuments);
            switch(file)
            {
                case VocabulaFiles.StatisticsFile:
                    return Path.Combine(userDocPath, _defaultStatisticsFileName);
                case VocabulaFiles.WordsFile:
                    return Path.Combine(userDocPath, _defaultWordsFileName);
                default:
                    throw new NotSupportedException(file + " is not a supported file");
            }
        }

        private const string _vocabulaSubfolder = "Vocabula";

        private const string _defaultWordsFileName = "Words.xml";
        private const string _defaultStatisticsFileName = "Statistics.xml";
    }
}
