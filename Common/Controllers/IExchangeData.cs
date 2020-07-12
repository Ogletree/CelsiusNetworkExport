using System.Text;

namespace Common.Controllers
{
    public interface IExchangeData
    {
        string Id { get; }
        string GetName(string symbol);
        decimal GetPrice(string symbol);
        decimal GetPercentChange24Hour(string symbol);
    }
}
