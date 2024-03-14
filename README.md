# Ecommerce


O objetivo do projeto é criar uma arquitetura baseada em microserviço de um e-commece com que foi passado em sala de aula. O diagrama do projeto segue o diagrama C4:

# 1) Diagrama de Contexto

![alt text](https://www.dropbox.com/scl/fi/yeh358li8bhez93pdksnh/ecommerce_context-P-gina-5.drawio.png?rlkey=ykddvcufco62m2v099a26rll4&raw=1)

Utilizamos dois serviços externos, de email que pode ser a solução MaiGun utilizada no mercado, e um Gateway de pagamento como por exemplo o PagSeguro.


# 2) Diagrama de container (Ecommerce)

![alt text](https://www.dropbox.com/scl/fi/i6mbejt4nzwdmi66caai4/ecommerce_context-P-gina-6.drawio-1.png?rlkey=ty4vmn8m6qq0cyn196isjfy8t&raw=1)

Aqui representada o digrama contendo maiores informações sobre o e-commece e como seus sistemas estão distribuídos em um arquitetura de microserviços.

O frontend tem a versão web app,  que é uma SPA feita em React, além de possuir a versão mobile feito em flutter atendendo o sistema IOS e Android.

A comunicação feita entre as APIs se dá via mensageria feita pelo RabbitMQ. Caso precise de uma resposta síncrona entre as requisições as APIs se comunicam via Rabbit RPC, um recurso utilizado pelo Rabbit para fazer Remote Procedure Call, sem precisar expor um endpoint para tal. 

Agora para a autenticação, a API Identity expõe uma chave pública, as apis consultam via protocolo http a mesma para verificação do token.

Também utilizamos uma BFF para consolidar os dados para o Cliente para a exibição em tela, além de fazer algumas poucas validações para verificar a consistência dos dados inseridos pelo usuário.

# 3.1) Diagrama de Componentes (Identity API)
![alt text](https://www.dropbox.com/scl/fi/fyathxuhouecpjvrxbmil/ecommerce_context-P-gina-7.drawio.png?rlkey=pcj94i7bbeq345vtqa85yqkcn&raw=1)


Essa api tem como função fazer o cadastro de um usuário, persistindo dados exclusivos de autenticação como email e senha. Além disso expõe uma jwk pública para que outra Api que utilize este sistema de autenticação consiga identificar a validade de um token.

Quando é feito um cadastro de um usuário a API Identidade faz uma chamada a Api de cliente para que a última validade os dados fornecidos, e persista caso coerente. Caso dados inválidos pela Api de cliente, a Api de identidade deleta o registro cadastrado e retorna para a bff que os dados fornecidos foram inválidos.

# 3.2) Diagrama de Componentes (Client API)

![alt text](https://www.dropbox.com/scl/fi/ot8zu56w3xpdn4g8zy2dh/ecommerce_context-P-gina-8.drawio.png?rlkey=23ukf9xgsqioua2dqu6t2tv2l&raw=1)

Essa api tem como função persistir dados do cliente, e fazer a validação desses dados.

# 3.3) Diagrama de Componentes (Product API)
![alt text](https://www.dropbox.com/scl/fi/lojrnoimyuhoeba8dcqnb/ecommerce_context-P-gina-9.drawio.png?rlkey=acyjz73jvt1khpo45mkh9ldp8&raw=1)

Essa api tem como função exibir o catálogo do Ecommerce, assim como manipular os dados do estoque.

Essa api tem um listener do Rabbit para receber eventos de Pedido Autorizado (mandada pela API de ordem). Caso a ordem contenha todos os pedidos no estoque, ela envia um evento de caixa baixado, que reduz o valor em estoque. Caso contrário, envia um evento de cancelamento do pedido.


# 3.4) Diagrama de Componentes (Carrinho API)
![alt text](https://www.dropbox.com/scl/fi/3qjo4c9228c5crxbaqpdc/ecommerce_context-P-gina-10.drawio.png?rlkey=6sckkhldaai77m3pierzno2uf&raw=1)


Essa api tem como função exibir o os items no carrinho de um cliente. Quando receber uma ordem de pedido realizado (realizado pela api de ordem), os items no carrinho são excluídos.

# 3.5) Diagrama de Componentes (Order API)
![alt text](https://www.dropbox.com/scl/fi/pqwn48zv1mjovkrr1c2tm/ecommerce_context-P-gina-11.drawio.png?rlkey=jbp0mu0709ultt5dgkpl8oyk5&raw=1)

Essa api tem como função gerenciar os pedidos feito pelo cliente.

É feito uma chamada pela cliente, que passa pela BFF, para um determinado pedido. Esse pedido por sua vez é enviado para o Rabbit como Pedido realizado, que por sua vez limpa o carrinho como mencionado anteriormente.
 
O pedido é salvo em uma base de pedidos. É feito um job na aplicacao que lê de tempos em tempos os pedidos que foram realizados e mandados para fila como pedido autorizado.

O Pedido autorizado é pego pela API de Produto (anteriormente mencionada) que verifica a validade do estoque daqueles produtos.

Essa API também consome eventos de pedidos finalizados, isto é, cancelado ou pago, para desativar o pedido ( para não ficar mais sendo pega no job). Além disso, notifica o usuário  que chama uma api de terceiros para envio de email notificando o usuário a finalização do pedido (parte a ser feita).


# 3.6) Diagrama de Componentes (Pagamento API)

![alt text](https://www.dropbox.com/scl/fi/uqzkqa21lmv2r7xs7pbg2/ecommerce_context-P-gina-12.drawio.png?rlkey=pg855ua25re0s0zr9pwaa03yl&raw=1)



Essa api tem como função ser um adapter (ou como preferir uma ACL) para o Gateway de pagamento.

Recebe eventos de pedido baixado para realizar o pagamento, ou pedido cancelado para realizar o extorno. Caso sucesso emite o evento de pedido pago que é consumido pela api de pedido (Order API).

OBS: Esse serviço não foi codificado.


Execução do projeto:

Via docker (Recomendado):

Na raiz execute: 
      
      docker compose -f docker-compose.yaml up -d

Ele já vai subir todas os serviços comunicando com seus respectivos banco de dados. Além de subir um container do RabbitMQ e fazer um seed das colunas dos bancos.

Via Solução:

Executar a Solução “Ecommerce.sln” startando todos os projetos que possuem o entrypoint
Precisa ter o dotnet8 instalado
Na raiz execute: 
      
      docker compose -f docker-compose.db-mq.yaml up -d 

para subir o banco de dados e o rabbit mq sem as soluções.


* * * *

TODO Melhorias:


1. Enviar o email quando o usuario for cadastrado com uma redirect url para validar o email
2. Ter o um endpoint para reset de senha
3. Ter um endpoint para solicitar que esqueceu a senha
4. Usar SSO e open ID para fazer autenticação via rede social
5. Criação da BFF
6. Criação da Api de pagamento




