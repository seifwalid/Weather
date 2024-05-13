using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Model;

public class Userr
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; } 
    public string UserName { get; set; } = String.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public PermissibleLimits? UserPermissibleLimits { get; set; } = new PermissibleLimits();
    
}