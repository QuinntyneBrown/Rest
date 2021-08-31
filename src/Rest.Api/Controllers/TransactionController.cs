using System.Net;
using System.Threading.Tasks;
using Rest.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Rest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{transactionId}", Name = "GetTransactionByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTransactionById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTransactionById.Response>> GetById([FromRoute]GetTransactionById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.Transaction == null)
            {
                return new NotFoundObjectResult(request.TransactionId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetTransactionsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTransactions.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTransactions.Response>> Get()
            => await _mediator.Send(new GetTransactions.Request());
        
        [HttpPost(Name = "CreateTransactionRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateTransaction.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTransaction.Response>> Create([FromBody]CreateTransaction.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetTransactionsPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTransactionsPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTransactionsPage.Response>> Page([FromRoute]GetTransactionsPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateTransactionRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateTransaction.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateTransaction.Response>> Update([FromBody]UpdateTransaction.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{transactionId}", Name = "RemoveTransactionRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveTransaction.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveTransaction.Response>> Remove([FromRoute]RemoveTransaction.Request request)
            => await _mediator.Send(request);
        
    }
}
