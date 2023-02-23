GS Invoice API

The project consists of a set of .NET Core API endpoints which handles the below functionalities.

1) Create an invoice
2) Update an invoice
3) Delete an invoice
4) Get all invoices
5) Get invoice by id

The technologies used to implement the project is ASP.NET Core 6 and Azure CosmosDB.

The API was created aligning with the clean architecture which mainly focuses on Domain Driven Design.

The project is structured based on the following aspects of the clean architecture.

1) Domain - This consists of the domain models as well as the interface of the repository class. Model validator classes have been implemented in this layer.
2) Application - This consists of the interface and implementation class of the invoice service. The application layer can be used to handle any business logics related to the API.
3) Infrastructure - This cosists of the implementation class of the invoice repository interface defined in the Domain layer. 
4) API - This consists of the controller class which includes functions for the operations performed exposed as API endpoints.