using FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero;
using FCG.Application.UseCases.Feature.Usuario.Commands.AddUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.DeleteUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.EditUsuario;
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
        private readonly Mock<IGrupoUsuarioRepository> _grupoUsuarioRepository;

        public UsuarioTest()
        {
            _mediatorMock = new();
            _usuarioRepositoryMock = new();
            _grupoUsuarioRepository = new();
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
            var validator = new LoginUsuarioCommandValidator(_usuarioRepositoryMock.Object);

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
            var validator = new LoginUsuarioCommandValidator(_usuarioRepositoryMock.Object);

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


        [Fact(DisplayName = "Add usuário sem o nome")]
        public async Task Add_Usuario_Nao_Valido_Sem_Nome()
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome="", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var validator = new AddUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.UsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(usuario);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o nome.", erros);
        }

        [Fact(DisplayName = "Add usuário com o e-mail invalido")]
        public async Task Add_Usuario_Nao_Valido_Email_Invalido()
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gilfiap.com.br", Senha = "Carlos@987" };
            var validator = new AddUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe um e-mail válido.", erros);
        }

        [Fact(DisplayName = "Add usuário com o e-mail existente")]
        public async Task Add_Usuario_Nao_Valido_Email_Existente()
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var validator = new AddUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Este e-mail já está registrado. Por favor, tente outro.", erros);
        }

        [Theory(DisplayName = "Adicionar usuário com senha inválida")]
        [InlineData("carlos")]
        [InlineData("Carlossss")]
        [InlineData("Carlos@aa")]
        [InlineData("")]
        public async Task Add_Usuario_Senha_Invalida(string senha)
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = senha };
            var validator = new AddUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("A senha deve ter no mínimo 8 caracteres e incluir pelo menos uma letra maiúscula, um número e um caractere especial.", erros);
        }


        [Fact(DisplayName = "Add usuário com grupo de usuario inexistente")]
        public async Task Add_Usuario_Nao_Valido_Grupo_Usuario_Inexistente()
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var validator = new AddUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((GrupoUsuario)null);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id de grupo de usuário não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Adicionar Usuário válido")]
        public async Task Add_Usuario_Valido()
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var validator = new AddUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            // Act
            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar Usuario")]
        public async Task Add_Usuario()
        {
            // Arrange
            var command = new AddUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var handler = new AddUsuarioCommandHandler(_usuarioRepositoryMock.Object, _grupoUsuarioRepository.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            usuario.GrupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(usuario);

            var usuarioDto = await handler.Handle(command, default);

            // Assert
            usuarioDto.Nome.Should().Be(command.Nome);
        }

        [Fact(DisplayName = "Edit usuário sem o nome")]
        public async Task Edit_Usuario_Nao_Valido_Sem_Nome()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987", Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o nome.", erros);
        }

        [Fact(DisplayName = "Edit usuário com o e-mail invalido")]
        public async Task Edit_Usuario_Nao_Valido_Email_Invalido()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gilfiap.com.br", Senha = "Carlos@987", Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe um e-mail válido.", erros);
        }

        [Fact(DisplayName = "Edit usuário com o e-mail existente")]
        public async Task Edit_Usuario_Nao_Valido_Email_Existente()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "alexandre@fiap.com.br", Senha = "Carlos@987", Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Este e-mail já está registrado. Por favor, tente outro.", erros);
        }

        [Theory(DisplayName = "Edit usuário com senha inválida")]
        [InlineData("carlos")]
        [InlineData("Carlossss")]
        [InlineData("Carlos@aa")]
        [InlineData("")]
        public async Task Edit_Usuario_Senha_Invalida(string senha)
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "alexandre@fiap.com.br", Senha = senha, Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act

            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("A senha deve ter no mínimo 8 caracteres e incluir pelo menos uma letra maiúscula, um número e um caractere especial.", erros);
        }


        [Fact(DisplayName = "Edit usuário com grupo de usuario inexistente")]
        public async Task Edit_Usuario_Nao_Valido_Grupo_Usuario_Inexistente()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "alexandre@fiap.com.br", Senha = "Carlos@987", Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act

            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
          
            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((GrupoUsuario)null);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id de grupo de usuário não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Edit usuário com usuario inexistente")]
        public async Task Edit_Usuario_Nao_Valido_Usuario_Inexistente()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "alexandre@fiap.com.br", Senha = "Carlos@987", Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act

            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Usuario) null);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do usuário informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Editar Usuário válido")]
        public async Task Edit_Usuario_Valido()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "alexandre@fiap.com.br", Senha = "Carlos@987", Id = 1 };
            var validator = new EditUsuarioValidator(_grupoUsuarioRepository.Object, _usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            var grupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock.Setup(rep => rep.VerificarSeExisteUsuarioEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _grupoUsuarioRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(grupoUsuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Edit Jogo")]
        public async Task Edit_Jogo()
        {
            // Arrange
            var command = new EditUsuarioCommand { Nome = "Gil Dias", UsuarioGrupoId = 1, Email = "gil@fiap.com.br", Senha = "Carlos@987" };
            var handler = new EditUsuarioCommandHandler(_usuarioRepositoryMock.Object, _grupoUsuarioRepository.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            usuario.GrupoUsuario = new GrupoUsuario("ADMINISTRADOR");

            // Act
            _usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Usuario>()));

            var usuarioDto = await handler.Handle(command, default);

            // Assert
            usuarioDto.Nome.Should().Be(command.Nome);
        }


        [Fact(DisplayName = "Deletar Usuário inexistente")]
        public async Task Delete_Usuario_Inexistente()
        {
            // Arrange
            var command = new DeleteUsuarioCommand { Id = 1 };
            var validator = new DeleteUsuarioValidator(_usuarioRepositoryMock.Object);

            // Act
            _usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Usuario)null);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do usuario informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Deletar Usuario existente")]
        public async Task Delete_Usuario_Existente()
        {
            // Arrange
            var command = new DeleteUsuarioCommand { Id = 1 };
            var validator = new DeleteUsuarioValidator(_usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);
            usuario.GrupoUsuario = new GrupoUsuario("ADMINISTRADOR");
            // Act
            _usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Deletar Usuario")]
        public async Task Delete_Jogo()
        {
            // Arrange
            var command = new DeleteUsuarioCommand { Id = 1 };
            var handler = new DeleteUsuarioCommandHandler(_usuarioRepositoryMock.Object);
            var usuario = new Usuario("Gil Dias", new Email("gil@fiap.com.br"), new Senha("Carlos@987"), 1);

            // Act
            _usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            _usuarioRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            var deletado = await handler.Handle(command, default);

            // Assert
            Assert.True(deletado);
        }

    }
}
