using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TarefasBackEnd.Config;
using TarefasBackEnd.DTOs;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController: ControllerBase
{
    
    private readonly AppSettings _settings;
    
    public UsuarioController(IOptions<AppSettings> options)
    {
        _settings = options.Value;
    }
    
    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] Usuario usuario, [FromServices] IUsuarioRepository repository)
    {
      if (!ModelState.IsValid) return BadRequest();
      
      repository.Create(usuario);
      return Created();
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UsuarioLogin login, [FromServices] IUsuarioRepository repository)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            Usuario? usuario = repository.Read(login.Email, login.Senha);
        
            if (usuario == null) return Unauthorized();
            return Ok(new
            {
                Messagem = "Login realizado com sucesso",
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = GenerateToken(usuario)
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem("Um erro ocorreu durante o login", null, 500);
        }
    }

    private string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _settings.getSecretKeyBytes();

        var descriptor = new SecurityTokenDescriptor
        {
            // informações do usuário
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, "user"),
            }),
            Expires = DateTime.UtcNow.AddHours(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
        };
        
        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
}