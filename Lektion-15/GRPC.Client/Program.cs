using Grpc.Core;
using Grpc.Net.Client;
using GRPC.Client;

Console.WriteLine("Tryck på valfri knapp för att starta...");
Console.ReadKey();


using var channel = GrpcChannel.ForAddress("https://localhost:7044");

// Greeting
var greetClient = new Greeter.GreeterClient(channel);
var greetReply = await greetClient.SayHelloAsync(new HelloRequest { Name = "Hans Mattin-Lassei" });
Console.WriteLine($"{greetReply.Message}\n");
Console.ReadKey();

// Get Products
var productClient = new Product.ProductClient(channel);
var productReply = await productClient.GetProductAsync(new ProductRequest { Id = 1 });
Console.WriteLine($"{productReply.Name} {productReply.Price} SEK \n");
Console.ReadKey();

// Products
var productsClient = new Product.ProductClient(channel);
using (var productsReply = productsClient.GetProducts(new ProductEmptyRequest()))
{
    await foreach(var product in productsReply.ResponseStream.ReadAllAsync())
        Console.WriteLine($"{product.Name} {product.Price} SEK");

}


Console.ReadKey();
