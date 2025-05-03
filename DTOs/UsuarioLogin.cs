using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.DTOs;

public class UsuarioLogin
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Senha { get; set; }
}