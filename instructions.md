# Instruções

## Requisitos

- .NET 10 SDK

## Executar a API

Na raiz do repositório:

```bash
dotnet run --project src/Parfois.Pedidos.Api
```

A API fica disponível em `http://localhost:5081`.

- Swagger: http://localhost:5081/swagger
- Endpoints: `/api/pedido` (GET, POST, PUT, DELETE) e `/api/status` (POST)

A base de dados é em memória, por isso os dados perdem-se quando a aplicação é parada.

O ficheiro `src/Parfois.Pedidos.Api/Parfois.Pedidos.Api.http` tem pedidos prontos a usar com os exemplos do enunciado (é preciso criar primeiro o pedido `123456`).

## Executar os testes

```bash
dotnet test
```

Inclui testes unitários das regras de estado e testes de integração aos endpoints.

## Estrutura

- `Parfois.Pedidos.Domain` - entidades e constantes de estado
- `Parfois.Pedidos.Application` - serviços, DTOs, interfaces e avaliação do estado
- `Parfois.Pedidos.Infrastructure` - Entity Framework Core (InMemory) e repositório
- `Parfois.Pedidos.Api` - controllers e configuração
- `Parfois.Pedidos.Tests` - testes

## Notas

- No exemplo #6 do enunciado o pedido enviado é `123456-N` mas a resposta mostra `123456`. A API devolve o código que foi enviado no pedido (`123456-N`), por me parecer o comportamento mais coerente.
- Num `PUT /api/pedido/{codigo}` o código do URL é o que conta; só os itens são atualizados.
