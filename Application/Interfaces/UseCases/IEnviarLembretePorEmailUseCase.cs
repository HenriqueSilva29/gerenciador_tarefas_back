using Application.Dtos.LembreteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases
{
    public interface IEnviarLembretePorEmailUseCase
    {
        Task ExecuteAsync(LembreteMensagemDto message);
    }
}
