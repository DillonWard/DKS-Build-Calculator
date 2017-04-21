# Dark Souls Build Calculator
###### *Dillon Ward - G00326756 - Mobile Apps Development*
---
#### About
The following repository contains a Universal Web Application for a third-year undergraduate project for the module Mobile Apps Development. The module is taught to undergraduate students at GMIT in the Department of Computer Science and Applied Physics. The lecturer is Damien Costello.

#### Overview
The project is used to calculating your total stats from armor/weapons, calculating total offence and defence stats that are read in from a database. The app contains drop down boxes that allows the user to pick items. These items will inrement/decrement variables that display totals. The database uses MongoDB and Spring-Boot. 

#### How it works
The Database is actually broken up into 2 different databases. One for Weapons, one for Armor. Armor and Weapons have different values, so they were seperated so they could be read in and seperated differently making them easier to format. Items have 5 number variables, as well as a name and a type. The types are what seperates the items. The items are read in and parsed using the `JSonParser` class, which reads in from the database and saves everything into a list, one for Offence and one for Defence. Items are binded to a dropdown box, seperating them by their type.

[App](https://www.microsoft.com/en-ie/store/p/dark-souls-calculator/9nz5xcpr54d3)
[Back-End](https://github.com/DillonWard/Mobile-Apps-Backend)
