using FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario;
using MediatR;
using Moq;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario;

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
        public async Task Grupo_Usuario_Nao_Exite()
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
        public async Task Grupo_Usuario_Exite()
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

        //[Fact(DisplayName = "Adicionar Grupo de Usuário")]
        //public async Task Grupo_Usuario_Adicionar()
        //{
        //    // Arrange
        //    var command = new AddGrupoUsuarioCommand { Nome = "ADMINISTRADOR" };
        //    var handler = new AddGrupoUsuarioCommandHandler(_grupoUsuarioRepositoryMock.Object);
        //    var grupo = new GrupoUsuario("ADMINISTRADOR");

        //    // Act
        //    _grupoUsuarioRepositoryMock
        //        .Setup(repo => repo.AddAsync(It.IsAny<GrupoUsuario>()))
        //        .ReturnsAsync(grupo);

        //    var grupoDto = await handler.Handle(command, default);

        //    grupoDto..Should().Be(nome);
        //    // Assert
        //    await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        //}
    }
}
