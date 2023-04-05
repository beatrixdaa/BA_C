using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exerchise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan Database Tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik k untuk terhubung ke database");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-GBDHL8HF/BEATRIXDEVANTI; " +
                                    "initial catalog = {0}; " +
                                    "USer ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("\nEnter your choice (1-3) : ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Mahasiswa\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                    conn.Close();
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Mahasiswa\n");
                                                    Console.WriteLine("Masukkan NIM :");
                                                    string NIM = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Mahasiswa :");
                                                    string NmaMhs = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Mahasiswa :");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan jenis kelamin (L/P) :");
                                                    string jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(NIM, NmaMhs, Almt, jk, notlpn, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda ridak memiliki " + "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                {
                                                    string nim;
                                                    Console.Clear();
                                                    Console.WriteLine("Input student data  ");
                                                    Console.WriteLine("Input NIM :");
                                                    nim = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.delete(nim, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n You dont have premission to delete data");
                                                    }
                                                }
                                                break;
                                                {
                                                    string nim, almt;
                                                    Console.Clear();
                                                    Console.WriteLine("Input student NIM that you want to change their address");
                                                    nim = Console.ReadLine();
                                                    Console.WriteLine("Input the new student Address : ");
                                                    almt = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.delete(almt, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n You dont have premission to delete data");
                                                    }

                                                }
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }

                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From HRD.Mahasiswa", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }

        public void delete(string nim, SqlConnection con)
        {
            string str = "";
            str = "delete from HRD.Mahasiswa where NIM " + " = '" + nim + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data have been deleted");
        }
    

        public void insert(string NIM, string NmaMhs, string Almt, string jk, string notlpn, SqlConnection con)
        {
            string str = "";
            str = "insert into HRD.MAHASISWA (NIM, NamaMhs, Sex, PhoneMhs)" + "values(@nim, @nma, @alamat, @JK, @Phn)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nim", NIM));
            cmd.Parameters.Add(new SqlParameter("nma", NmaMhs));
            cmd.Parameters.Add(new SqlParameter("alamat", Almt));
            cmd.Parameters.Add(new SqlParameter("JK", jk));
            cmd.Parameters.Add(new SqlParameter("Phn", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");


        }
    }
}