# api-filmes-exs

Projeto em .NET Core 2.1
Necessário SDK instalado na máquina.

Para rodar o server na sua máquina faça:

Por default o server está apontando para a base de dados da AWS então basta rodar!
Caso queira executar em Localhost, siga os passos:
Nos arquivos de configuração, package.json alterar a connection string apontando para o seu banco de dados. São três arquivos de configuração, onde um fica no projeto API para startar a aplicação outro para o projeto Identity que faz a parte de autenticação e autorização, e outro para o contexto do domínio da aplicação.

<p>1º arquivo está aqui: https://bit.ly/2OZNMFP</p>
<p>2º arquivo está aqui: https://bit.ly/2Rem4a2</p>
<p>3º arquivo está aqui: https://bit.ly/2DHqMdO</p>

Configuração para todos os arquivos:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=WAGNER-PC;Database=exs;Trusted_Connection=True;MultipleActiveResultSets=True",
    "AWS": "Data Source=rds-nogueira.crwawwopzg32.sa-east-1.rds.amazonaws.com;Initial Catalog=exs;User ID=wagner;Password=Wagner1234"
    "Local": "minha-connection-string"
  }
}

Caso você prefira alterar somente o valor da propriedade AWS, não terá de fazer mais nada! Mas caso siga o exemplo acima, terá que fazer referência a sua connection string nas classes necessárias para o Entity Framework entender, que são três:

<p>1º aqui: https://bit.ly/2y2T2RQ linha 23</p>
<p>2º aqui: https://bit.ly/2y4bBFr linha 33</p>
<p>3º aqui: https://bit.ly/2NeGD2G linha 32</p>

Alterar somente o valor colocando o mesmo que colocou no arquivo Json.

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
  var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

  optionsBuilder.UseSqlServer(config.GetConnectionString("Local")); 
}

Como o Entity framework irá recriar a base e como existem dois contextos um para o domínio da aplicação e outro para o Identity então basta fazer o procedimento utilizando o Package Manager Console, irá apontar para dois projetos e executar o comando:

update-database -c ContextDB para o projeto Exs.Infra.Data
update-database -c ApplicationDbContext para o projeto Exs.Infra.Identity

-------------

Execute o projeto Exs.Api, não deixe apontando para o IIS.
Ao rodar por default vai abrir a documentação da API fornecida pela Swagger.
Documentação API Localhost: http://localhost:3405/swagger/index.html

