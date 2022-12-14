using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LeitorController : ControllerBase
  {

    private readonly Context _context;


    public LeitorController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/leitores")]
    public async Task<ActionResult> GetLeitores()
    {
      return Ok(await _context.Leitores.ToListAsync());
    }

    [HttpGet()]
    [Route("/leitores/{codLeitor}")]
    public async Task<ActionResult> GetById(int codLeitor)
    {
      var result = await _context.Leitores.FindAsync(codLeitor);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    [Route("/leitores")]
    public async Task<ActionResult> CreateLeitor(Leitor leitor)
    {
      await _context.Leitores.AddAsync(leitor);
      await _context.SaveChangesAsync();
      return Ok(leitor);
    }

    [HttpPut()]
    [Route("/leitores/{codLeitor}")]
    public async Task<IActionResult> UpdateLeitor(int codLeitor, Leitor leitor)
    {
      if (codLeitor != leitor.CodLeitor)
      {
        return BadRequest();
      }
      _context.Entry(leitor).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LeitorExists(leitor.CodLeitor))
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
    [Route("/leitores/{codLeitor}")]
    public async Task<IActionResult> DeleteLeitor(int codLeitor)
    {
      var leitor = await _context.Leitores.FindAsync(codLeitor);
      if (leitor == null)
      {
        return NotFound();
      }

      _context.Leitores.Remove(leitor);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool LeitorExists(int codLeitor)
    {
      return _context.Leitores.Any(e => e.CodLeitor == codLeitor);
    }
  }
}
