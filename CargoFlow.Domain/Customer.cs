using System.ComponentModel.DataAnnotations.Schema;

namespace CargoFlow.Domain;

[Table("customers")]
public class Customer
{
    [Column("id")]
    public int Id { get; set; }

    [Column("login")]
    public string Login { get; set; } = null!;

    [Column("password")]
    public string Password { get; set; } = null!;

    public List<Order>? Orders { get; set; }
}
