using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EditoraController : ControllerBase
  {

    private readonly Context _context;


    public EditoraController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/editoras")]
    public async Task<ActionResult> GetEditoras()
    {
      return Ok(await _context.Editoras.ToListAsync());
    }

    [HttpGet()]
    [Route("/editoras/{codEditora}")]
    public async Task<ActionResult> GetById(int codEditora)
    {
      var result = await _context.Editoras.FindAsync(codEditora);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    [Route("/editoras")]
    public async Task<ActionResult> CreateEditora(Editora editora)
    {
      await _context.Editoras.AddAsync(editora);
      await _context.SaveChangesAsync();
      return Ok(editora);
    }

    [HttpPut()]
    [Route("/editoras/{codEditora}")]
    public async Task<IActionResult> UpdateEditora(int codEditora, Editora editora)
    {
      if (codEditora != editora.CodEditora)
      {
        return BadRequest();
      }
      _context.Entry(editora).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!EditoraExists(editora.CodEditora))
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
    [Route("/editoras/{codEditora}")]
    public async Task<IActionResult> DeleteEditora(int codEditora)
    {
      var editora = await _context.Editoras.FindAsync(codEditora);
      if (editora == null)
      {
        return NotFound();
      }

      _context.Editoras.Remove(editora);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool EditoraExists(int codEditora)
    {
      return _context.Editoras.Any(e => e.CodEditora == codEditora);
    }
  }
}
