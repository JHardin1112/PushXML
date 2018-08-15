using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PushOnBaseXMLToStaging
{
    public partial class Form1 : Form
    {
        private const string sourceLocation = @"\\192.168.2.12\e$\Archive_OnBaseXMLDestination";
        private const string copyDestination = @"\\192.168.2.12\e$\OnBaseStageXML_RepushToGP";

        public Form1()
        {
            InitializeComponent();
            txtSource.Text = sourceLocation;
            txtDestination.Text = copyDestination;
        }

        private void btnCopyFiles_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>(
                           textBox1.Text.Split(new string[] { "\r\n" },
                           StringSplitOptions.RemoveEmptyEntries));

            string singleLine = "";
            string message = "";

            for (int x = 0; x <= list.Count - 1; x++)
            {
                message += list[x] + ".xml" + Environment.NewLine;

                if (x != list.Count - 1)
                {
                    singleLine += list[x].ToString() + ".xml " + " OR ";
                }
                else
                {
                    singleLine += list[x].ToString() + ".xml";
                }
            }

            if (MessageBox.Show(@"Do you want to copy the following files from " + Environment.NewLine + Environment.NewLine
                          + sourceLocation + Environment.NewLine + Environment.NewLine + " to " + Environment.NewLine + Environment.NewLine
                          + copyDestination + ":" + Environment.NewLine + Environment.NewLine + message, "Confirm file copy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string missingFiles = "";
                int missingFilesCount = 0;
                int filesCopiedCount = 0;

                for (int x = 0; x <= list.Count - 1; x++)
                {
                    if (File.Exists(sourceLocation + @"\" + list[x] + ".xml"))
                    {
                        File.Copy(sourceLocation + @"\" + list[x] + ".xml", copyDestination + @"\" + list[x] + ".xml", true);
                        filesCopiedCount += 1;
                    }
                    else
                    {
                        missingFiles += sourceLocation + @"\" + list[x] + ".xml" + Environment.NewLine;
                        missingFilesCount += 1;
                    }
                }

                if (filesCopiedCount > 0)
                {
                    MessageBox.Show("Files copied over succesfully.", "Job Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (missingFilesCount > 0)
                {
                    MessageBox.Show("The following files were not found: " + Environment.NewLine + Environment.NewLine + missingFiles + Environment.NewLine + Environment.NewLine + "This list has been copied to your Clipboard for your convenience.", "Missing Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clipboard.SetText(missingFiles);
                }

            }
        }

    }
}
