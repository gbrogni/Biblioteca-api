using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPITeste.Api.Models
{
  public class Autor
  {
    [Key]
    public int CodAutor { get; set; }
    public string NomeAutor { get; set; }
    public string Descricao { get; set; }
  }
}
