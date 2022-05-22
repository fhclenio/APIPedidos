using Flunt.Validations;

namespace APIPedidos.Model.Product;

public class Category : Entity
{
    public Category(string name, string createdBy, string editedBy, bool active)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name");
        AddNotifications(contract);

        Name = name;
        Active = active;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;
    }
    public string Name { get; set; }
    public bool Active { get; set; }
}
