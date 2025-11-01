# FIAP Cloud Games (FCG)

## Sobre o Projeto

O **Fiap Cloud Games (FCG)** é uma plataforma inovadora de venda de jogos em nuvem que nasceu dentro do ecossistema educacional da FIAP (Faculdade de Informática e Administração Paulista). O projeto tem como principal objetivo proporcionar aos alunos uma experiência prática e integrada, cobrindo o desenvolvimento, a implementação (deployment) e o consumo de jogos que são hospedados em ambientes cloud.

## Sumário

## Tecnologias e Plataformas <a name="tech"></a>

- [Visual Studio](https://visualstudio.microsoft.com/pt-br/)
- [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- [EF Core](https://learn.microsoft.com/pt-br/ef/core/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server)
- [Azure](https://azure.microsoft.com/)
- [Swagger](https://swagger.io/)
- [XUnit](https://xunit.net/)

## Estrutura do Projeto
A arquitetura será dividida em quatro camadas principais:

- Domain (Domínio): O coração da aplicação. Contém as entidades, interfaces e regras de negócio.
- Application (Aplicação): Orquestra o fluxo de dados e interage com as camadas de domínio e infraestrutura. Contém os casos de uso, DTOs e as interfaces de repositório.
- Infrastructure (Infraestrutura): Lida com detalhes externos, como banco de dados (Entity Framework Core), sistema de arquivos, e-mails, etc. É onde a lógica de persistência e auditoria será implementada.
- API (Camada de Apresentação): A camada de entrada da aplicação. Utiliza a Controllers API do .NET para expor as informações.

```bash
+ document
  TC NETT - Fase 1
+ src
  + FCG.sln
    FCG.Application.csproj
    FCG.Domain.csproj
    FCG.Infrastructure.csproj
    FCG.WebAPI.csproj
+ tests
  FCG.Application.tests
  FCG.Domain.tests
```

## Desenvolvimento <a name="dev"></a>

Para rodar o sistema na sua máquina, siga os passos abaixo:

1. Clone o projeto para sua máquina

```bash
git clone -b develop https://github.com/GilDias987/FCG.git
```

Executar a **Migration** para criar ou atualizar as tabelas de banco de dados.

1. Abra o Console do Gerenciador de Pacotes.
2. Selecione o projeto Padrão como **FCG.Infrastructure**.
3. Execute o comando abaixo para construir e iniciar os containers:

```bash
Add-Migration Primeira-migracao
```
```bash
Update-Database
```

Para Executar o API

1. Clique no botão Executar (que é o triângulo verde ao lado do menu suspenso de perfis) ou pressione a tecla F5 (para Executar com Depuração) ou Ctrl + F5 (para Executar sem Depuração).

```bash
dotnet run
```

2. Ao rodar a API, o Visual Studio utiliza as portas configuradas no arquivo launchSettings.json (localizado dentro da pasta Properties do projeto). Geralmente, ele define uma porta HTTP e uma HTTPS (ex: http://localhost:5122 e https://localhost:7258). O URL que é aberto no navegador é baseado nesta configuração.
 
Para se autenticar, vá para o endpoint '/api/Autenticacao/Login' e use as credenciais abaixo:

```json
{
  "email": "administrador@fiap.com.br",
  "password": "@Adm123!"
}
```
ou
```json
{
  "email": "usuario@fiap.com.br",
  "password": "@Usu123!"
}
```
Obs: Essas credenciais são criadas automaticamente.

## Testes <a name="tech"></a>

- Para rodar os testes, utilize o **Test Explorer** do Visual Studio ou execute via terminal:

```bash
dotnet test
```

## Contribuições <a name="contributions"></a>

Qualquer contribuição é bem-vinda!

Fique à vontade para usar o sistema, abrir issues, enviar pull requests e tirar dúvidas.
