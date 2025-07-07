using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers;

[Authorize(Roles = "Admin")]
public class LocalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public LocalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Locals
    public async Task<IActionResult> Index()
    {
        return View(await _context.Locals.ToListAsync());
    }

    // GET: Locals/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var local = await _context.Locals
            .FirstOrDefaultAsync(m => m.IdLocal == id);
        if (local == null)
        {
            return NotFound();
        }

        return View(local);
    }

    // GET: Locals/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Locals/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdLocal,Nome,Descricao,Endereco,PrecoPorNoite")] Local local, IFormFile? imagemFile, IFormFile? imagemFile2, IFormFile? imagemFile3, IFormFile? imagemFile4, IFormFile? imagemFile5, IFormFile? imagemFile6)
    {
        if (ModelState.IsValid)
        {
            if (imagemFile != null && imagemFile.Length > 0)
            {
                // Define o caminho para salvar a imagem
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Cria um nome de arquivo único para evitar conflitos
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemFile.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Salva o arquivo no disco
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemFile.CopyToAsync(fileStream);
                }

                // Salva o caminho do arquivo no modelo
                local.ImagemUrl = "/imagens_locais/" + fileName;
            }
            //2
            if (imagemFile2 != null && imagemFile2.Length > 0)
            {
                // Define o caminho para salvar a imagem
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Cria um nome de arquivo único para evitar conflitos
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemFile2.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Salva o arquivo no disco
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemFile2.CopyToAsync(fileStream);
                }

                // Salva o caminho do arquivo no modelo
                local.ImagemUrl2 = "/imagens_locais/" + fileName;
            }
            //3
            if (imagemFile3 != null && imagemFile3.Length > 0)
            {
                // Define o caminho para salvar a imagem
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Cria um nome de arquivo único para evitar conflitos
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemFile3.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Salva o arquivo no disco
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemFile3.CopyToAsync(fileStream);
                }

                // Salva o caminho do arquivo no modelo
                local.ImagemUrl3 = "/imagens_locais/" + fileName;
            }
            //4
            if (imagemFile4 != null && imagemFile4.Length > 0)
            {
                // Define o caminho para salvar a imagem
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Cria um nome de arquivo único para evitar conflitos
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemFile4.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Salva o arquivo no disco
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemFile4.CopyToAsync(fileStream);
                }

                // Salva o caminho do arquivo no modelo
                local.ImagemUrl4 = "/imagens_locais/" + fileName;
            }
            //5
            if (imagemFile5 != null && imagemFile5.Length > 0)
            {
                // Define o caminho para salvar a imagem
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Cria um nome de arquivo único para evitar conflitos
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemFile5.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Salva o arquivo no disco
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemFile5.CopyToAsync(fileStream);
                }

                // Salva o caminho do arquivo no modelo
                local.ImagemUrl5 = "/imagens_locais/" + fileName;
            }
            //6
            if (imagemFile6 != null && imagemFile6.Length > 0)
            {
                // Define o caminho para salvar a imagem
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Cria um nome de arquivo único para evitar conflitos
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemFile6.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Salva o arquivo no disco
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemFile6.CopyToAsync(fileStream);
                }

                // Salva o caminho do arquivo no modelo
                local.ImagemUrl6 = "/imagens_locais/" + fileName;
            }
        }
        _context.Add(local);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Locals/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var local = await _context.Locals.FindAsync(id);
        if (local == null)
        {
            return NotFound();
        }
        return View(local);
    }

    // POST: Locals/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdLocal,Nome,Descricao,Endereco,PrecoPorNoite,ImagemUrl")] Local local)
    {
        if (id != local.IdLocal)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(local);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalExists(local.IdLocal))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(local);
    }

    // GET: Locals/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var local = await _context.Locals
            .FirstOrDefaultAsync(m => m.IdLocal == id);
        if (local == null)
        {
            return NotFound();
        }

        return View(local);
    }

    // POST: Locals/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var local = await _context.Locals.FindAsync(id);
        if (local != null)
        {
            _context.Locals.Remove(local);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LocalExists(int id)
    {
        return _context.Locals.Any(e => e.IdLocal == id);
    }
}
