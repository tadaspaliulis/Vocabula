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
    /// <summary>
    /// Responsible for serialising the internal learned word data format to the file file format and back
    /// </summary>
    public class Serialiser
    {
        private static DataFileVersionNumber CurrentFileVersion = new DataFileVersionNumber(1, 0, 0);

        private string _dataStorePath;

        private LanguageContext _languageContext;

        private LearnedWordListDataV1 _fileData;

        public Serialiser(string dataStoraPath, LanguageContext context)
        {
            _dataStorePath = dataStoraPath;
            _languageContext = context;
            _fileData = new LearnedWordListDataV1
            {
                VersionNumber = CurrentFileVersion,
                LearnedAdjectives = new List<LearnedAdjectiveDataRecordV1>(),
                LearnedNouns = new List<LearnedNounDataRecordV1>(),
                LearnedVerbs = new List<LearnedVerbDataRecordV1>()
            };
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

        public void Write()
        {
            using (FileStream stream = new FileStream(_dataStorePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(LearnedWordListDataV1));
                serialiser.Serialize(stream, _fileData);
            }
        }

        public void Read()
        {
            if (!File.Exists(_dataStorePath))
                return;
            
            using (FileStream stream = new FileStream(_dataStorePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(LearnedWordListDataV1));
                _fileData = (LearnedWordListDataV1)serialiser.Deserialize(stream);
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
    }

}
