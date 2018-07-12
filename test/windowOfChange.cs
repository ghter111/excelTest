using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace test
{
    public partial class windowOfChange : Form
    {
        public windowOfChange()
        {
            InitializeComponent();
        }
        public string globalPath = "";
        public void button1_Click(object sender, EventArgs e)
        {
            var pathTemp = globalPath;
            var unionReslt = new ArrayList();
            var readLists = new List<codeDate>();
            foreach (var item in listBox1.Items)
            {
                var path = pathTemp + '\\' + item;
                using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("gb2312")))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        readLists.Add(new codeDate
                        {
                            code = (s.Split(','))[0],
                            datas = s,
                        });
                    }
                }
            }
            var readListByCode = readLists.GroupBy(o => o.code);
            foreach (var codes in readListByCode)
            {
                var result = "";
                foreach (var code in codes)
                {
                    result += code.datas + ",";
                }
                unionReslt.Add(result);
            }

            var outPath = pathTemp + "\\T0" + ".csv";
            SaveCSV(unionReslt, outPath);

            MessageBox.Show("已计算完毕，请到源数据目录下查看收益表");
        }
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //按住shift可以实现多选
            listBox1.SelectionMode = SelectionMode.MultiExtended;

        }

        public static bool SaveCSV(ArrayList DataList, string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            bool re = true;
            try
            {
                FileStream FileStream = new FileStream(fullPath, FileMode.Append);
                StreamWriter sw = new StreamWriter(FileStream, System.Text.Encoding.UTF8);
                foreach (var Data in DataList)
                {
                    sw.WriteLine(Data);
                }
                //清空緩沖區
                sw.Flush();
                //關閉流
                sw.Close();
                FileStream.Close();
            }
            catch (Exception e)
            {
                var x = e;
                re = false;
            }
            return re;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] files = fileDialog.FileNames;
                var path = files[0].Split('\\');
                for (var i = 0; i < path.Length - 1; i++)
                {
                    if (i != path.Length - 2)
                    {
                        globalPath += path[i] + '\\';
                    }
                    else
                    {
                        globalPath += path[i];
                    }
                }
                var fileNames = new ArrayList();
                listBox1.Items.Clear();
                foreach (var file in files)
                {
                    var paths = file.Split('\\');
                    listBox1.Items.Add(paths[paths.Length - 1]);
                }

            }
        }
    }
}
