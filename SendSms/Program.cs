using System;
using qcloudsms_csharp;

namespace SendSms
{
    class Program
    {
        static void Main(string[] args)
        {
            var appid = 1400061152;
            var appkey = "aed63c940dcfc946340f9b280e0dabfc";
            var phoneNumbers = new string[]{ "5142438578", ""};
            var templateId = 75975;
            var ssender = new SmsSingleSender(appid, appkey);
//            var result = ssender.sendWithParam("1", phoneNumbers[0],
//                templateId, new string[]{}, "", "", "");
//            Console.WriteLine(result.result);
        }
    }
}
