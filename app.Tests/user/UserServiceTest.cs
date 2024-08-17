namespace app.user.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public void Deposit_ValidInput_UpdateUserBalance()
        {
            // Arrange
            var userService = new UserService();
            var userModel = new UserModel();
            int expectedBalance = 100;

            // Act
            using (StringReader sr = new StringReader("100"))
            {
                Console.SetIn(sr);
                userService.Deposit(userModel);
            }

            // Assert
            Assert.Equal(expectedBalance, userModel.Balance);
        }

        [Fact]
        public void UpdateBalance_ShouldUpdateUserBalanceCorrectly()
        {
            // Arrange
            UserModel user = new UserModel();
            user.Balance = 100;
            int stake = 10;
            int winnings = 50;
            UserService userService = new UserService();

            // Act
            userService.UpdateBalance(user, stake, winnings);

            // Assert
            Assert.Equal(140, user.Balance);
        }
    }
}
