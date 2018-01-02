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
    /// Represents the XML structure of the learned words file
    /// </summary>
    [Serializable]
    public class LearnedWordListDataV1 : BaseDataStorageFileFormat
    {
        public List<LearnedNounDataRecordV1> LearnedNouns;
        public List<LearnedVerbDataRecordV1> LearnedVerbs;
        public List<LearnedAdjectiveDataRecordV1> LearnedAdjectives;
    }
}