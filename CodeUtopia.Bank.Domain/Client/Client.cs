using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Bank.Events.v1.Client;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Domain.Client
{
    public class Client : Aggregate, IOriginator
    {
        public Client()
        {
            _accountIds = new List<Guid>();
            _bankCards = new EntityList<BankCard>(this);
            _clientName = new ClientName("");

            RegisterEventHandlers();
        }

        private Client(Guid clientId, ClientName clientName)
            : this()
        {
            Apply(new ClientCreatedEvent(clientId, GetNextVersionNumber(), clientName));
        }

        public void AssignAccount(Guid accountId)
        {
            EnsureClientIsInitialized();

            Apply(new AccountAssignedEvent(AggregateId, GetNextVersionNumber(), accountId));
        }

        public void AssignNewBankCard(Guid accountId, Guid bankCardId)
        {
            EnsureClientIsInitialized();

            EnsureAccountBelongsToClient(accountId);

            Apply(new NewBankCardAssignedEvent(AggregateId, GetNextVersionNumber(), bankCardId, accountId));
        }

        public static Client Create(Guid clientId, ClientName clientName)
        {
            return new Client(clientId, clientName);
        }

        public IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        private void EnsureAccountBelongsToClient(Guid accountId)
        {
            if (!_accountIds.Contains(accountId))
            {
                throw new AccountDoesNotBelongToClientException(AggregateId, accountId);
            }
        }

        protected void EnsureClientIsInitialized()
        {
            EnsureIsInitialized();
        }

        public IBankCard GetBankCard(Guid bankCardId)
        {
            var bankCard = _bankCards.FirstOrDefault(x => x.EntityId == bankCardId);

            if (bankCard == null)
            {
                throw new BankCardDoesNotExistException(bankCardId);
            }

            return bankCard;
        }

        public void LoadFromMemento(Guid aggregateId, int versionNumber, IMemento memento)
        {
            throw new NotImplementedException();
        }

        private void OnAccountAdded(AccountAssignedEvent accountAssignedEvent)
        {
            _accountIds.Add(accountAssignedEvent.AccountId);
        }

        private void OnBankCardAdded(NewBankCardAssignedEvent newBankCardAssignedEvent)
        {
            var bankCard = BankCard.Create(AggregateId,
                                           this,
                                           newBankCardAssignedEvent.BankCardId,
                                           newBankCardAssignedEvent.AccountId);

            _bankCards.Add(bankCard);
        }

        private void OnClientCreated(ClientCreatedEvent clientCreatedEvent)
        {
            AggregateId = clientCreatedEvent.ClientId;
            _clientName = clientCreatedEvent.ClientName;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<ClientCreatedEvent>(OnClientCreated);
            RegisterEventHandler<AccountAssignedEvent>(OnAccountAdded);
            RegisterEventHandler<NewBankCardAssignedEvent>(OnBankCardAdded);
        }

        private readonly List<Guid> _accountIds;

        private readonly EntityList<BankCard> _bankCards;

        private ClientName _clientName;
    }
}