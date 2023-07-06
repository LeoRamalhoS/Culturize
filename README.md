# Culturize

Plataforma de gerenciamento e transformação cultural para empresas.

Criada com .Net 7, MVC, EF Core, MS Identity. Integrada com AzureStorage (Blobs) e Application Insights.

Esse repositório possui integraçao com CI/CD da Azure, ao realizar push na branch MASTER a versão publicada será também atualizada.

Atualmente conta com 3 simples camadas e a WebApp. Futuramente será refatorada para uma arquitetura mais enxuta como Cebola ou Hexagonal por exemplo.


## Como Utilizar

É possível acessar a aplicação publicada em: https://culturize.azurewebsites.net/ (mediante disponibilidade do plano free do Azure)

Antes de rodar a aplicação, abra o console do gerenciador de pacotes apontando para a class library **Culturize.DataAccess** e entao rode o comando **update-database**, 
isso irá gerar o banco localmente (DefaultConnection - appsettings.json)

Ao rodar a aplicação será possível registrar usuarios e fazer login (canto superior direito). 

A plataforma possui 4 roles de usuario. No estado atual apenas a ADMIN é relevante pois libera o acesso ao cadastro de empresas.

No cadastro de empresas é possível criar/editar/excluir empresas.


## Tech Challenge 

Para cumprir os requisitos do challenge, foram feitas as seguintes implementações:

**Requisito**: Integração com AzureStorage fazendo upload de um Blob
**Solução**: É possível realizar upload de uma imagem para servir como logo da empresa. A imagem será armazenada como um Blob no Azure, e se for alterada ou a empresa for
removida, o blob será atualizado de acordo.

**Requisito**: Possuir uma API
**Solução**: A aplicação em sua maior parte utiliza MVC, então, para cumprir o requisito a exclusão de Empresas é feita por um [HttpDelete] que retorna JSON na CompanyController 
(Admin/CompanyController/Delete).






