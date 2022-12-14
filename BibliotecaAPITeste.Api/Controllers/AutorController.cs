using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[AutorController]")]
  [ApiController]
  public class AutorController : ControllerBase
  {

    private readonly Context _context;


    public AutorController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/autores")]
    public async Task<ActionResult> GetAutores()
    {
      return Ok(await _context.Autores.ToListAsync());
    }

    [HttpGet()]
    [Route("/autores/{codAutor}")]
    public async Task<ActionResult> GetById(int codAutor)
    {
      var result = await _context.Autores.FindAsync(codAutor);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    [Route("/autores")]
    public async Task<ActionResult> CreateAutor (Autor autor)
    {
      await _context.Autores.AddAsync(autor);
      await _context.SaveChangesAsync();
      return Ok(autor);
    }

    [HttpPut()]
    [Route("/autores/{codAutor}")]
    public async Task<IActionResult> UpdateAutor (int codAutor, Autor autor)
    {
      if (codAutor != autor.CodAutor)
      {
        return BadRequest();
      }

      _context.Entry(autor).State = EntityState.Modified;
      
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AutorExists(autor.CodAutor))
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
    [Route("/autores/{codAutor}")]
    public async Task<IActionResult> DeleteAutor(int codAutor)
    {
      var autor = await _context.Autores.FindAsync(codAutor);
      if (autor == null)
      {
        return NotFound();
      }

      _context.Autores.Remove(autor);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool AutorExists(int codAutor)
    {
      return _context.Autores.Any(e => e.CodAutor == codAutor);
    }
  }
}
