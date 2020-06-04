using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Inha.Services.Demo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading;
using Inha.Commons.Log.Extensions;

namespace Inha.Services.Demo.v5.Controller
{
    [ApiVersion("4.0")]
    [ApiController]
    [Intercept("log-calls")]
    public class TienController : ControllerBase
    {
        private readonly IAService _aService;
        private readonly IConfiguration _config;
        public TienController(IAService aService, IConfiguration config)
        {
            this._aService = aService;
            this._config = config;
        }

        [Route("api/v{version:apiVersion}/abc")]
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }
        [Route("api/v{version:apiVersion}/GetService")]
        [HttpGet]
        public async Task<IActionResult> GetService()
        {
            SerilogHelper.GetInstance(_config).Information("hello");
            await Async1("tien", "nam");
            return Ok(_aService.GetName());
        }

        protected async Task<string> Async1(string thamso1, string thamso2)
        {
            Func<object, string> myfunc = (object thamso) =>
            {
                dynamic ts = thamso;
                for (int i = 1; i <= 15; i++)
                {

                    Thread.Sleep(500);
                }
                return $"Kết thúc! {ts.x}";
            };
            Task<string> task = new Task<string>(myfunc, new { x = thamso1, y = thamso2 });
            task.Start();

            Thread.Sleep(1000);
            await task;     // Gọi Async1 sẽ quay về chỗ gọi nó từ đây
            string ketqua = task.Result;       // Đọc kết quả trả về của task - không phải lo block thread gọi Async1
            Console.WriteLine("Làm gì đó khi task đã kết thúc");
            //Console.WriteLine(ketqua);          // In kết quả trả về của task
            return ketqua;
        }

        [Route("api/v{version:apiVersion}/GetYield")]
        [HttpGet]
        public ActionResult<string> GetYield()
        {
            string a = "nam";
            var cl = new List<abc> { new abc { ID = 1, Name = "tien" },
                                    new abc { ID = 10, Name = "tien1" },
                                    new abc { ID = 12, Name = "tien2" },
                                    new abc { ID = 13, Name = "tien3" },
                                    new abc { ID = 15, Name = "tien4" },
                                    new abc { ID = 16, Name = "tien5" },
                                    new abc { ID = 17, Name = "tien6" },
                                    new abc { ID = 23, Name = "tien7" },
                                    new abc { ID = 21, Name = "tien8" },
                                    new abc { ID = 22, Name = "tien9" },
                                    new abc { ID = 39, Name = "tien10" },
            };
            var response =  FindWork(cl);
            //  excecute(response);


            if (response.Where(x => x.ID == 10).Any())
                excecute(response);
               // a = "phat";
            //foreach (var item in response)
            //{
            //    a = item.Name;
            //}

            return Ok(response);
        }
        protected IEnumerable<abc> FindWork(IEnumerable<abc> wisActive)
        {
            for (int i = 1; i < 500; i++)
            {
                var current = wisActive.Where(p => p.ID.Equals(i)).FirstOrDefault();
                if (current != null)
                    yield return current;
            }
        }
        protected void excecute(IEnumerable<abc> items)
        {
            var ac = items.Select(p => new { Ten = p.Name });
            var b = "tine";
        }
    }
}
public class abc
{
    public int ID { get; set; }
    public string Name { get; set; }
}