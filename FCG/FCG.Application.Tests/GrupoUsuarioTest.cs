using FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario;
using FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;

namespace FCG.Application.Tests
{
    public class GrupoUsuarioTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IGrupoUsuarioRepository> _grupoUsuarioRepositoryMock;
        public GrupoUsuarioTest()
        {
            _mediatorMock = new();
            _grupoUsuarioRepositoryMock = new();
        }

        [Fact(DisplayName = "Verificar se o grupo de usuário não existe")]
        public async Task Get_Grupo_Usuario_Nao_Exite()
        {
            // Arrange
            var query = new GetGrupoUsuarioQuery { Id = 3 };
            var handler = new GetGrupoUsuarioQueryHandler(_grupoUsuarioRepositoryMock.Object);

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((GrupoUsuario)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        }

        [Fact(DisplayName = "Verificar se o grupo de usuário existe")]
        public async Task Get_Grupo_Usuario_Exite()
        {
            // Arrange
            var result = true;
            var parametroId = 1;
            var query = new GetGrupoUsuarioQuery { Id = parametroId };
            var handler = new GetGrupoUsuarioQueryHandler(_grupoUsuarioRepositoryMock.Object);
            var grupo = new GrupoUsuario("ADMINISTRADOR");

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupo);

            // Act
            var grupoUsuario = await handler.Handle(query, default);

            // Assert
            Assert.Equal(result, grupoUsuario != null);
        }

        [Fact(DisplayName = "Adicionar Grupo de Usuário não válido com nome existente")]
        public async Task Add_Grupo_Usuario_Com_Nome_Existente()
        {
            // Arrange
            var grupoUsuaro = new AddGrupoUsuarioCommand { Nome = "ADMINISTRADOR" };
            var validator = new AddGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.VerificarSeExisteGrupoAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Assert
            var result = await validator.ValidateAsync(grupoUsuaro);
            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe um grupo de usuário com esse nome.", erros);
        }

        [Fact(DisplayName = "Adicionar Grupo de Usuário não válido sem nome")]
        public async Task Add_Grupo_Usuario_Sem_Nome_Grupo()
        {
            // Arrange
            var grupoUsuaro = new AddGrupoUsuarioCommand { Nome = "" };
            var validator = new AddGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.VerificarSeExisteGrupoAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o nome do grupo.", erros);
        }

        [Fact(DisplayName = "Adicionar Grupo de Usuário válido")]
        public async Task Add_Grupo_Usuario_Valido()
        {
            // Arrange
            var grupoUsuaro = new AddGrupoUsuarioCommand { Nome = "ADMINISTRADOR" };
            var validator = new AddGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.VerificarSeExisteGrupoAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar Grupo de Usuário")]
        public async Task Add_Grupo_Usuario()
        {
            // Arrange
            var command = new AddGrupoUsuarioCommand { Nome = "ADMINISTRADOR" };
            var handler = new AddGrupoUsuarioCommandHandler(_grupoUsuarioRepositoryMock.Object);
            var grupo = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<GrupoUsuario>()))
                .ReturnsAsync(grupo);

            var grupoDto = await handler.Handle(command, default);

            // Assert
            grupoDto.Nome.Should().Be(grupo.Nome);
        }

        [Fact(DisplayName = "Editar Grupo de Usuário não válido sem nome")]
        public async Task Edit_Grupo_Usuario_Sem_Nome_Grupo()
        {
            // Arrange
            var grupoUsuaro = new EditGrupoUsuarioCommand { Id = 1, Nome = "" };
            var validator = new EditGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new GrupoUsuario("ADMINISTRADOR"));

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.VerificarSeExisteGrupoAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o nome do grupo.", erros);
        }

        [Fact(DisplayName = "Editar Grupo de Usuário não válido com nome existente")]
        public async Task Edit_Grupo_Usuario_Com_Nome_Existente()
        {
            // Arrange
            var grupoUsuaro = new EditGrupoUsuarioCommand { Id = 1, Nome = "MASTER" };
            var validator = new EditGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act 

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new GrupoUsuario("ADMINISTRADOR"));

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.VerificarSeExisteGrupoAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe um grupo de usuário com esse nome.", erros);
        }

        [Fact(DisplayName = "Editar Grupo de Usuário válido")]
        public async Task Edit_Grupo_Usuario_Valido()
        {
            // Arrange
            var grupoUsuaro = new EditGrupoUsuarioCommand { Id = 1, Nome = "ADMINISTRADOR" };
            var validator = new EditGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new GrupoUsuario("ADMINISTRADOR"));

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.VerificarSeExisteGrupoAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Editar Grupo de Usuário")]
        public async Task Edit_Grupo_Usuario()
        {
            // Arrange
            var command = new EditGrupoUsuarioCommand { Nome = "ADMINISTRADOR" };
            var handler = new EditGrupoUsuarioCommandHandler(_grupoUsuarioRepositoryMock.Object);
            var grupo = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupo);

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<GrupoUsuario>()));

            var grupoDto = await handler.Handle(command, default);

            // Assert
            grupoDto.Nome.Should().Be(grupo.Nome);
        }

        [Fact(DisplayName = "Deletar Grupo de Usuário inexistente")]
        public async Task Delete_Grupo_Usuario_Inexistente()
        {
            // Arrange
            var grupoUsuaro = new DeleteGrupoUsuarioCommand { Id = 1};
            var validator = new DeleteGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);

            // Act

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((GrupoUsuario)null);


            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Deletar Grupo de Usuário existente")]
        public async Task Delete_Grupo_Usuario_Existente()
        {
            // Arrange
            var grupoUsuaro = new DeleteGrupoUsuarioCommand { Id = 1 };
            var validator = new DeleteGrupoUsuarioValidator(_grupoUsuarioRepositoryMock.Object);
            var grupo = new GrupoUsuario("ADMINISTRADOR");
            // Act

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupo);


            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Deletar Grupo de Usuário")]
        public async Task Delete_Grupo_Usuario()
        {
            // Arrange
            var command = new DeleteGrupoUsuarioCommand { Id = 1 };
            var handler = new DeleteGrupoUsuarioCommandHandler(_grupoUsuarioRepositoryMock.Object);
            var grupo = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupo);

            _grupoUsuarioRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            var deletado = await handler.Handle(command, default);

            // Assert
            Assert.True(deletado);
        }
    }
}
