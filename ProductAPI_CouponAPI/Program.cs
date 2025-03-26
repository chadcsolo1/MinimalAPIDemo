using Microsoft.AspNetCore.Mvc;
using ProductAPI_CouponAPI.Data;
using ProductAPI_CouponAPI.Models;

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

app.MapGet("/api/customers", () => Results.Ok(CustomerStore.customerList));

//app.MapGet("api/customers", () =>
//{
//    return Results.Ok(CustomerStore.customerList);
//});

app.MapGet("/api/customer/{id:int}", (int id) => 
{
    //var customer = CustomerStore.customerList.FirstOrDefault(x => x.Id.Equals(id)
    return Results.Ok(CustomerStore.customerList.FirstOrDefault(x => x.Id.Equals(id)));
});

app.MapPost("/api/customer", ([FromBody] CustomerAccount customer) =>
{
    if (customer.Id != 0 || string.IsNullOrEmpty(customer.Name))
    {
        return Results.BadRequest("Make sure to leave the Id value equal to zero. Id is system generated. Also, ensure you enter a value for the Name field.");
    }

    if (CustomerStore.customerList.FirstOrDefault(x => x.Name.ToLower() == customer.Name.ToLower()) != null) {return Results.BadRequest("An account with this information already exists.");}

    customer.Id = CustomerStore.customerList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

    CustomerAccount _customer = new CustomerAccount
    {
        Id = customer.Id,
        Name = customer.Name,
        MemebershipLevel = customer.MemebershipLevel,
    };

    CustomerStore.customerList.Add(_customer);

    return Results.Ok(_customer);
});

app.MapPut("/api/customer", () =>
{

});

app.MapDelete("/api/delete/{id:int}", (int id) =>
{

});

app.UseHttpsRedirection();

app.Run();

