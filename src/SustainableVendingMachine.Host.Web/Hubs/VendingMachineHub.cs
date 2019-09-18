﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.Enitities.Products;
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
                    coinToBeInserted = Coin.TenCent;
                    break;
                case "twentyCentCoin":
                    coinToBeInserted = Coin.TwentyCent;
                    break;
                case "fiftyCentCoin":
                    coinToBeInserted = Coin.FiftyCent;
                    break;
                case "oneEuroCoin":
                    coinToBeInserted = Coin.OneEuro;
                    break;
                default:
                    await SendVendingMachineMessage("Something went Wrong please try again!");
                    throw new Exception();
            }

            var result = _useCase.InsertCoin(coinToBeInserted);

            await SendVendingMachineMessage(result.Message + result.CurrentAmount);
        }

        public async Task ReceiveSelectedProduct(string productId)
        {
            Product productSelected;

            switch (productId)
            {
                case "teaProduct":
                    productSelected = new TeaProduct();
                    break;
                case "espressoProduct":
                    productSelected = new EspressoProduct();
                    break;
                case "juiceProduct":
                    productSelected = new JuiceProduct();
                    break;
                case "chickenSoupProduct":
                    productSelected = new ChickenSoupProduct();
                    break;
                default:
                    await SendVendingMachineMessage("Something went Wrong please try again!");
                    throw new Exception();
            }

            var result = _useCase.PurchaseProduct(productSelected);

            await SendVendingMachineMessage(result.Message + string.Join(',', result.CoinsReturned));
        }
    }
}
