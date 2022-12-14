using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocalController : ControllerBase
  {

    private readonly Context _context;


    public LocalController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/locais")]
    public async Task<ActionResult> GetLocais()
    {
      return Ok(await _context.Locais.ToListAsync());
    }

    [HttpGet()]
    [Route("/locais/{codLocal}")]
    public async Task<ActionResult> GetById(int codLocal)
    {
      var result = await _context.Locais.FindAsync(codLocal);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    [Route("/locais")]
    public async Task<ActionResult> CreateLocal(Local local)
    {
      await _context.Locais.AddAsync(local);
      await _context.SaveChangesAsync();
      return Ok(local);
    }

    [HttpPut()]
    [Route("/locais/{codLocal}")]
    public async Task<IActionResult> UpdateLocal(int codLocal, Local local)
    {
      if (codLocal != local.CodLocal)
      {
        return BadRequest();
      }
      _context.Entry(local).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LocalExists(local.CodLocal))
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
    [Route("/locais/{codLocal}")]
    public async Task<IActionResult> DeleteEditora(int codLocal)
    {
      var local = await _context.Locais.FindAsync(codLocal);
      if (local == null)
      {
        return NotFound();
      }

      _context.Locais.Remove(local);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool LocalExists(int codLocal)
    {
      return _context.Locais.Any(e => e.CodLocal == codLocal);
    }
  }
}
