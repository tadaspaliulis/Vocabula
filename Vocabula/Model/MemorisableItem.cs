using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.LearnableItems;
using Vocabula.Model.Storage;

namespace Vocabula.Model
{    
    public class Statistics
    {
        public DateTime lastTimeAnswered;
        public int NumberOfTimesAsked;
        public int NumberOfTimesAnsweredCorrectly;
    }

    public class MemorisableItemStorage
    {
        public MemorisableItemStorage(string dataStoragePath)
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

            _serialiser = new Serialiser(dataStoragePath, _germanLanguageContext);

            /*TryAdd(new MemorisableItem(new NounWord(GenderEnum.Neutral, "Wood", "Holz", _germanLanguageContext)));
            TryAdd(new MemorisableItem(new NounWord(GenderEnum.Masculine, "Table", "Tisch", _germanLanguageContext)));
            TryAdd(new MemorisableItem(new NounWord(GenderEnum.Neutral, "Animal", "Tier", _germanLanguageContext)));

            TryAdd(new MemorisableItem(new VerbWord(PersonalPronounEnum.I, "drink", "trinke", _germanLanguageContext)));*/
        }

        public bool TryAdd(IToBeLearnedItem item)
        {
            //Check if an item with this question already exists (don't want duplicates!)
            var itemQuestion = item.GetQuestion();
            if (_loadedItems.Any(m => string.Compare(m.GetTheQuestion(), itemQuestion, true) == 0))
                return false;

            _loadedItems.Add(new MemorisableItem(item));
            _serialiser.AddItemForSerialisation(item);
            _serialiser.Write(); //Probably shouldn't rewrite the whole thing after every new item added
            return true;
        }

        public void UpdateFromDataStorage()
        {
            _serialiser.Read();
            var items = _serialiser.GenerateListOfLearnedItems();

            foreach(var item in items)
            {
                TrustedAdd(new MemorisableItem(item));
            }
        }

        public void WriteToStorage()
        {
            _serialiser.Write();
        }

        public int GetNumberOfKnownItems()
        {
            return _loadedItems.Count;
        }

        public List<MemorisableItem> GetItemList(int count)
        {
            return _loadedItems;
        }

        public LanguageContext GetLanguageContext()
        {
            return _germanLanguageContext;
        }

        private void TrustedAdd(MemorisableItem item)
        {
            _loadedItems.Add(item);
        }

        private List<MemorisableItem> _loadedItems = new List<MemorisableItem>();

        private LanguageContext _germanLanguageContext;

        private Serialiser _serialiser;
    }

    public class MemorisableItem
    {
        public MemorisableItem(IToBeLearnedItem toBeLearnedItem)
        {
            _toBeLearned = toBeLearnedItem;
        }
        
        public string GetTheAnswer()
        {
            return _toBeLearned.GetExpectedResult();
        }

        public string GetTheQuestion()
        {
            return _toBeLearned.GetQuestion();
        }

        public bool CheckAnswer(string submittedAnswer)
        {
            return GetTheAnswer().Equals(submittedAnswer);
        }

        public IToBeLearnedItem GetLearnableItem()
        {
            return _toBeLearned;
        }

        private IToBeLearnedItem _toBeLearned;
    }
}
