using GreenStoreWeb.Models;
using GreenStoreWeb.Utilities;

namespace GreenStoreWeb.Services.Contract
{
    public interface IUserServices
    {
        public RequiredData AddData(RequiredData requiredData);
        public string Verify(RequiredData RequiredData);
        public string CreateToken(Authentication login);
        List<string> GetVegetables();
        List<decimal?> GetVegetablePrices(int userId);
        void UpdateOrCreatePrice(int userId, string vegetable, decimal price);
    }

    }
