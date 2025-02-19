Instruções para inicialização do projeto: Recomendo a utilização do VSCODE 2022 para análise.
-Antes de tudo, cabe salientar que a aplicação utiliza banco de dados SQL server. Dessa forma, antes de inicializar o projeto, insira a Connection String referente ao seu login SQL server
- "Connection": "Data Source=NOMEDOSEUSERVIDOR; Initial Catalog=gastosresidenciais;User ID=SEULOGONDOSQLSERVER;Password=SUASENHA;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"

- A ConnectionString deverá sem implementada em "Connection": "". Você encontrará no diretório: Server/Backend/GastosResidenciais.API/appsettings/appsettings.Development

-Após realizar a conexão com o banco de dados, siga as instruções para inicializar a aplicação:
	- Inicie a API teclando F5, ou cliando no botão de play acima.
	- para iniciar o backend, no terminal da solution, insira os seguintes comandos:
		- cd Client
		- cd Client
		- npm run dev
		- a porta http://localhost:49918 será inicializada, abra em seu navegador para ter acesso ao projeto.
