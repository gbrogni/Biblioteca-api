using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConsultaItemController : ControllerBase
  {

    private readonly Context _context;


    public ConsultaItemController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/consultas")]
    public async Task<ActionResult> GetConsultas()
    {
      var query = (from i in _context.Itens
                   join a in _context.Autores on i.CodAutor equals a.CodAutor
                   join e in _context.Editoras on i.CodEditora equals e.CodEditora
                   join l in _context.Locais on i.CodLocal equals l.CodLocal
                   join s in _context.Secoes on i.CodSecao equals s.CodSecao
                   select new
                   {
                     CodItem = i.CodItem,
                     NomeItem = i.Nome,
                     TipoItem = i.TipoItem,
                     NumExemplar = i.NumExemplar,
                     NomeAutor = a.NomeAutor,
                     NomeEditora = e.NomeEditora,
                     DescricaoLocal = l.Descricao,
                     DescricaoSecao = s.Descricao,
                     Situacao = i.Status
                   });

      return Ok(await query.ToListAsync());
    }

    [HttpGet]
    [Route("/consultasE")]
    public async Task<ActionResult> GetConsultasE()
    {
      var query = (from r in _context.Reservas
                   join i in _context.Itens on r.CodItem equals i.CodItem
                   join e in _context.Editoras on i.CodEditora equals e.CodEditora
                   join l in _context.Locais on i.CodLocal equals l.CodLocal
                   join s in _context.Secoes on i.CodSecao equals s.CodSecao
                   join a in _context.Autores on i.CodAutor equals a.CodAutor
                   select new
                   {
                     CodReserva = r.CodReserva,
                     DataReserva = r.DataReserva,
                     PrazoReserva = r.PrazoReserva,
                     CodItem = i.CodItem,
                     NomeItem = i.Nome,
                     TipoItem = i.TipoItem,
                     NomeAutor = a.NomeAutor,
                     NomeEditora = e.NomeEditora,
                     DescricaoLocal = l.Descricao,
                     DescricaoSecao = s.Descricao,

                   });

      return Ok(await query.ToListAsync());
    }
  }
}
