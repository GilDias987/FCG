using FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo;
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.VincularDescontoJogo
{

    public class VincularDescontoJogoCommandHandler : IRequestHandler<VincularDescontoJogoCommand, JogoDto>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IPlataformaRepository _plataformaRepository;

        public VincularDescontoJogoCommandHandler(IJogoRepository jogoRepository, IGeneroRepository generoRepository, IPlataformaRepository plataformaRepository)
        {
            _jogoRepository = jogoRepository;
            _generoRepository = generoRepository;
            _plataformaRepository = plataformaRepository;
        }

        public async Task<JogoDto> Handle(VincularDescontoJogoCommand request, CancellationToken cancellationToken)
        {
            var objJogo = await _jogoRepository.GetByIdAsync(request.Id);
            objJogo.AplicarDesconto(request.Desconto);
            await _jogoRepository.UpdateAsync(objJogo);
            
            var dtoJogo = new JogoDto()
            {
                Id = objJogo.Id,
                Titulo = objJogo.Titulo,
                Descricao = objJogo.Descricao,
                Preco = objJogo.Preco,
                Desconto = objJogo.Desconto,
                GeneroId = objJogo.GeneroId,
                PlataformaId = objJogo.PlataformaId,
                PrecoDesconto = objJogo.CalcularPrecoComDesconto().ToString("N2")
            };

            return dtoJogo;
        }
    }
}
