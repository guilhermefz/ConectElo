using AutoMapper;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Eventos.Enuns;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Domain.Exceptions;

namespace ConectElo.Application.Areas.EventosArea.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IMapper _mapper;

        public EventoService(IEventoRepository eventoRepository, IMapper mapper, IArquivoRepository arquivoRepository)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
            _arquivoRepository = arquivoRepository;
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

        public async Task<List<ExibirEventoDto>> ListarPorGrupo(Guid grupoId)
        {
            var eventos = await _eventoRepository.ListarPorGrupo(grupoId);
            return _mapper.Map<List<ExibirEventoDto>>(eventos);
        }

        public async Task<List<ExibirEventoDto>> ListarPorUsuario(Guid usuarioId)
        {
            var eventos = await _eventoRepository.ListarPorUsuario(usuarioId);
            return _mapper.Map<List<ExibirEventoDto>>(eventos);
        }
    }
}
