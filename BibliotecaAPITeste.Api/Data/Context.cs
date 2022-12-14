using BibliotecaAPITeste.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPITeste.Api.Data
{
  public class Context : DbContext
  {
    public Context (DbContextOptions<Context> options)
      : base(options) { }

    public DbSet<Autor> Autores { get; set; }
    public DbSet<Editora> Editoras { get; set; }
    public DbSet<Item> Itens { get; set; }
    public DbSet<Local> Locais { get; set; }
    public DbSet<Secao> Secoes { get; set; }
    public DbSet<Leitor> Leitores { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .Build();

      optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
    }
  }
} 
