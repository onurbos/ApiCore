using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Entities
{
    [Table("profiles")]
    public class Profile : BaseEntity
    {
        //public Profile()
        //{
        //	Permissions = new List<ProfilePermission>();
        //}

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string DescriptionEn { get; set; }

        [Column("has_admin_rights")]
        public bool HasAdminRights { get; set; }

        [Column("send_email")]
        public bool SendEmail { get; set; }

        [Column("send_sms")]
        public bool SendSms { get; set; }

        [Column("export_data")]
        public bool ExportData { get; set; }

        [Column("import_data")]
        public bool ImportData { get; set; }

        [Column("word_pdf_download")]
        public bool WordPdfDownload { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }

        [Column("order")]
        public int Order { get; set; }

        [Column("system_code")]
        public string SystemCode { get; set; }

        [Column("change_email")]
        public bool ChangeEmail { get; set; }

        //public virtual IList<ProfilePermission> Permissions { get; set; }

        [InverseProperty("Profile")]
        public virtual IList<User> Users { get; set; }

    }
}
