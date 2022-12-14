using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPITeste.Api.Models
{
  public class Reserva
  {
    [Key]
    public int CodReserva { get; set; }
    public int CodItem { get; set; }
    public string Situacao { get; set; }
    public int CodLocal { get; set; }
    public int CodLeitor { get; set; }
    public DateTime DataReserva { get; set; }
    public DateTime PrazoReserva { get; set; }
  }
}
