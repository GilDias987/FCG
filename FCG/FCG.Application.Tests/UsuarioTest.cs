using FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero;
using FCG.Application.UseCases.Feature.Usuario.Commands.LoginUsuario;
using FCG.Application.UseCases.Feature.Usuario.Queries.GetUsuario;
using FCG.ApplicationCore.Dto.Jogo;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FCG.Application.Tests
{
    public class UsuarioTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        public UsuarioTest()
        {
            _mediatorMock = new();
            _usuarioRepositoryMock = new();
        }

        [Fact(DisplayName = "Verificar se o usuário não existe")]
        public async Task Get_Usuario_Nao_Existe()
        {
            // Arrange
            var query = new GetUsuarioQuery { Id = 100 };
            var handler = new GetUsuarioQueryHandler(_usuarioRepositoryMock.Object);

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.GetUsuarioAsync(It.IsAny<int>()))
                .ReturnsAsync((Usuario?)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        }

        [Fact(DisplayName = "Verificar se o usuário existe")]
        public async Task Get_Usuario_Existe()
        {
            // Arrange
            var result = true;
            var query = new GetUsuarioQuery { Id = 100 };
            var handler = new GetUsuarioQueryHandler(_usuarioRepositoryMock.Object);

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.GetUsuarioAsync(It.IsAny<int>()))
                .ReturnsAsync(new Usuario("Gil Dias",new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1));

            // Act
            var usuario = await handler.Handle(query, default);

            // Assert
            Assert.Equal(result, usuario != null);
        }

        [Theory(DisplayName = "Login usuário não válido")]
        [InlineData("","")]
        [InlineData("gil@fiap.com.br","")]
        [InlineData("gil.fiap.com.br","Carlos@987")]
        public async Task Login_Usuario_Nao_Valido(string email, string senha)
        {
            // Arrange
            var command = new LoginUsuarioCommand { Email = email, Senha = senha };
            var validator = new LoginUsuarioValidator(_usuarioRepositoryMock.Object);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Count > 0);
        }

        [Fact(DisplayName = "Login usuário válido")]
        public async Task Login_Usuario_Valido()
        {
            // Arrange
            var command = new LoginUsuarioCommand { Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var validator = new LoginUsuarioValidator(_usuarioRepositoryMock.Object);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Login usuário email não encontrado ")]
        public async Task Login_Usuario_Email_Nao_Encontrado()
        {
            // Arrange
            var command = new LoginUsuarioCommand { Email="gil@fiap.com.br", Senha="Carlos@987" };
            var handler = new LoginUsuarioCommandHandler(_usuarioRepositoryMock.Object);

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.UsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(command, default));
        }

        [Fact(DisplayName = "Login usuário senha inválida")]
        public async Task Login_Usuario_Senha_Invalida()
        {
            // Arrange
            var command = new LoginUsuarioCommand { Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var handler = new LoginUsuarioCommandHandler(_usuarioRepositoryMock.Object);

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.UsuarioEmailAsync(It.IsAny<string>()))
                 .ReturnsAsync(new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlossssssssss@987"), 1));


            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(command, default));
        }

        [Fact(DisplayName = "Login usuário sucesso")]
        public async Task Login_Usuario_Sucesso()
        {
            // Arrange
            var command = new LoginUsuarioCommand { Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var handler = new LoginUsuarioCommandHandler(_usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            usuario.GrupoUsuario = new GrupoUsuario("ADMINISTRADOR");
            // Act
            _usuarioRepositoryMock.Setup(rep => rep.UsuarioEmailAsync(It.IsAny<string>()))
                 .ReturnsAsync(usuario);

            var usuariooDto = await handler.Handle(command, default);

            // Assert
            usuariooDto.Nome.Should().Be(usuario.Nome);
        }


    }
}
