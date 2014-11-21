﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Bank.Domain.Account;
using CodeUtopia.Bank.Events.v1.Client;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Domain.Client
{
    public class Client : Aggregate
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
            Apply(new ClientCreated(clientId, GetNextVersionNumber(), clientName));
        }

        public Account.Account AddAccount(Guid accountId, AccountName accountName)
        {
            EnsureClientIsInitialized();

            var account = Account.Account.Create(accountId, AggregateId, accountName);

            Apply(new AccountAddedToClient(AggregateId, GetNextVersionNumber(), account.AggregateId));

            return account;
        }

        public void AddBankCard(Guid bankCardId, Guid accountId)
        {
            EnsureClientIsInitialized();

            EnsureAccountBelongsToClient(accountId);

            Apply(new BankCardAddedToClient(AggregateId, GetNextVersionNumber(), bankCardId, accountId));
        }

        public static Client Create(Guid clientId, ClientName clientName)
        {
            return new Client(clientId, clientName);
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

        private void OnAccountAddedToClient(AccountAddedToClient accountAddedToClient)
        {
            _accountIds.Add(accountAddedToClient.AccountId);
        }

        private void OnBankCardAddedToClient(BankCardAddedToClient bankCardAddedToClient)
        {
            var bankCard = BankCard.Create(AggregateId,
                                           this,
                                           bankCardAddedToClient.BankAccountId,
                                           bankCardAddedToClient.AccountId);

            _bankCards.Add(bankCard);
        }

        private void OnClientCreated(ClientCreated clientCreated)
        {
            AggregateId = clientCreated.AggregateId;
            _clientName = clientCreated.ClientName;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<ClientCreated>(OnClientCreated);
            RegisterEventHandler<AccountAddedToClient>(OnAccountAddedToClient);
            RegisterEventHandler<BankCardAddedToClient>(OnBankCardAddedToClient);
        }

        private readonly List<Guid> _accountIds;

        private readonly EntityList<BankCard> _bankCards;

        private ClientName _clientName;
    }
}