Profit Calculator
================

A mining profitability calculator for Windows. 

Usage
================
- Hashrates screen:
This give the possibility to add your own algorithms but preloaded with a default list for one gtx750ti. 
Synonyms is a csv string with any other name your algo might appear in the used APIs.
Target is a magic number (log 2 diff 1 target), set to 32 for most algos but some use other values, like quark => 24. Play with this number if you think an algo's calculation is off. 

- STYLES:
For a correct calculation to happen, each style needs it own variables, taken from both the algo and the (custom) coin, for their own formula:
1. "Classic" : Used in most cases. (Algo) Your hashrate & target. (Coin) Difficulty & block reward.
2. "NetHashRate" : Used when Target is unknown. (Algo) Your hashrate. (Coin) Block reward, block time & net hashrate.
3. "CryptoNight" : Used only for CryptoNight coins. (Algo) Your hashrate. (Coin) Difficulty, block reward and block time.

- Market API:
Uncheck/check the markets from which you want to check the prices. Beware: Some are very slow, especially Cryptsy.

- Coin info:
Uncheck/check the APIs that you want to use to get information about loads of coins, including stuff like difficulty and blockreward. CoinTweak and CoinWarz require personal API keys, please check below for more on that.

- Custom coins:
This gives the possibility to add your own coins if one of the coin info APIs don't have it yet. 
Make sure Tag is correct! This should be unique to each coin and is used to search for on exchanges etc. 
Algo can be anything, but make sure it has an equivalent in the "Hashrates" screen, so it can be calculated properly. 
Difficulty and Block Reward are required. Block Time is only required when using the "NetHashRate" style for your algo. 
Use Price if none of the supported exchanges have your coin, but once a checked market API has it, it will ignore this price.

- Multipools:
Currently checks the actual prices for NiceHash and historic data from PoolPicker.eu and Cryp.today. 
Cryp.today is run by "suchmoon" who runs a MH at several different multipools and rental site, and lists his actual profits. Results are named with "CT"
PoolPicker.eu collects the data from the multipools themselves, so if you think a price is off, it's probably the pool. Results are named with "PP". 
If you think it might be a help, you might also let the community vote decide. I've added an option that weighs the rating of a multipool on PoolPicker into the price.

- Coin Price Calc:
Several options for price calculation.
Possibility to use the price across all exchanges, weighted by volume.
Can check highest bid, recente tradeprice or lowest ask.
Can convert the BTC profit to USD/EUR/GBP/CNY using Coindesk.
Can subtract the electricity cost from your profits, make sure the Wattage is set in the "Hashrates" screen. 
If you want to just add the hashrate of 1GPU/rig in the "Hashrates" screen, you can set a multiplier here for easy scaling.

- Filter:
Several filters to clear out some unwanted results from the table. Also makes it able to color in the table, according to the filters. 

- Misc settings:
Not much yet, but you can set a proxy here and the desired timeout for APIs.

- Log: 
Log screen, gives more info about any error encountered. 

- Readme:
This :D

- Profiles:
Add new profiles by setting a new name first, then click "Add profile". This is not fully tested, so beware. But if you do, please report back any bugs ;)


How to get API keys
================
Get your CoinTweak API key here, one free key per mailaddress, 2000 usages max:
http://cointweak.com/API

Get your CoinWarz API key here, one free key per mailaddress, 25 usages/day, 1000 usages max:
http://www.coinwarz.com/v1/api/documentation

!!! Requires NET4.5 !!!
================
Downloadlink: http://www.microsoft.com/en-us/download/details.aspx?id=30653


Supported APIs
================
- Bittrex:		https://bittrex.com/
- Mintpal:		https://mintpal.com/
- Poloniex:	https://poloniex.com/
- Cryptsy:		https://cryptsy.com/
- BTer:		https://bter.com/
- AllCoin:		https://allcoin.com/
- AllCrypt:	https://allcrypt.com/
- C-Cex:		https://c-cex.com/
- Comkort:		https://comkort.com/
- AtomicTrade:	https://www.atomic-trade.com/
- Cryptoine: 	https://cryptoine.com/
- WhatToMine:	http://whattomine.com/
- CoinTweak:	https://cointweak.com/
- CoinWarz:	http://coinwarz.com/
- NiceHash:	https://nicehash.com
- PoolPicker:	http://poolpicker.eu/
- Cryp.Today:	http://cryp.today/
- CoinDesk:	http://coindesk.com/


Donations are welcome!
================
- BTC: 	1MVBPhMaeuj5daZtaKaVu8BZL5K44CCq7E
- BC: 	B4s7UnNYKePfGz5DVBzyNbaeiwi2ExLy7D
- TAC: 	TswDiAfmHdTnCpiRJVgfEtFxr3a4z3yHQk
- JPC: 	JY81D2jfvcD8WdisdGj7Rz6AcdBmGX9kRV