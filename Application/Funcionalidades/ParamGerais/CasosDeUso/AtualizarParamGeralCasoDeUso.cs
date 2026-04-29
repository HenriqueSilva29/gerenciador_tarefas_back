using Application.Funcionalidades.ParamGerais.Dtos;
using Application.Funcionalidades.ParamGerais.Contratos.CasosDeUso;
using Application.Funcionalidades.ParamGerais.Mapeadores;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Domain.Entidades;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.ParamGerais;
using Repository.Repositorios.Usuarios;

namespace Application.Funcionalidades.ParamGerais.CasosDeUso
{
    public class AtualizarParamGeralCasoDeUso : IAtualizarParamGeralCasoDeUso
    {
        private readonly IRepParamGeral _rep;
        private readonly IRepUsuario _repUsuario;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public AtualizarParamGeralCasoDeUso(
            IRepParamGeral rep,
            IRepUsuario repUsuario,
            IUnitOfWork unitOfWork,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _rep = rep;
            _repUsuario = repUsuario;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task ExecuteAsync(AtualizarParamGeralRequisicao dto)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var paramGeral = await _rep.ObterPorUsuarioAsync(idUsuario);

            var novaParametrizacao = paramGeral is null;

            if (paramGeral is null)
            {
                var usuario = await _repUsuario.RecuperarPorIdAsync(idUsuario);

                if (usuario is null)
                    throw new ExcecaoAplicacao(
                        EnumCodigosDeExcecao.RegistroNaoEncontrado,
                        "Usuario autenticado nao encontrado.",
                        StatusCodes.Status404NotFound);

                paramGeral = ParamGeral.CriarPadrao(usuario);
            }

            ParamGeralMapeador.AtualizarEntidade(paramGeral, dto);

            await _unitOfWork.BeginTransactionAsync();

            if (novaParametrizacao)
                _rep.Adicionar(paramGeral);
            else
                _rep.Atualizar(paramGeral);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}



