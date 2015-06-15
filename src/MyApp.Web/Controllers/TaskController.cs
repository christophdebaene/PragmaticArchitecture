using MyApp.Application.Commands;
using MyApp.ReadModel.Queries;
using SlickBus;
using System;
using System.Web.Mvc;

namespace MyApp.Web.Controllers
{
    public class TaskController : MyAppController
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException("mediator");

            _mediator = mediator;
        }

        public ActionResult Dashboard()
        {
            var result = _mediator.Send(new GetTaskSummary());
            return View(result);
        }

        public ActionResult Index()
        {
            var result = _mediator.Send(new GetTasks());
            return View(result);
        }

        public ActionResult Detail(Guid id)
        {
            var result = _mediator.Send(new GetTaskDetail(id));
            return View(result);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string title)
        {
            var command = new CreateNewTask
            {
                TaskId = Guid.NewGuid(),
                Title = title,
            };

            _mediator.Send(command);
            return RedirectToAction("Index");
        }

        public ActionResult DecreasePriority(Guid id)
        {
            var command = new DecreasePriority
            {
                TaskId = id
            };

            _mediator.Send(command);
            return RedirectToAction("Index");
        }

        public ActionResult IncreasePriority(Guid id)
        {
            var command = new IncreasePriority
            {
                TaskId = id
            };

            _mediator.Send(command);
            return RedirectToAction("Index");
        }

        public ActionResult Complete(Guid id)
        {
            _mediator.Send(new CompleteTask(id));
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            _mediator.Send(new DeleteTask(id));
            return RedirectToAction("Index");
        }
    }
}