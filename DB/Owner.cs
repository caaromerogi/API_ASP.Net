using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB;

public class Owner
{   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OwnerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual ICollection<Pet> Pets { get; set; }
}
