using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Abstracts;
using Vocabula.Model.Enums;
using Vocabula.Model.LearnableItems;

namespace Vocabula.Model
{
    public class TestController : IAddLearnedWords
    {
        
        public TestController()
        {
            //Make sure the directory exists
            var vocabulaPath = UserDirectoryManager.CreateDirectory(VocabulaDirectories.VocabulaUserDocuments);

            _memorisableItems = new MemorisableItemStorage(
                wordsFilePath: UserDirectoryManager.GetFilePath(VocabulaFiles.WordsFile),
                statsFilePath: UserDirectoryManager.GetFilePath(VocabulaFiles.StatisticsFile));

            _memorisableItems.UpdateFromDataStorage();
        }

        public void CheckAnswers()
        {
            _examinationItems.ForEach(item => item.CheckAnswer());

            //Record the results to the file system
            _memorisableItems.WriteStatsToStorage();
        }

        public int GetNumberOfKnownItems()
        {
            return _memorisableItems.GetNumberOfKnownItems();
        }

        public List<ExaminationItem> GetCurrentListOfExaminationItems()
        {
            if (_examinationItems.Count == 0)
                throw new InvalidOperationException("Requested a list of examination items when none were generated.");

            return _examinationItems;
        }

        public List<ExaminationItem> GenerateListOfExaminationItems(int count)
        {
            _examinationItems.Clear();
            foreach (var item in _memorisableItems.GetItemList(count))
            {
                _examinationItems.Add(new ExaminationItem(item));
            }

            return _examinationItems;
        }

        #region IAddLearnedWords implementation

        /// <summary>
        /// Try adding a new adjective to the dictionary (does this belong in test controller?)
        /// </summary>
        /// <param name="questionWord"></param>
        /// <param name="answerWord"></param>
        /// <returns>Returns false if word already exists</returns>
        public bool TryAddingLearnedAdjective(string questionWord, string answerWord)
        {
            return _memorisableItems.TryAdd(new AdjectiveWord(questionWord, answerWord));
        }

        public bool AddLearnedVerb(PersonalPronounEnum personalPronoun, string questionWord, string answerWord)
        {
            return _memorisableItems.TryAdd(new VerbWord(personalPronoun, questionWord, answerWord, GetLanguageContext()));
        }

        public bool AddLearnedNoun(GenderEnum gender, string questionWord, string answerWord)
        {
            return _memorisableItems.TryAdd(new NounWord(gender, questionWord, answerWord, GetLanguageContext()));
        }

        #endregion

        public LanguageContext GetLanguageContext()
        {
            return _memorisableItems.GetLanguageContext();
        }

        /// <summary>
        /// List of items currently being examined
        /// </summary>
        private List<ExaminationItem> _examinationItems = new List<ExaminationItem>();

        private MemorisableItemStorage _memorisableItems;
    }
}
