# Desafio Web Backend
![logo-rissi](imagens/Logo%20Rissi.png)


Entre para o time dos **#Rissianos**

A Rissi Contabilidade Médica está sempre crescendo assim como nosso time dos **#Rissianos**. Se o que te move é fazer a diferença e trabalhar em um time que joga junto, então vem contabilizar histórias com a gente.

# Por que trabalhar conosco?

Gosta de desafios em um lugar agradável e cheio de benefícios? Vem com a gente!

### Buscamos pessoas que:
- São obcecadas por fazer o melhor pelos clientes
- Tenham a cultura de dono da empresa
- Que jogam pelo time
- Que buscam em criar uma carreira                                                                                                                                                                                                      
# Desafio
O objetivo do desafio é avaliar o candidato em áreas como: lógica de programação; conhecimento em linguagem orientada a objeto, C#, ASP.NET Core, APIs REST e banco de dados NoSQL ou SQL. Não há somente um jeito certo de realizar esse desafio, há várias maneiras de resolvê-lo, e cabe a você achar a melhor solução!

## Como devo realizar o desafio?
Você deve fazer um fork desse repositório, nele, você vai realizar todas as alterações necessárias para finalizar o desafio. Quando finalizado, crie um pull request do seu repositório para o nosso repositório para avaliarmos suas alterações.

## O problema
Precisamos que você desenvolva uma API que cadastre, retorne, altere e exclua pessoas que estão armazenadas em um banco de dados, e como essas pessoas têm um endereço no cadastro, há também CEPs guardados nesse banco para preencher os endereços dessas pessoas. No cadastro de pessoas, para o cadastro de endereço, só será enviado o CEP, Complemento e Número, o resto será preenchido com os dados que já vão estar atrelados ao CEP. E, como esses dados são confidenciais, essa API não pode ser publica, então ela tem que ter uma autenticação previa para poder ser usada.

## Requisitos Mínimos para a API
- Geral
  - O backend deve ser feito em C# usando ASP.NET Core 5, outras linguagens ou tecnologias não serão aceitas.
  - Não há necessidade de providenciar um dump do banco ou dados mockados para serem usados na avaliação.
  - Todos os dados confidenciais da aplicação, como: connection strings, tokens de api, etc; Devem ser salvas em arquivo de ambiente (ex.: appsettings.json), e não devem ser providenciadas na finalização do desafio. 
- Autenticação em JWT
  - As tabelas que iram guardar os dados de autenticação ficam de livre escolha do desenvolvedor para serem modeladas do jeito que preferirem.
  - Essas tabelas criadas não podem interferir nas tabelas de Pessoas e Ceps. Ex.: Relacionamento de tabela Usuário (usada para autenticação) com Pessoas; ou também, salvar as credenciais de usuários em Pessoas.
  - Não há necessidade de implementar autorização, somente autenticação.
- CEP
  - Essa é uma rota que deve conter métodos para retornar o endereço a partir do cep enviado, e outro para atualizar todos os CEPs da tabela a partir de uma API de terceiros.
    - Para o retorno de endereço a partir de CEP, você deve criar um método que receba uma requisição GET com um parâmetro de CEP em string, e retornar o endereço buscando o cep no banco de dados.
    - O outro método deve atualizar o CEP enviado por query string a partir de uma API de terceiros, ela usara a API para incluir ou atualizar o CEP enviado. A API que deve ser usada é a do [CEP Aberto](https://www.cepaberto.com/), ela deve ser implementada usando um modelo de serviço do ASP.NET. Esse método tem que ser GET também!
- Pessoa
  - Essa rota deve ter todas as operações para se realizar um CRUD, mas com alguns a serem observados:
    - Na criação/edição de uma pessoa, ela deve enviar apenas: nome, CPF, sexo, data de nascimento, cep, complemento, número e telefone. Caso não tenha um CEP cadastrado no banco, deve retornar uma mensagem de erro informando que não existe o CEP na base.
    - No retorno de pessoas, para o endereço, não pode somente retornar CEP, complemento e logradouro. Tem que ser retornado todos os dados de endereço, como logradouro, bairro, cidade e estado.
- Banco de dados
  - O banco de dados desejável é o MongoDB, mas caso prefira, qualquer outro banco relacional sera aceito. O uso do MongoDB é um **diferencial**.
  - As tabelas de Pessoas e CEPs tem que ter os modelos especificados abaixo.

## Diferenciais
- Será um diferencial, além do MongoDB, a implementação de um frontend para exibição das pessoas, usando a API para receber os dados e realizar autenticação. O Front teria as seguintes telas:
  - Uma tela para login, para se autenticar com o backend
  - Uma tela com cards, sendo que cada card é uma pessoa
- O Front pode ser realizado em qualquer linguagem ou tecnologia que preferir, mas tem que ficar no mesmo repositório da API.

## Modelos do banco
### Pessoas
```json
{
  "Nome": "string",
  "CPF": "long",
  "Sexo": "enum",
  "DataNascimento": "datetime",
  "Endereco": {
    "Cep": "long",
    "Complemento": "string",
    "Numero": "string"
  },
  "Telefone": "long"
}
```
### CEP
```json
{
  "Cep": "long",
  "Logradouro": "string",
  "Bairro": "string",
  "Cidade": "string",
  "Estado": "string"
}
```

Qualquer dúvida, sugestão ou problema que encontrar, entre em contato criando uma issue aqui neste repositório ou nesse e-mail: **dev01@rissicontabilidade.com.br**. **Iremos te ajudar no que precisar!**
Boa sorte e até breve!

> Tente uma, duas, três vezes e se possível tente a quarta, a quinta e quantas vezes for necessário. Só não desista nas primeiras tentativas, a persistência é amiga da conquista. Se você quer chegar onde a maioria não chega, faça o que a maioria não faz.
> - Bill Gates
