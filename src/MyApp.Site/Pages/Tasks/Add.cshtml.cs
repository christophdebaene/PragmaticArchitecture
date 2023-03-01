﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.Tasks;

namespace MyApp.Site.Pages.Tasks
{
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public string Title { get; set; }
        public AddModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var command = new CreateNewTodo
            {
                TodoId = Guid.NewGuid(),
                Title = Title
            };

            await _mediator.Send(command);

            return RedirectToPage("./Dashboard");
        }
    }
}
