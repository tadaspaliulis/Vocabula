using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;

namespace Vocabula.Model.Abstracts
{
    public interface IGenderContext
    {
        string GetQuestionGender(GenderEnum gender);
        string GetAnswerGender(GenderEnum gender);
    }
}
