using HMSApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HMSApi.Context
{
    public class HospitalContext:DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

    }
}
