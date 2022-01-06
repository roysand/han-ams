// See https://aka.ms/new-console-template for more information


using MessageService.Services;



Console.WriteLine("Hello, World!");

var publisher = new AzureServiceBusPublisher();
await publisher.Publish(new CustomerCreated()
{
    FullName = "Roy Sandman IV",
    DateOfBirth = DateTime.Now
});



public class CustomerCreated
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FullName { get; set; }

    public DateTime DateOfBirth { get; set; }
}
