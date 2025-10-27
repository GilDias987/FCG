using FCG.Domain.Common.Exceptions;
using FCG.Domain.Entities;
using FluentAssertions;

namespace FCG.Domain.Tests
{
    public class GrupoUsuarioTest
    {
        [Fact(DisplayName = "Validar se o grupo usuário não é válido")]
        public void Grupo_Usuario_Invalido_Throws_ArgumentException()
        {
            var grupoUsuario = "";
            Assert.Throws<DomainException>(() => new GrupoUsuario(grupoUsuario));
        }

        [Fact(DisplayName = "Validar se o usuário é válido")]
        public void Grupo_Usuario_Valido()
        {
            var nomeGrupoUsuario = "ADMINISTRADOR";
            var grupoUsuario = new GrupoUsuario(nomeGrupoUsuario);
            grupoUsuario.Nome.Should().Be(nomeGrupoUsuario);
        }
    }
}
