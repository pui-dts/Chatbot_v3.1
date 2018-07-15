using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BotApplication.Dialogs;

namespace BotApplication.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private User user = new User();

        private string language;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
           

        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(context, this.OnlanguageSelected, new List<string>() { "ไทย", "English" }, $"Please select Language", "Have no your choice", 3);

        }

        private async Task OnlanguageSelected(IDialogContext context, IAwaitable<String> result)
        {
            this.language = await result;
            
            try
            {
                switch (language) {
                    case "ไทย":


                    context.Call(new ThaiDialog(this.user), this.ThaiDialogResumeAfter);
                        break;


                    case "English":
                        
                            context.Call(new EnglishDialog(this.user), this.EnglishDialogResumeAfter);

                        break;
                }

            }
            catch (TooManyAttemptsException ex)
            {
                context.Fail(new TooManyAttemptsException("Please try again."));
            }
        }

        private async Task ThaiDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            var end = await result;
            await context.PostAsync(end);
        }

        private async Task EnglishDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {

            var end = await result;
            await context.PostAsync(end);
        }
    }
}