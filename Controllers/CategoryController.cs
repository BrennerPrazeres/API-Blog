using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext ctx)
        {
            try
            {
                var categories = await ctx.Categories.ToListAsync(); // SELECT - RETORNANDO TODAS AS CATEGORIAS
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no sevidor");
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetAsync(
                    [FromRoute] int id,
                    [FromServices] BlogDataContext ctx)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id); // SELECT - RETORNANDO UMA CATEGORIA
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no sevidor");
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext ctx)
        {
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };
                await ctx.Categories.AddAsync(category); // CREATE - CRIANDO UMA NOVA CATEGORIA
                await ctx.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,"Não foi possível incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no sevidor");
            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext ctx)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id); // UPDATE - ALTERANDO UMA CATEGORIA
                if (category == null)
                {
                    return NotFound();
                }
                category.Name = model.Name;
                category.Slug = model.Slug;

                ctx.Categories.Update(category);
                await ctx.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no sevidor");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext ctx)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id); // DELETE - DELETANDO UMA CATEGORIA
                if (category == null)
                {
                    return NotFound();
                }

                ctx.Categories.Remove(category);
                await ctx.SaveChangesAsync();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no sevidor");
            }
        }
    }
}
