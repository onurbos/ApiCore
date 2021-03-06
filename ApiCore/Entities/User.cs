﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id"), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("email"), Required, MaxLength(200)]
        public string Email { get; set; }

        [Column("first_name"), Required, MaxLength(200)]
        public string FirstName { get; set; }

        [Column("last_name"), Required, MaxLength(200)]
        public string LastName { get; set; }

        [Column("full_name"), Required, MaxLength(400)]
        public string FullName { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("culture"), MaxLength(10)]
        public string Culture { get; set; }

        [Column("currency"), MaxLength(3)]
        public string Currency { get; set; }

        [Column("is_subscriber")]
        public bool IsSubscriber { get; set; }

        [Column("created_by")]
        public string CreatedByEmail { get; set; }

        [Column("updated_by")]
        public string UpdatedByEmail { get; set; }

        [Column("created_at"), Required]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }

        [Column("picture")]
        public string Picture { get; set; }

        [Column("profile_id"), ForeignKey("Profile")]
        public int? ProfileId { get; set; }

        [Column("role_id"), ForeignKey("Role")]
        public int? RoleId { get; set; }

        [Column("phone"), MaxLength(50)]
        public string Phone { get; set; }

        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }

        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; }

        public Role Role { get; set; }
        public virtual Profile Profile { get; set; }

    }
}
