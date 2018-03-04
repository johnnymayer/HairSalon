# Wizard Salon
**Johnny Mayer**

## Hair Salon C# Project 


A C# simulated Hair Salon.  This is an MVC app that allows a user to add Stylists, and add Clients to specific Stylists.

###Specifications
* User can create a Stylist
	* Input: "Jane Doe"
	* Output: "Jane Doe"

* User can create a Client
	* Input: "Super Client"
	* Output: "Super Client"

* Client is assigned to Stylist upon creation
	* Input: "Super Client" assigned to "Jane Doe"
	* Output: "Client: Super Client, Stylist: Jane Doe"


## Setup/Installation/Requirements

* .NET Framework and MAMP required
* Clone the repository via terminal ```$git clone https://github.com/johnnymayer/HairSalon```
* Launch MAMP and Start Servers
* Create the database in MySql with the following command:

```
CREATE TABLE `clients` (
  `name` varchar(255) DEFAULT NULL,
  `id` int(11) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `stylists` (
  `name` varchar(255) DEFAULT NULL,
  `id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
* From the terminal, navigate to ~/HairSalon.Solution/HairSalon
* Run ```$dotnet restore```
* Launch the program with ```dotnet run```
* Navigate to [http://localhost:5000](http://localhost:5000) in your browser, and engage with the program.

##Built With
* HTML
* Razor
* Bootstrap & Custom CSS
* C#
* .NET Core 1.1
* MySql
* Mamp
* Atom

####License
MIT License applies.

**Copyright 2018 Johnny Mayer**
[Johnny Mayer GitHub](https://github.com/johnnymayer)