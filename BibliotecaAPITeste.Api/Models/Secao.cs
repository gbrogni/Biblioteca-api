using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPITeste.Api.Models
{
  public class Secao
  {
    [Key]
    public int CodSecao { get; set; }
    public string Descricao { get; set; }
    
  }
}
