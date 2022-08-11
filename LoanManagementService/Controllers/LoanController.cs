using AutoMapper;
using LoanManagementService.Database;
using LoanManagementService.Models.Entities;
using LoanManagementService.Models.Repository;
using LoanManagementService.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoanManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        DatabaseContext db;
        ILoanInformation _loanInformation;
        //Mapper _mapper;
        public LoanController()
        {
            db = new DatabaseContext();
            _loanInformation = new LoanInformation();
            //_mapper = mapper;
        }

        // GET: api/<LoanController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _loanInformation.GetAllLoanDetails();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<LoanController>/5
        [HttpGet("{loanId}"), Authorize]
        public async Task<IActionResult> Get(int loanId)
        {
            try
            {
                var result = await _loanInformation.GetLoanDetailsByLoanId(loanId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<LoanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Loan loan)
        {
            try
            {
                var result = await _loanInformation.CreateLoanDetails(loan);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<LoanController>/5
        [HttpPut("{loanId}")]
        public async Task<IActionResult> Put(int loanId, [FromBody] Loan loan)
        {
            try
            {
                var result = await _loanInformation.UpdateLoanDetails(loanId, loan);
                if (result.Item1)
                    return StatusCode(StatusCodes.Status200OK, result.Item2);
                else
                    return StatusCode(StatusCodes.Status204NoContent, result.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{loanId}")]
        public async Task<IActionResult> Delete(int loanId)
        {
            try
            {
                var result = await _loanInformation.DeleteLoanDetailsByLoanId(loanId);
                if (result.Item1)
                    return StatusCode(StatusCodes.Status200OK, result.Item2);
                else
                    return StatusCode(StatusCodes.Status204NoContent, result.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}