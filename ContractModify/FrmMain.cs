using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;
using System.Data.OleDb;

namespace ContractModify
{
    public partial class FrmMain : Form
    {

        string filePath_origin_SH = @"D:\qsfile\zqqs\sh\";      // 上海清算文件目录
        string filePath_origin_SZ = @"D:\qsfile\zqqs\sz\";      // 深圳清算文件目录
        string filePath_modify = @"D:\qsxg\";                   // 清算修改目录

        string connectString = @"Provider=VFPOLEDB.1;Data Source=D:\qsxg;Collating Sequence=MACHINE";   // 连接串


        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            /* 逻辑
             * 1.检查d:\qsfile\zqqs\sh\目录下的jsmx01(02、03)_js326文件是否存在，是否为当天
             * 2.拷贝到d:\qsxg
             * 3.执行语句
             * 4.将文件拷回
             */

            btnExecute.Text = "运行中...";
            btnExecute.Enabled = false;

            tbLog.Text = "开始处理..." + System.Environment.NewLine;

            bwTask.RunWorkerAsync();

        }

        private void bwTask_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            DateTime dtNow = DateTime.Now;      // 当天日期

            try
            {
                int updateRows = 0;
                // ------jsmx01、02、03
                List<string> list_filePath_jsmx = new List<string>();
                list_filePath_jsmx.Add(Util.Filename_Date_Convert("jsmx01_js326.mdd"));
                list_filePath_jsmx.Add(Util.Filename_Date_Convert("jsmx02_js326.mdd"));
                list_filePath_jsmx.Add(Util.Filename_Date_Convert("jsmx03_js326.mdd"));

                foreach (string filePath_jsmx in list_filePath_jsmx)
                {
                    string filePath_origin_jsmx = string.Format("{0}{1}", filePath_origin_SH, filePath_jsmx);
                    string filePath_modify_jsmx = string.Format("{0}{1}", filePath_modify, filePath_jsmx);

                    // 文件存在判断 
                    if (!File.Exists(filePath_origin_jsmx))
                    {
                        throw new FileNotFoundException(string.Format(@"{0} 文件不存在", filePath_origin_jsmx));

                        //bw.ReportProgress(0, new ReportObj(filePath_origin_jsmx, updateRows, true));
                        // continue;
                    }


                    // 当天文件
                    FileInfo fi_jsmx = new FileInfo(filePath_origin_jsmx);
                    //if (fi_jsmx.CreationTime.Date != dtNow.Date)
                    //    throw new Exception(string.Format("{0} 不是当天的文件.", filePath_origin_jsmx));

                    // 文件拷到修改目录
                    File.Copy(filePath_origin_jsmx, filePath_modify_jsmx, true);

                    // 修改逻辑
                    using (OleDbConnection connection = new OleDbConnection(connectString))
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;

                        command.CommandText = string.Format(@"UPDATE a SET a.sqbh=padl(b.htqz,3,'0')+substr(a.sqbh,4) from {0} a , sh_branprefix b WHERE a.zqzh=b.zqzh and substr(a.sqbh,1,3)='900' and a.ywlx in ('117','118','625','626','627','628','632','633','634','635','636','637')", filePath_jsmx);

                        updateRows = command.ExecuteNonQuery();
                    }

                    File.Copy(filePath_modify_jsmx, filePath_origin_jsmx, true);   // 拷回去


                    bw.ReportProgress(0, new ReportObj(filePath_origin_jsmx, updateRows));

                }



                //------sjsjg
                string strSJSJGFileName = Util.Filename_Date_Convert("sjsjgmmdd.dbf");


                string filePath_origin_sjsjg = string.Format("{0}{1}", filePath_origin_SZ, strSJSJGFileName);
                string filePath_modify_sjsjg = string.Format("{0}{1}", filePath_modify, strSJSJGFileName);

                // 文件存在判断 
                if (!File.Exists(filePath_origin_sjsjg))
                    throw new FileNotFoundException(string.Format(@"{0} 文件不存在", filePath_origin_sjsjg));

                // 当天文件
                FileInfo fi_sjsjg = new FileInfo(filePath_origin_sjsjg);
                //if (fi_sjsjg.CreationTime.Date != dtNow.Date)
                //    throw new Exception(string.Format(@"{0} 不是当天的文件", filePath_origin_sjsjg));

                // 文件拷到修改目录
                File.Copy(filePath_origin_sjsjg, filePath_modify_sjsjg, true);

                // 修改逻辑
                using (OleDbConnection connection = new OleDbConnection(connectString))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    command.CommandText = string.Format(@"UPDATE a SET a.jgddbh=substr(a.jgddbh,1,14)+padl(b.htqz,2,'0')+substr(a.jgddbh,17) from {0} a,sz_branprefix b  WHERE a.jgywlb in ('GZCS','GZBC','GZBF','GZDQ','GZ05','GZ06') and substr(a.jgddbh,15,2)='A0' and a.jgzqzh=b.zqzh", strSJSJGFileName);

                    updateRows = command.ExecuteNonQuery();
                }

                File.Copy(filePath_modify_sjsjg, filePath_origin_sjsjg, true);   // 拷回去

                bw.ReportProgress(0, new ReportObj(filePath_origin_sjsjg, updateRows));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            e.Result = 0;

        }

        private void bwTask_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportObj ro = e.UserState as ReportObj;
            if (ro._fileNotExist)
            {
                tbLog.Text = string.Format(@"{0}：{1} 文件不存在，跳过处理.", DateTime.Now.ToString("HH:mm:ss.fff"), ro._fileName) + System.Environment.NewLine + tbLog.Text;

            }
            else
            {
                tbLog.Text = string.Format(@"{0}：{1} 处理完成，更新{2}行.", DateTime.Now.ToString("HH:mm:ss.fff"), ro._fileName, ro._rowsUpdated.ToString()) + System.Environment.NewLine + tbLog.Text;
            }
        }

        private void bwTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ToString() == "0")
                {
                    tbLog.Text = "处理完成." + System.Environment.NewLine + tbLog.Text;
                }
                else
                {
                    tbLog.Text = "有错误." + System.Environment.NewLine + tbLog.Text;
                }
            }
            else
            {
                tbLog.Text = "处理失败：" + e.Error.Message + System.Environment.NewLine + tbLog.Text;
            }

            btnExecute.Enabled = true;
            btnExecute.Text = "运行";
        }
    }
}
