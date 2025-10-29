using FCG.Application.UseCases.Feature.Jogo.Commands.AddPlataforma;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditPlataforma;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetPlataforma;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace FCG.Application.Tests
{
    public class PlataformaTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IPlataformaRepository> _plataformaRepositoryMock;

        public PlataformaTest()
        {
            _mediatorMock = new();
            _plataformaRepositoryMock = new();
        }

        [Fact(DisplayName = "Verificar se o plataforma não existe")]
        public async Task GetPlataformaNaoExiste()
        {
            // Arrange
            var query   = new GetPlataformaQuery { Id = 100 };
            var handler = new GetPlataformaQueryHandler(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(rep => rep.GetByOrDefaultIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Plataforma?)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        }

        [Fact(DisplayName = "Verificar se o plataforma existe")]
        public async Task GetPlataformaExiste()
        {
            // Arrange
            var result     = true;
            var query      = new GetPlataformaQuery { Id = 1 };
            var handler    = new GetPlataformaQueryHandler(_plataformaRepositoryMock.Object);
            var plataforma = new Plataforma("Playstation 5");

            _plataformaRepositoryMock
                .Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(plataforma);

            // Act
            var hGenero = await handler.Handle(query, default);

            // Assert
            Assert.Equal(result, hGenero != null);
        }

        [Fact(DisplayName = "Adicionar plataforma não válida com título existente.")]
        public async Task AddPlataformaComTituloExistente()
        {
            // Arrange
            var plataforma = new AddPlataformaCommand { Titulo = "Playstation 5" };
            var validator  = new AddPlataformaCommandValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
              .ReturnsAsync(true);

            // Assert
            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe uma plataforma com esse título.", erros);
        }

        [Fact(DisplayName = "Adicionar plataforma não válida sem título.")]
        public async Task AddPlataformaSemTitulo()
        {
            // Arrange
            var plataforma = new AddPlataformaCommand { Titulo = "" };
            var validator  = new AddPlataformaCommandValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
            .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o titulo da plataforma.", erros);
        }

        [Fact(DisplayName = "Adicionar plataforma válida.")]
        public async Task AddPlataformaValido()
        {
            // Arrange
            var plataforma = new AddPlataformaCommand { Titulo = "Playstation 5" };
            var validator  = new AddPlataformaCommandValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
             .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar plataforma.")]
        public async Task AddPlataforma()
        {
            // Arrange
            var plataforma  = new AddPlataformaCommand { Titulo = "Playstation 5" };
            var handler     = new AddPlataformaCommandHandler(_plataformaRepositoryMock.Object);
            var oPlataforma = new Plataforma("Playstation 5");

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Plataforma>()))
                .ReturnsAsync(oPlataforma);

            var plataformaDto = await handler.Handle(plataforma, default);

            // Assert
            plataformaDto.Titulo.Should().Be(plataforma.Titulo);
        }

        [Fact(DisplayName = "Editar plataforma não válido sem titulo.")]
        public async Task EditPlataformaSemTitulo()
        {
            // Arrange
            var plataforma = new EditPlataformaCommand { Titulo = "" };
            var validator  = new EditPlataformaCommandValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock
              .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(new Plataforma("Playstation 5"));

            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o titulo da plataforma.", erros);
        }
        [Fact(DisplayName = "Editar plataforma não válido com título existente.")]
        public async Task EditPlataformaComTituloExistente()
        {
            // Arrange
            var plataforma = new EditPlataformaCommand { Titulo = "" };
            var validator  = new EditPlataformaCommandValidator(_plataformaRepositoryMock.Object);

            // Act 
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("Playstation 5"));

            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
            .ReturnsAsync(true);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe uma plataforma com esse título.", erros);
        }
    }
}
