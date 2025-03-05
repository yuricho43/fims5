﻿using System;

using Microsoft.AspNetCore.Identity;

using Fims5.Data.Contracts;


namespace Fims5.Data.Entities
{
    public class FimsRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public FimsRole()
            : this(null)
        {
        }

        public FimsRole(string name)
            : base(name)
            => this.Id = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}