using LoanManagementService.Database;
using LoanManagementService.Models.Entities;
using LoanManagementService.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementService.Models.Repository
{
    public class LoanInformation : ILoanInformation
    {
        DatabaseContext db;
        public LoanInformation()
        {
            db = new DatabaseContext();
        }

        public async Task<IEnumerable<Loan>> GetAllLoanDetails()
        {
            var result = db.Loans.ToList();
            if (result == null)
                return null;

            return result;
        }

        public async Task<Loan> GetLoanDetailsByLoanId(int loanId)
        {
            var result = await db.Loans.FindAsync(loanId);
            if (result == null)
                return null;

            return result;
        }

        public async Task<Loan> CreateLoanDetails(Loan loan)
        {
            try
            {

                db.Loans.Add(loan);
                await db.SaveChangesAsync();
                return loan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tuple<bool, string>> UpdateLoanDetails(int loanId, Loan loan)
        {
            try
            {
                var loanDetails = await db.Loans.FindAsync(loanId);
                if (loanDetails == null)
                    return Tuple.Create(false, "No Loan details found.");
                
                //loanDetails = _mapper.Map<Loan>(loan);
                loanDetails.FirstName = loan.FirstName;
                loanDetails.LastName = loan.LastName;
                loanDetails.LoanAmount = loan.LoanAmount;
                loanDetails.LoanNumber = loan.LoanNumber;
                loanDetails.LoanTerm = loan.LoanTerm;
                loanDetails.LoanType = loan.LoanType;
                loanDetails.PropertyAddress = loan.PropertyAddress;

                await db.SaveChangesAsync();
                return Tuple.Create(true, "Loan Details Updated Successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tuple<bool, string>> DeleteLoanDetailsByLoanId(int loanId)
        {
            try
            {
                var loanDetails = await db.Loans.FindAsync(loanId);
                if (loanDetails == null)
                    return Tuple.Create(false, "No Loan details found.");

                db.Loans.Remove(loanDetails);
                await db.SaveChangesAsync();
                return Tuple.Create(true, "Loan Details Deleted Successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}