using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using System.Diagnostics;

namespace ProjetoFinal.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _context = context; // Inje��o de depend�ncia do DbContext
    }

    public async Task<IActionResult> Index(DateTime? dataInicio, DateTime? dataFim, int pagina = 1, int tamPagina = 2)
    {
        /*
         *phind
        // Mant�m a p�gina atual quando novos filtros s�o aplicados
        if (dataInicio.HasValue || dataFim.HasValue)
        {
            pagina = 1; // Volta para a primeira p�gina quando aplica novos filtros
        }
        */

        // Inicia a consulta por todos os locais
        var query = _context.Locals.AsQueryable();

        // Se datas foram informadas, filtra os locais
        if (dataInicio.HasValue && dataFim.HasValue)
        {
            // L�gica: Queremos locais que N�O tenham NENHUMA reserva que se sobreponha ao per�odo desejado.
            // Uma sobreposi��o ocorre se:           
            query = query.Where(l => !_context.Reservas.Any(r =>
                r.LocalId == l.IdLocal &&
                r.DataInicio < dataFim.Value &&
                r.DataFim > dataInicio.Value
            ));

            // Passa as datas para a View para preencher o formul�rio e calcular o pre�o
            ViewBag.DataInicio = dataInicio.Value;
            ViewBag.DataFim = dataFim.Value;
            
        }

        int totalRegistros = query.Count();       

        var locaisDisponiveis = await query
            .OrderBy(c => c.Nome)
            .Skip((pagina - 1) * tamPagina)
            .Take(tamPagina).ToListAsync();

        ViewBag.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamPagina);
        ViewBag.PaginaAtual = pagina;
       
        return View(locaisDisponiveis);        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
