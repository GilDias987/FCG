using FCG.ApplicationCore.Feature.Usuario.Queries;
using FCG.ApplicationCore.Feature.Usuario.Queries.GetGrupoUsuario;
using FCG.ApplicationCore.Feature.Usuario.Queries.GetUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;
using MediatR;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Test.Usuario
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
        [Theory(DisplayName = "Validar se o grupo de usuário é válido")]
        [InlineData("")]
        [InlineData(null)]
        public void Grupo_Usuario_Throws_ArgumentException(string grupo)
        {
            Assert.Throws<ArgumentException>(() => new Email(grupo));
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


    }
}
