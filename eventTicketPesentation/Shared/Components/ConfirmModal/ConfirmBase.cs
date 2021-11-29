using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace eventTicketPesentation.Shared.Components
{
    public class ConfirmBase : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter] public string ConfirmationTitle { get; set; } = "Confirm Action";

        [Parameter] public string ConfirmationMessage { get; set; } = "Are you sure you want to cancel this event?";


        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter] public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}