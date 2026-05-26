using AutoMapper;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Eventos.Enuns;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Geral.Entities;
using ConectElo.Domain.Areas.Geral.Enuns;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Domain.Exceptions;

namespace ConectElo.Application.Areas.EventosArea.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IConfirmacaoEventoRepository _confirmacaoEventoRepository;
        private readonly IItensListaDesejosRepository _itensListaDesejosRepository;
        private readonly IMapper _mapper;

        public EventoService(IEventoRepository eventoRepository, IMapper mapper, IArquivoRepository arquivoRepository, IConfirmacaoEventoRepository confirmacaoEventoRepository, IItensListaDesejosRepository itensListaDesejosRepository)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
            _arquivoRepository = arquivoRepository;
            _confirmacaoEventoRepository = confirmacaoEventoRepository;
            _itensListaDesejosRepository = itensListaDesejosRepository;
        }

        public async Task<ExibirItemListaDesejosDto> AdicionarItemListaDesejos(Guid listaId, CriarItemListaDesejosDto dto, Guid criadorId)
        {
            var evento = await _eventoRepository.BuscarAniversarioPorListaDesejosId(listaId);

            if (evento is null)
                throw new NotFoundException("Lista de desejos não encontrada.");

            if (evento.Criador != criadorId)
                throw new UnathorizedException("Apenas o criador pode adicionar itens.");

            var item = new ItensListaDesejos
            {
                Descricao = dto.Descricao,
                UrlReference = dto.UrlReference ?? string.Empty,
                ListaDesejosId = listaId
            };

            await _itensListaDesejosRepository.Inserir(item);

            item = await _itensListaDesejosRepository.BuscarPorId(item.Id);
            return _mapper.Map<ExibirItemListaDesejosDto>(item!);
        }

        public async Task<string> AtualizarFotoCapa(Guid eventoId, Stream conteudo, string nomeArquivo, long tamanho)
        {
            var evento = await _eventoRepository.SelecionarPorId(eventoId);

            if (evento == null)
                throw new NotFoundException("Evento não encontrado!");

            var url = await _arquivoRepository.SalvarFotoCapaEventoASync(conteudo, nomeArquivo, tamanho, eventoId);

            evento.FotoCapaUrl = url;
            await _eventoRepository.Atualizar(evento);

            return url;
        }

        public async Task<ExibirEventoDto> BuscarEventoPorId(Guid id)
        {
            var evento = await _eventoRepository.SelecionarPorId(id);

            if (evento is null)
                throw new NotFoundException("Evento não encontrado.");

            return evento switch
            {
                AniversarioEvento aniversario => _mapper.Map<ExibirAniversarioDto>(aniversario),
                AmigoSecretoEvento amigoSecreto => _mapper.Map<ExibirAmigoSecretoDto>(amigoSecreto),
                _ => _mapper.Map<ExibirEventoDto>(evento)
            };
        }

        public async Task<ExibirAmigoSecretoDto> CriarAmigoSecreto(CriarAmigoSecretoDto dto, Guid criadorId)
        {
            var evento = new AmigoSecretoEvento
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataInicio = dto.DataInicio,
                Localizacao = dto.Localizacao,
                GrupoId = dto.GrupoId,
                Criador = criadorId,
                Status = StatusEvento.Iniciado,
                TipoEvento = TipoEventoEnum.AmigoSecreto,
                Valor = dto.ValorMinimo,
                DataSorteio = dto.DataSorteio,
                Sorteado = false
            };

            await _eventoRepository.Inserir(evento);
            return _mapper.Map<ExibirAmigoSecretoDto>(evento);
        }

        public async Task<ExibirAniversarioDto> CriarAniversario(CriarAniversarioDto dto, Guid criadorId)
        {
            var evento = new AniversarioEvento
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataInicio = dto.DataInicio,
                Localizacao = dto.Localizacao,
                GrupoId = dto.GrupoId,
                Criador = criadorId,
                Status = StatusEvento.Iniciado,
                TipoEvento = TipoEventoEnum.Aniversario,
                NomeAniversariante = dto.NomeAniversariante,
                Idade = dto.Idade
            };

            if (dto.ListaDesejos is not null)
            {
                evento.ListaDesejos = new ListaDesejos
                {
                    Titulo = dto.ListaDesejos.Titulo,
                    Itens = dto.ListaDesejos.Itens.Select(i => new ItensListaDesejos
                    {
                        Descricao = i.Descricao,
                        UrlReference = i.UrlReference ?? string.Empty
                    }).ToList()
                };
            }

            await _eventoRepository.Inserir(evento);
            return _mapper.Map<ExibirAniversarioDto>(evento);
        }

        public async Task<CriarEventoDto> CriarEvento(CriarEventoDto dto)
        {
            var evento = _mapper.Map<Evento>(dto);
            await _eventoRepository.Inserir(evento);
            return dto;
        }

        public async Task<ExibirItemListaDesejosDto> DeselecionarItem(Guid itemId, Guid usuarioId)
        {
            var item = await _itensListaDesejosRepository.BuscarPorId(itemId);

            if (item is null)
                throw new NotFoundException("Item não encontrado.");

            if (item.ReservadoPorId != usuarioId)
                throw new UnathorizedException("Você não pode desfazer a seleção de outro usuário.");

            item.ReservadoPorId = null;
            await _itensListaDesejosRepository.Atualizar(item);

            return _mapper.Map<ExibirItemListaDesejosDto>(item);
        }

        public async Task<EditarEventoDto> EditarEvento(EditarEventoDto dto)
        {
            var evento = await _eventoRepository.SelecionarPorId(dto.Id);
            _mapper.Map(dto, evento);

            await _eventoRepository.Atualizar(evento);
            return dto;

        }

        public async Task ExcluirEvento(Guid id)
        {
            var evento = await _eventoRepository.SelecionarPorId(id);
            if (evento is null)
                throw new NotFoundException("Não existe um evento correspondente ao id enviado.");

            await _eventoRepository.Excluir(evento);
        }

        public async Task<List<ExibirEventoDto>> ListarPorGrupo(Guid grupoId, Guid usuarioId)
        {
            var eventos = await _eventoRepository.ListarPorGrupo(grupoId);
            var dtos = _mapper.Map<List<ExibirEventoDto>>(eventos);

            if (dtos.Count > 0)
            {
                var ids = dtos.Select(e => e.Id).ToList();
                var participacoes = await _confirmacaoEventoRepository.BuscarParticipacoesPorEventos(ids, usuarioId);
                foreach (var dto in dtos)
                {
                    dto.ParticipacaoUsuario = participacoes.GetValueOrDefault(dto.Id);
                }
            }

            return dtos;
        }

        public async Task<List<ExibirEventoDto>> ListarPorUsuario(Guid usuarioId)
        {
            var eventos = await _eventoRepository.ListarPorUsuario(usuarioId);
            var dtos = _mapper.Map<List<ExibirEventoDto>>(eventos);

            if (dtos.Count > 0)
            {
                var ids = dtos.Select(e => e.Id).ToList();
                var participacoes = await _confirmacaoEventoRepository.BuscarParticipacoesPorEventos(ids, usuarioId);
                foreach (var dto in dtos)
                {
                    dto.ParticipacaoUsuario = participacoes.GetValueOrDefault(dto.Id);
                }
            }

            return dtos;
        }

        public async Task RegistrarParticipacao(Guid eventoId, Guid usuarioId, StatusConfirmacaoEventoEnum status)
        {
            var confirmacao = await _confirmacaoEventoRepository.BuscarPorEventoEUsuario(eventoId, usuarioId);

            if (confirmacao is not null)
            {
                confirmacao.Status = status;
                confirmacao.DataAtualizacao = DateTime.UtcNow;
                await _confirmacaoEventoRepository.Atualizar(confirmacao);
            }
            else
            {
                await _confirmacaoEventoRepository.Inserir(new ConfirmacaoEvento
                {
                    EventoId = eventoId,
                    UsuarioId = usuarioId,
                    Status = status,
                    DataAtualizacao = DateTime.UtcNow
                });
            }
        }

        public async Task RemoverItemListaDesejos(Guid itemId, Guid criadorId)
        {
            var item = await _itensListaDesejosRepository.BuscarPorId(itemId);

            if (item is null)
                throw new NotFoundException("Item não encontrado.");

            var evento = await _eventoRepository.BuscarAniversarioPorListaDesejosId(item.ListaDesejosId);

            if (evento is null || evento.Criador != criadorId)
                throw new UnathorizedException("Apenas o criador pode remover itens.");

            await _itensListaDesejosRepository.Excluir(item);
        }

        public async Task<ExibirItemListaDesejosDto> SelecionarItem(Guid itemId, Guid usuarioId)
        {
            var item = await _itensListaDesejosRepository.BuscarPorId(itemId);

            if (item is null)
                throw new NotFoundException("Item não encontrado.");

            if (item.ReservadoPorId is not null)
                throw new BusinessException("Este item já foi selecionado por outra pessoa.");

            item.ReservadoPorId = usuarioId;
            await _itensListaDesejosRepository.Atualizar(item);

            item = await _itensListaDesejosRepository.BuscarPorId(itemId);
            return _mapper.Map<ExibirItemListaDesejosDto>(item!);
        }
    }
}
