using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.Commands;
using MyApp.ReadModel.Model;
using MyApp.ReadModel.Queries;
using System;
using System.Threading.Tasks;

namespace MyApp.Site.Pages.Todo
{
    public class DetailModel : PageModel
    {
        private readonly IMediator _mediator;
        public DetailModel(IMediator mediator) => _mediator = mediator;
        public TodoDetailModel Data { get; private set; }
        public async Task OnGetAsync(Guid id) => Data = await _mediator.Send(new GetTodoDetail { TodoId = id });

        public async Task<IActionResult> OnPostIncreasePriorityAsync(Guid id)
        {
            var command = new IncreasePriority
            {                
                TodoId = id
            };

            await _mediator.Send(command);

            return RedirectToPage("./Dashboard");
        }
        public async Task<IActionResult> OnPostDecreasePriorityAsync(Guid id)
        {
            var command = new DecreasePriority
            {
                TodoId = id
            };

            await _mediator.Send(command);

            return RedirectToPage("./Dashboard");
        }
        public async Task<IActionResult> OnPostCompleteAsync(Guid id)
        {
            await _mediator.Send(new CompleteTodo { TodoId = id });
            return RedirectToPage("./Dashboard");
        }
    }
}
