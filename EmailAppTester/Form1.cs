using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Mail;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
// using NUnit.Framework;

	
namespace EmailAppTester
{
    
    public partial class Form1 : Form
    {
        static StringCollection emailList = new StringCollection(); // will contain a list of email names
        static StringBuilder messageBody = new StringBuilder(); // will contain the message body read from a file
        static StreamWriter sError; // will contain the stream for error-log
        static string emailDocumentPath;
        static int sentCount = 0; // will contain how many records sent without error
        static SmtpClient myServer; // will hold the fully authorized server reference that we try to send messages too
        static MailMessage myMessage; // will hold the message sent to all recipients
        // Get the application configuration file.
        //System.Configuration.Configuration config =
        //  ConfigurationManager.OpenExeConfiguration(
        //        ConfigurationUserLevel.None);

        
        // static ProgressBar progressBar1 = new ProgressBar(); // will show status of sends on form
        static void SetupEmailCredentials(String sendFrom, String sendSubject, StringBuilder messageBody)
        {
            // string emailServerName = ConfigurationManager.AppSettings["EmailServerName"];
            string emailServerName = Properties.Settings.Default.EmailServerName;
            // string emailCredentialAcct = ConfigurationSettings.AppSettings["EmailCredentialsAcct"];
            string emailCredentialAcct = Properties.Settings.Default.EmailCredentialsAcct;
            // string emailCredentialPassword = ConfigurationSettings.AppSettings["EmailCredentialsPassword"];
            string emailCredentialPassword = Properties.Settings.Default.EmailCredentialsPassword;
            // string that holds the whole message, including the logo
            string messageContent = messageBody.ToString();
                
            // setup an email client, and fill in the credentials -- these two actions really should be in try.. catch blocks in case unsuccessful
            myServer = new SmtpClient(emailServerName);
            myServer.Credentials = new System.Net.NetworkCredential( emailCredentialAcct, emailCredentialPassword );

            // there are two possibilities for a message to be sent:
            // 1. Without an attachment file or 
            // 2. WITH an attachment file
            //  in either case, the message only needs to be created ONCE

            // check for over-loaded call WITHOUT setting sendTo property
            // myMessage = new MailMessage(sendFrom, sendTo, sendSubject, messageBody.ToString());
            // instantiate our message, fill in the from:, subject, and body. to: address will be filled in later.
            MailAddress from = new MailAddress(sendFrom);
            myMessage.From = from;
            myMessage.Subject = sendSubject;
            // myMessage.Body = messageBody.ToString();
            // myMessage.IsBodyHtml = true;
            messageContent = messageBody.ToString();

            //first we create the Plain Text part
            string plainText;

            // strip HTML from marked up text for plainView
            plainText = Regex.Replace(messageContent, "<.*?>", string.Empty);
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

            //then we create the Html part
            //to embed images, we need to use the prefix 'cid' in the img src value
            //the cid value will map to the Content-Id of a Linked resource.
            //thus <img src='cid:orgLogo'> will map to a LinkedResource with a ContentId of 'orgLogo'
            messageContent = "<img src=cid:orgLogo align=center>" + messageBody.ToString();
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(messageContent, null, "text/html");

            //create the LinkedResource (embedded image)
            string logoPath = Properties.Settings.Default.EmailLogoPath;
            if (!String.IsNullOrEmpty(logoPath))
            {
                LinkedResource logo = new LinkedResource(logoPath);
                logo.ContentId = "orgLogo";
                //add the LinkedResource to the appropriate view
                htmlView.LinkedResources.Add(logo);
  
            } // endif non-null logoPath
                
            
            //add the views
            myMessage.AlternateViews.Add(plainView);
            myMessage.AlternateViews.Add(htmlView);

            // all the code above this line need only be executed ONCE -- unless an error occurs which causes us to re-authenticate
        } // end SetupEmailCredentials
            
