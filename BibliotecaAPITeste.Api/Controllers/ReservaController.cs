using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReservaController : ControllerBase
  {

    private readonly Context _context;


    public ReservaController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/reservas")]
    public async Task<ActionResult> GetReservas()
    {
      var query = (from r in _context.Reservas
                   join i in _context.Itens on r.CodItem equals i.CodItem
                   join le in _context.Leitores on r.CodLeitor equals le.CodLeitor
                   join l in _context.Locais on i.CodLocal equals l.CodLocal
                   select new
                   {
                     CodReserva = r.CodReserva,
                     Nome = i.Nome,
                     TipoItem = i.TipoItem,
                     NumExemplar = i.NumExemplar,
                     DescricaoLocal = l.Descricao,
                     NomeLeitor = le.Nome,
                     DataReserva = r.DataReserva,
                     PrazoReserva = r.PrazoReserva,
                     Situacao = r.Situacao 
                   });

      return Ok(await query.ToListAsync());

      //return Ok(await _context.Reservas.ToListAsync());
    }

    [HttpGet()]
    [Route("/reservas/{codItem}")]
    public async Task<ActionResult> GetById(int codItem)
    {
      var result = await _context.Reservas.FindAsync(codItem);

      if (result == null)
      {
        return NotFound();
      }
      else
      {
        var query = (from r in _context.Reservas
                     join i in _context.Itens on r.CodItem equals i.CodItem
                     join le in _context.Leitores on r.CodLeitor equals le.CodLeitor
                     join l in _context.Locais on i.CodLocal equals l.CodLocal
                     select new
                     {
                       CodReserva = r.CodReserva,
                       Nome = i.Nome,
                       TipoItem = i.TipoItem,
                       NumExemplar = i.NumExemplar,
                       DescricaoLocal = l.Descricao,
                       NomeLeitor = le.Nome,
                       DataReserva = r.DataReserva,
                       PrazoReserva = r.PrazoReserva,
                       Situacao = r.Situacao
                     });
      }

      return Ok(result);
    }

    [HttpPost]
    [Route("/reservas")]
    public async Task<ActionResult> CreateReserva(Reserva reserva)
    {
      await _context.Reservas.AddAsync(reserva);
      await _context.SaveChangesAsync();
      return Ok(reserva);
    }

    [HttpPut()]
    [Route("/reservas/{codItem}")]
    public async Task<IActionResult> UpdateReserva(int codItem, Reserva reserva)
    {
      if (codItem != reserva.CodItem)
      {
        return BadRequest();
      }
      _context.Entry(reserva).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReservaExists(reserva.CodItem))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

    [HttpDelete()]
    [Route("/reservas/{codItem}")]
    public async Task<IActionResult> DeleteReserva(int codItem)
    {
      var reserva = await _context.Reservas.FindAsync(codItem);
      if (reserva == null)
      {
        return NotFound();
      }

      _context.Reservas.Remove(reserva);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ReservaExists(int codItem)
    {
      return _context.Reservas.Any(e => e.CodItem == codItem);
    }
  }
}
