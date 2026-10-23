using System;
using System.Collections.Generic;
using Basilisk.DataAccess.Models.Enums;

namespace Basilisk.DataAccess.Models
{
    public partial class Salesman
    {
        // public Salesman()
        // {
        //     InverseSuperiorEmployeeNumberNavigation = new HashSet<Salesman>();
        //     Orders = new HashSet<Order>();
        //     Regions = new HashSet<Region>();
        // }

        public string EmployeeNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public Level Level { get; set; }    
        public DateTime BirthDate { get; set; }
        public DateTime HiredDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? SuperiorEmployeeNumber { get; set; }

        public virtual Salesman? SuperiorEmployeeNumberNavigation { get; set; }
        public virtual ICollection<Salesman> InverseSuperiorEmployeeNumberNavigation { get; set; } = new List<Salesman>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

        public string FullName => $"{FirstName} {LastName}";
    }
}
