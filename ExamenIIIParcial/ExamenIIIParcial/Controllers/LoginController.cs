using ExamenIIIParcial.Data;
using Microsoft.AspNetCore.Mvc;
using Datos.Interfaces;
using Datos.Repositorios;
using ExamenIIIParcial.Pages;
using Modelos;
using System.Security.Claims;

namespace ExamenIIIParcial.Controllers;

public class LoginController : Controller
{
    private readonly MySQLConfiguration _configuration;
    private IUsuarioRepositorio _usuarioRepositorio;

    public LoginController(MySQLConfiguration configuration )
    {
        _configuration = configuration;
        _usuarioRepositorio = new UsuarioRepositorio(configuration.CadenaConexion);
    }

    [HttpPost("/account/login")]
    public async Task <IActionResult> Login(Login login)
    {
        string rol = string .Empty;
        try
        {
            bool usuarioValido = await _usuarioRepositorio.ValidarUsuario(login);
            if (usuarioValido)
            {
                Usuario usu = await _usuarioRepositorio.GetPorCodigo(login.Codigo);
                if(usu.EstaActivo)
                {
                    rol = usu.Rol;

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, usu.Codigo),
                        new Claim(ClaimTypes.Role, rol)
                    };

                    var claimsIdentify = new ClaimTypes(claims, CookieAunthenticationDefaults.AunthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentify);

                    await HttpContext.SignInAsync(CookieAunthenticationDefaults.AunthenticationScheme, claimsPrincipal,
                                                 new AunthenticationProperties
                                                 {
                                                     IsPersistent = true,
                                                     ExpiresUtc = DateTime.UtcNow.AddMinutes(20)



                                                  });


                    else
                    {
                        return LocalDirect("/login/Usuario Inactivo");

                    }
                   
                }
            }
        }
    }
}

