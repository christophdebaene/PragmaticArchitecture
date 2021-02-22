using System;
using Microsoft.AspNetCore.Http;
using MyApp.Domain.Users;

namespace MyApp.Site.Infrastructure
{
    public class SessionUserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public SessionUserContext(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor ?? throw new ArgumentNullException(nameof(httpAccessor));
        }
        public User CurrentUser => _httpAccessor.HttpContext.Session.Get<User>("CurrentUser") ?? User.Unknown;
    }
}
