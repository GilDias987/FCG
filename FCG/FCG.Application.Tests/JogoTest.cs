using FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.VincularDescontoJogo;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;

namespace FCG.Application.Tests
{

    public class JogoTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IJogoRepository> _jogoRepositoryMock;
        private readonly Mock<IGeneroRepository> _generoRepositoryMock;
        private readonly Mock<IPlataformaRepository> _plataformaRepositoryMock;
        public JogoTest()
        {
            _mediatorMock = new();
            _jogoRepositoryMock = new();
            _generoRepositoryMock = new();
            _plataformaRepositoryMock = new();
        }

        [Fact(DisplayName = "Verificar se o jogo não existe")]
        public async Task Get_Jogo_Nao_Exite()
        {
            // Arrange
            var query = new GetJogoQuery { Id = 3 };
            var handler = new GetJogoQueryHandler(_jogoRepositoryMock.Object);

            // Act
            _jogoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Jogo)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        }

        [Fact(DisplayName = "Verificar se o jogo existe")]
        public async Task Get_Jogo_Exite()
        {
            // Arrange
            var result = true;
            var parametroId = 1;
            var query = new GetJogoQuery { Id = parametroId };
            var handler = new GetJogoQueryHandler(_jogoRepositoryMock.Object);
            var jogo = new Jogo("Mario", "Jogo de Aventura", 200,50,1,1);

            _jogoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(jogo);

            // Act
            var objJogo = await handler.Handle(query, default);

            // Assert
            Assert.Equal(result, objJogo != null);
        }

        [Fact(DisplayName = "Adicionar Jogo não válido sem título")]
        public async Task Add_Jogo_Sem_Titulo()
        {
            // Arrange
            var usuario = new AddJogoCommand {Titulo = "", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o título.", erros);
        }

        [Fact(DisplayName = "Adicionar Jogo não válido com preço com casas decimais excedentes")]
        public async Task Add_Jogo_Com_Preco_Casas_Decimais_Excedente()
        {
            // Arrange
            var usuario = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200.555m, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O preço não pode conter mais de duas casas decimais.", erros);
        }

        [Fact(DisplayName = "Adicionar Jogo não válido com genero id inexistente")]
        public async Task Add_Jogo_Com_Genero_Id_Inexistente()
        {
            // Arrange
            var usuario = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genero)null);

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do genero do jogo não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Adicionar Jogo não válido com plataforma id inexistente")]
        public async Task Add_Jogo_Com_Plataforma_Id_Inexistente()
        {
            // Arrange
            var usuario = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Plataforma) null);

            // Assert
            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do plataforma do jogo não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Adicionar Jogo não válido com desconto maior que 100%")]
        public async Task Add_Jogo_Com_Valor_Desconto_Maior_100()
        {
            // Arrange
            var usuario = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 101, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O percentual do desconto não pode ser negativo ou maior que 100.", erros);
        }

        [Fact(DisplayName = "Adicionar Jogo não válido com desconto com casas decimais excedentes")]
        public async Task Add_Jogo_Com_Desconto_Casas_Decimais_Excedente()
        {
            // Arrange
            var usuario = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50.333m, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O percentual do desconto não pode conter mais de duas casas decimais.", erros);
        }

        [Fact(DisplayName = "Adicionar Jogo válido")]
        public async Task Add_Jogo_Valido()
        {
            // Arrange
            var usuario = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new AddJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            var result = await validator.ValidateAsync(usuario);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar Jogo")]
        public async Task Add_Jogo()
        {
            // Arrange
            var command = new AddJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var handler = new AddJogoCommandHandler(_jogoRepositoryMock.Object, _generoRepositoryMock.Object, _plataformaRepositoryMock.Object);
            var objUsuario = new Jogo("Mario", "Jogo de Aventura", 200, 50,1,1);

            // Act
            _jogoRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Jogo>()))
                .ReturnsAsync(objUsuario);

            var grupoDto = await handler.Handle(command, default);

            // Assert
            grupoDto.Titulo.Should().Be(objUsuario.Titulo);
        }

        [Fact(DisplayName = "Editar Jogo não válido com id inexistente")]
        public async Task Edit_Jogo_Id_Inexistente()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync((Jogo)null);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do jogo informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Editar Jogo não válido sem título")]
        public async Task Edit_Jogo_Sem_Titulo()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);
            
            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o título.", erros);
        }

        [Fact(DisplayName = "Editar Jogo não válido com preço com casas decimais excedentes")]
        public async Task Edit_Jogo_Com_Preco_Casas_Decimais_Excedente()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200.555m, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O preço não pode conter mais de duas casas decimais.", erros);
        }

        [Fact(DisplayName = "Editar Jogo não válido com genero id inexistente")]
        public async Task Edit_Jogo_Com_Genero_Id_Inexistente()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genero)null);

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do genero do jogo não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Editar Jogo não válido com plataforma id inexistente")]
        public async Task Edit_Jogo_Com_Plataforma_Id_Inexistente()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Plataforma)null);

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do plataforma do jogo não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Editar Jogo não válido com desconto maior que 100%")]
        public async Task Edit_Jogo_Com_Valor_Desconto_Maior_100()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 101, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O percentual do desconto não pode ser negativo ou maior que 100.", erros);
        }

        [Fact(DisplayName = "Editar Jogo não válido com desconto com casas decimais excedentes")]
        public async Task Edit_Jogo_Com_Desconto_Casas_Decimais_Excedente()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50.333m, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O percentual do desconto não pode conter mais de duas casas decimais.", erros);
        }

        [Fact(DisplayName = "Editar Jogo válido")]
        public async Task Edit_Jogo_Valido()
        {
            // Arrange
            var jogo = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var validator = new EditJogoValidator(_generoRepositoryMock.Object, _plataformaRepositoryMock.Object, _jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Aventura"));

            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("PS4"));

            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Editar Jogo")]
        public async Task Edit_Jogo()
        {
            // Arrange
            var command = new EditJogoCommand { Titulo = "Mario", Descricao = "Jogo de Aventura", Preco = 200, Desconto = 50, GeneroId = 1, PlataformaId = 1 };
            var handler = new EditJogoCommandHandler(_jogoRepositoryMock.Object, _generoRepositoryMock.Object, _plataformaRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(objJogo);

            _jogoRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Jogo>()));

            var grupoDto = await handler.Handle(command, default);

            // Assert
            grupoDto.Titulo.Should().Be(objJogo.Titulo);
        }

        [Fact(DisplayName = "Deletar Jogo inexistente")]
        public async Task Delete_Jogo_Inexistente()
        {
            // Arrange
            var grupoUsuaro = new DeleteJogoCommand { Id = 1 };
            var validator = new DeleteJogoValidator(_jogoRepositoryMock.Object);

            // Act
            _jogoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Jogo)null);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do jogo informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Deletar Jogo existente")]
        public async Task Delete_Jogo_Existente()
        {
            // Arrange
            // Arrange
            var grupoUsuaro = new DeleteJogoCommand { Id = 1 };
            var validator = new DeleteJogoValidator(_jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);
            // Act
            _jogoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(objJogo);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Deletar Jogo")]
        public async Task Delete_Jogo()
        {
            // Arrange
            var command = new DeleteJogoCommand { Id = 1 };
            var handler = new DeleteJogoCommandHandler(_jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(objJogo);

            _jogoRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            var deletado = await handler.Handle(command, default);

            // Assert
            Assert.True(deletado);
        }


        [Fact(DisplayName = "Vincular desconto em jogo com id do jogo inexistente")]
        public async Task Vincular_Desconto_Jogo_Id_Inexistente()
        {
            // Arrange
            var jogo = new VincularDescontoJogoCommand { Id = 1, Desconto = 10 };
            var validator = new VincularDescontoJogoValidator(_jogoRepositoryMock.Object);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync((Jogo)null);

            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do jogo informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Vincular desconto em jogo com desconto maior que 100%")]
        public async Task Vincular_Desconto_Jogo_Com_Valor_Desconto_Maior_100()
        {
            // Arrange
            var jogo = new VincularDescontoJogoCommand { Id = 1, Desconto = 101 };
            var validator = new VincularDescontoJogoValidator(_jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O percentual do desconto não pode ser negativo ou maior que 100.", erros);
        }

        [Fact(DisplayName = "Vincular desconto jogo com desconto com casas decimais excedentes")]
        public async Task Vincular_Desconto_Jogo_Casas_Decimais_Excedente()
        {
            // Arrange
            var jogo = new VincularDescontoJogoCommand { Id = 1, Desconto = 10.333m };
            var validator = new VincularDescontoJogoValidator(_jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);

            // Assert
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O percentual do desconto não pode conter mais de duas casas decimais.", erros);
        }

        [Fact(DisplayName = "Vinculo desconto jogo válido")]
        public async Task Vinculo_Desconto_Jogo_Valido()
        {
            // Arrange
            var jogo = new VincularDescontoJogoCommand { Id = 1, Desconto = 10 };
            var validator = new VincularDescontoJogoValidator(_jogoRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
               .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(objJogo);
            var result = await validator.ValidateAsync(jogo);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Vincular desconto jogo")]
        public async Task Vincular_Desconto_Jogo()
        {
            // Arrange
            var command = new VincularDescontoJogoCommand { Id = 1, Desconto = 50, };
            var handler = new VincularDescontoJogoCommandHandler(_jogoRepositoryMock.Object, _generoRepositoryMock.Object, _plataformaRepositoryMock.Object);
            var objJogo = new Jogo("Mario", "Jogo de Aventura", 200, 50, 1, 1);

            // Act
            _jogoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(objJogo);

            _jogoRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Jogo>()));

            var grupoDto = await handler.Handle(command, default);

            // Assert
            grupoDto.Titulo.Should().Be(objJogo.Titulo);
        }


    }

}
