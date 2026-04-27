namespace learn_asp.net_mvc
{
    public interface IUserRepository
    {
        void Add(string user);
        IEnumerable<string> User {  get; }
    }
}
