namespace lngCollector
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(string email, string name, int uID)
        {
            Email = email;
            Name = name;
            UID = uID;
        }

        public string Email { get; }
        public string Name { get; }
        public int UID { get; }
    }
}
