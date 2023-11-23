using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.CustomerApp
{
    public partial class CustomerService : ICustomerService
    {

        public CustomerService(
            IRepository<Customer> customerRepo,
            IRepository<CustomerInvoice> invoiceRepo,
            IRepository<CustomerPwd> pwdRepo)
        {
            CustomerRepo = customerRepo;
            CustomerInvoiceRepo = invoiceRepo;
            CustomerPwdRepo = pwdRepo;
        }

        public IRepository<Customer> CustomerRepo { get; }
        public IRepository<CustomerInvoice> CustomerInvoiceRepo { get; }
        public IRepository<CustomerPwd> CustomerPwdRepo { get; }


        public async Task<Customer> GetCustomerAsync(string customerNo)
        {
            return await CustomerRepo.GetAsync(a => a.CustomerNo == customerNo);
        }
    }
}
