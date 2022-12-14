using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPITeste.Api.Models
{
  public class Editora
  {
    [Key]
    public int CodEditora { get; set; }
    public string NomeEditora { get; set; }
  }
}
