﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HealthcareAnalytics.Models;

namespace HealthcareAnalytics.Models
{
    public class EmploymentDetails
    {
        public EmploymentDetails()
        {
            ID = Guid.NewGuid();
        }

        [Key]
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [ScaffoldColumn(false)]
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime TerminationDate { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }
    }

    public class CheckinDetails
    {

        public CheckinDetails()
        {
            ID = Guid.NewGuid();
        }

        [Key]
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public DateTime CheckinDate { get; set; }

        public DateTime DischargeDate { get; set; }

        public string IllnessDetails { get; set; }

        public string Diagnosis { get; set; }

    }


    public class Patient : Person
    {
        public int MedicareCardNumber { get; set; }

        public ICollection<CheckinDetails> ChecckinDetails { get; set; }

        public Guid BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
    }

    public class Employee : Person
    {
        public ICollection<EmploymentDetails> EmploymentDetails { get; set; } 

        public Guid BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
    }

    // TODO: Incident model is incomplete
    public class Incident
    {
        public Incident()
        {
            ID = Guid.NewGuid();
        }

        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Required]
        public Guid BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

    }

    public class Branch
    {
        public Branch()
        {
            ID = Guid.NewGuid();
        }

        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Required]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [ForeignKey("ContactInformationId")]
        public ContactInformation ContactInformation { get; set; }
        public Guid ContactInformationId { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Incident> Incidents { get; set; }

    }

    public class HospitalDBContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<NameDetails> NameDetails { get; set; }
        public DbSet<EmploymentDetails> EmployementDetails { get; set; }
        public DbSet<CheckinDetails> CheckinDetails { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<EmploymentDetails> EmploymentDetails { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public HospitalDBContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Patient>().ToTable("Patients");

            modelBuilder.Entity<Person>().HasRequired(m => m.HomeContactInfo).WithMany().HasForeignKey(m => m.HomeContactInfoId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Person>().HasRequired(m => m.WorkContactInfo).WithMany().HasForeignKey(m => m.WorkContactInfoId)
                .WillCascadeOnDelete(false);
        }
    }
}