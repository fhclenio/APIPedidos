using Flunt.Validations;

namespace APIPedidos.Model.Product;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }
    public Category(string name, string createdBy, string editedBy, bool active)
    {
        Name = name;
        Active = active;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
                    .IsNotNullOrEmpty(Name, "Name")
                    .IsGreaterOrEqualsThan(Name, 2, "Name");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active)
    {
        Name = name;
        Active = active;

        Validate();
    }
}
