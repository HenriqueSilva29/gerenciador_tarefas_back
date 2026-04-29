
using Application.Funcionalidades.Usuarios.Dtos;
using Application.Funcionalidades.Usuarios.Contratos.CasosDeUso;
using Application.Utils.Transacao;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Usuarios;

namespace Application.Funcionalidades.Usuarios.CasosDeUso
{
    public class AtualizarNomeUsuarioCasoDeUso : IAtualizarNomeUsuarioCasoDeUso
    {
        private readonly IRepUsuario _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarNomeUsuarioCasoDeUso(IRepUsuario rep, IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecutarAsync(int id, AtualizarNomeUsuarioRequisicao dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuario = await _rep.RecuperarPorIdAsync(id);

            if (usuario == null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.RegistroNaoEncontrado,
                    "Usuario nao encontrado na base de dados",
                    StatusCodes.Status404NotFound);
            
            usuario.AtualizarNomeDoUsuario(dto.Nome);

            _rep.Atualizar(usuario);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}



