using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Vocabula.Model.Enums;

namespace Vocabula.Model.Storage.DataFormats.Version1
{
    [Serializable]
    public abstract class LearnedWordDataRecordBaseV1
    {
        public string UniqueId;
        public string QuestionWord;
        public string AnswerWord;
    }

    [Serializable]
    public class LearnedNounDataRecordV1 : LearnedWordDataRecordBaseV1
    {
        public GenderEnum Gender;
    }

    [Serializable]
    public class LearnedVerbDataRecordV1 : LearnedWordDataRecordBaseV1
    {
        public PersonalPronounEnum PersonalPronoun;
    }

    [Serializable]
    public class LearnedAdjectiveDataRecordV1 : LearnedWordDataRecordBaseV1
    {
    }

    /// <summary>
    /// File version as stored in the data file, should inherit from IComparable once several file types are supported
    /// </summary>
    [Serializable]
    public class DataFileVersionNumber
    {
        public DataFileVersionNumber()
        {

        }

        public DataFileVersionNumber(int version, int major, int minor)
        {
            Version = version;
            Major = major;
            Minor = minor;
        }

        public int Version;
        public int Major;
        public int Minor;
    }

    [Serializable]
    public class LearnedWordListDataV1 : BaseDataStorageFileFormat
    {
        public List<LearnedNounDataRecordV1> LearnedNouns;
        public List<LearnedVerbDataRecordV1> LearnedVerbs;
        public List<LearnedAdjectiveDataRecordV1> LearnedAdjectives;
    }
}