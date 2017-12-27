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
    public class LearnedWordListDataV1
    {
        public DataFileVersionNumber VersionNumber;

        public List<LearnedNounDataRecordV1> LearnedNouns;
        public List<LearnedVerbDataRecordV1> LearnedVerbs;
        public List<LearnedAdjectiveDataRecordV1> LearnedAdjectives;
    }

    public class DataRecordWriter
    {
        public void Setup()
        {
            _dataForWriting = new LearnedWordListDataV1
            {
                LearnedAdjectives = new List<LearnedAdjectiveDataRecordV1>(),
                LearnedNouns = new List<LearnedNounDataRecordV1>(),
                LearnedVerbs = new List<LearnedVerbDataRecordV1>()
            };

            _dataForWriting.LearnedAdjectives.Add(new LearnedAdjectiveDataRecordV1
            {
                AnswerWord = "Answer",
                QuestionWord = "Question",
                UniqueId = "Question",
            });
            _dataForWriting.LearnedNouns.Add(new LearnedNounDataRecordV1
            {
                AnswerWord = "NounAnswer",
                QuestionWord = "NounQuestion",
                Gender = GenderEnum.Masculine,
                UniqueId = "Very Unique Id"
            });
        }

        public void Write()
        {
            using (FileStream stream = new FileStream("TestFile.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(LearnedWordListDataV1));
                serialiser.Serialize(stream, _dataForWriting);
            }
        }

        public void Read()
        {
            using (FileStream stream = new FileStream("TestFile.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(LearnedWordListDataV1));
                _dataForWriting = (LearnedWordListDataV1)serialiser.Deserialize(stream);
            }
        }

        private LearnedWordListDataV1 _dataForWriting;
    }
}
