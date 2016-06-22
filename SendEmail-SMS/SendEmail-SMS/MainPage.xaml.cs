using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SendEmail_SMS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContactPicker contactPicker = new ContactPicker();
            Contact contact = await contactPicker.PickContactAsync();

            // You need to add required capabilities in your App manifest file for access contact list.
            ContactStore contactStore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);

            if (contact != null)
            {
                // Get Selected Contact
                Contact realContact = await contactStore.GetContactAsync(contact.Id);

                SendEmail(realContact, "This is test email.");
            }
        }

        private async void SendEmail(Contact toRecipient, string messageBody)
        {
            var to = toRecipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
            var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(to.Address, toRecipient.FullName);
            EmailMessage objEmail = new EmailMessage();
            objEmail.Subject = "Send Email Using UWP APP";
            objEmail.Body = messageBody;
            objEmail.To.Add(emailRecipient);
            await EmailManager.ShowComposeNewEmailAsync(objEmail);
        }

        private async void MultiSelectContact_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContactPicker contactPicker = new ContactPicker();
            contactPicker.CommitButtonText = "Select";
            contactPicker.SelectionMode = ContactSelectionMode.Fields;
            contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);
            contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Email);

            IList<Contact> contacts = await contactPicker.PickContactsAsync();
            if (contacts != null && contacts.Count > 0)
            {
                foreach (Contact contact in contacts)
                {
                    System.Diagnostics.Debug.WriteLine(contact?.DisplayName + contact.Emails?[0]?.Address);
                }
            }
        }

        private async void SendSMSButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContactPicker contactPicker = new ContactPicker();
            Contact contact = await contactPicker.PickContactAsync();

            // You need to add required capabilities in your App manifest file for access contact list.
            ContactStore contactStore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);

            if (contact != null)
            {
                // Get Selected Contact
                Contact realContact = await contactStore.GetContactAsync(contact.Id);

                SendSMS(realContact, "This is test SMS.");
            }
        }

        private async void SendSMS(Contact toContatc, string message)
        {
            var chatMessage = new Windows.ApplicationModel.Chat.ChatMessage();
            chatMessage.Body = message;

            var phone = toContatc.Phones.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactPhone>();
            if (phone != null)
            {
                chatMessage.Recipients.Add(phone.Number);

                await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatMessage);
            }        }

    }
}
