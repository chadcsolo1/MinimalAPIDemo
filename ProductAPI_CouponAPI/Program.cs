using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProductAPI_CouponAPI.Data;
using ProductAPI_CouponAPI.Models;
using ProductAPI_CouponAPI.Models.DTO;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Minimal API Endpoints - I will show multiple ways to format these endpoints
//app.MapGet("/helloworld", () => "Hello World!");

//app.MapGet("/helloworld", () =>
//{
//    return 1 + 1;
//});

//app.MapGet("/helloworld/{id:int}", (int id) =>
//{
//    if (id == 0) {return Results.BadRequest("Bad request");}
//    return Results.Ok(id);
    
//});

app.MapGet("/api/customers", (ILogger<Program> _logger) =>
{
    APIResponse response = new();
    _logger.Log(LogLevel.Information, "Getting all customer accounts");
    response.Result = CustomerStore.customerList;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;

    Results.Ok(response);
}).WithName("GetCustomers").Produces<APIResponse>(200);

//app.MapGet("api/customers", () =>
//{
//    return Results.Ok(CustomerStore.customerList);
//});

app.MapGet("/api/customer/{id:int}", (int id) => 
{
    APIResponse response = new();
    if (id == 0 || id < 0) 
    {
        response.IsSuccess = false;
        response.ErrorMessages.Add("The id you provided was either null or less than or equal to zero.");
        return Results.BadRequest(response);
    } 

    var customer = CustomerStore.customerList.FirstOrDefault(x => x.Id.Equals(id));

    if (customer == null) 
    {
        response.IsSuccess = false;
        response.ErrorMessages.Add("The account your looking for was not found.");
        return Results.NotFound(response);
    }

    response.IsSuccess = true;
    response.Result = customer;
    response.StatusCode = HttpStatusCode.OK;


    return Results.Ok(response);
}).WithName("GetCustomer").Produces<APIResponse>(200);

//app.MapGet("/api/customer/{id:int}", (int id) => 
//{
//    if (id == 0 || id < 0) { return Results.BadRequest("The id you provided was either null or less than or equal to zero.");} 

//    var customer = CustomerStore.customerList.FirstOrDefault(x => x.Id.Equals(id));

//    if (customer == null) { return Results.NotFound("The account your looking for was not found."); }

//    return Results.Ok(customer);
//}).WithName("GetCustomer").Produces<CustomerAccount>(200).Produces(400).Produces(404);


app.MapPost("/api/customer", ([FromBody] CustomerAccountCreateDTO customerDTO) =>
{
    APIResponse response = new();
    //if (customer.Id != 0 || string.IsNullOrEmpty(customer.Name))
    //{
    //    return Results.BadRequest("Make sure to leave the Id value equal to zero. Id is system generated. Also, ensure you enter a value for the Name field.");
    //}

    //if (CustomerStore.customerList.FirstOrDefault(x => x.Name.ToLower() == customer.Name.ToLower()) != null) {return Results.BadRequest("An account with this information already exists.");}

    //customer.Id = CustomerStore.customerList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

    CustomerAccount _customer = new CustomerAccount
    {
        Name = customerDTO.Name,
        MemebershipLevel = customerDTO.MemebershipLevel,
    };
    _customer.Id = CustomerStore.customerList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
    CustomerStore.customerList.Add(_customer);

    response.IsSuccess = true;
    response.Result = _customer;
    response.StatusCode= HttpStatusCode.Created;

    return Results.Ok(response);
    //return Results.CreatedAtRoute("GetCustomer", new {id = _customer.Id}, _customer);
    //return Results.Created($"/api/customer/{_customer.Id}", _customer);
}).WithName("CreateCustomer").Accepts<CustomerAccount>("application/json").Produces<APIResponse>(200);

app.MapPut("/api/customer", () =>
{

});

app.MapDelete("/api/delete/{id:int}", (int id) =>
{

});

app.UseHttpsRedirection();

app.Run();