        static void SendSimpleMessage( String sendTo )
        // send a simple message via SMTP
        {

            sentCount++; // if successful, mark up the count sent

            // fill in the to: field for each address in our list
            MailAddress toAddr = new MailAddress(sendTo);
            myMessage.To.Add(toAddr);
   
            try
                {
                // try to send the messaage (should go into a try/catch block)
                myServer.Send(myMessage);
                } // end try


            catch (ArgumentNullException xcp)
            {
                sentCount--;
                sError.WriteLine(xcp.Message); // see if this will send something intelligent to error-log
                sError.WriteLine("To: or From: or message is NULL");
            } // end catch to: or from: or message is NULL
            catch (ArgumentOutOfRangeException xcp)
            {
                sentCount--;
                sError.WriteLine(xcp.Message); // see if this will send something intelligent to error-log
                sError.WriteLine("No recipients in to: or cc: or bcc:");
            } // end catch no recipients in to:, cc:, or bcc:
            catch (InvalidOperationException xcp)
            {
                sentCount--;
                sError.WriteLine(xcp.Message); // see if this will send something intelligent to error-log
                sError.WriteLine("Host is NULL or port is zero-value");
            } // end catch Host is NULL or port is zero

            catch (SmtpFailedRecipientsException xfr)
            {
                sentCount--; // any exception is not sent

                // SmtpStatusCode status = xcp.InnerException[i].StatusCode;
                SmtpStatusCode statusXFR = xfr.StatusCode;
                if (statusXFR == SmtpStatusCode.MailboxBusy ||
                    statusXFR == SmtpStatusCode.MailboxUnavailable)
                {
                    sError.WriteLine("Delivery failed to {0}- retrying in 5 seconds.", sendTo);
                    System.Threading.Thread.Sleep(5000);
                    myServer.Send(myMessage);
                }
                else
                {
                    sError.WriteLine("Failed to deliver message to {0}", sendTo);

                }

                sError.WriteLine(xfr.Message); // see if this will send something intelligent to error-log

                sError.WriteLine("Failed to deliver message to {0}, FailedRecipient exception.", sendTo);
            } // end catch on failed recipient
            
            catch (SmtpException xcp)
            {
                SmtpStatusCode status = xcp.StatusCode;
                sentCount--;

                sError.WriteLine("Service send error at {0}, at {1} messages.", sendTo, (sentCount + 1)); // see if this will send something intelligent to error-log
                sError.WriteLine("Error-message: {0}, StatusCode = {1}", xcp.Message, status); 
                sError.WriteLine("Inner Error = {0}", xcp.InnerException);

                switch (status)
                {
                    case SmtpStatusCode.ClientNotPermitted:
                        sError.WriteLine("Host SMTP server (sender) not authenticated");
                        break;
                    case SmtpStatusCode.LocalErrorInProcessing:
                        sError.WriteLine("Either failed reverse-lookup or sender identified as spam for {0}.", sendTo);
                        break;
                    case SmtpStatusCode.MailboxBusy:
                        sError.WriteLine("Receiving mailbox busy for {0}.", sendTo);
                        break;
                    case SmtpStatusCode.MailboxNameNotAllowed:
                        sError.WriteLine("Badly formed email address for {0}.", sendTo);
                        break;
                    case SmtpStatusCode.MailboxUnavailable:
                        sError.WriteLine("Mailbox not found or could not be accessed for {0}.", sendTo);
                        
                        break; // case MailboxUnavailable
                    case SmtpStatusCode.ServiceNotAvailable:
                        sError.WriteLine("Smtp connection failed at {0} recipient", sendTo);
                        break;
                    case SmtpStatusCode.GeneralFailure:
                        sError.WriteLine("Connection did not occur, Host not found at {0} recipient.", sendTo);
                        break;
                    case SmtpStatusCode.TransactionFailed:
                        sError.WriteLine("Connection timed out to: {0}.", sendTo);
                        break;
                    case SmtpStatusCode.ExceededStorageAllocation:
                        sError.WriteLine("Mailbox of {0} is full.", sendTo);
                        break;
                    default:
                        sError.WriteLine("Something else failed for {0}.", sendTo);
                        break;
                } // end switch on status 
            } // end catch Connection to SMTP Server failed, or Authentication failed, or Send timed out

            
                    
            // now take out the old to: address from this message so that we don't just keep adding to it indefinitely
            myMessage.To.Clear();
        } // end SendSimpleMessage

        
        private void ReadFiles()
        {
            String eMessage;

            // Displays an OpenFileDialog so the user can select an email addresses file.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select an email address file";
            // string emailListPath = ConfigurationSettings.AppSettings["EmailListPath"];
            string emailListPath = Properties.Settings.Default.EmailListPath;
            openFileDialog1.InitialDirectory = emailListPath;

            // Show the Dialog for the addresses file read
            // If the user clicked OK in the dialog and
            // a .TXT file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // load the file
                try
                {
                    // Create an instance of StreamReader to read addresses from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        String line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            // add the line to our string collection
                            if (String.Compare( line, "Email") != 0)
                                emailList.Add(line);
                        } // end while not at end of file
                    } // end using on file open
                } // end try on file open
                catch (Exception e)
                {
                    // Let the user know what went wrong.
                    eMessage = String.Concat( "The addresses file could not be read:" , e.Message );
                    MessageBox.Show( eMessage );
                } // end catch on file open exceptions

            } // end if open OK on addresses file
            
 
            // Displays an OpenFileDialog so the user can select an email body file.
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Any Files|*.*";
            openFileDialog2.Title = "Select a file containing body of text to send";
            // emailDocumentPath = ConfigurationSettings.AppSettings["EmailDocumentPath"];
            emailDocumentPath = Properties.Settings.Default.EmailDocumentPath;
            openFileDialog2.InitialDirectory = emailDocumentPath;


            // Show the Dialog for the email body file read
            // If the user clicked OK in the dialog and
            // a file was selected, open it.
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                // load the file
                try
                {
                    string contentFileName = openFileDialog2.FileName;

                    //display the filename (and MIME-type) of the file we're sending
                    displayFileNameSent(contentFileName);

                    // Create an instance of StreamReader to read addresses from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(contentFileName))
                    {
                        String line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            // concatenate the line into a single massive string
                            // String.Concat( line, NewLine"\r\n" ); // preserve CRLF sequence
                            messageBody.AppendLine(line);
                        } // end while not at end of file
                    } // end using on file open
                } // end try on file open
                catch (Exception e)
                {
                    // Let the user know what went wrong.
                    eMessage = String.Concat("The message body file could not be read:", e.Message);
                    MessageBox.Show(eMessage);
                } // end catch on file open exceptions

                // finally, open an error-log in the same directory as the message-body file
                // Create an instance of StreamWriter to write error-log to.
                try
                {
                   sError = new StreamWriter("errorLog.txt", true, Encoding.ASCII); // open stream in append-mode, ASCII encoded
                } // end try on write-file open
                catch (Exception e)
                {
                    // Let the user know what went wrong.
                    eMessage = String.Concat("The error-log file could not be opened:", e.Message);
                    MessageBox.Show(eMessage);
                } // end catch on file open exceptions

                // OK, if we got this far, then all files are opened. Attampt to get attchement file names
                hasAttachments.Visible = false;

                // also, show the to: and from: fields now
                // make the background transparent on the to:, from:, and subject fields
                toLabel.BackColor = Color.Transparent;
                toLabel.Show();
                toValue.Text = openFileDialog1.FileName;
                toValue.Show();

                fromLabel.BackColor = Color.Transparent;
                fromLabel.Show();
                fromValue.Text = openFileDialog2.FileName;
                fromValue.Show();

                // and refresh the subject line since we've just rendered a few file-open dialogs over the top of it
                subLabel.BackColor = Color.Transparent;
                subLabel.Show();
                displayedSubject.Show();

                // and refresh file to be sent
                fileSentLabel.BackColor = Color.Transparent;
                fileSentLabel.Show();                               
            } // end if open OK
        } // end ReadFiles

        private void SendMessages( String messageFrom, String messageSubject )
        {
            String progress;

            // get server credentials, authorize server, create message instance, only need 'to:' field and optional attachments
            SetupEmailCredentials(messageFrom, messageSubject, messageBody);

            // for each string -- send it out
            foreach (String messageTo in emailList)
            {
                if (messageTo != "")
                {

                    progress = String.Format("Sending Message {0} of {1}", sentCount, this.progressBar1.Maximum);
                    this.progressLabel.Text = progress;
                    this.progressLabel.Update();
                    this.progressBar1.Value++;
                    this.progressBar1.Update();

                    SendSimpleMessage(messageTo);
                
                } // endif non-null to: address from list
                else
                {
                    sError.WriteLine("NULL address in list, position {0}", sentCount);
                } // endelse bad address from list
                
                //progress = String.
            } // end foreach message
        } // end sendMessages
        
        public Form1()
        {
            InitializeComponent();
        } // end Form1 initializer

        private void Form1_Load(object sender, EventArgs e)
        {
            // turn off the progress bar for now
            progressBar1.Hide();
        } // end Form1_Load

        
        private void button1_Click(object sender, EventArgs e)
        {
            // hide the button
            StartButton.Hide();

            // make Subject-entry visible
            subLabel.Show();
            textBox1.Show();
            subjectLabel.Show();
            textBox1.Focus(); // give focus to data entry text box for subject of email

            // before we start this whole process, we need to instantiate our email message
            myMessage = new MailMessage();
            
        } // end button1_Click event handler

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String subject;
             
            // check for CR before proceeding
            if (textBox1.Text.IndexOfAny(new char[] { '\r' }) == -1)
                return;

            // turn off the subject line entry prompt
            // textBox1.Hide();
            subjectLabel.Hide();

            // read in the file of email addresses and the message body
            ReadFiles();
            progressBar1.Maximum = emailList.Count; // scale from 0 to number of addresses to send

            //hasAttachments.Hide();
            // Allow user the opportunity to attach files, or begin an immediate send
            hasAttachments.Show();
            sendButton.Show();

            textBox1.Hide(); // done with data entry, just display it

            if (textBox1.TextLength < 1)
            {
                subject = "Default Subject";
                MessageBox.Show("Default Subject used");
            } // endif nothing entered for subject
            else
            {
                // take out any illegal characters in the subject line
                subject = Regex.Replace(textBox1.Text, @"[^\w\ .!@-]", "");
                //textBox1.Text = subject; // show what we're really sending
            } // endelse more than one char in text-box

            displayedSubject.Text = subject;
            displayedSubject.Show();

           
        } // end Text Box changed event

        private void hasAttachments_CheckedChanged(object sender, EventArgs e)
        {
            addAttachmentButton.Show();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            // Start the send when we have some input here
            DateTime dateNow;
            string dtString;
            String subject;
           
            // OK, we're starting our send, so hide all the other user controls\
            hasAttachments.Hide();
            addAttachmentButton.Hide();
            sendButton.Hide();

            // turn on the progress bar now
            progressBar1.Show();
            progressLabel.Show();

            // if there attached files, make sure these still show during send
            if (hasAttachments.Checked)
                filesAttachedListview.Refresh();

            // mark error-log with start time
            dateNow = DateTime.Now;
            dtString = dateNow.ToString();
            sError.WriteLine("{0} Began send to {1} addresses.", dtString, emailList.Count); // count of messages to send

            // our subject line should have the displayable text in it
            subject = displayedSubject.Text;

            // send the messages out to each 'line' in input-file
            // string emailSendFromAcct = ConfigurationSettings.AppSettings["EmailSendFromAcct"];
            string emailSendFromAcct = Properties.Settings.Default.EmailSendFromAcct;
            SendMessages(emailSendFromAcct, subject); // from, subject
            
            // mark error-log with completion time
            dateNow = DateTime.Now;
            dtString = dateNow.ToString();
            sError.WriteLine("{0} Send Complete, {1} emails sent.", dtString, sentCount);

            // turn off the progress bar
            progressBar1.Hide();

            // Give user some feedback on send
            MessageBox.Show("All Email sent!");

            sError.Close(); // shut down error-log

            // time to get outa Dodge!
            Application.Exit();
        }

        private void displayFileNameSent(string fileName)
            // this function takes the file-name to be sent (fileName), and extracts the icon associated with the file-type of this file name, and then attaches the icon-image
            // to the label's imageList. The label text gets the the fileName string.
        {
            ImageList imageList1;
            imageList1 = new ImageList();
            displayedSentFile.ImageList = imageList1;

            // go see if you can extact the icon image
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            Icon iconForFile = SystemIcons.WinLogo;

            iconForFile = Icon.ExtractAssociatedIcon(fileInfo.FullName);

            // Check to see if the image collection contains an image
            // for this extension, using the extension as a key.
            if (!imageList1.Images.ContainsKey(fileInfo.Extension))
            {
                // If not, add the image to the image list.
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(fileInfo.FullName);
                imageList1.Images.Add(fileInfo.Extension, iconForFile);
            }
            //put the image extracted into the label
            displayedSentFile.ImageKey = fileInfo.Extension;
            displayedSentFile.ImageList = imageList1;

            // now put the file-name (in text) into the label
            displayedSentFile.Text = "        " + fileName; // extra spaces allow for display of file icon image above

            // and display it
            fileSentLabel.Show();
            displayedSentFile.Show();
        }// end displayFileNameSent

        private void addAttachmentButton_Click(object sender, EventArgs e)
        {
            // string attachFile;
            string eMessage;
            string attachedFileName;
            ImageList imageList1;
            imageList1 = new ImageList();
            filesAttachedListview.SmallImageList = imageList1;

            // Displays an OpenFileDialog so the user can select an email attachment file.
            OpenFileDialog openFileDialogA = new OpenFileDialog();
            openFileDialogA.Filter = "Any Files|*.*";
            openFileDialogA.Title = "Select a file containing an attachment you wish to add to this message";
            openFileDialogA.InitialDirectory = emailDocumentPath;

            if (openFileDialogA.ShowDialog() != DialogResult.OK)
            {
                // Let the user know what went wrong.
                eMessage = String.Concat("Bad Attachment file selected:", openFileDialogA.FileName);
                MessageBox.Show(eMessage);
            } // endif bad attachment file selected
            else
            {
                // if we got a good file-name, then add it to the attachments collection for our email message
                attachedFileName = openFileDialogA.FileName;
                Attachment fileAttach = new Attachment(attachedFileName);

                // and add it to the list-box display of files attached
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(attachedFileName);

                ListViewItem item;
                filesAttachedListview.BeginUpdate();


                // this.filesAttachedListview.Items.Add(openFileDialogA.FileName);
                // this.filesAttachedListview.Show();
                // Set a default icon for the file.
                Icon iconForFile = SystemIcons.WinLogo;

                item = new ListViewItem(fileInfo.Name, 1);
                iconForFile = Icon.ExtractAssociatedIcon(fileInfo.FullName);

                // Check to see if the image collection contains an image
                // for this extension, using the extension as a key.
                if (!imageList1.Images.ContainsKey(fileInfo.Extension))
                {
                    // If not, add the image to the image list.
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(fileInfo.FullName);
                    imageList1.Images.Add(fileInfo.Extension, iconForFile);
                }
                item.ImageKey = fileInfo.Extension;
                filesAttachedListview.Items.Add(item);

                // done updateing our ListView
                filesAttachedListview.EndUpdate();

                // make our attached files list visible
                filesAttachedListview.Show();


                // we need to instantiate myMessage BEFORE we attempt to add this attachment
                myMessage.Attachments.Add(fileAttach);
            } // endelse we got a good file-name to attach to our message
   
        }  // end hasAttachments_CheckedChanged
    } // end class Form1 
} // end namespace EmailAppTester

