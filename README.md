# PayService

This is a REST API made on .NET for register customers and its charges, and calculate after the costs of each one and grouped (to have
more an analytical view of the costs).
This project was built with the following resources:

* .NET 6 (C#) as the programming language;
* xUnit for integration and unit tests;
* Swagger as the api documantation;
* Azure Cache for Redis as the data persistance on the cloud.

## Installation

You don't need to install any dependencie outside the .NET 6 framework and the packages on NuGet. But, you can
run the application on Docker too, the ```Dockerfile``` in on ```PayService/src/PayService.API/```.
The connection string to the Redis instance is on the ``` PayService/src/PayService.Data/ ```, on the json files, ```settings_dev.json``` (running
the application on DEBUG mode) and ```settings_prod.json``` (running on RELEASE mode).

## Architecture and Design

The application was built based on the DDD approachs. I am not a DDD expert, so the application may not be the best application
ever made on this approach.

![image](https://user-images.githubusercontent.com/40045069/168699501-40667591-a1b6-44ea-b37f-b8a9cf3ff4bc.png)

On the general architecture was used the Clean Architecture, building the overall application on tiers/levels. This helps to 
reduce the coupling on the source code, and makes easily to implement new features.

![image](https://user-images.githubusercontent.com/40045069/168700641-9e3aad1c-f197-499c-ac15-b9bc4af92eab.png)

The development methodology used was TDD, the development was fully focusing on the tests. Testing every single class and method used
on the business logic of the application. The most amount of test are units ones, and some integration tests on the e2e, testing the 
endpoints of the REST API.

![tests](https://user-images.githubusercontent.com/40045069/168700713-76cc410a-60a2-47d7-bac4-f5d2cec708d0.png)

## Endpoints

### POST `payservice/customer`

This endpoint is for creating a new customer.
The body of the request is like above.


```json
{
  "name": "Leonardo",
  "state": "RS",
  "cpf": "999.999.999-99"
}
```

where:

| Field       | Type   | Description                    |
|-------------|--------|--------------------------------|
| name        | String | Customer's name                |
| state       | String | Customer's regionality         |
| cpf         | String | Customer's cpf document number |


> Note:
> Must have all the fields completed to success the request.

And the return is going to be the object itself.

```json
{
  "name": "Leonardo",
  "state": "RS",
  "cpf": "99999999999"
}
```
The possible return status code for this request are:

| Status Code | Description                       |
|-------------|-----------------------------------|
| 201         | Customer successfully created     |
| 400         | Missing field or in wrong format  |


### GET `payservice/customer?cpf={}`

Find a previously created customer by its cpf (can pass it formated or not).
This request has no body. And the return is same as the POST version of this endpoint.

| Status Code | Description                       |
|-------------|-----------------------------------|
| 200         | Customer successfully found       |
| 400         | Missing field or in wrong format  |
| 404         | Customer not found                |

### POST `payservice/charge`

This endpoint is for create a customer's charge.
The body of this request must be as the following model:

```json
{
  "dueDate": "2022-05-15",
  "totalAmount": 0,
  "cpf": "999.999.999-99"
}
```
| Field       | Type   | Description                                     |
|-------------|--------|-------------------------------------------------|
| dueDate     | Date   | The date of the charge on the format YYYY-MM-dd |
| totalAmount | Double | Customer's regionality                          |
| cpf         | String | Customer's cpf document number                  |

> Note:
> Must have all the fields completed to success the request.

And the return is going to be the object itself.

The possible return status code for this request are:

| Status Code | Description                       |
|-------------|-----------------------------------|
| 201         | Customer successfully created     |
| 400         | Missing field or in wrong format  |

### GET `payservice/charge?cpf={}&month={}`

This endpoint is for listing all the charges by a customer (cpf) or period (month). This endpoint only uses one
of these filters, the cpf has preferences if the user inputed both.

```json
[
  {
    "dueDate": "2022-05-15T00:00:00",
    "totalAmount": 56.78,
    "cpf": "99999999999"
  },
  {
    "dueDate": "2022-06-10T00:00:00",
    "totalAmount": 43.56,
    "cpf": "99999999999"
  },
  {
    "dueDate": "2022-02-19T00:00:00",
    "totalAmount": 420.34,
    "cpf": "99999999999"
  }
]
```

| Status Code | Description                           |
|-------------|---------------------------------------|
| 200         | Charges successfully found            |
| 400         | Missing parameter or in wrong format  |


### GET `payservice/invoice/state`

This endpoint is for calculate the invoices of all the customers registered and group it by state.
The return of the request is going to be as the following:

```json
[
  {
    "state": "SP",
    "totalAmount": 6340
  },
  {
    "state": "RS",
    "totalAmount": 385
  },
  {
    "state": "CA",
    "totalAmount": 9778
  },
  {
    "state": "NC",
    "totalAmount": 1035
  },
  {
    "state": "NY",
    "totalAmount": 2368
  },
  {
    "state": "SC",
    "totalAmount": 8344
  }
]
```
