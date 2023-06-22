# ShopZilla.Clientes

Projeto ShopZilla responsável por enviar mensagens de atualização do pedido aos clientes.

## Geral

Este projeto faz parte de um conjunto de outros projetos ShopZilla, destinados aos estudos da arquitetura orientada a eventos, Kafka, Entity Framework e execução de processos em segundo plano via BackgroundServices.

O ShopZilla é um projeto que busca simular a efetivação de compras, atualização de estoque e envio de notificações para os clientes através de aplicações independentes, onde aproveitando da arquitetura orientada a eventos, caso algum sistema esteja completamente fora, não vai afetar o conjunto como um todo.

![alt text](https://github.com/felipetoscano/shopzilla_clientes-console-dotnet/blob/main/resources/shopzilla-geral.jpg)

## Aplicação Clientes 

Esta aplicação envia mensagens de atualização do pedido conforme os eventos no tópico CONFIRMACAO_PEDIDO onde é inscrita.

![alt text](https://github.com/felipetoscano/shopzilla_clientes-console-dotnet/blob/main/resources/shopzilla-clientes.jpg)

## Execução

### Para a geração da imagem Docker

A Aplicação, utilizando os recursos mais recentes do .NET 7, possui suporte integrado a geração de imagens Docker.

Basta inserir o comando abaixo na raiz do projeto: 

`
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release
`

## Para a execução do ecossistema

Para a simulação no ambiente local, executar o arquivo docker-compose.yml presente no projeto abaixo:
