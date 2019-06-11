using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        // we can change the collection without comeback here and change the type of collection
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        // reuse Customer entity
        public Customer Customer { get; set; }
    }
}