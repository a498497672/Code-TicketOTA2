using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    public class OtaBusinessUserService
    {
        private readonly OtaBusinessUserRepository _otaBusinessUserRepository;

        public OtaBusinessUserService(OtaBusinessUserRepository otaBusinessUserRepository)
        {
            _otaBusinessUserRepository = otaBusinessUserRepository;
        }

        public Tbl_OTABusinessUser Get(string userName,string passWord)
        {
            return _otaBusinessUserRepository.FirstOrDefault(a => a.UserName == userName && a.PassWord == passWord);
        }

        public void Update(Tbl_OTABusinessUser tbl_OTABusinessUser)
        {
            _otaBusinessUserRepository.Update(tbl_OTABusinessUser);
        }
    }
}
