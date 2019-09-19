using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SustainableVendingMachine.Domain.Entities;
using SustainableVendingMachine.Domain.Helpers;
using SustainableVendingMachine.Domain.UseCases;

namespace SustainableVendingMachine.Host.Web.Hubs
{
    public class VendingMachineHub : Hub
    {
        private readonly PurchaseProductUseCase _useCase;

        public VendingMachineHub(PurchaseProductUseCase useCase)
        {
            Guard.AgainstNull(useCase, nameof(useCase));

            _useCase = useCase;
        }

        public async Task SendVendingMachineMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveVendingMachineDisplayMessage", message);
        }

        public async Task ReceiveInsertedCoin(string coinId)
        {
            Coin coinToBeInserted;

            switch (coinId)
            {
                case "tenCentCoin":
                    coinToBeInserted = Coin.TenCents;
                    break;
                case "twentyCentCoin":
                    coinToBeInserted = Coin.TwentyCents;
                    break;
                case "fiftyCentCoin":
                    coinToBeInserted = Coin.FiftyCents;
                    break;
                case "oneEuroCoin":
                    coinToBeInserted = Coin.OneEuro;
                    break;
                default:
                    await SendVendingMachineMessage("Coin not found, please try again!");
                    throw new Exception($"Please insert valid coinId, actual: {coinId}");
            }

            var result = _useCase.InsertCoin(coinToBeInserted);

            string message;

            if (result.HasFailed)
            {
                message = $"{result.Message} failed, because money can't exceed above €2,00 \r\n" +
                          $"Amount of money: €{result.CurrentAmount}";
            }
            else
            {
                message = $"{result.Message} \r\n" +
                          $"Amount of money: {result.CurrentAmount}";
            }

            await SendVendingMachineMessage(message);
        }

        public async Task ReceiveSelectedProduct(string productId)
        {
            Product productSelected;

            switch (productId)
            {
                case "teaProduct":
                    productSelected = Product.Tea;
                    break;
                case "espressoProduct":
                    productSelected = Product.Espresso;
                    break;
                case "juiceProduct":
                    productSelected = Product.Juice;
                    break;
                case "chickenSoupProduct":
                    productSelected = Product.ChickenSoup;
                    break;
                default:
                    await SendVendingMachineMessage("Product not found, please try again!");
                    throw new Exception($"Please insert valid productId, actual: {productId}");
            }

            var result = _useCase.PurchaseProduct(productSelected);

            string message;

            if (result.CoinsReturned.Any())
            {
                message = $"{result.Message} \r\n" +
                          $"Coins to return: {string.Join(", ", result.CoinsReturned.Select(slot => $"{slot.Amount} x €{slot.Value}"))}";
            }
            else
            {
                message = $"{result.Message} \r\n" +
                          "Coins to return: None";
            }

            await SendVendingMachineMessage(message);
        }

        public async Task ReceiveCancelPurchase()
        {
            var result = _useCase.CancelPurchase();

            string message;

            if (result.CoinsReturned.Any())
            {
                message = $"{result.Message} \r\n" +
                          $"Coins to return: {string.Join(", ", result.CoinsReturned.Select(slot => $"{slot.Amount} x €{slot.Value}"))}";
            }
            else
            {
                message = $"{result.Message} \r\n" +
                          "Coins to return: None";
            }

            await SendVendingMachineMessage(message);
        }
    }
}
