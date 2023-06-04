using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahoaAES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public String[,] cypherText = new String[4, 4];
        public String[,] plainText = new String[4, 4];
        public String[,] khoa = new String[4, 4];
        public String[,] khoa1 = new String[4, 4];
        public String[,] khoa2 = new String[4, 4];
        public String[,] khoa3 = new String[4, 4];
        public String[,] khoa4 = new String[4, 4];
        public String[,] khoa5 = new String[4, 4];
        public String[,] khoa6 = new String[4, 4];
        public String[,] khoa7 = new String[4, 4];
        public String[,] khoa8 = new String[4, 4];
        public String[,] khoa9 = new String[4, 4];
        public String[,] khoa10 = new String[4, 4];
        public String[,] addRoundKey = new String[4, 4];
        public String[,] subBytes = new String[4, 4];
        public String[,] shiftRows = new String[4, 4];
        public String[,] mixColumns = new String[4, 4];
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtBR.Clear();
            txtBM.Clear();
            txtKhoaK.Clear();
            KQ.Clear();
            txtBR.Focus();
        }

        private void btnMaHoa_Click(object sender, EventArgs e)
        {
            KQ.Text = "";
            input();
            tao10khoa();
            AddRoundKey();

            for (int i = 1; i <= 9; i++)
            {
                KQ.Text += "Vòng lặp thứ " + i + " : " + "\n";
                SubBytes();
                ShiftRows();
                MixColumns();
                if (i == 1) AddRoundKeyvonglap(mixColumns, khoa1);
                else if (i == 2) AddRoundKeyvonglap(mixColumns, khoa2);
                else if (i == 3) AddRoundKeyvonglap(mixColumns, khoa3);
                else if (i == 4) AddRoundKeyvonglap(mixColumns, khoa4);
                else if (i == 5) AddRoundKeyvonglap(mixColumns, khoa5);
                else if (i == 6) AddRoundKeyvonglap(mixColumns, khoa6);
                else if (i == 7) AddRoundKeyvonglap(mixColumns, khoa7);
                else if (i == 8) AddRoundKeyvonglap(mixColumns, khoa8);
                else if (i == 9) AddRoundKeyvonglap(mixColumns, khoa9);

            }
            KQ.Text += "Bước tạo ngõ ra : " + "\n";
            SubBytes();
            ShiftRows();
            KQ.Text += "Bản mã :\n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    addRoundKey[i, j] = Aes.XOR16(shiftRows[i, j], khoa10[i, j]);
                    if (j == 3) KQ.Text += "  " + addRoundKey[i, j] + "\n";
                    else KQ.Text += "  " + addRoundKey[i, j] + " ";

                }
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    txtBM.Text += addRoundKey[i, j] + " ";
                }
        }
        public void input()
        {
            String[] plainTextStr = txtBR.Text.Split(' ');
            String[] key = txtKhoaK.Text.Split(' ');
            int i = 0;
            int j = 0;
            foreach (String w in plainTextStr)
            {
                plainText[i, j] = w;
                i++;
                if (i > 3 && j != 3)
                {
                    i = 0;
                    j++;
                }

            }
            int a = 0;
            int b = 0;
            foreach (String w in key)
            {
                khoa[a, b] = w;
                a++;
                if (a > 3 && b != 3)
                {
                    a = 0;
                    b++;
                }

            }

        }
        public void xuatmatran(String[,] matran)
        {
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {

                    if (j == 3) KQ.Text += "  " + matran[i, j] + "\n";
                    else KQ.Text += "  " + matran[i, j] + " ";

                }
        }
        private void AddRoundKey()
        {
            KQ.Text += "AddRoundKey: \n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    addRoundKey[i, j] = Aes.XOR16(plainText[i, j], khoa[i, j]);
                    if (j == 3) KQ.Text += "  " + addRoundKey[i, j] + "\n";
                    else KQ.Text += "  " + addRoundKey[i, j] + " ";
                }
        }
        private void AddRoundKeyvonglap(String[,] kqdr, String[,] khoamoi)
        {
            KQ.Text += "AddRoundKey (input đầu ra của vòng lặp kết hợp với Khóa mới): \n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    addRoundKey[i, j] = Aes.XOR16(kqdr[i, j], khoamoi[i, j]);
                    if (j == 3) KQ.Text += "  " + addRoundKey[i, j] + "\n";
                    else KQ.Text += "  " + addRoundKey[i, j] + " ";

                }
        }
        private void SubBytes()
        {
            KQ.Text += "SubBytes : \n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    String ark = addRoundKey[i, j];
                    subBytes[i, j] = Aes.sbox[Aes.chuyen16sangso(ark[0]), Aes.chuyen16sangso(ark[1])];
                    if (j == 3) KQ.Text += "  " + subBytes[i, j] + "\n";
                    else KQ.Text += "  " + subBytes[i, j] + " ";
                }
        }
        public void ShiftRows()
        {
            String teap;
            teap = subBytes[1, 0];
            subBytes[1, 0] = subBytes[1, 3];
            subBytes[1, 3] = teap;
            teap = subBytes[1, 0];
            subBytes[1, 0] = subBytes[1, 2];
            subBytes[1, 2] = teap;
            teap = subBytes[1, 0];
            subBytes[1, 0] = subBytes[1, 1];
            subBytes[1, 1] = teap;

            teap = subBytes[2, 0];
            subBytes[2, 0] = subBytes[2, 2];
            subBytes[2, 2] = teap;
            teap = subBytes[2, 1];
            subBytes[2, 1] = subBytes[2, 3];
            subBytes[2, 3] = teap;

            teap = subBytes[3, 0];
            subBytes[3, 0] = subBytes[3, 3];
            subBytes[3, 3] = teap;
            teap = subBytes[3, 1];
            subBytes[3, 1] = subBytes[3, 3];
            subBytes[3, 3] = teap;
            teap = subBytes[3, 2];
            subBytes[3, 2] = subBytes[3, 3];
            subBytes[3, 3] = teap;

            shiftRows = subBytes;
            KQ.Text += " ShiftRows : \n";
            xuatmatran(shiftRows);
        }
        public void MixColumns()
        {
            for (int j = 0; j <= 3; j++)
            {
                String[] lay1cot = new String[4];
                for (int i = 0; i <= 3; i++)
                {
                    lay1cot[i] = shiftRows[i, j];
                }
                for (int k = 0; k <= 3; k++)
                {
                    String[] kqsaubangbd = new String[4];
                    for (int q = 0; q <= 3; q++)
                    {
                        kqsaubangbd[q] = Aes.nhanbangbd(lay1cot[q], Aes.matranbd[k, q]);
                    }
                    mixColumns[k, j] = Aes.XOR16voi4kytu(kqsaubangbd[0], kqsaubangbd[1], kqsaubangbd[2], kqsaubangbd[3]);
                }
            }
            KQ.Text += " MixColumns: \n";
            xuatmatran(mixColumns);
        }
        public void tao10khoa()
        {
            KQ.Text += "Tính khóa : " + "\n";
            for (int i = 1; i <= 10; i++)
            {
                String[] R_con = new String[4];
                R_con = Aes.layRcon(i);
                if (i == 1)
                {
                    khoa1 = Aes.tinhkhoa(R_con, khoa);
                    KQ.Text += "Khóa 1 " + "\n";
                    xuatmatran(khoa1);
                }
                else if (i == 2)
                {
                    khoa2 = Aes.tinhkhoa(R_con, khoa1);
                    KQ.Text += "Khóa 2 " + "\n";
                    xuatmatran(khoa2);
                }
                else if (i == 3)
                {
                    khoa3 = Aes.tinhkhoa(R_con, khoa2);
                    KQ.Text += "Khóa 3 " + "\n";
                    xuatmatran(khoa3);
                }
                else if (i == 4)
                {
                    khoa4 = Aes.tinhkhoa(R_con, khoa3);
                    KQ.Text += "Khóa 4 " + "\n";
                    xuatmatran(khoa4);
                }
                else if (i == 5)
                {
                    khoa5 = Aes.tinhkhoa(R_con, khoa4);
                    KQ.Text += "Khóa 5 " + "\n";
                    xuatmatran(khoa5);
                }
                else if (i == 6)
                {
                    khoa6 = Aes.tinhkhoa(R_con, khoa5);
                    KQ.Text += "Khóa 6 " + "\n";
                    xuatmatran(khoa6);
                }
                else if (i == 7)
                {
                    khoa7 = Aes.tinhkhoa(R_con, khoa6);
                    KQ.Text += "Khóa 7 " + "\n";
                    xuatmatran(khoa7);
                }
                else if (i == 8)
                {
                    khoa8 = Aes.tinhkhoa(R_con, khoa7);
                    KQ.Text += "Khóa 8 " + "\n";
                    xuatmatran(khoa8);
                }
                else if (i == 9)
                {
                    khoa9 = Aes.tinhkhoa(R_con, khoa8);
                    KQ.Text += "Khóa 9 " + "\n";
                    xuatmatran(khoa9);
                }
                else if (i == 10)
                {
                    khoa10 = Aes.tinhkhoa(R_con, khoa9);
                    KQ.Text += "Khóa 10 " + "\n";
                    xuatmatran(khoa10);
                }
            }
        }
    }
}
