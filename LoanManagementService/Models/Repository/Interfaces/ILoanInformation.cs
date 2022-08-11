using LoanManagementService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManagementService.Models.Repository.Interfaces
{
    interface ILoanInformation
    {
        Task<IEnumerable<Loan>> GetAllLoanDetails();
        Task<Loan> GetLoanDetailsByLoanId(int loanId);
        Task<Loan> CreateLoanDetails(Loan loan);
        Task<Tuple<bool, string>> UpdateLoanDetails(int loanId, Loan loan);
        Task<Tuple<bool, string>> DeleteLoanDetailsByLoanId(int loanId);
    }
}