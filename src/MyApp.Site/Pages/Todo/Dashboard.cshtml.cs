using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.ReadModel.Model;
using MyApp.ReadModel.Queries;
using System;
using System.Threading.Tasks;

namespace MyApp.Site.Pages.Todo
{
    public class DashboardModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public TaskSummaryModel Data { get; set; } = new TaskSummaryModel();
        public DashboardModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Data =  await _mediator.Send(new GetTaskSummary());
            return Page(); 
        }
    }
}