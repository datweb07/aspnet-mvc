using learn_asp.net_mvc.Controllers;

namespace learn_asp.net_mvc
{
    public class MyRepository : IRepository
    {
        private readonly ILogger<MyRepository> _logger;
        public MyRepository(ILogger<MyRepository> logger)
        {
            this._logger = logger;
            _logger.LogInformation("New MyRepository");
        }
        public string GetById(string id)
        {
            return "Id: " + id;
        }
    }
}
