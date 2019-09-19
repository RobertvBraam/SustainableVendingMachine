using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.UseCases;

namespace SustainableVendingMachine.Host.Web.Hubs
{
    public class VendingMachineHub : Hub
    {
        private readonly PurchaseProductUseCase _useCase;

        public VendingMachineHub(PurchaseProductUseCase useCase)
        {
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
                    await SendVendingMachineMessage("Something went Wrong please try again!");
                    throw new Exception();
            }

            var result = _useCase.InsertCoin(coinToBeInserted);

            await SendVendingMachineMessage($"{result.Message} \r\n Amount of money: {result.CurrentAmount}");
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
                    await SendVendingMachineMessage("Something went Wrong please try again!");
                    throw new Exception();
            }

            var result = _useCase.PurchaseProduct(productSelected);

            await SendVendingMachineMessage($"{result.Message} \r\n Coins to return: {string.Join(',', result.CoinsReturned)}");
        }

        public async Task ReceiveCancelPurchase()
        {
            var result = _useCase.CancelPurchase();

            await SendVendingMachineMessage($"{result.Message} \r\n  Coins to return: {string.Join(',', result.CoinsReturned)}");
        }
    }
}
