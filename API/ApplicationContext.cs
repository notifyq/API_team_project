using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class ApplicationContext: teamprojectContext
    {
        public static teamprojectContext Context { get; } = new teamprojectContext();
    }
}
