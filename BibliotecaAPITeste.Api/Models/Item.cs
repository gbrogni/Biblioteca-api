using BibliotecaAPITeste.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPITeste.Api.Models
{
  public class Item
  {
    [Key]
    public int CodItem { get; set; }
    public string Nome { get; set; }
    public string TipoItem { get; set; }
    public string NumExemplar { get; set; }
    public string Volume { get; set; }
    public string Edicao { get; set; }
    public string Idioma { get; set; }
    public string Status { get; set; }

    public int CodAutor { get; set; }


    public int CodEditora { get; set; }

    public int CodLocal { get; set; }


    public int CodSecao { get; set; }

  }
}

