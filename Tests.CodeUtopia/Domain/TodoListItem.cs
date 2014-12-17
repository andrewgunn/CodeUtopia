using System;
using CodeUtopia.Domain;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListItem : Entity
    {
        public TodoListItem(Guid todoListId,
                            IVersionNumberProvider versionNumberProvider,
                            Guid todoListItemId,
                            string description)
            : base(todoListId, versionNumberProvider, todoListItemId)
        {
            _description = description;

            RegisterEventHandlers();
        }

        public void Complete()
        {
            Apply(new TodoListItemCompletedEvent
                  {
                      AggregateId = TodoListId,
                      AggregateVersionNumber = GetNextVersionNumber(),
                      EntityId = TodoListItemId
                  });
        }

        private void OnTodoListItemCompletedEvent(TodoListItemCompletedEvent todoListItemCompletedEvent)
        {
            _isComplete = true;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<TodoListItemCompletedEvent>(OnTodoListItemCompletedEvent);
        }

        protected Guid TodoListId
        {
            get
            {
                return AggregateId;
            }
        }

        protected Guid TodoListItemId
        {
            get
            {
                return EntityId;
            }
        }

        private readonly string _description;

        private bool _isComplete;
    }
}