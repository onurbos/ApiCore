using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Entities
{
    [Table("roles")]
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new List<User>();
            _ownersList = new List<string>();

        }

        [Column("label"), MaxLength(200), Required]
        public string Label { get; set; }

        [Column("description"), MaxLength(500)]
        public string Description { get; set; }

        [Column("master")]
        public bool Master { get; set; }

        [Column("owners")]
        public string Owners
        {
            get
            {
                return string.Join(",", _ownersList);
            }
            set
            {
                _ownersList = value?.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
            }
        }

        [Column("share_data")]
        public bool ShareData { get; set; }

        [Column("reports_to_id"), ForeignKey("ReportsTo")]
        public int? ReportsToId { get; set; }

        [Column("system_code")]
        public string SystemCode { get; set; }

        public virtual Role ReportsTo { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }


        [NotMapped]
        private List<string> _ownersList { get; set; }

        [NotMapped]
        public List<string> OwnersList
        {
            get
            {
                return _ownersList;
            }
            set
            {
                _ownersList = value;
            }
        }
    }
}
