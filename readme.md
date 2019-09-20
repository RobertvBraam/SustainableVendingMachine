# Welcome to Sustainable Vending Machine

## Initial data

Vending machine contains the following products:
-	Tea (1.30 eur), 10 portions
-	Espresso (1.80 eur), 20 portions
-	Juice  (1.80 eur), 20 portions
-	Chicken soup (1.80 eur), 15 portions

At the start the vending machine wallet contains the following coins (for exchange):
-	10 cent, 100 coins
-	20 cent, 100 coins
-	50 cent, 100 coins
-	1 euro, 100 coins

The customer has an unlimited supply of coins.

## The requirements

Accept coins: Customer should be able to insert coins to the vending machine.

Return coins: Customer should be able to take the back the inserted coins in case customer decides to cancel his purchase.

Sell a product: Customer should be able to buy a product:
-	If the product price is less than the deposited amount, Vending machine should show a message “Thank you” and return the difference between the inserted amount and the price using the smallest number of coins possible.
-	If the product price is higher than the amount inserted, Vending machine should show a message “Insufficient amount”

UI: The amount and type of coins returned should be shown by the UI.

## Additional questions about requirements

What to do when:
-	Products are out of stock? (show product is out of stock my making the button unavailable)
-	Coins are out of stock? (Show warning sign “Vending machine does not return any change”)
-	Someone is inserting hundreds of coins into the vending machine? (Limit the amount inserted to 2 euro’s in any type of coin and when reaching the limit then display message “Coin added: €{Coin Amount} failed, because amount of money can't exceed above €2,00”)
-	The amount of coins is equal to the price for the requested product? (Show “Thank you”)
-	Someone inserts a invalid coin (5 cent or 2 euro)? (Refuse coin and show “Coin invalid please try again”)
-	The vending machine needs maintenance? (These and any maintenance related use cases are out of scope of this assignment)

## Setup

UI: ASP.NET Core 2.2 + SignalR

Backend: .netstandard 2.0
