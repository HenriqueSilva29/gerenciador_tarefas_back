using Application.Funcionalidades.Usuarios.Dtos;
using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;
using Application.Funcionalidades.Usuarios.Contratos.CasosDeUso;
using Application.Utils.Transacao;
using Domain.Entidades;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.ParamGerais;
using Repository.Repositorios.Usuarios;

namespace Application.Funcionalidades.Usuarios.CasosDeUso
{
    public class RegistrarUsuarioCasoDeUso : IRegistrarUsuarioCasoDeUso
    {
        private readonly IRepUsuario _rep;
        private readonly IRepParamGeral _repParamGeral;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashSenhaCasoDeUso _hashSenha;
        public RegistrarUsuarioCasoDeUso(
            IRepUsuario rep,
            IRepParamGeral repParamGeral,
            IUnitOfWork unitOfWork,
            IHashSenhaCasoDeUso hashSenha)
        {
            _rep = rep;
            _repParamGeral = repParamGeral;
            _unitOfWork = unitOfWork;
            _hashSenha = hashSenha;
        }

        public async Task<UsuarioResposta> ExecutarAsync(RegistrarUsuarioRequisicao dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuarioExistente = await _rep.ObterUsuarioPorEmail(dto.Email);

            if (usuarioExistente is not null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.UsuarioJaCadastrado,
                    "Usuario ja cadastrado na base de dados",
                    StatusCodes.Status409Conflict);

            var senhaHash = _hashSenha.Executar(dto.Senha);

            var usuario = new Usuario(dto.Email, senhaHash);
            var paramGeral = ParamGeral.CriarPadrao(usuario);

            _rep.Adicionar(usuario);
            _repParamGeral.Adicionar(paramGeral);

            await _unitOfWork.CommitTransactionAsync();

            return new UsuarioResposta { id = usuario.Id };
        }
    }
}



