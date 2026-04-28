
using Application.Dtos.Usuarios;
using Application.Interfaces.UseCases.Usuarios;
using Application.Utils.Transacao;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Repository.Repositorys.UsuarioRep;

namespace Application.UseCase.Usuarios
{
    public class AtualizarNomeUsuarioUseCase : IAtualizarNomeUsuarioUseCase
    {
        private readonly IRepUsuario _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarNomeUsuarioUseCase(IRepUsuario rep, IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecutarAsync(int id, AtualizarNomeUsuarioRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuario = await _rep.RecuperarPorIdAsync(id);

            if (usuario == null)
                new ExceptionApplication(EnumCodigosDeExcecao.RegistroNaoEncontrado, "Usuário não encontrado na base de dados", StatusCodes.Status409Conflict);
            
            usuario.AtualizarNomeDoUsuario(dto.Nome);

            _rep.Atualizar(usuario);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
