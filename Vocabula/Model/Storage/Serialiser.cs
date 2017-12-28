using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Storage.DataFormats.Version1;
using Vocabula.Model.LearnableItems;
using System.IO;
using System.Xml.Serialization;

namespace Vocabula.Model.Storage
{
    [Serializable]
    public class WordStatV1
    {
        public DateTime DateLearned;
        public DateTime LastTimeAnswered;
        public int NumberOfTimesAsked;
        public int NumberOfTimesAnsweredCorrectly;
        public string UniqueId;
    }

    [Serializable]
    public abstract class BaseDataStorageFileFormat
    {
        public DataFileVersionNumber VersionNumber;
    }

    [Serializable]
    public class WordStatsListV1 : BaseDataStorageFileFormat
    {
        public List<WordStatV1> WordStats;
    }

    public abstract class BaseDataSerialiser<TDataStoreType> where TDataStoreType : BaseDataStorageFileFormat
    {
        protected BaseDataSerialiser(string dataStorePath)
        {
            _dataStorePath = dataStorePath;
            _fileData = NewFileDataObject();
        }

        private string _dataStorePath;
        protected TDataStoreType _fileData;

        public void Write()
        {
            using (FileStream stream = new FileStream(_dataStorePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(TDataStoreType));
                serialiser.Serialize(stream, _fileData);
            }
        }

        public void Read()
        {
            if (!File.Exists(_dataStorePath))
                return;

            using (FileStream stream = new FileStream(_dataStorePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(TDataStoreType));
                _fileData = (TDataStoreType)serialiser.Deserialize(stream);
            }
        }

        public void ResetInMemoryFileData()
        {
            _fileData = NewFileDataObject();
        }

        protected abstract TDataStoreType NewFileDataObject();
    }

    public class StatisticsSerialiser : BaseDataSerialiser<WordStatsListV1>
    {
        private static DataFileVersionNumber CurrentFileVersion = new DataFileVersionNumber(1, 0, 0);

        public StatisticsSerialiser(string dataStoraPath) : base(dataStoraPath)
        {
        }

        public void AddStatisticsItem(Statistics item)
        {
            _fileData.WordStats.Add(ConvertToDataRecord(item));
        }
        
        public List<Statistics> GenerateListOfStatistics()
        {
            var statsList = new List<Statistics>();
            foreach (var item in _fileData.WordStats)
                statsList.Add(ConvertFromDataRecord(item));

            return statsList;
        }

        protected override WordStatsListV1 NewFileDataObject()
        {
            return new WordStatsListV1
            {
                VersionNumber = CurrentFileVersion,
                WordStats = new List<WordStatV1>()
            };
        }

        private WordStatV1 ConvertToDataRecord(Statistics item)
        {
            return new WordStatV1
            {
                DateLearned = item.DateLearned,
                LastTimeAnswered = item.LastTimeAnswered,
                NumberOfTimesAnsweredCorrectly = item.NumberOfTimesAnsweredCorrectly,
                NumberOfTimesAsked = item.NumberOfTimesAsked,
                UniqueId = item.UniqueId,
            };
        }

        private Statistics ConvertFromDataRecord(WordStatV1 item)
        {
            return new Statistics
            {
                DateLearned = item.DateLearned,
                LastTimeAnswered = item.LastTimeAnswered,
                NumberOfTimesAnsweredCorrectly = item.NumberOfTimesAnsweredCorrectly,
                NumberOfTimesAsked = item.NumberOfTimesAsked,
                UniqueId = item.UniqueId,
            };
        }
    }

    /// <summary>
    /// Responsible for serialising the internal learned word data format to the file file format and back
    /// </summary>
    public class WordsSerialiser : BaseDataSerialiser<LearnedWordListDataV1>
    {
        private static DataFileVersionNumber CurrentFileVersion = new DataFileVersionNumber(1, 0, 0);

        private LanguageContext _languageContext;

        public WordsSerialiser(string dataStoraPath, LanguageContext context) : base(dataStoraPath)
        {
            _languageContext = context;
        }

        public void AddItemForSerialisation(IToBeLearnedItem item)
        {
            switch(item)
            {
                case NounWord n:
                    _fileData.LearnedNouns.Add(ConvertToDataRecord(n));
                    break;
                case AdjectiveWord a:
                    _fileData.LearnedAdjectives.Add(ConvertToDataRecord(a));
                    break;
                case VerbWord v:
                    _fileData.LearnedVerbs.Add(ConvertToDataRecord(v));
                    break;
                default:
                    throw new InvalidOperationException("Attempted serializing unsupported type: " + item.GetType());
            }
        }

        public List<IToBeLearnedItem> GenerateListOfLearnedItems()
        {
            var items = new List<IToBeLearnedItem>();

            //Convert from file format to internal data format
            foreach (var noun in _fileData.LearnedNouns)
                items.Add(ConvertFromDataRecord(noun));

            foreach (var verb in _fileData.LearnedVerbs)
                items.Add(ConvertFromDataRecord(verb));

            foreach (var adjective in _fileData.LearnedAdjectives)
                items.Add(ConvertFromDataRecord(adjective));

            return items;
        }

        private LearnedNounDataRecordV1 ConvertToDataRecord(NounWord item)
        {
            return new LearnedNounDataRecordV1
            {
               AnswerWord = item.AnswerWord,
               QuestionWord = item.QuestionWord,
               Gender = item.NounGender,
               UniqueId = item.GetQuestion()
            };
        }

        private LearnedAdjectiveDataRecordV1 ConvertToDataRecord(AdjectiveWord item)
        {
            return new LearnedAdjectiveDataRecordV1
            {
                AnswerWord = item.AnswerWord,
                QuestionWord = item.QuestionWord,
                UniqueId = item.GetQuestion()
            };
        }

        private LearnedVerbDataRecordV1 ConvertToDataRecord(VerbWord item)
        {
            return new LearnedVerbDataRecordV1
            {
                AnswerWord = item.AnswerWord,
                QuestionWord = item.QuestionWord,
                PersonalPronoun = item.PersonalPronoun,
                UniqueId = item.GetQuestion()
            };
        }

        private IToBeLearnedItem ConvertFromDataRecord(LearnedAdjectiveDataRecordV1 word)
        {
            return new AdjectiveWord(word.QuestionWord, word.AnswerWord);
        }

        private IToBeLearnedItem ConvertFromDataRecord(LearnedNounDataRecordV1 word)
        {
            return new NounWord(word.Gender, word.QuestionWord, word.AnswerWord, _languageContext);
        }

        private IToBeLearnedItem ConvertFromDataRecord(LearnedVerbDataRecordV1 word)
        {
            return new VerbWord(word.PersonalPronoun, word.QuestionWord, word.AnswerWord, _languageContext);
        }

        protected override LearnedWordListDataV1 NewFileDataObject()
        {
            return new LearnedWordListDataV1
            {
                VersionNumber = CurrentFileVersion,
                LearnedAdjectives = new List<LearnedAdjectiveDataRecordV1>(),
                LearnedNouns = new List<LearnedNounDataRecordV1>(),
                LearnedVerbs = new List<LearnedVerbDataRecordV1>()
            };
        }
    }

}
