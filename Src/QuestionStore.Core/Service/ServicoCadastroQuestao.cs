﻿using QuestionStore.Core.Mapping;
using QuestionStore.Domain.Domain;
using System.Collections.Generic;

namespace QuestionStore.Core.Processos
{
    public class ServicoCadastroQuestao
    {
        public List<Question> Obtenha()
        {
            var map = new MapperQuestion();
            var q = map.Obtenha();

            return q;
        }
    }
}
