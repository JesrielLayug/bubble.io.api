using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IProfileRepository
    {
       Task Add(Profile profile);
    }
}
