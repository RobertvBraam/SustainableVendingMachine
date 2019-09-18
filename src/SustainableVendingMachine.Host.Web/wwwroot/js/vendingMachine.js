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

connection.start().then(function () {
    for (let coin of coinElements) {
        EnableElement(coin);
    }
    for (let product of productElements) {
        EnableElement(product);
    }
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveVendingMachineDisplayMessage", function (message) {
    document.getElementById("vendingMachineDisplay").textContent = message;
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

function DisableElement(element) {
    element.disabled = true;
} 

function EnableElement(element) {
    element.disabled = false;
} 

