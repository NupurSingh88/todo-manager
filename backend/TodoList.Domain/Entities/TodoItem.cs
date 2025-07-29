using System;

public class TodoItem
{
	
    public Guid ID { get; set; }

    public string Name { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsDeleted { get; set; }

    public static TodoItem Create(string name, DateTime dueDate) 
    { 
        TodoItem item = new TodoItem();
        item.ID = Guid.NewGuid();
        item.Name = name;
        item.DueDate = dueDate;
        item.IsCompleted = false;
        item.IsDeleted = false;

        return item;    
    }

    public void Update(string name, DateTime dueDate)
    {
        Name = name;
        DueDate = dueDate;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void MarkComplete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
    }
}
