Instru��es para inicializa��o do projeto: Recomendo a utiliza��o do VSCODE 2022 para an�lise.
-Antes de tudo, cabe salientar que a aplica��o utiliza banco de dados SQL server. Dessa forma, antes de inicializar o projeto, insira a Connection String referente ao seu login SQL server
- "Connection": "Data Source=NOMEDOSEUSERVIDOR; Initial Catalog=gastosresidenciais;User ID=SEULOGONDOSQLSERVER;Password=SUASENHA;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"

- A ConnectionString dever� sem implementada em "Connection": "". Voc� encontrar� no diret�rio: Server/Backend/GastosResidenciais.API/appsettings/appsettings.Development

-Ap�s realizar a conex�o com o banco de dados, siga as instru��es para inicializar a aplica��o:
	- Inicie a API teclando F5, ou cliando no bot�o de play acima.
	- para iniciar o backend, no terminal da solution, insira os seguintes comandos:
		- cd Client
		- cd Client
		- npm run dev
		- a porta http://localhost:49918 ser� inicializada, abra em seu navegador para ter acesso ao projeto.
