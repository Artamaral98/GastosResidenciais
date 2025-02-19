Instruções para inicialização do projeto: Recomendo a utilização do VSCODE 2022 para análise.
-Antes de tudo, cabe salientar que a aplicação utiliza banco de dados SQL server. Dessa forma, antes de inicializar o projeto, insira a Connection String referente ao seu login SQL server
- "Connection": "Data Source=NOMEDOSEUSERVIDOR; Initial Catalog=gastosresidenciais;User ID=SEULOGONDOSQLSERVER;Password=SUASENHA;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"

- A ConnectionString deverá sem implementada em "Connection": "". Você encontrará no diretório: Server/Backend/GastosResidenciais.API/appsettings/appsettings.Development
- - Não é necessária a criação manual do servidor/schema, apenas insira suas credenciais de login SQL SERVER e inicialize o projeto.

-Após realizar a conexão com o banco de dados, siga as instruções para inicializar a aplicação:
	- Inicie a API teclando F5, ou cliando no botão de play acima.
	- para iniciar o frontend, no terminal da solution, insira os seguintes comandos:
		- cd Client
		- cd Client
		-npm install (instalar as dependencias do projeto)
		- npm run dev
		- a porta http://localhost:49918 será inicializada, abra em seu navegador para ter acesso ao projeto.

-Ao iniciar a API, o Swagger será aberto, mostrando todos os endpoints e funcionalidades em relação a usuários e transações.
- Em relação a usuários, é possível:
	- criar usuário .
	- listar um único usuário .
	- listar todos os usuários, demonstrando o balanço total de cada um.
	- Deletar um usuário.
	- 
- Em relação as transações, é possível:
	-Criar, deletar ou atualizar a transação de um usuário.
	-Listar todas as transações e os seus respectivos usuários, ou apenas uma transação.


- Acesse o frontend em http://localhost:49918 para uma melhor usabilidade do projeto.

- Sobre o projeto a sua construção lógica:
	- A presente aplicação foi desenvolvida, no que tange a API, seguindo a abordagem do DDD (Domain Driven Design) em relação às práticas de organização, estruturação e interação das camadas,
facilitando a escalabilidade, manutenção e desenvolvimento do código.
	- Em relação ao Frontend, este foi desevolvido utilizando React, TypeScript, Tailwind CSS, além de seguir boas práticas de desenvolvimento, escrita e organização, assim como o backend.

Backend

Arquitetura, Fluxo e Domain Driven Design

O backend foi desenvolvido com uma arquitetura baseada na abordagem Domain Driven Design (DDD), que consiste em dividir o sistema em quatro camadas principais: API, Application, Infrastrcture e Domain

Responsabilidades dos projetos:
Api: Local onde são definidos os controllers, que recebem requisições e devolvem respostas (sucesso ou erro).

Appication: Projeto que recebe a requisição capturada e repassada pelo projeto e API. Neste projeto, são implementada as regras de negócio. Por exemplo, ao criar uma transação para um  
usuário cadastrado como menor de idade (menor de 18), apenas despesas deverão ser aceitas. Essa validação é realizada através de uma classe específica, usando a biblioteca FluentValidator, que checa
se todos os elementos da requisição estão válidos, caso contrário retorna uma mensagem de erro específica para a validação que não foi aprovada
	
Infrastructure: Implementa os códigos que executam serviços externos a API, como Banco de Dados. A implementação do EntityFramework e conexão com SQL Server estão presentes neste projeto. Os métodos
responsáveis por criar, deletar, atualizar e lista estão presentes neste projeto.

Domain: Uma das ideias chave do DDD é criar uma linguagem comum a todos os envolvidos no projeto. Então todas as propriedades que compoem as entidades usuário ou transações estarão presentes neste 
projeto. Ex: nome, idade. Além disso, neste projeto estão situados as interfaces (contratos), que definem os métodos implementados em Infrastructure. Essa interface será recebida pelo projeto de 
Application, para implementar os métodos, separando as regras de negócio dos dados propriamente ditos, contidos em Infrastructure. Para isso, é utilizada a Injeção de dependência.

Exception: Projeto responsável por conter a base das execções que serão tratadas e enviadas como resposta, além das mensagens de erros customizadas para cada caso.

Communication: Responsável por armazenar os modelos de Requests e Responses para cada funcionalidade da aplicação.

Fluxo: A requisição é recebida pela API -> API chama o controller pertinente de acordo com a rota, que chama o useCase do caso específico -> Requisição enviada para a regra de negócio (projeto de 
Application) dentro do useCase-> Após a validação, caso tudo esteja correto, haverá a adoção dos métodos do projeto de Infrastructure, repassados a através de uma interface para que seja 
realizada a operação pertinente, e haja a conversação com a base de dadose salvar as alterações.

FrontEnd.

O frontend foi desenvolvido com React, TypeScript, Tailwind CSS, seguindo práticas de componentização, hooks e gerenciamento de estado e roteamento do React Router Dom´.

Principais Funcionalidades

Novo Usuário, Rota "/": Formulário de criação de usuário, devendo ser repassado o nome e a idade.

Listar usuários Rota "/usuarios": Retorna uma lista de todos os usuários, informando nome, idade e permite a deleção e verificação de todas as transações daquele usuário através da abertura de um modal,
com rolagem e cores distintas para receitas e despesa.

Nova Transação, Rota"/nova-transacao": Permite a criação de transação para uma pessoa, implementando a regra de negócio de verificação de menor de idade. O Caso o usuário selecione um menor de idade 
(menor de 18), apenas despesas serão criadas

Transações, Rota "/transacoes": Listagem de todas as transações criadas até o momento, ordenadas pela criada mais recente, mostrando a qual usuário pertence. Além de permitir a deleção e atualização 
da respectiva transação através da abertuda de modal.

Consulta de Totais: Exibe todos os usuários cadastrados, exibindo o total de receitas, despesas e o saldo (receita – despesa) de cada um. Ao final da listagem exibe o total geral de todos os 
usários incluindo o total de receitas, total de despesas e o saldo líquido.