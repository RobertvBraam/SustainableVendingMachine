"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/VendingMachine").build();

var coinElements = document.getElementsByClassName("coinBtn");

for (let coin of coinElements) {
    DisableElement(coin);
    AddCoinClickEvent(coin);
}

var productElements = document.getElementsByClassName("productBtn");

for (let product of productElements) {
    DisableElement(product);
    AddProductClickEvent(product);
}

DisableElement(document.getElementById("cancelPayment"));
AddCancelClickEvent(document.getElementById("cancelPayment"));

connection.start().then(function () {
    for (let coin of coinElements) {
        EnableElement(coin);
    }
    for (let product of productElements) {
        EnableElement(product);
    }
    EnableElement(document.getElementById("cancelPayment"));
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveVendingMachineDisplayMessage", function (message) {
    document.getElementById("vendingMachineDisplay").textContent = message;
});

connection.on("SendProductOutOfStockMessage", function (message) {
    for (let product of productElements) {
        if (product.id.toLowerCase().includes(message.toLowerCase())) {
            product.value = "OUT OF STOCK";
            product.disabled = true;
        }
    }
});

function AddCoinClickEvent(element) {
    element.addEventListener("click", function (event) {
        var coinId = event.target.id;
        connection
            .invoke("ReceiveInsertedCoin", coinId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    });
}

function AddProductClickEvent(element) {
    element.addEventListener("click", function (event) {
        var coinId = event.target.id;
        connection
            .invoke("ReceiveSelectedProduct", coinId)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    });
} 

function AddCancelClickEvent(element) {
    element.addEventListener("click", function (event) {
        connection
            .invoke("ReceiveCancelPurchase")
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    });
} 

function DisableElement(element) {
    element.disabled = true;
} 

function EnableElement(element) {
    element.disabled = false;
} 

