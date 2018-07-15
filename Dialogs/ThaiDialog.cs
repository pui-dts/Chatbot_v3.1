using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;

namespace BotApplication.Dialogs
     //context.Wait(this.Translate);
{

    [Serializable]
    public class ThaiDialog : IDialog<string>
    {

        private User user = new User();

        List<string> yesNoOptions = new List<string>() { "ใช่", "ไม่" };
        List<string> yesNoOptions2 = new List<string>() { "Yes", "No" };


        public ThaiDialog(User user)
        {
            this.user = user;

        }



        public async Task StartAsync(IDialogContext context)
        {

            PromptDialog.Choice(context, this.OnUsernameSelect, yesNoOptions, "ยินดีต้อนรับสู่ Metro Systems MA Service ไม่ทราบว่าท่านมีบัญชีผู้ใช้กับเราหรือไม่?", "เลือกไม่ถูกต้อง", 3);


        }
        public async Task OnUsernameSelect(IDialogContext context, IAwaitable<String> result)
        {



            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    //case "Yes":
                    case "ใช่":

                        await context.PostAsync("โปรดป้อนชื่อผู้ใช้ของท่าน");
                        context.Wait(this.MessageUsernameAsync);
                        break;

                    //case "No":
                    case "ไม่":
                        //await context.PostAsync($"Oh, I'm sorry to hear that. You can chat to me again anytime.");
                        context.Done("ขอบคุณสำหรับการใช้บริการของเรา เราหวังว่าจะมีโอกาสให้บริการท่านอีกครั้งในอนาคต");
                        break;
                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("ขออภัยระบบของเรามีปัญหา โปรดลองใหม่อีกครั้ง"));
            }



        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var massage = await result;
        }

        private async Task MessageUsernameAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var username = await result;
            if (this.user.Username == username.Text)
            {
                await context.PostAsync("โปรดใส่รหัสผ่านของท่าน");
                context.Wait(this.MessagePasswordAsync);

            }
            else
            {
                await context.PostAsync($"ชื่อผู้ใช้ของท่านผิด กรุณาใส่อีกครั้ง");
                context.Wait(this.MessageUsernameAsync);

            }

        }
        private async Task MessagePasswordAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var password = await result;
            if (this.user.Password == password.Text)
            {

                PromptDialog.Choice(context, this.OnIncidentsSelect, yesNoOptions, $"ขณะนี้ท่านมี Incidents เหลืออยู่  { this.user.Incidents } Incidents" + Environment.NewLine + $" หมดอายุ: { this.user.Expire }" + Environment.NewLine + $" ไม่ทราบว่าท่านต้องการเปิด Incident Ticket หรือไม่?", "เลือกไม่ถูกต้อง", 3);

            }
            else
            {
                await context.PostAsync($"พาสเวิร์ดของท่านผิด กรุณาใส่อีกครั้ง");
                context.Wait(this.MessagePasswordAsync);

            }

        }

        public async Task OnIncidentsSelect(IDialogContext context, IAwaitable<String> result)
        {

            List<string> ProductOptions = new List<string>() { this.user.Product1, this.user.Product2, this.user.Product3 };


            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    //case "Yes":
                    case "ใช่":
                        PromptDialog.Choice(context, this.OnProductSelect, ProductOptions, $"โปรดเลือกผลิตภัณฑ์ที่เกี่ยวข้อง", "ไม่มีสินค้าที่คุณเลือก", 3);
                        break;

                    //case "No":
                    case "ไม่":
                        //await context.PostAsync($"Oh, I'm sorry to hear that. You can chat to me again anytime.");
                        context.Done("ขอบคุณสำหรับการใช้บริการของเรา เราหวังว่าจะมีโอกาสให้บริการท่านอีกครั้งในอนาคต");
                        break;
                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("ขออภัยระบบของเรามีปัญหา โปรดลองใหม่อีกครั้ง"));
            }



        }

        public async Task OnProductSelect(IDialogContext context, IAwaitable<String> result)
        {

            List<string> PriorityOptions = new List<string>() { this.user.Priorityth1, this.user.Priorityth2 };


            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {

                    case "Office 365":
                        PromptDialog.Choice(context, this.OnPrioritySelect, PriorityOptions, $"กรุณาเลือกระดับความสำคัญ", "กรุณาเลือกอีกครั้ง", 3);
                        break;

                    case "Microsoft SQL Server":
                        PromptDialog.Choice(context, this.OnPrioritySelect, PriorityOptions, $"กรุณาเลือกระดับความสำคัญ", "กรุณาเลือกอีกครั้ง", 3);
                        break;

                    case "Microsoft Exchange":
                        PromptDialog.Choice(context, this.OnPrioritySelect, PriorityOptions, $"กรุณาเลือกระดับความสำคัญ", "กรุณาเลือกอีกครั้ง", 3);
                        break;

                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("ขออภัยระบบของเรามีปัญหา โปรดลองใหม่อีกครั้ง"));
            }



        }

        public async Task OnPrioritySelect(IDialogContext context, IAwaitable<String> result)
        {



            try
            {


                string optionSelected = await result;
                await context.PostAsync($"กรุณาเลือกรูปแบบการแจ้งปัญหา 1. ป้อนข้อความ (ภาษาอังกฤษเท่านั้น) 2. อัปโหลดไฟล์");
                context.Wait(this.MessageQnA);



            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("ขออภัยระบบของเรามีปัญหา โปรดลองใหม่อีกครั้ง"));
            }



        }

        private async Task MessageQnA(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var question = await result;
            dynamic ans = await GetQnAAPI.Post(question.Text);
            string ans2 = ans["answers"][0]["answer"];
            if (ans2 == "No good match found in KB.") {
                await context.PostAsync("ไม่พบคำถามของท่าน กรุณาใส่คำถามของท่านใหม่");
                context.Wait(MessageQnA);
            }
            else
            {

                PromptDialog.Choice(context, this.OnSolveSelect, yesNoOptions2, ans2, "กรุณาเลือกอีกครั้ง", 3);
            }

        }

        public async Task OnSolveSelect(IDialogContext context, IAwaitable<String> result)
        {



            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {

                    case "Yes":

                        var reply = context.MakeMessage();
                        reply.Attachments = new List<Attachment>();
                        var actions = new List<CardAction>();
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url1 }", Value = $"1" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url2 }", Value = $"2" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url3 }", Value = $"3" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url4 }", Value = $"4" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url5 }", Value = $"5" });

                        var card = new HeroCard() { Title = $"กรุณาให้คะแนนบริการของเรา (1-5)", Buttons = actions };
                        reply.Attachments.Add(card.ToAttachment());

                        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                        await context.PostAsync(reply);
                        context.Wait(this.OnRateSelect);


                        break;

                    case "No":
                        await context.PostAsync($"โปรดติดต่อ chat manager");
                        context.Done("ขอบคุณสำหรับการใช้บริการของเรา เราหวังว่าจะมีโอกาสให้บริการท่านอีกครั้งในอนาคต");
                        break;

                }



            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("ขออภัยระบบของเรามีปัญหา โปรดลองใหม่อีกครั้ง"));
            }



        }

        private async Task MessageRate(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var rate = await result;
            context.Done("ขอบคุณสำหรับการใช้บริการของเรา เราหวังว่าจะมีโอกาสให้บริการท่านอีกครั้งในอนาคต");



        }
        private async Task OnRateSelect(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var selectedCard = await result;
            if (selectedCard.Text == "1")
            {
                await context.PostAsync($" { this.user.ImageRate1Url1 }");
            }
            else if (selectedCard.Text == "2")
            {
                await context.PostAsync($" { this.user.ImageRate1Url2 }");

            }
            else if (selectedCard.Text == "3")
            {
                await context.PostAsync($" { this.user.ImageRate1Url3 }");

            }
            else if (selectedCard.Text == "4")
            {
                await context.PostAsync($" { this.user.ImageRate1Url4 }");

            }
            else if (selectedCard.Text == "5")
            {
                await context.PostAsync($" { this.user.ImageRate1Url5 }");

            }


            try
            {
                context.Done("ขอบคุณสำหรับการใช้บริการของเรา เราหวังว่าจะมีโอกาสให้บริการท่านอีกครั้งในอนาคต");




            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("ขออภัยระบบของเรามีปัญหา โปรดลองใหม่อีกครั้ง"));
            }






        }
    }
}