using Domain.Lembretes;
using Hangfire;
using Repository.Repositorys.LembreteRep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServLembretes
{
    public class ServLembrete
    {
        private readonly IRepLembrete _rep;

        public ServLembrete(IRepLembrete rep)
        {
            _rep = rep;
        }

    }
}
