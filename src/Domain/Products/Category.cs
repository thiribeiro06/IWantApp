using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public Category(string name, string editedBy, string createdBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(EditedBy, "EditedBy")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy");
        AddNotifications(contract);
    }
    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;

        Validate();
    }
}
