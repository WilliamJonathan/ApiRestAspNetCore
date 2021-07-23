using ApiRest.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("addres")]
        public string Addres { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
    }
}
