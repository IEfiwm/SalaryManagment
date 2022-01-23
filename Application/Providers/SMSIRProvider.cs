using SmsIrRestful;
using System;
using System.Collections.Generic;

namespace Application.Providers
{
    public static class SMSIRProvider
    {
        private static string _userAPIKey;

        private static string _secretKey;

        public static void Initialization(string APIKey, string SecretKey)
        {
            _userAPIKey = APIKey;

            _secretKey = SecretKey;
        }

        public static bool SendMessage(string mobile, int templateId, List<UltraFastParameters> parameters)
        {
            var token = new Token().GetToken(_userAPIKey, _secretKey);

            var ultraFastSend = new UltraFastSend();

            if (parameters is not null)
                ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt32(mobile),
                    TemplateId = templateId,
                    ParameterArray = parameters.ToArray()
                };
            else
                ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt32(mobile),
                    TemplateId = templateId,
                    ParameterArray = parameters.ToArray()
                };


            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
    }
}
