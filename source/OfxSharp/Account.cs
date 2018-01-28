using System.Xml;

namespace OfxSharp
{
    public class Account
    {
        public Account(XmlNode node, AccountType type)
        {
            AccountType = type;

            AccountId = node.GetValue("//ACCTID");
            AccountKey = node.GetValue("//ACCTKEY");

            switch (AccountType)
            {
                case AccountType.BANK:
                    InitializeBank(node);
                    break;

                case AccountType.AP:
                    InitializeAp(node);
                    break;

                case AccountType.AR:
                    InitializeAr(node);
                    break;
            }
        }

        public string AccountId { get; set; }
        public string AccountKey { get; set; }
        public AccountType AccountType { get; set; }

        /// <summary>
        ///     Initializes information specific to bank
        /// </summary>
        private void InitializeBank(XmlNode node)
        {
            BankId = node.GetValue("//BANKID");
            BranchId = node.GetValue("//BRANCHID");

            //Get Bank Account Type from XML
            var bankAccountType = node.GetValue("//ACCTTYPE");

            //Check that it has been set
            if (string.IsNullOrEmpty(bankAccountType))
                throw new OFXParseException("Bank Account type unknown");

            //Set bank account enum
            _bankAccountType = bankAccountType.GetBankAccountType();
        }

        #region Bank Only

        private BankAccountType _bankAccountType = BankAccountType.NA;

        public string BankId { get; set; }

        public string BranchId { get; set; }

        public BankAccountType BankAccountType
        {
            get => AccountType == AccountType.BANK ? _bankAccountType : BankAccountType.NA;
            set => _bankAccountType = AccountType == AccountType.BANK ? value : BankAccountType.NA;
        }

        #endregion Bank Only

        #region Account types not supported

        private void InitializeAp(XmlNode node)
        {
            throw new OFXParseException("AP Account type not supported");
        }

        private void InitializeAr(XmlNode node)
        {
            throw new OFXParseException("AR Account type not supported");
        }

        #endregion Account types not supported
    }
}