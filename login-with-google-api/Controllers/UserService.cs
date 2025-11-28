namespace login_with_google_api.Controllers;

public class UserService
{
    public static List<AppUser> _users = new List<AppUser>();

    public AppUser GetOrCreateUser(string email, string name)
    {
        // 1. Try to find existing user
        var user = _users.FirstOrDefault(x => x.Email == email);

        if (user != null)
        {
            // Optionally update name if changed
            if (user.Name != name)
            {
                user.Name = name;
            }

            return user;
        }

        // 2. If not found, create a new user
        var newUser = new AppUser
        {
            Email = email,
            Name = name
        };

        _users.Add(newUser);

        return newUser;
    }
}
