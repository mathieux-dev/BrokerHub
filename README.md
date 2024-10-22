# BrokerHub API

BrokerHub é uma API para corretoras de imóveis, oferecendo funcionalidades como cadastro de imóveis, alteração de dados, consulta de informações e agendamento de visitas. A API é desenvolvida em **.NET 8** seguindo uma arquitetura hexagonal e implementa autenticação via **JWT** para rotas protegidas.

## Funcionalidades

- Cadastro de imóveis
- Atualização de imóveis
- Consulta de imóveis por id
- Remoção de imóveis
- Autenticação JWT para rotas protegidas
- Disponibilidade de agenda (não implementado)

## Tecnologias Utilizadas

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Swagger / OpenAPI**
- **Autenticação JWT**
- **Arquitetura Hexagonal**
- **MediatR**

## Requisitos

- **.NET SDK** versão 8.0 ou superior
- **SQL Server**
- **Postman** ou **Swagger** para testar a API

## Configuração do Projeto

### 1. Clone o Repositório

```bash
git clone https://github.com/seu-usuario/brokerhub-api.git
cd brokerhub-api
```

### 2. Configuração do Banco de Dados

No arquivo `appsettings.json`, configure a connection string para o SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BrokerHubDB;User Id=sa;Password=sua-senha;"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-aqui",
    "Issuer": "BrokerHub",
    "Audience": "BrokerHubAPI"
  }
}
```

### 3. Aplicar Migrations

Execute os comandos abaixo para criar o banco de dados e aplicar as migrations:

```bash
dotnet ef database update
```

### 4. Rodando a API

Para iniciar a aplicação, execute o seguinte comando:

```bash
dotnet run
```

A API estará disponível em [https://localhost:5001](https://localhost:5001).

## Autenticação JWT

A API utiliza **JWT** para autenticação nas rotas de criação, atualização e remoção de imóveis. Para acessar essas rotas, é necessário realizar login e obter um token JWT.

### Exemplo de Login:

Faça uma requisição `POST` para `/api/brokerhub/auth/login` com as seguintes credenciais:

```json
{
  "email": "mateus.mourao@brokerhub.com",
  "password": "garçagaiata"
}
```

O retorno será um token JWT que deve ser utilizado no header das próximas requisições:

```
Authorization: Bearer <seu-token-jwt-aqui>
```

### Rotas Protegidas

As seguintes rotas exigem autenticação:

- **POST** `/api/brokerhub/imovel/create` - Criação de um imóvel
- **PUT** `/api/brokerhub/imovel/update` - Atualização de um imóvel
- **DELETE** `/api/brokerhub/imovel/delete` - Remoção de um imóvel

## Testes

Este projeto utiliza **xUnit** para testes unitários. Para rodar os testes, utilize o comando:

```bash
dotnet test
```
