# Ambev Developer Evaluation

Este repositório contém a solução desenvolvida para o teste do time **DeveloperStore**, cobrindo os domínios de **Produto**, **Item de Venda** e **Venda**. A implementação inclui operações completas de CRUD, validações de negócio específicas e aplicação automática de descontos conforme o volume de itens.

---

## 🗂️ Estrutura do Projeto

```
Ambev.DeveloperEvaluation/
├── Adapters
│   ├── Driven
│   │   ├── Ambev.DeveloperEvaluation.ORM
│   │   │   ├── Mapping/        # Configurações de mapeamento do Entity Framework Core
│   │   │   ├── Migrations/     # Scripts de migração do banco de dados
│   │   │   ├── Repositories/   # Implementações de repositórios de dados
│   │   │   ├── appsettings.json
│   │   │   └── DefaultContext.cs
│   ├── Drivers
│   │   └── Ambev.DeveloperEvaluation.WebApi
│   │       ├── Common/        # Utilitários gerais da API
│   │       ├── Features/      # Endpoints organizados por funcionalidade
│   │       │   ├── Auth
│   │       │   ├── Branchs
│   │       │   ├── Products
│   │       │   ├── SaleItems
│   │       │   ├── Sales
│   │       │   └── Users
│   │       ├── Logs/          # Implementação de logging
│   │       ├── Mappings/      # Perfis do AutoMapper
│   │       ├── Middleware/    # Filtros e tratamento de exceções
│   │       ├── appsettings.json
│   │       ├── Dockerfile
│   │       └── Program.cs     # Ponto de entrada da aplicação
├── Core
│   ├── Application             # Casos de uso e orquestração
│   ├── Domain
│   │   ├── Common             # Entidades e classes auxiliares
│   │   ├── Entities           # Modelos de domínio
│   │   ├── Enums              # Enumerações
│   │   ├── Events             # Eventos de domínio
│   │   ├── Exceptions         # Exceções customizadas
│   │   ├── Repositories       # Interfaces para persistência
│   │   ├── Services           # Lógica de negócio
│   │   ├── Specifications     # Regras de validação
│   │   └── Validation         # Classes de validação
├── Crosscutting               # Serviços transversais (HealthChecks, Logging, Security, Validation, IoC)
├── Testes
│   ├── Functional             # Testes ponta a ponta
│   ├── Integration            # Testes de integração com banco e serviços
│   └── Unit                   # Testes unitários de componentes isolados
└── README.md                  # Documentação principal
```

---

## 🔐 Cadastro e Autenticação de Usuários

A autenticação utiliza **JWT** para proteger os endpoints. O fluxo básico envolve:

1. **Registrar Usuário**
   - **POST** `/api/users/register`
   - Exemplo de corpo:
     ```json
     {
       "username": "Alex Peter",
       "password": "@Ap987654321T",
       "phone": "73981294386",
       "email": "alex@gmail.com",
       "status": 1,
       "role": 2
     }
     ```
   - Retorno inclui `id` do usuário criado.

2. **Login**
   - **POST** `/api/auth/login`
   - Exemplo de corpo:
     ```json
     {
       "email": "alex@gmail.com",
       "password": "@Ap987654321T
     }
     ```
   - Retorna o **token JWT**, além de dados básicos (`email`, `name`, `role`).

3. **Acesso a rotas seguras**
   - Adicione no cabeçalho:
     ```bash
     Authorization: Bearer <seu_token>
     ```

---

## 🗄️ Configuração do Banco de Dados

Para provisionar o esquema usando o **Entity Framework Core**:

```bash
cd Adapters/Driven/Ambev.DeveloperEvaluation.ORM
# Criar a migração inicial
dotnet ef migrations add InitialMigration
# Aplicar ao banco de dados
dotnet ef database update
```

Os detalhes de conexão estão em `appsettings.json` dessa pasta.

---

## 📋 Regras de Negócio Principais

- **Branch** (Filial)
  - Identificador único e nome obrigatório.
  - Pode gerenciar múltiplos produtos.

- **Product**
  - ID, nome e preço unitário.
  - Operações de cadastro, atualização e remoção.

- **SaleItem**
  - Associado a uma venda (`SaleId`) e a um produto (`ProductId`).
  - Define preço unitário e quantidade.
  - Descontos automáticos:
    - Acima de 10 unidades → 20% de desconto
    - Entre 4 e 9 unidades → 10% de desconto
    - Máximo permitido: 20 unidades por item

- **Sale**
  - Composta por número, data, filial e cliente.
  - Total calculado somando os `SaleItems` com descontos.
  - Não é possível cancelar uma venda após finalizada.
  - Apenas produtos em estoque podem ser vendidos.

---

## ▶️ Iniciando a Aplicação

### Pelo Docker Compose

```bash
cd template/backend
docker-compose up -d --build
```

- **Web API**: `http://localhost:8080` / `https://localhost:8081`
- **PostgreSQL**: `localhost:5432`
- **MongoDB**: `localhost:27017`
- **Redis**: `localhost:6379`
- **RabbitMQ**: `localhost:5672` (Dashboard: `http://localhost:15672`)

Use `docker-compose down` para encerrar os serviços.

### Local (sem container)

```bash
cd template/backend/src/Ambev.DeveloperEvaluation.WebApi
dotnet restore
dotnet run
```
Acesse `http://localhost:5000/swagger` para explorar os endpoints.

---

## ✅ Testes Automatizados

A suíte de testes está dividida em:

- **Unitários**: Validam componentes isolados (serviços, validações, cálculos de desconto).
- **Integração**: Conferem integração com repositórios, banco e demais serviços.
- **Funcionais**: Cenários completos de uso (cadastro de filial, criação de venda, etc.).

### Executando

```bash
# Unitários
dotnet test --filter Category=Unit
# Integração
dotnet test --filter Category=Integration
# Funcionais
dotnet test --filter Category=Functional
```

Antes dos testes de integração, verifique se o banco está ativo e com as migrações aplicadas.

---


