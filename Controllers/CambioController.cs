using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using practicaprogra.Models;


namespace practicaprogra.Controllers
{
   public class CambioController : Controller
{
    private readonly ILogger<CambioController> _logger;

    public CambioController(ILogger<CambioController> logger)
    {
        _logger = logger;
    }

    // Diccionario de tasas de cambio relativas a PEN
    private readonly Dictionary<string, decimal> tasasCambio = new()
    {
        { "PEN", 1m },
        { "USD", 0.27m },       // 1 PEN = 0.27 USD
        { "BRL", 0.6338236m }   // 1 PEN = 0.6338236 BRL
    };

    [HttpGet]
    public IActionResult Index()
    {
        return View(new CambioModel());
    }

    [HttpPost]
    public IActionResult Index(CambioModel modelo)
    {
        if (!tasasCambio.ContainsKey(modelo.PaisOrigen) || !tasasCambio.ContainsKey(modelo.PaisDestino))
        {
            ModelState.AddModelError("", "Moneda no válida.");
            return View(modelo);
        }

        // Convertimos de origen a PEN, luego de PEN al destino
        decimal cantidadEnPEN = modelo.Cantidad / tasasCambio[modelo.PaisOrigen];
        decimal resultado = cantidadEnPEN * tasasCambio[modelo.PaisDestino];

        modelo.Resultado = Math.Round(resultado, 2);
        ViewBag.Mensaje = $"Convertiste {modelo.Cantidad} {modelo.PaisOrigen} a {modelo.Resultado} {modelo.PaisDestino}.";

        return View(modelo);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}

}
