using Application.Dtos.UsuarioDtos;
using Application.Interfaces.UseCases.Autenticacaos;
using Application.Interfaces.UseCases.Usuarios;
using Application.Utils.Transacao;
using Domain.Entities;
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

        public async Task Executar(RegistrarUsuarioDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuarioExistente = await _rep.ObterUsuarioPorEmail(dto.Email);

            if (usuarioExistente is not null)
                throw new ApplicationException("Email já cadastrado na base de dados");

            var senhaHash = _hashSenha.Executar(dto.Senha);

            var usuario = new Usuario(dto.Email, senhaHash);

            _rep.Adicionar(usuario);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
