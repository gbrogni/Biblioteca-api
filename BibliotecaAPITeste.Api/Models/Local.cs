using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPITeste.Api.Models
{
  public class Local
  {
    [Key]
    public int CodLocal { get; set; }
    public string Descricao { get; set; }
  }
}
