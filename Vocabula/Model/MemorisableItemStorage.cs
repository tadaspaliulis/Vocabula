using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.LearnableItems;
using Vocabula.Model.Enums;
using Vocabula.Model.Storage;

namespace Vocabula.Model
{
    public class MemorisableItemStorage
    {
        public MemorisableItemStorage(string wordsFilePath, string statsFilePath)
        {
            _germanLanguageContext = new LanguageContext(
               questionGenderMap: new Dictionary<GenderEnum, string>
               {
                   { GenderEnum.Feminine, "the" },
                   { GenderEnum.Masculine, "the" },
                   { GenderEnum.Neutral, "the" },
               },
               answerGenderMap: new Dictionary<GenderEnum, string>
               {
                   { GenderEnum.Feminine, "die" },
                   { GenderEnum.Masculine, "der" },
                   { GenderEnum.Neutral, "das" }
               },
               questionPronounMap: new Dictionary<PersonalPronounEnum, string>
               {
                   { PersonalPronounEnum.I, "I" },
                   { PersonalPronounEnum.YouSingular, "You (Sngl.)" },
                   { PersonalPronounEnum.He, "He" },
                   { PersonalPronounEnum.She, "She" },
                   { PersonalPronounEnum.It, "It" },
                   { PersonalPronounEnum.We, "We" },
                   { PersonalPronounEnum.YouPlural, "You (Pl.)" },
                   { PersonalPronounEnum.They, "They" },
               },
               answerPronounMap: new Dictionary<PersonalPronounEnum, string>
               {
                   { PersonalPronounEnum.I, "Ich" },
                   { PersonalPronounEnum.YouSingular, "Du" },
                   { PersonalPronounEnum.He, "Er" },
                   { PersonalPronounEnum.She, "Sie" },
                   { PersonalPronounEnum.It, "Es" },
                   { PersonalPronounEnum.We, "Wir" },
                   { PersonalPronounEnum.YouPlural, "Ihr" },
                   { PersonalPronounEnum.They, "Sie" },
               }
               );

            _wordSerialiser = new WordsSerialiser(wordsFilePath, _germanLanguageContext);
            _statsSerialiser = new StatisticsSerialiser(statsFilePath);

            var seed = Environment.TickCount;
            _itemSelector = new WeighedRandomItemSelector<MemorisableItem>(_loadedItems, new Random((int)seed), new StatisticsBasedWeightCalculator());
        }

        public bool TryAdd(IToBeLearnedItem item)
        {
            //Check if an item with this question already exists (don't want duplicates!)
            var itemQuestion = item.GetQuestion();
            if (_loadedItems.Any(m => string.Compare(m.GetTheQuestion(), itemQuestion, true) == 0))
                return false;

            //Generate a new statistics item for this
            var wordStats = new WordStatistics
            {
                DateLearned = DateTime.Now,
                NumberOfTimesAnsweredCorrectly = 0,
                NumberOfTimesAsked = 0,
                UniqueId = item.UniqueId,
            };

            _loadedItems.Add(new MemorisableItem(item, wordStats));
            _wordSerialiser.AddItemForSerialisation(item);

            //Probably shouldn't rewrite the whole thing after every new item added, but works for now
            WriteWordsToStorage();
            WriteStatsToStorage();
            
            return true;
        }

        public void UpdateFromDataStorage()
        {
            //Read everything out from files
            _wordSerialiser.Read();
            _statsSerialiser.Read();
            var learnableItems = _wordSerialiser.GenerateListOfLearnedItems();
            var statsItems = _statsSerialiser.GenerateListOfStatistics();

            foreach (var item in learnableItems)
            {
                var matchingStatItem = statsItems.FirstOrDefault(s => s.UniqueId.Equals(item.UniqueId));
                if (matchingStatItem == null)
                    throw new Exception("Could not find a matching statistics in deserialised data using UniqueId: " + item.UniqueId);

                TrustedAdd(new MemorisableItem(item, matchingStatItem));
            }
        }

        public void WriteWordsToStorage()
        {
            _wordSerialiser.Write();
        }

        public int GetNumberOfKnownItems()
        {
            return _loadedItems.Count;
        }

        public void WriteStatsToStorage()
        {
            //Regenerates the list of statistics used by serialiser
            _statsSerialiser.ResetInMemoryFileData();

            foreach (var memItem in _loadedItems)
                _statsSerialiser.AddStatisticsItem(memItem.statistics);

            //And then write it to file
            _statsSerialiser.Write();
        }

        public List<MemorisableItem> GetItemList(int count)
        {
            return _itemSelector.GetRandomItems(count);
        }

        public LanguageContext GetLanguageContext()
        {
            return _germanLanguageContext;
        }

        /// <summary>
        /// Adds a memorisable item without any internal checking, should only be used when loading data from file
        /// </summary>
        /// <param name="item"></param>
        private void TrustedAdd(MemorisableItem item)
        {
            _loadedItems.Add(item);
        }

        private List<MemorisableItem> _loadedItems = new List<MemorisableItem>();

        private LanguageContext _germanLanguageContext;

        private WeighedRandomItemSelector<MemorisableItem> _itemSelector;

        private WordsSerialiser _wordSerialiser;

        private StatisticsSerialiser _statsSerialiser;
    }
}
