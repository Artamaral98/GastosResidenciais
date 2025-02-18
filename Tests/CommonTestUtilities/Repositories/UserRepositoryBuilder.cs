using GastosResidenciais.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UserRepositoryBuilder
{
    private readonly Mock<IUserRepository> _userRepository;

    public UserRepositoryBuilder() => _userRepository = new Mock<IUserRepository>();

  
    public void IsUnderage (long userId)
    {
        _userRepository.Setup(repo => repo.IsUnderage(userId)).ReturnsAsync(false);
    }



    public IUserRepository Build()
    {
        return _userRepository.Object;
    }

}
