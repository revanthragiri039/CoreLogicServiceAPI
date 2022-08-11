using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementService.Models.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long LoanNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanType { get; set; }
        public string LoanTerm { get; set; }
        public string PropertyAddress { get; set; }
    }
}
