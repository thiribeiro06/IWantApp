using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; }
    public bool Active { get; set; }

    public Category(string name, string editedBy, string createdBy) 
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name")
            .IsNotNullOrEmpty(editedBy, "EditedBy")
            .IsNotNullOrEmpty(createdBy, "CreatedBy");        
        AddNotifications(contract);

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
    }

}
