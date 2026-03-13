# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Subir o banco de dados (primeira vez / ambiente do zero)
docker-compose up -d

# Subir o banco de dados (container já existente)
docker start bancoconectelo

# Build da solução
dotnet build ConectElo.sln

# Rodar a API
dotnet run --project src/ConectElo.API

# Aplicar migrations ao banco
dotnet ef database update --project src/ConectElo.Infra --startup-project src/ConectElo.API

# Criar nova migration
dotnet ef migrations add <NomeDaMigration> --project src/ConectElo.Infra --startup-project src/ConectElo.API
```

A documentação interativa da API fica disponível em `/scalar/v1` e o schema OpenAPI em `/openapi/v1.json` (apenas em ambiente Development).

## Arquitetura

O projeto segue **Clean Architecture** com 4 camadas:

```
ConectElo.Domain      → Entidades, interfaces de repositório, enums, exceções customizadas
ConectElo.Application → Services, DTOs, AutoMapper Profiles
ConectElo.Infra       → Implementação dos Repositories, AppDbContext, Migrations
ConectElo.API         → Controllers (herdam BaseController), configuração DI no Program.cs
```

O domínio está organizado em **áreas de negócio**: `Social`, `Eventos`, `Comunicacao`, `Dinamicas`, `Geral`. Cada camada espelha essa mesma estrutura de pastas em `Areas/`.

### Fluxo de uma requisição
`Controller → IService → IRepository → AppDbContext → PostgreSQL`

## Padrões importantes

### Resposta padronizada
Todos os endpoints retornam `BaseResponse<T>` com os campos `sucesso`, `mensagem`, `dados` e `erros`. Nunca retorne `Ok(objeto)` diretamente — use os helpers do `BaseController`: `OkResponse<T>()`, `CreatedReponse<T>()`, `BadRequestResponse()`, `NotFoundResponse()`, `ErrorResponse()`.

### Repository genérico
`RepositoryGeneric<TEntity>` (em `ConectElo.Infra/Areas/Base/`) fornece: `Inserir`, `Atualizar`, `Excluir`, `SelecionarPorId`, `Consultar` (retorna `IQueryable` com `AsNoTracking`). Repositories específicos herdam dessa classe e implementam a interface correspondente do Domain.

### Exceções customizadas
Sempre lance exceções tipadas do `ConectElo.Domain/Exceptions/ExceptionsModel.cs` nos services:
- `NotFoundException` → 404
- `ConflictException` → 409
- `BusinessException` → 400
- `UnathorizedException` → 401 *(typo intencional no nome, não corrigir sem refatorar tudo)*

O `BaseController.ErrorResponse()` faz o mapeamento automático dessas exceções para os status HTTP corretos.

### Entidade base
Todas as entidades (exceto `Usuario`) herdam de `EntityBase`, que auto-gera o `Id` como `Guid` no construtor.

### Identidade
`Usuario` herda de `IdentityUser<Guid>` (não de `EntityBase`). O `AppDbContext` herda de `IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>`. Os endpoints built-in do Identity são mapeados via `app.MapIdentityApi<Usuario>()`.

### AutoMapper
Novos profiles devem ser registrados manualmente no `Program.cs` dentro do `MapperConfiguration`. Os profiles existentes ficam em `ConectElo.Application/Areas/{Area}/Mappers/`.

### Registro de dependências
Toda nova interface/service/repository deve ser registrada como `Scoped` no `Program.cs`.

## Banco de dados

- **PostgreSQL 17.2** rodando via Docker na porta `8080` (mapeada para `5432` internamente)
- As migrations ficam em `ConectElo.Infra/Migrations/`
- A connection string está em `appsettings.json` — em produção deve ser substituída por variável de ambiente
