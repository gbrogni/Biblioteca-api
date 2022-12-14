using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPITeste.Api.Models
{
  public class Leitor
  {
    [Key]
    public int CodLeitor { get; set; }
    public string Nome { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string RG { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Celular { get; set; }
    public string EnderecoRua { get; set; }
    public string EnderecoBairro { get; set; }
    public string EnderecoCidade { get; set; }
    public string EnderecoNumero { get; set; }
    public string CEP { get; set; }
    public string UF { get; set; }
  }
}
