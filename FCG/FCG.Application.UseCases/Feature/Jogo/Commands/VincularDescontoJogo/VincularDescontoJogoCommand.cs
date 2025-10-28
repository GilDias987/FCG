using FCG.ApplicationCore.Dto.Jogo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.VincularDescontoJogo
{
    public class VincularDescontoJogoCommand : IRequest<JogoDto>
    {
        public int Id { get; set; }
        public decimal? Desconto { get; set; }
    }
}
