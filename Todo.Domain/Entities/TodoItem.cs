namespace Todo.Domain.Entities
{
    public class TodoItem : Entity
    {
        public TodoItem(string title, string user, DateTime executionDate)
        {
            Title = title;
            User = user;
            ExecutionDate = executionDate;

            CreatedAt = DateTime.UtcNow;
            Done = false;
        }

        public string Title { get; private set; }
        public bool Done { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ExecutionDate { get; private set; }
        public string User { get; private set; }

        public void SetAsDone()
        {
            if (!Done)
                Done = true;
        }

        public void SetAsUndone()
        {
            if (Done)
                Done = false;
        }

        public void UpdateTile(string title)
        {
            Title = title;
        }

        public void UpdateExecutionDate(DateTime executionDate)
        {
            ExecutionDate = executionDate;
        }
    }

}
