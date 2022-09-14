using System.Threading.Tasks;

namespace Tweet_Api.EventHub
{
    public interface IProduceMessages
    {
        Task ProduceAsync(string value);

        Task Dispose();
    }
}