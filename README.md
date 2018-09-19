# SQL_Contact_Wallet_V2
Program that manages a contact wallet with SQL Server 2017 in WinForms mode

This second version is in visual mode thanks to WinForms, when you open the program you got to click rather on contacts or societies and then you can :

-see the full card of a contact/society after clicking on it and edit it

-delete a contact or society

-add a new contact or society

-display the list of the contacts working in a society by double-clicking on it

-display the list of contacts working in the same society as the one you double-clicked

Each contact needs to be linked to a society, but you can create a society without any contact working in it

If you delete a society it will also delete all your contacts that works in this society

The database contains 2 tables :

Contact : -[PK] id -[FK] id of the society -title -surname -name -function (can be NULL)

Society : -[PK] id -name of the society -address -second address (can be NULL) -postal code (numbers only) -city -standard (can be NULL)

All SQL and database settings are in the Constants.cs class

I have only produced the code in the Source folder, files in the Designer folder are automatically generated by Visual Studio
