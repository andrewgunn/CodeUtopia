using System;
using System.Linq;
using CodeUtopia.Domain;

namespace Tests.CodeUtopia.Domain
{
    public class TodoList : Aggregate
    {
        public TodoList()
        {
            _todoListItems = new EntityList<TodoListItem>(this);

            RegisterEventHandlers();
        }

        private TodoList(Guid todoListId, string name)
            : base(todoListId)
        {
            _todoListItems = new EntityList<TodoListItem>(this);
           
            RegisterEventHandlers();

            Apply(new TodoListCreatedEvent
                  {
                      Name = name
                  });
        }

        public void AddTodoListItem(Guid todoListItemId, string description)
        {
            Apply(new TodoListItemAddedEvent
                  {
                      TodoListItemId = todoListItemId,
                      Description = description
                  });
        }

        public static TodoList Create(Guid todoListId, string name)
        {
            return new TodoList(todoListId, name);
        }

        public TodoListItem GetTodoListItem(Guid todoListItemId)
        {
            return _todoListItems.SingleOrDefault(x => x.EntityId == todoListItemId);
        }

        private void OnTodoListCreatedEvent(TodoListCreatedEvent todoListCreatedEvent)
        {
            _name = todoListCreatedEvent.Name;
        }

        private void OnTodoListItemAddedEvent(TodoListItemAddedEvent todoListItemAddedEvent)
        {
            var todoListItem = new TodoListItem(TodoListId,
                                                this,
                                                todoListItemAddedEvent.TodoListItemId,
                                                todoListItemAddedEvent.Description);

            _todoListItems.Add(todoListItem);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<TodoListCreatedEvent>(OnTodoListCreatedEvent);
            RegisterEventHandler<TodoListItemAddedEvent>(OnTodoListItemAddedEvent);
        }

        protected Guid TodoListId
        {
            get
            {
                return AggregateId;
            }
        }

        private string _name;

        private readonly EntityList<TodoListItem> _todoListItems;
    }
}