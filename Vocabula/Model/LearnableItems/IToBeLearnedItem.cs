using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.LearnableItems
{
    public interface IToBeLearnedItem 
    {
        string GetExpectedResult();
        string GetQuestion();
        string UniqueId { get; }
    }
}
