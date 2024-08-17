namespace app.user;
public interface IUserService
{
    public void Deposit(UserModel user);
    void UpdateBalance(UserModel user, int stake, int winnings);
}
