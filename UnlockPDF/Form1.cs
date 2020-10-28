using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnlockPDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            textBox1.Text = "";

            textBox1.Text = String.Join(Environment.NewLine, openFileDialog1.SafeFileNames);

            label2.Text = openFileDialog1.FileNames.Length + " dosya seçildi";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileNames.Length == 1)
            {

                try
                {
                    var inputFile = openFileDialog1.FileName;

                    PdfDocument maindoc = PdfReader.Open(inputFile, PdfDocumentOpenMode.Import);

                    PdfDocument OutputDoc = new PdfDocument();

                    foreach (PdfPage page in maindoc.Pages)
                    {
                        OutputDoc.AddPage(page);
                    }

                    OutputDoc.Save(inputFile.Replace(".pdf", "_unlocked.pdf"));
                    maindoc.Dispose();
                    OutputDoc.Dispose();

                    label2.Text = "Toplamda bir adet dosya şifresi çözüldü. İşlem başarılı.";

                    linkLabel1.Text = "Dosyayı Aç";

                } catch (Exception ex)
                {
                    label2.Text = ex.Message;
                }

            } else if (openFileDialog1.FileNames.Length > 1)
            {
                try
                {
                    foreach (var file in openFileDialog1.FileNames)
                    {


                        PdfDocument maindoc = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                        PdfDocument OutputDoc = new PdfDocument();

                        foreach (PdfPage page in maindoc.Pages)
                        {
                            OutputDoc.AddPage(page);
                        }

                        OutputDoc.Save(file.Replace(".pdf", "_unlocked.pdf"));
                        maindoc.Dispose();
                        OutputDoc.Dispose();


                    }

                    label2.Text = "Toplamda " + openFileDialog1.FileNames.Length + " adet dosya şifresi çözüldü. İşlem başarılı.";

                    linkLabel1.Text = "Klasörü Aç";
                }
                catch (Exception ex)
                {
                    label2.Text = ex.Message;
                }
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.FileNames.Length == 1)
            {
                System.Diagnostics.Process.Start(openFileDialog1.FileName.Replace(".pdf", "_unlocked.pdf"));
            } else if (openFileDialog1.FileNames.Length > 1)
            {

                var listDir = openFileDialog1.FileNames[0].Split('\\');

                listDir = (string[]) listDir.Take(listDir.Length - 1).ToArray();

                var strListDir = String.Join("\\", listDir);

                System.Diagnostics.Process.Start(strListDir);
            }
        }
    }
}
