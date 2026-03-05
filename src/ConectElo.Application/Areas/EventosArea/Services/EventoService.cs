using AutoMapper;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;

namespace ConectElo.Application.Areas.EventosArea.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;

        public EventoService(IEventoRepository eventoRepository, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        public async Task<ExibirEventoDto> BuscarEventoPorId(Guid id)
        {
            var evento = await _eventoRepository.SelecionarPorId(id);

            if (evento is null)
                throw new KeyNotFoundException("Evento não encontrado.");

            return _mapper.Map<ExibirEventoDto>(evento);
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
                throw new Exception("Não existe um evento correspondente ao id enviado.");

            await _eventoRepository.Excluir(evento);
        }
    }
}
