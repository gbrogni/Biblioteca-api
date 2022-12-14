using BibliotecaAPITeste.Api.Data;
using BibliotecaAPITeste.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BibliotecaAPITeste.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
  {

    private readonly Context _context;


    public ItemController(Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("/itens")]
    public async Task<ActionResult> GetItens()
    {
      var query = (from i in _context.Itens
                   join a in _context.Autores on i.CodAutor equals a.CodAutor
                   join e in _context.Editoras on i.CodEditora equals e.CodEditora
                   join l in _context.Locais on i.CodLocal equals l.CodLocal
                   join s in _context.Secoes on i.CodSecao equals s.CodSecao
                   select new
                   {
                     CodItem = i.CodItem,
                     Nome = i.Nome,
                     TipoItem = i.TipoItem,
                     NumExemplar = i.NumExemplar,
                     Volume = i.Volume,
                     NomeAutor = a.NomeAutor,
                     NomeEditora = e.NomeEditora,
                     DescricaoLocal = l.Descricao,
                     DescricaoSecao = s.Descricao,
                     Edicao = i.Edicao,
                     Idioma = i.Idioma,
                     Status = i.Status,
                   });

                   return Ok(await query.ToListAsync());
    }

    [HttpGet()]
    [Route("/itens/{codItem}")]
    public async Task<ActionResult> GetById(int codItem)
    {
      var result = await _context.Itens.FindAsync(codItem);

      if (result == null)
      {
        return NotFound();
      }
      else { 
      var query = (from i in _context.Itens
                   join a in _context.Autores on i.CodAutor equals a.CodAutor
                   join e in _context.Editoras on i.CodEditora equals e.CodEditora
                   join l in _context.Locais on i.CodLocal equals l.CodLocal
                   join s in _context.Secoes on i.CodSecao equals s.CodSecao
                   select new
                   {
                     CodItem = i.CodItem,
                     Nome = i.Nome,
                     TipoItem = i.TipoItem,
                     NumExemplar = i.NumExemplar,
                     Volume = i.Volume,
                     NomeAutor = a.NomeAutor,
                     NomeEditora = e.NomeEditora,
                     DescricaoLocal = l.Descricao,
                     DescricaoSecao = s.Descricao,
                     Edicao = i.Edicao,
                     Idioma = i.Idioma,
                     Status = i.Status,
                   });
      }

      return Ok(result);
    }

    [HttpGet()]
    [Route("/itens/getNumExemplar")]
    public async Task<ActionResult> GetItemById(int codItem)
    {
      //var result = await _context.Itens.FindAsync(codItem);

      //if (result == null)
      //{
      //  return NotFound();
      //}
      //else
      //{
        var query =  _context.Itens.Where(Item => Item.CodItem == codItem)
                     .Select(Item => new
                     {
                       CodItem = Item.CodItem,
                       TipoItem = Item.TipoItem,
                       NumExemplar = Item.NumExemplar,
                     });
      

      return Ok(query);
    }

    [HttpPost]
    [Route("/itens")]
    public async Task<ActionResult> CreateItem(Item item)
    {
      await _context.Itens.AddAsync(item);
      await _context.SaveChangesAsync();
      return Ok(item);
    }

    [HttpPut()]
    [Route("/itens/{codItem}")]
    public async Task<IActionResult> UpdateItem(int codItem, Item item)
    {
      if (codItem != item.CodItem)
      {
        return BadRequest();
      }
      _context.Entry(item).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ItemExists(item.CodItem))
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
    [Route("/itens/{codItem}")]
    public async Task<IActionResult> DeleteItem(int codItem)
    {
      var item = await _context.Itens.FindAsync(codItem);
      if (item == null)
      {
        return NotFound();
      }

      _context.Itens.Remove(item);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ItemExists(int codItem)
    {
      return _context.Itens.Any(e => e.CodItem == codItem);
    }
  }
}
