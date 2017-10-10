using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aida2EddyOne
{
    public partial class AidaToAddyOne : Form
    {
        public AidaToAddyOne()
        {
            InitializeComponent();

            // string s = DateTime.Now.ToString("yy");
            
            //     for (int i = 1; i < 7; SG.Items.Add("SG" + i++)) ;
            //    H_C.Items.Add("H");
            //    H_C.Items.Add("C");
        }
        private DateTime s;
        private string[] files;
        private string[] SG_dirs;
        private string[] LOT_dirs;
        private string CalGroupEddyOne;
        private string  DirControl;
        private int index ;
        private void SELECTAIDAFILE_Click(object sender, EventArgs e)/// Select Aida Files
        { }

        private void SETCALEDDYONE_Click(object sender, EventArgs e)
        {
       //     if (SelectDirAidafiles.SelectedPath.Length == 0)
       //     {
       //         MessageBox.Show("Please, select Aida Dep files");
       //         return;
       //     }                      
        //    CalGroupEddyOne = SG.Text + H_C.Text + "CAL";
       //     for (int i = 3; i > 0; i--)
     //       {
      //          CalGroupEddyOne += SelectDirAidafiles.SelectedPath[SelectDirAidafiles.SelectedPath.Length - i];
       //     }


            if (SelectDirAidafiles.ShowDialog() == DialogResult.OK)
            {
               SETCALEDDYONE.Text = SelectDirAidafiles.SelectedPath ;
            }
        }

        private void ConvertCalGroup_Click(object sender, EventArgs e)
        {
            if (SelectDirAidafiles.SelectedPath.Length == 0)
            {
                MessageBox.Show("Please, select Aida Dep files");
                return;
            }
            if (SETCALEDDYONE.Text == "Set Path CalGroup")
            {
                MessageBox.Show("Please, select Cal group for EddyOne");
                return;
            }
            Directory.CreateDirectory(SETCALEDDYONE.Text);
             FileInfo fn = new FileInfo(Application.StartupPath + "\\summary.aaa");

            fn.CopyTo(SETCALEDDYONE.Text + "\\summary.aaa", true);
            listEddyOneFile.Clear();
            int index = 1;
            
            foreach (string infile in files)
            {
                string onlyfilename = Path.GetFileName(infile);

                string row = onlyfilename.Substring(1, 3);
                string col = onlyfilename.Substring(5, 3);

                string section = "";
                if (index < 10) section = "00" + index.ToString();
                if ((index >= 10) && (index < 100)) section = "0" + index.ToString();
                if (index >= 100) section = index.ToString();
                index++;
                string outfile;
           //     if (H_C.Text == "H")
       //         {
         //           outfile = SETCALEDDYONE.Text + "\\DHotR" + row + "C" + col + "I" + section + ".aaa";
           //     }
          //      else
             //   {
                    outfile = SETCALEDDYONE.Text + "\\DColR" + row + "C" + col + "I" + section + ".aaa";
               // }

                Make_Header_block(Application.StartupPath + "\\Headrblok.tmp");
                Make_tube_information(Application.StartupPath + "\\tube_information.tmp", infile, row, col, section, section);
                Make_tube_channel_description(Application.StartupPath + "\\280.tmp", "0", "F1:280", "F1", "280", "1");
                Make_tube_channel_description(Application.StartupPath + "\\130.tmp", "1", "F2:130", "F2", "130", "1");
                Make_tube_channel_description(Application.StartupPath + "\\60.tmp", "2", "F3:60", "F3", "60", "1");
                Make_tube_channel_description(Application.StartupPath + "\\60a.tmp", "3", "F4:60a", "F4", "60", "2");
                Sum_files(Application.StartupPath + "\\Headrblok.tmp", outfile);
                Sum_files(Application.StartupPath + "\\tube_information.tmp", outfile);
                Sum_files(Application.StartupPath + "\\280.tmp", outfile);
                Sum_files(Application.StartupPath + "\\130.tmp", outfile);
                Sum_files(Application.StartupPath + "\\60.tmp", outfile);
                Sum_files(Application.StartupPath + "\\60a.tmp", outfile);
                SaveData(infile, outfile);
                progress_Bar.Increment(1);
                listEddyOneFile.Items.Add(outfile);

                File.Delete(Application.StartupPath + "\\Headrblok.tmp");
                File.Delete(Application.StartupPath + "\\tube_information.tmp");
                File.Delete(Application.StartupPath + "\\280.tmp");
                File.Delete(Application.StartupPath + "\\130.tmp");
                File.Delete(Application.StartupPath + "\\60.tmp");
                File.Delete(Application.StartupPath + "\\60a.tmp");
            }
        }
        private void Make_Header_block(string filename) /// <summary>
                                                        /// Make tmp file with Header block
                                                        /// </summary>
                                                        /// <param name="filename"></param>
        {
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Default))
            {
                sw.Write("<?xml version=\"1.0\"?>"); sw.Write('\x0D'); sw.Write('\x0A');
                sw.Write("<Root Type=\"Inetec.Utility.Persistency.InetecFileHeader\">"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<headerVersion>1</headerVersion>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<fileType>EddyOneTubeRawData</fileType>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<createdByApplication>EddyOne Analysis 2013</createdByApplication>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<versionMajor>2</versionMajor>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<versionMinor>2</versionMinor>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<dalAssemblyVersion>1.2.1.0</dalAssemblyVersion>"); sw.Write('\x0D'); sw.Write('\x0A');
                sw.Write("</Root>");
            }

        }
        private void Make_tube_information(string filename, string Aida_inputFile, string row, string col, string Sec, string Id) /// <summary>
                                                                                                           /// Make tmp file with First content block - recording tube information
                                                                                                           /// </summary>
                                                                                                           /// <param name="filename"></param>
        {
            FileStream inDatafile = new FileStream(Aida_inputFile, FileMode.Open, FileAccess.Read);
            inDatafile.Seek(80, 0);
            byte[] Control_name = new byte[10];
            inDatafile.Read(Control_name, 0, 10);
            string Cont = "";
            char[] st = new char[10];
            for (int i = 0; i < 10; ++i)
            {
                st[i] = Convert.ToChar(Control_name[i]);
                //       Cont += Convert.ToString(st[i]);
                if (Control_name[i] != 0) Cont += Convert.ToString(st[i]);
                else Cont += "-";
            }
            inDatafile.Close();
            inDatafile.Dispose();
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Default))
            {
              //  sw.Write("<?xml version=\"1.0\"?>"); sw.Write('\x0D'); sw.Write('\x14');
                sw.Write("<?xml version=\"1.0\"?>"); sw.Write('\x0D'); sw.Write('\x0A');
              //  sw.Write("<?xml version=\"1.0\"?>"); sw.Write('\x17'); sw.Write('\x0A');
                sw.Write("<Root Type=\"DataLibrary.BE.TubeRawData\">"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<TubeId>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Row>" + row + "</Row>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Column>" + col + "</Column>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Section>" + Sec + "</Section>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<ObjectName>");
            //    sw.Write("<ObjectName>" + Cont);
                sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</ObjectName>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Plant>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Plant>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Unit>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Unit>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</TubeId>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<TubeRecordingInfo>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<RecordingSpeed>0</RecordingSpeed>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<RecordingDirection>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<value__>2</value__>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</RecordingDirection>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<RecordedFromLeg>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<value__>1</value__>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</RecordedFromLeg>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<SyncToEncoder>False</SyncToEncoder>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<sampleRate>400000</sampleRate>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<MinValueDpComponent>-32768</MinValueDpComponent>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<MaxValueDpComponent>32767</MaxValueDpComponent>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<BitCountDpComponent>16</BitCountDpComponent>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<TubeDiameter>0</TubeDiameter>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</TubeRecordingInfo>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Info>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<NumRawChans>4</NumRawChans>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<NumEncoders>0</NumEncoders>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<AcquireTime>636103189004516907</AcquireTime>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Comment>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Comment>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<NumberOfProbes>1</NumberOfProbes>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Info>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Id>" + Id + "</Id>"); sw.Write('\x0D'); sw.Write('\x0A');
                sw.Write("</Root>");
            }


        }

        private void Make_tube_channel_description(string filename, string Id, string Name, string ShortName, string Frequency, string Type_dif_abs)
        /// <summary>
        /// Make tmp file with Second content block - channel description
        /// </summary>
        /// <param name="filename"></param>
        {
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Default))
            {
                sw.Write("<?xml version=\"1.0\"?>"); sw.Write('\x0D'); sw.Write('\x0A');
                sw.Write("<Root Type=\"DataLibrary.BE.ChannelData\">"); sw.Write('\x0D'); sw.Write('\x0A');
                sw.Write("<ChannelDescriptor>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Id>" + Id + "</Id>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Name>" + Name + "</Name>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<ShortName>" + ShortName + "</ShortName>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Frequency>" + Frequency + "</Frequency>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Type>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<value__>" + Type_dif_abs + "</value__>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Type>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Comment>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Comment>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<FrequencyUnit>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<value__>0</value__>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</FrequencyUnit>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</ChannelDescriptor>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<ChannelSetupRaw>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Rotation>0</Rotation>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<HorzNull>0</HorzNull>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<VertNull>0</VertNull>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<Span>200</Span>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("<VoltScale>1</VoltScale>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</ChannelSetupRaw>"); sw.Write('\x0D'); sw.Write('\x0A'); sw.Write('\x20'); sw.Write('\x20');
                sw.Write("</Root>");
            }
        }
        public static int MakeWord(byte b, byte a) /// create int from 2 bytes,  little endian format
        {
            //int tmpk = (int)high << 25;
            //tmpk = tmpk >> 25;

            ////    return ((int)high << 8) | low;
            //return (tmpk << 8) | low;
            //uint i_low = (uint)low;
            //uint i_high = (uint)high;
            //uint result = 256 * i_high + i_low;
            //  return result;
            int a1 = (int)a;// & 127;
            int b1 = (int) b;// & 127;
            // int res = ((int)(((byte)(a)) | ((int)((byte)(b))) << 8));
            int res = ((int)(((a1)) | ((int)((b1))) << 8));
            int res2 = res; //& 16383;
            return res2;
        }

        private void SaveData(string inputFile, string outputfile) /// converter binary formats data
        {
            FileStream outDatafile = new FileStream(outputfile, FileMode.Append);
            BinaryWriter outbinfile = new BinaryWriter(outDatafile);
            /*   outbinfile.Seek(460, 0);
               outbinfile.Write(row);
               outbinfile.Seek(483, 0);
               outbinfile.Write(col);
               outbinfile.Seek(510, 0);
               outbinfile.Write(section);
               outbinfile.Seek((int)outDatafile.Length, 0);
               */

            FileStream inDatafile = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
            inDatafile.Seek(80, 0);
            byte[] Control_name = new byte[10];
            inDatafile.Read(Control_name, 0, 10);
            string Cont="";
            char[] st = new char[10];
            for (int i=0; i < 10; ++i)
            {
                         st[i] = Convert.ToChar(Control_name[i]);
                //       Cont += Convert.ToString(st[i]);
                if (Control_name[i] != 0) Cont += Convert.ToString(st[i]);
                else Cont += "-";
            }


            const int bufferSize = 16;
            int count;
            long fLen = inDatafile.Length;
            uint numPoints = (uint)(fLen - 1024) * 2;
            int zero = 0;
            inDatafile.Seek(1024, 0);
            using (BinaryReader inbinfile = new BinaryReader(inDatafile))
            {
                byte[] buffer = new byte[bufferSize];

                outbinfile.Write(numPoints);
                outbinfile.Write(zero);
                numPoints = numPoints / 32;
                outbinfile.Write(numPoints);
                int x280, x130, x60, x60a;
                int y280, y130, y60, y60a;
                while ((count = inbinfile.Read(buffer, 0, buffer.Length)) != 0)
                {
                    x280 = MakeWord(buffer[0], buffer[1]);
                    y280 = MakeWord(buffer[2], buffer[3]);

                    x130 = MakeWord(buffer[4], buffer[5]);
                    y130 = MakeWord(buffer[6], buffer[7]);


                    x60 = MakeWord(buffer[8], buffer[9]);
                    y60 = MakeWord(buffer[10], buffer[11]);

                    x60a = MakeWord(buffer[12], buffer[13]);
                    y60a = MakeWord(buffer[14], buffer[15]);

                    outbinfile.Write(x280);
                    outbinfile.Write(x130);
                    outbinfile.Write(x60);
                    outbinfile.Write(x60a);
                    outbinfile.Write(y280);
                    outbinfile.Write(y130);
                    outbinfile.Write(y60);
                    outbinfile.Write(y60a);
                }
            }
            outbinfile.Close();
            outDatafile.Dispose();
            inDatafile.Dispose();
        }

        private void Sum_files(string sourcefn, string destinfn) /// <summary>
                                                                 /// function for compare files
                                                                 /// </summary>
                                                                 /// <param name="sourcefn"></param>
                                                                 /// <param name="destinfn"></param>
        {
            FileStream outDatafile = new FileStream(destinfn, FileMode.Append);
            BinaryWriter outbinfile = new BinaryWriter(outDatafile);
            FileStream inDatafile = new FileStream(sourcefn, FileMode.Open, FileAccess.Read);
            int fLen = (int)inDatafile.Length;
            int zero = 0;
            int count;
            using (BinaryReader inbinfile = new BinaryReader(inDatafile))
            {
                outbinfile.Write(fLen);
                outbinfile.Write(zero);

                byte[] buffer = new byte[fLen];

                while ((count = inbinfile.Read(buffer, 0, buffer.Length)) != 0)
                {
                    outbinfile.Write(buffer, 0, count);
                }
            }
            outbinfile.Close();
            outDatafile.Dispose();
            inDatafile.Dispose();

        }

        private void AidaToAddyOne_Load(object sender, EventArgs e)
        {

        }

        private void SELECTAIDAFILE_Click_1(object sender, EventArgs e)
        {

        }

        private void SELECTAIDAFILE_Click_2(object sender, EventArgs e)
        {
            //s = DateTime.Now;
            //string tmpDir;
            //if (s.Year > 2017)
            //{

            //     tmpDir = Path.GetTempPath();
            //    FileStream outDatafile = new FileStream(tmpDir + "\\~ckeckfile.txt", FileMode.Create);
            //    outDatafile.Close();
            //    Application.Exit();
            //}
            //    tmpDir = Path.GetTempPath();
            //if ( File.Exists(tmpDir + "\\~ckeckfile.txt")) Application.Exit();

            ListDEP_File.Clear();
                if (SelectDirAidafiles.ShowDialog() == DialogResult.OK)
                {
                /// Создаем директорию контроля
                /// 
                string [] tmplistAllfile = Directory.GetFiles(SelectDirAidafiles.SelectedPath, "*.DEP", SearchOption.AllDirectories);
                progress_Bar.Maximum = tmplistAllfile.Length;
                string AIDAControl = SelectDirAidafiles.SelectedPath;
                SELECTAIDAFILE.Text = AIDAControl;
                this.Refresh();
                index = 1;
                FindDirConvertFile( AIDAControl, "");


                //string tmpPath  = Path.GetDirectoryName(AIDAControl);

                //DirControl = AIDAControl.Substring(tmpPath.Length, AIDAControl.Length - tmpPath.Length);
                //Directory.CreateDirectory(SETCALEDDYONE.Text + DirControl); 

                //    SG_dirs = Directory.GetDirectories(SelectDirAidafiles.SelectedPath);
                //    foreach (string SG_dir in SG_dirs)
                //    {
                //    string tmpPathSG = Path.GetDirectoryName(SG_dir);
                //    string SG_name = SG_dir.Substring(tmpPathSG.Length, SG_dir.Length - tmpPathSG.Length);
                //    Directory.CreateDirectory(SETCALEDDYONE.Text + DirControl + SG_name);

                //    LOT_dirs = Directory.GetDirectories(SG_dir);
                //        foreach (string LOT in LOT_dirs)
                //        {
                //        string tmpPathLOT = Path.GetDirectoryName(LOT);
                //        string LOT_name = LOT.Substring(tmpPathLOT.Length, LOT.Length - tmpPathLOT.Length);
                //        Directory.CreateDirectory(SETCALEDDYONE.Text + DirControl + SG_name + LOT_name);
                //        FileInfo fn = new FileInfo(Application.StartupPath + "\\summary.aaa");

                //        fn.CopyTo(SETCALEDDYONE.Text + DirControl + SG_name + LOT_name + "\\summary.aaa", true);
                        
                //        files = Directory.GetFiles(LOT, "*.DEP", SearchOption.AllDirectories);
                        //  progress_Bar.Maximum = files.Length;
                        
                            //foreach (string file in files)
                            //{
                            //    ListDEP_File.Items.Add(file);
                            //    ConvertFileAida2EddyOne(file, SETCALEDDYONE.Text + DirControl + SG_name + LOT_name);
                                
                            //}

                //           this.Refresh();
                //    }
                //    }
                //progress_Bar.Increment(100);

                //   progress_Bar.
                MessageBox.Show("Завдання виконано", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }


private string GreateSGDir(string AidaSGDir, string ROOT_dir )
{
    return ROOT_dir + AidaSGDir;
}

private string GreateCalDir(string AidaLOTDir, string SGDir)
{

  //  Directory.CreateDirectory(SETCALEDDYONE.Text);
    FileInfo fn = new FileInfo(Application.StartupPath + "\\summary.aaa");

     fn.CopyTo(SETCALEDDYONE.Text + "\\summary.aaa", true);

    return SGDir + AidaLOTDir;
}

        private void FindDirConvertFile( string Dir, string pathDir)
        {

               string tmpPathDir = Path.GetDirectoryName(Dir);
                string Dir_name = Dir.Substring(tmpPathDir.Length, Dir.Length - tmpPathDir.Length);

                Directory.CreateDirectory(SETCALEDDYONE.Text + pathDir + Dir_name);

                files = Directory.GetFiles(Dir, "*.DEP", SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {
                    FileInfo fn = new FileInfo(Application.StartupPath + "\\summary.aaa");
                    fn.CopyTo(SETCALEDDYONE.Text + pathDir + Dir_name + "\\summary.aaa", true);
                    foreach (string file in files)
                    {
                        ListDEP_File.Items.Add(file);
                        ConvertFileAida2EddyOne(file, SETCALEDDYONE.Text + pathDir + Dir_name);

                    }

                }

            string[] dirs = Directory.GetDirectories(Dir);
            foreach (string subdir in dirs)
            {
                FindDirConvertFile(subdir, pathDir + Dir_name);
                this.Refresh();
                
            }

            this.Invalidate();
        }



private void  ConvertFileAida2EddyOne(string Aidafile, string CalDir)
        {
           
            
        //    listEddyOneFile.Clear();
            
        //    progress_Bar.Maximum = files.Length;
       //     foreach (string infile in files)          
            string onlyfilename = Path.GetFileName(Aidafile);

string row = onlyfilename.Substring(1, 3);
string col = onlyfilename.Substring(5, 3);

string section = "";
                if (index< 10) section = "00" + index.ToString();
                if ((index >= 10) && (index< 100)) section = "0" + index.ToString();
                if (index >= 100) section = index.ToString();
                index++;
                string outfile;

              string tmpDir = Path.GetTempPath();

            outfile = CalDir + "\\DColR" + row + "X" + col + "I" + section + ".aaa";
             
                Make_Header_block(tmpDir + "\\Headrblok.tmp");
                Make_tube_information(tmpDir + "\\tube_information.tmp", Aidafile, row, col, section, section);
                Make_tube_channel_description(tmpDir + "\\280.tmp", "0", "F1:280", "F1", "280", "1");
                Make_tube_channel_description(tmpDir + "\\130.tmp", "1", "F2:130", "F2", "130", "1");
                Make_tube_channel_description(tmpDir + "\\60.tmp", "2", "F3:60", "F3", "60", "1");
                Make_tube_channel_description(tmpDir + "\\60a.tmp", "3", "F4:60a", "F4", "60", "2");
                Sum_files(tmpDir + "\\Headrblok.tmp", outfile);
                Sum_files(tmpDir + "\\tube_information.tmp", outfile);
                Sum_files(tmpDir + "\\280.tmp", outfile);
                Sum_files(tmpDir + "\\130.tmp", outfile);
                Sum_files(tmpDir + "\\60.tmp", outfile);
                Sum_files(tmpDir + "\\60a.tmp", outfile);
                SaveData(Aidafile, outfile);
                progress_Bar.Increment(1);
                listEddyOneFile.Items.Add(outfile);
                File.Delete(tmpDir + "\\Headrblok.tmp");
                File.Delete(tmpDir + "\\tube_information.tmp");
                File.Delete(tmpDir + "\\280.tmp");
                File.Delete(tmpDir + "\\130.tmp");
                File.Delete(tmpDir + "\\60.tmp");
                File.Delete(tmpDir + "\\60a.tmp");
            
        }


    }

}