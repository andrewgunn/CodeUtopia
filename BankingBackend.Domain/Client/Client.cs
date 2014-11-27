using System;
using System.Collections.Generic;
using System.Linq;
using BankingBackend.Domain.Account;
using BankingBackend.Events.v1.Client;
using CodeUtopia.Domain;

namespace BankingBackend.Domain.Client
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

        public void AssignNewBankCard(Guid bankCardId, Guid accountId)
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

        private void OnAnyBankCardEvent(BankCardEvent bankCardEvent)
        {
            IEntity bankCard;

            if (!_bankCards.TryGetValue(bankCardEvent.BankCardId, out bankCard))
            {
                throw new BankCardDoesNotExistException(bankCardEvent.BankCardId);
            }

            bankCard.LoadFromHistory(new[]
                                     {
                                         bankCardEvent
                                     });
        }

        private void OnClientCreated(ClientCreatedEvent clientCreatedEvent)
        {
            AggregateId = clientCreatedEvent.ClientId;
            _clientName = clientCreatedEvent.ClientName;
        }

        private void OnNewBankCardAssigned(NewBankCardAssignedEvent newBankCardAssignedEvent)
        {
            var bankCard = BankCard.Create(AggregateId,
                                           this,
                                           newBankCardAssignedEvent.BankCardId,
                                           newBankCardAssignedEvent.AccountId);

            _bankCards.Add(bankCard);
        }

        public Account.Account OpenNewAccount(Guid accountId, AccountName accountName)
        {
            EnsureClientIsInitialized();

            var account = Account.Account.Create(accountId, AggregateId, accountName);

            Apply(new AccountAssignedEvent(AggregateId, GetNextVersionNumber(), accountId));

            return account;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<ClientCreatedEvent>(OnClientCreated);
            RegisterEventHandler<AccountAssignedEvent>(OnAccountAdded);
            RegisterEventHandler<NewBankCardAssignedEvent>(OnNewBankCardAssigned);

            RegisterEventHandler<BankCardReportedStolenEvent>(OnAnyBankCardEvent);
        }

        private readonly List<Guid> _accountIds;

        private readonly EntityList<BankCard> _bankCards;

        private ClientName _clientName;
    }
}