Instru��es para inicializa��o do projeto: Recomendo a utiliza��o do VSCODE 2022 para an�lise.
-Antes de tudo, cabe salientar que a aplica��o utiliza banco de dados SQL server. Dessa forma, antes de inicializar o projeto, insira a Connection String referente ao seu login SQL server
- "Connection": "Data Source=NOMEDOSEUSERVIDOR; Initial Catalog=gastosresidenciais;User ID=SEULOGONDOSQLSERVER;Password=SUASENHA;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"

- A ConnectionString dever� sem implementada em "Connection": "". Voc� encontrar� no diret�rio: Server/Backend/GastosResidenciais.API/appsettings/appsettings.Development
- - N�o � necess�ria a cria��o manual do servidor/schema, apenas insira suas credenciais de login SQL SERVER e inicialize o projeto.

-Ap�s realizar a conex�o com o banco de dados, siga as instru��es para inicializar a aplica��o:
	- Inicie a API teclando F5, ou cliando no bot�o de play acima.
	- para iniciar o frontend, no terminal da solution, insira os seguintes comandos:
		- cd Client
		- cd Client
		-npm install (instalar as dependencias do projeto)
		- npm run dev
		- a porta http://localhost:49918 ser� inicializada, abra em seu navegador para ter acesso ao projeto.

-Ao iniciar a API, o Swagger ser� aberto, mostrando todos os endpoints e funcionalidades em rela��o a usu�rios e transa��es.
- Em rela��o a usu�rios, � poss�vel:
	- criar usu�rio .
	- listar um �nico usu�rio .
	- listar todos os usu�rios, demonstrando o balan�o total de cada um.
	- Deletar um usu�rio.
	- 
- Em rela��o as transa��es, � poss�vel:
	-Criar, deletar ou atualizar a transa��o de um usu�rio.
	-Listar todas as transa��es e os seus respectivos usu�rios, ou apenas uma transa��o.


- Acesse o frontend em http://localhost:49918 para uma melhor usabilidade do projeto.

- Sobre o projeto a sua constru��o l�gica:
	- A presente aplica��o foi desenvolvida, no que tange a API, seguindo a abordagem do DDD (Domain Driven Design) em rela��o �s pr�ticas de organiza��o, estrutura��o e intera��o das camadas,
facilitando a escalabilidade, manuten��o e desenvolvimento do c�digo.
	- Em rela��o ao Frontend, este foi desevolvido utilizando React, TypeScript, Tailwind CSS, al�m de seguir boas pr�ticas de desenvolvimento, escrita e organiza��o, assim como o backend.

Backend

Arquitetura, Fluxo e Domain Driven Design

O backend foi desenvolvido com uma arquitetura baseada na abordagem Domain Driven Design (DDD), que consiste em dividir o sistema em quatro camadas principais: API, Application, Infrastrcture e Domain

Responsabilidades dos projetos:
Api: Local onde s�o definidos os controllers, que recebem requisi��es e devolvem respostas (sucesso ou erro).

Appication: Projeto que recebe a requisi��o capturada e repassada pelo projeto e API. Neste projeto, s�o implementada as regras de neg�cio. Por exemplo, ao criar uma transa��o para um  
usu�rio cadastrado como menor de idade (menor de 18), apenas despesas dever�o ser aceitas. Essa valida��o � realizada atrav�s de uma classe espec�fica, usando a biblioteca FluentValidator, que checa
se todos os elementos da requisi��o est�o v�lidos, caso contr�rio retorna uma mensagem de erro espec�fica para a valida��o que n�o foi aprovada
	
Infrastructure: Implementa os c�digos que executam servi�os externos a API, como Banco de Dados. A implementa��o do EntityFramework e conex�o com SQL Server est�o presentes neste projeto. Os m�todos
respons�veis por criar, deletar, atualizar e lista est�o presentes neste projeto.

Domain: Uma das ideias chave do DDD � criar uma linguagem comum a todos os envolvidos no projeto. Ent�o todas as propriedades que compoem as entidades usu�rio ou transa��es estar�o presentes neste 
projeto. Ex: nome, idade. Al�m disso, neste projeto est�o situados as interfaces (contratos), que definem os m�todos implementados em Infrastructure. Essa interface ser� recebida pelo projeto de 
Application, para implementar os m�todos, separando as regras de neg�cio dos dados propriamente ditos, contidos em Infrastructure. Para isso, � utilizada a Inje��o de depend�ncia.

Exception: Projeto respons�vel por conter a base das exec��es que ser�o tratadas e enviadas como resposta, al�m das mensagens de erros customizadas para cada caso.

Communication: Respons�vel por armazenar os modelos de Requests e Responses para cada funcionalidade da aplica��o.

Fluxo: A requisi��o � recebida pela API -> API chama o controller pertinente de acordo com a rota, que chama o useCase do caso espec�fico -> Requisi��o enviada para a regra de neg�cio (projeto de 
Application) dentro do useCase-> Ap�s a valida��o, caso tudo esteja correto, haver� a ado��o dos m�todos do projeto de Infrastructure, repassados a atrav�s de uma interface para que seja 
realizada a opera��o pertinente, e haja a conversa��o com a base de dadose salvar as altera��es.

FrontEnd.

O frontend foi desenvolvido com React, TypeScript, Tailwind CSS, seguindo pr�ticas de componentiza��o, hooks e gerenciamento de estado e roteamento do React Router Dom�.

Principais Funcionalidades

Novo Usu�rio, Rota "/": Formul�rio de cria��o de usu�rio, devendo ser repassado o nome e a idade.

Listar usu�rios Rota "/usuarios": Retorna uma lista de todos os usu�rios, informando nome, idade e permite a dele��o e verifica��o de todas as transa��es daquele usu�rio atrav�s da abertura de um modal,
com rolagem e cores distintas para receitas e despesa.

Nova Transa��o, Rota"/nova-transacao": Permite a cria��o de transa��o para uma pessoa, implementando a regra de neg�cio de verifica��o de menor de idade. O Caso o usu�rio selecione um menor de idade 
(menor de 18), apenas despesas ser�o criadas

Transa��es, Rota "/transacoes": Listagem de todas as transa��es criadas at� o momento, ordenadas pela criada mais recente, mostrando a qual usu�rio pertence. Al�m de permitir a dele��o e atualiza��o 
da respectiva transa��o atrav�s da abertuda de modal.

Consulta de Totais: Exibe todos os usu�rios cadastrados, exibindo o total de receitas, despesas e o saldo (receita � despesa) de cada um. Ao final da listagem exibe o total geral de todos os 
us�rios incluindo o total de receitas, total de despesas e o saldo l�quido.