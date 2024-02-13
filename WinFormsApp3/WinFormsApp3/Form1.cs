using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string endpoint = "https://ocr87ayu.cognitiveservices.azure.com/";
            string key = "3c6fd063f65c43a88f422184b5bb6c59";

            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);
            Stream fileStream = File.OpenRead(ofd.FileName);
            AnalyzeDocumentOperation operation = client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-read", fileStream);
            AnalyzeResult result = operation.Value;
            foreach(var page in result.Pages)
            {
                foreach(var line in page.Lines)
                {
                    richTextBox1.AppendText(line.Content);
                    richTextBox1.AppendText("\n");
                }
            }
        }
    }
}
