using GastosResidenciais.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories;

//O Mock fará uma implementação fake da interface
public class SaveChangesUnitBuilder
{
    public static ISaveChangesUnit Build()
    {
        var mock = new Mock<ISaveChangesUnit>();

        return mock.Object;
    }
}
