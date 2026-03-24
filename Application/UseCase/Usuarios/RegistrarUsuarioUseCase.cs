using Application.Dtos.Usuarios;
using Application.Interfaces.UseCases.Autenticacaos;
using Application.Interfaces.UseCases.Usuarios;
using Application.Utils.Transacao;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Repository.Repositorys.UsuarioRep;

namespace Application.UseCase.Usuarios
{
    public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
    {
        private readonly IRepUsuario _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashSenhaUseCase _hashSenha;
        public RegistrarUsuarioUseCase(IRepUsuario rep, IUnitOfWork unitOfWork, IHashSenhaUseCase hashSenha)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
            _hashSenha = hashSenha;
        }

        public async Task<UsuarioResponse> Executar(RegistrarUsuarioRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuarioExistente = await _rep.ObterUsuarioPorNome(dto.Nome);

            if (usuarioExistente is not null)
                throw new ExceptionApplication(EnumCodigosDeExcecao.RegistroNaoEncontrado, "Usuário já cadastrado na base de dados",StatusCodes.Status409Conflict);

            var senhaHash = _hashSenha.Executar(dto.Senha);

            var usuario = new Usuario(dto.Nome, senhaHash);

            _rep.Adicionar(usuario);

            await _unitOfWork.CommitTransactionAsync();

            return new UsuarioResponse { id = usuario.Id };
        }
    }
}
