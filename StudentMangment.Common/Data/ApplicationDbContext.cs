using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentMangment.Common.Data.Models;
using StudentMangment.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMangment.Common.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasSequence(SequenceService.StudentSequence)
                .StartsAt(100);
        }
    }
}
