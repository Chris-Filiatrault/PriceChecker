## Price Checker

Price Checker is a small hobby project. 
It is an Azure Function App with a timer trigger for checking prices and conditionally sending email alerts.

## What problem does it solve

I often buy chocolate brownie protein bars from Chemist Warehouse and like to stock up when they are on sale. I also wanted to know if it was possible to get the price straight from the HTML listed on the [website](https://www.chemistwarehouse.com.au/buy/76850/musashi-high-protein-bar-chocolate-brownie-90g) each day using a trigger function, store it in a database, and perhaps see if there is a trend later on by making a graph.

The Azure Function spins up each morning around 8am local time, obtains the price from the HTML and sends an email if the price is below a threshold of around $3.

I use the free SMTP mail server version from Brevo (formerly sendinblue).