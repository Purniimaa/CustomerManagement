using CustomerManagement.DTO;
using CustomerManagement.Repositories.ITransactionRepositories;
using CustomerManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "V3")]
    [Authorize]
    public class TransactionController(ITransaction _transervices, JwtServices _jwtservices) : ControllerBase

    {
        
        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit([FromQuery] decimal amount, [FromQuery] int id)
        {
            var transid = await _transervices.Deposit(amount,id);
            if(transid == 0)
            {
                return NotFound("Deposit amount is empty");
            }
            return Ok(new { Message = "Successfully deposit", transactionid = transid });


        }

        [HttpPost("Withdraw")]

        public async Task<ActionResult> Withdraw([FromQuery] decimal amount, [FromQuery] int id)
        {
            var transid = await _transervices.Withdraw(amount,id);
            if(transid == 0)
            {
                return BadRequest("Insufficient amount to withdraw");
            }
            return Ok(new { Message = "Successfully Withdraw", transactionid = transid });
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> GetAllTransactions()
        {
            var get = await _transervices.GetAllTransactions();
            if(get == null )
            {
                return NotFound("Transactions is not found");
            }
            return Ok(get);

        }


        [HttpGet("id")]
        public async  Task<ActionResult <TransactionDTO>> GetTransactionsById(int id)
        {
            var trans =await _transervices.GetTransactionById(id);
            if (trans == null)
            {
                return NotFound($"Transaction  of {id} is not  found");
            }
            return Ok(trans);
        }

        [HttpGet("Totaldeposit")]
        public async Task<ActionResult<decimal>> TotalDeposit( int CustomerId)
        {
            var dep = await _transervices.TotalDeposit(CustomerId);
            if (dep == 0)
            {
                return NotFound("There is no deposit amount");
            }
            return Ok(new {TotalDeposit = dep});
        }

        [HttpGet("Totalwithdraw")]
        public async Task<ActionResult<decimal>> TotalWithdraw(int CustomerId)
        {
            var dep = await _transervices.TotalWithdraw(CustomerId);
            if (dep == 0)
            {
                return NotFound("There is no withdraw amount");
            }
            return Ok(new { TotalWithdraw = dep });
        }

        [HttpGet("CusDepositDetails")]
        public async Task<ActionResult <CustomerTransaction>> CusDeposit(int id)
        {
            var details =await  _transervices.CusDeposit(id);
            if( details == null)
            {
                return NotFound("Details is not found");

            }
            return Ok(details);

        }

        [HttpGet("CusWithdrawDetails")]
        public async Task<ActionResult<CustomerTransaction>> CusWithdraw(int id)
        {
            var details = await _transervices.CusWithdraw(id);
            if (details == null)
            {
                return BadRequest("Details is not found");

            }
            return Ok(details);

        }

    }
}




