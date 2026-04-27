namespace learn_asp.net_mvc
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<string> _users = new List<string>();
        public IEnumerable<string> User => _users;

        public void Add(string user)
        {
            _users.Add(user);
        }
    }
}
