﻿using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdLocal,Nome,Descricao,Endereco,PrecoPorNoite")] Local local, IFormFile[]? imagensFile, string[] descricaoImagens)
    {
        if (ModelState.IsValid)
        {
            // Verifica se o usuário enviou imagens
            if (imagensFile != null && imagensFile.Length > 0)
            {
                // Cria a lista de imagens (Fotos) que será associada ao local
                local.Imagem = new List<Foto>();

                for (int i = 0; i < imagensFile.Length; i++)
                {
                    var ArquivoImagens = imagensFile[i];
                    var descricao = descricaoImagens.Length > i ? descricaoImagens[i] : "Descrição padrão"; // Verifica se há descrição para a imagem

                    // Define o caminho para salvar a imagem
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens_locais");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    // Cria um nome de arquivo único para evitar conflitos
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ArquivoImagens.FileName);
                    var filePath = Path.Combine(uploadsDir, fileName);

                    // Salva o arquivo no disco
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ArquivoImagens.CopyToAsync(fileStream);
                    }

                    // Cria um objeto Foto e adiciona à lista de imagens
                    var foto = new Foto
                    {
                        Imagem = "/imagens_locais/" + fileName, // Caminho relativo da imagem
                        Descricao = descricao // A descrição associada à imagem
                    };

                    // Adiciona a foto à lista de imagens do local
                    local.Imagem.Add(foto);
                }
            }

            // Adiciona o local ao contexto e salva as alterações no banco de dados
            _context.Add(local);
            await _context.SaveChangesAsync();

            // Redireciona para a página inicial ou outra página de sua escolha
            return RedirectToAction(nameof(Index));
        }

        // Caso o ModelState não seja válido, você pode retornar a visão novamente com os dados já preenchidos
        return View(local);
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
