# FIAP Cloud Games (FCG)

## Sobre o Projeto

O **Fiap Cloud Games (FCG)** é uma plataforma inovadora de venda de jogos em nuvem que nasceu dentro do ecossistema educacional da FIAP (Faculdade de Informática e Administração Paulista). O projeto tem como principal objetivo proporcionar aos alunos uma experiência prática e integrada, cobrindo o desenvolvimento, a implementação (deployment) e o consumo de jogos que são hospedados em ambientes cloud.

## Sumário


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




