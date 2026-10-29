using ExpenseTrackerAPI.Models.Domain;
using ExpenseTrackerAPI.Models.DTOs.Category;
using ExpenseTrackerAPI.Models.DTOs.Transaction;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transactions = await _transactionRepository.GetAllByUserIdAsync(userId);

            var transactionDtos = transactions.Select(transaction => new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description,
                PaymentMethod = transaction.PaymentMethod,
                PaidTo = transaction.PaidTo,
                Type = transaction.Type,
                UserId = transaction.UserId,
                CategoryId = transaction.CategoryId,
                CreatedAt = transaction.CreatedAt
            });

            return Ok(transactionDtos);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTransactionById(int id)
        //{
        //    var transaction = await _transactionRepository.GetByIdAsync(id);

        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactionDto = new TransactionDto
        //    {
        //        Id = transaction.Id,
        //        Amount = transaction.Amount,
        //        TransactionDate = transaction.TransactionDate,
        //        Description = transaction.Description,
        //        PaymentMethod = transaction.PaymentMethod,
        //        PaidTo = transaction.PaidTo,
        //        Type = transaction.Type,
        //        UserId = transaction.UserId,
        //        CategoryId = transaction.CategoryId,
        //        CreatedAt = transaction.CreatedAt
        //    };

        //    return Ok(transactionDto);
        //}

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            // User is trying to access someone else's transaction
            if (transaction.UserId != userId)
            {
                return Forbid();
            }

            var transactionDto = new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description,
                PaymentMethod = transaction.PaymentMethod,
                PaidTo = transaction.PaidTo,
                Type = transaction.Type,
                UserId = transaction.UserId,
                CategoryId = transaction.CategoryId,
                CreatedAt = transaction.CreatedAt
            };

            return Ok(transactionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = new Transaction
            {
                Amount = request.Amount,
                TransactionDate = request.TransactionDate,
                Description = request.Description,
                PaymentMethod = request.PaymentMethod,
                PaidTo = request.PaidTo,
                Type = request.Type,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            var createdTransaction = await _transactionRepository.CreateAsync(transaction);

            var transactionDto = new TransactionDto
            {
                Id = createdTransaction.Id,
                Amount = createdTransaction.Amount,
                TransactionDate = createdTransaction.TransactionDate,
                Description = createdTransaction.Description,
                PaymentMethod = createdTransaction.PaymentMethod,
                PaidTo = createdTransaction.PaidTo,
                Type = createdTransaction.Type,
                UserId = createdTransaction.UserId,
                CategoryId = createdTransaction.CategoryId,
                CreatedAt = createdTransaction.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetTransactionById),
                new { id = transactionDto.Id },
                transactionDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Check whether the transaction exists
            var existingTransaction = await _transactionRepository.GetByIdAsync(id);

            if (existingTransaction == null)
            {
                return NotFound();
            }

            // Check ownership
            if (existingTransaction.UserId != userId)
            {
                return Forbid();
            }

            var transaction = new Transaction
            {
                Id = id,
                Amount = request.Amount,
                TransactionDate = request.TransactionDate,
                Description = request.Description,
                PaymentMethod = request.PaymentMethod,
                PaidTo = request.PaidTo,
                Type = request.Type,
                UserId = userId,          // Use logged-in user's ID
                CategoryId = request.CategoryId
            };

            var updatedTransaction = await _transactionRepository.UpdateAsync(transaction);

            if (updatedTransaction == null)
            {
                return NotFound();
            }

            var transactionDto = new TransactionDto
            {
                Id = updatedTransaction.Id,
                Amount = updatedTransaction.Amount,
                TransactionDate = updatedTransaction.TransactionDate,
                Description = updatedTransaction.Description,
                PaymentMethod = updatedTransaction.PaymentMethod,
                PaidTo = updatedTransaction.PaidTo,
                Type = updatedTransaction.Type,
                UserId = updatedTransaction.UserId,
                CategoryId = updatedTransaction.CategoryId,
                CreatedAt = updatedTransaction.CreatedAt
            };

            return Ok(transactionDto);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Check if the transaction exists
            var existingTransaction = await _transactionRepository.GetByIdAsync(id);

            if (existingTransaction == null)
            {
                return NotFound();
            }

            // Check ownership
            if (existingTransaction.UserId != userId)
            {
                return Forbid();
            }

            var deletedTransaction = await _transactionRepository.DeleteAsync(id);

            var transactionDto = new TransactionDto
            {
                Id = deletedTransaction!.Id,
                Amount = deletedTransaction.Amount,
                TransactionDate = deletedTransaction.TransactionDate,
                Description = deletedTransaction.Description,
                PaymentMethod = deletedTransaction.PaymentMethod,
                PaidTo = deletedTransaction.PaidTo,
                Type = deletedTransaction.Type,
                UserId = deletedTransaction.UserId,
                CategoryId = deletedTransaction.CategoryId,
                CreatedAt = deletedTransaction.CreatedAt
            };

            return Ok(transactionDto);
        }


    }
}