using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SecaoController : ControllerBase
  {

    private readonly Context _context;


    public SecaoController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/secoes")]
    public async Task<ActionResult> GetSecoes()
    {
      return Ok(await _context.Secoes.ToListAsync());
    }

    [HttpGet()]
    [Route("/secoes/{codSecao}")]
    public async Task<ActionResult> GetById(int codSecao)
    {
      var result = await _context.Secoes.FindAsync(codSecao);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    [Route("/secoes")]
    public async Task<ActionResult> CreateSecao(Secao secao)
    {
      await _context.Secoes.AddAsync(secao);
      await _context.SaveChangesAsync();
      return Ok(secao);
    }

    [HttpPut()]
    [Route("/secoes/{codSecao}")]
    public async Task<IActionResult> UpdateSecao(int codSecao, Secao secao)
    {
      if (codSecao != secao.CodSecao)
      {
        return BadRequest();
      }
      _context.Entry(secao).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SecaoExists(secao.CodSecao))
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
    [Route("/secoes/{codSecao}")]
    public async Task<IActionResult> DeleteSecao(int codSecao)
    {
      var secao = await _context.Secoes.FindAsync(codSecao);
      if (secao == null)
      {
        return NotFound();
      }

      _context.Secoes.Remove(secao);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool SecaoExists(int codSecao)
    {
      return _context.Secoes.Any(e => e.CodSecao == codSecao);
    }
  }
}
