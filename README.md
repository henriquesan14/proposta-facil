﻿## API PropostaFacil

### Features

- [x] Criação e gerenciamento de tenants
- [x] Criação de planos de assinaturas personalizados com preço e quantidade de propostas por mês (Opções de pagamento: Pix, boleto e cartão de crédito)
- [x] Criação de assinaturas recorrentes via integração com o Asaas
- [x] Webhooks para pagamentos criados, confirmados e vencidos, processados de forma assíncrona via RabbitMQ com controle de idempotência nos consumers para evitar duplicidade de eventos
- [x] Fluxo assíncrono de eventos utilizando RabbitMQ (integração entre domínios e processamento em background) 🕓
- [x] Jobs (Hangfire) para cancelamento automático de assinaturas vencidas
- [x] Envio de e-mails transacionais com SendGrid
- [x] Envio de propostas personalizadas para clientes dos tenants
- [x] Autenticação JWT + Refresh Token com HTTP-only cookies
- [x] Impersonate Tenant (admin acessa como usuário)
- [x] Alteração de plano de assinatura e atualização de senha
- [x] Remoção lógica (soft delete)
- [x] Cache distribuído com Redis para alto desempenho ⚡


### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:
- [.NET](https://dotnet.microsoft.com/en-us/)
- [Postgres](https://www.postgresql.org/)
- Redis
- RabbitMQ
- Hangfire

### 🛠 Padrões Utilizados

As seguintes padrões foram usados na construção do projeto:
- DDD (Domain-Driven Design)
- CQRS (Command Query Responsibility Segregation)
- SOLID
- UnitOfWork
- Repository
- Decorate
- Clean Architecture

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET](https://dotnet.microsoft.com/en-us/).
[Postgres](https://www.postgresql.org/) ou subir container utilizando o [Docker](https://www.docker.com/).
[Redis](https://redis.io/) ou subir container utilizando o [Docker](https://www.docker.com/).
[RabbitMQ](https://www.rabbitmq.com/) ou subir container utilizando o [Docker](https://www.docker.com/).
Também é preciso configurar ConnectionString do Postgres, ApiKey do SendGrid, ApiKey do Asaas, secret JTW no arquivo `proposta-facil/src/PropostaFacil.API/appsettings.Development.json` ou no `proposta-facil/src/PropostaFacil.API/Properties/launchSetting.json`.
Além disto é bom ter um editor para trabalhar com o código como [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/).


### 🎲 Rodando o Back End (servidor)

#### Rodando PropostaFacil.API

```bash
# Clone este repositório
$ git clone <https://github.com/henriquesan14/proposta-facil.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd proposta-facil

# Vá para a pasta da PropostaFacil.API
$ cd src/PropostaFacil.API

# Execute a aplicação com o comando do dotnet
$ dotnet run

# A API iniciará na porta:5003 com HTTP
```

### Autor
---

<a href="https://www.linkedin.com/in/henrique-san/">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/33522361?v=4" width="100px;" alt=""/>
 <br />
 <sub><b>Henrique Santos</b></sub></a> <a href="https://www.linkedin.com/in/henrique-san/">🚀</a>


Feito com ❤️ por Henrique Santos 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Henrique-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/henrique-san/)](https://www.linkedin.com/in/henrique-san/) 
