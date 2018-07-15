using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;

namespace BotApplication.Dialogs

{

    [Serializable]
    public class EnglishDialog : IDialog<string>
    {

        private User user = new User();

        List<string> yesNoOptions = new List<string>() { "Yes", "No" };

        public EnglishDialog(User user)
        {
            this.user = user;
        }

        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnUsernameSelect, yesNoOptions, this.user.MassegeWelcom, "Have no your choice", 3);


        }
        public async Task OnUsernameSelect(IDialogContext context, IAwaitable<String> result)
        {



            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    //case "Yes":
                    case "Yes":
                        await context.PostAsync(this.user.MassegeUsername);
                        context.Wait(this.MessageUsernameAsync);
                        break;

                    //case "No":
                    case "No":
                        //await context.PostAsync($"Oh, I'm sorry to hear that. You can chat to me again anytime.");

                        context.Done(this.user.MassegeThankyou);
                        break;
                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Sorry system went wrong, Please try again later."));
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
                await context.PostAsync($"{ this.user.MassegePassword  }");
                context.Wait(this.MessagePasswordAsync);

            }
            else
            {
                await context.PostAsync($"Your username is fail,Plese try again");
                context.Wait(this.MessageUsernameAsync);

            }

        }
        private async Task MessagePasswordAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var password = await result;
            if (this.user.Password == password.Text)
            {
                PromptDialog.Choice(context, this.OnIncidentsSelect, yesNoOptions, $"{ this.user.MassegeIncidents  } { this.user.Incidents } Incidents" + Environment.NewLine + $"Expired: { this.user.Expire }" + Environment.NewLine + $"{ this.user.MassegeIncidents2 }", "Have no your choice", 3);

            }
            else
            {
                await context.PostAsync($"Your password is fail,Plese try again");
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
                    case "Yes":
                        PromptDialog.Choice(context, this.OnProductSelect, ProductOptions, $"{ this.user.MassegeProduct }", "Have no your choice", 3);
                        break;

                    //case "No":
                    case "No":
                        //await context.PostAsync($"Oh, I'm sorry to hear that. You can chat to me again anytime.");

                        context.Done(this.user.MassegeThankyou);
                        break;
                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Sorry system went wrong, Please try again later."));
            }



        }

        public async Task OnProductSelect(IDialogContext context, IAwaitable<String> result)
        {

            List<string> PriorityOptions = new List<string>() { this.user.Priority1, this.user.Priority2 };


            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {

                    //case "Yes":
                    case "Office 365":
                        PromptDialog.Choice(context, this.OnPrioritySelect, PriorityOptions, $"{ this.user.MassegePriority }", "Have no your choice", 3);
                        break;

                    //case "No":
                    case "Microsoft SQL Server":
                        PromptDialog.Choice(context, this.OnPrioritySelect, PriorityOptions, $"{ this.user.MassegePriority }", "Have no your choice", 3);
                        break;
                    case "Microsoft Exchange":
                        PromptDialog.Choice(context, this.OnPrioritySelect, PriorityOptions, $"{ this.user.MassegePriority }", "Have no your choice", 3);
                        break;

                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Sorry system went wrong, Please try again later."));
            }



        }

        public async Task OnPrioritySelect(IDialogContext context, IAwaitable<String> result)
        {



            try
            {
                string optionSelected = await result;
                await context.PostAsync($"{ this.user.MassegeIssueFrom} { this.user.IssueFrom1 } { this.user.IssueFrom2 }");
                context.Wait(this.MessageQnA);



            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Sorry system went wrong, Please try again later."));
            }



        }

        private async Task MessageQnA(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var question = await result;
            dynamic ans = await GetQnAAPI.Post(question.Text);

            string ans2 = ans["answers"][0]["answer"];
            if (ans2 == "No good match found in KB.")
            {
                await context.PostAsync("No good match found for your question, Please try again.");
                context.Wait(MessageQnA);
            }
            else
            {
                PromptDialog.Choice(context, this.OnSolveSelect, yesNoOptions, ans2, "Have no your choice", 3);

            }


        }

        public async Task OnSolveSelect(IDialogContext context, IAwaitable<String> result)
        {


            try
            {
                string optionSelected = await result;


                switch (optionSelected)
                {

                    //case "Yes":
                    case "Yes":

                        var reply = context.MakeMessage();
                        reply.Attachments = new List<Attachment>();
                        var actions = new List<CardAction>();
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url1 }", Value = $"1" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url2 }", Value = $"2" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url3 }", Value = $"3" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url4 }", Value = $"4" });
                        actions.Add(new CardAction { Type = ActionTypes.ImBack, Title = $"{ this.user.ImageRate1Url5 }", Value = $"5" });

                        var card = new HeroCard() { Title = $" { this.user.MassageRating }", Buttons = actions };
                        reply.Attachments.Add(card.ToAttachment());

                        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                        await context.PostAsync(reply);
                        context.Wait(this.OnRateSelect);


                        break;

                    //case "No":
                    case "No":
                        await context.PostAsync($"Pleas contact chat manager");
                        context.Done(this.user.MassegeThankyou);
                        break;

                }



            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Sorry system went wrong, Please try again later."));
            }



        }

        private async Task MessageRate(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var rate = await result;



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
                context.Done(this.user.MassegeThankyou);




            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Sorry system went wrong, Please try again later."));
            }



        }




    }
}