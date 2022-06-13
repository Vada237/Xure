using Xure.Api.Interfaces;
using Xure.Data;
using Microsoft.AspNetCore.Mvc;
using Xure.App.Models;
using System;
using System.Security.Claims;
using System.Linq;

namespace Xure.App.Controllers
{
    public class MessageController : Controller
    {

        public IMessageRepository MessageRepository { get; set; }
        
        public IUserRepository UserRepository { get; set; } 

        public IOrderReportRepository orderReportRepository { get; set; }
        public MessageController(IMessageRepository repository,IUserRepository userRepository,IOrderReportRepository orderReportRepository)
        {
            MessageRepository = repository; 
            UserRepository = userRepository;
            this.orderReportRepository = orderReportRepository;
        }
        
    }
}
