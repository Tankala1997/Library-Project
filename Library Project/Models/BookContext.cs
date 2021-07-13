using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Library_Project.Models
{
    public class BookContext
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=MVC_PROJECTS;Integrated Security=true");

        public List<Library_Project.Models.Book> GetBooks()
        {
            Book bk = GetUploadImage(1);
            List<Book> books = new List<Book>();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("usp_GetBooks", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Book b = new Book();
                    b.BookId = Convert.ToInt32(dr["BookId"]);
                    b.BookName = Convert.ToString(dr["BookName"]);
                    b.BookDescription = Convert.ToString(dr["BookDescription"]);
                    
                    if (!Convert.IsDBNull(dr["ImageData"]))
                   
                    {
                        b.BookImage = (byte[])dr["ImageData"];
                    }
                    else
                    {
                       
                        b.BookImage = bk.BookImage;
                    }

                    //MemoryStream ms = new MemoryStream(b.BookImage);
                    //b.BookImage = Image.FromStream(ms);
                    books.Add(b);
                }


            }

            catch (Exception ex)
            {

            }
            finally
            {

                con.Close();
            }
            return books;

        }
        public Library_Project.Models.Book GetUploadImage(int? a)
        {
            Book b = new Book();
            con.Open();
            try
            {
                SqlCommand cmd = null;
                if (a==null)
                {
                     cmd = new SqlCommand("usp_GetUploadImage", con);
                    cmd.Parameters.AddWithValue("@Bookid", 1);
                }
                else
                {
                     cmd = new SqlCommand("usp_GetUploadImage", con);
                    cmd.Parameters.AddWithValue("@Bookid", a);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                 
                    if (!Convert.IsDBNull(dr["ImageData"]))

                    {
                        b.BookImage = (byte[])dr["ImageData"];
                    }
                   
                   
                }


            }

            catch (Exception ex)
            {

            }
            finally
            {

                con.Close();
            }
            return b;

        }
        public Library_Project.Models.Book GetBookById(int? id)
        {
            Book b = new Book();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("usp_GetBookById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    //Book b = new Book();
                    b.BookId = Convert.ToInt32(dr["BookId"]);
                    b.BookName = Convert.ToString(dr["BookName"]);
                    b.BookDescription = Convert.ToString(dr["BookDescription"]);

                }


            }

            catch (Exception ex)
            {

            }
            finally
            {

                con.Close();
            }
            return b;

        }
        public List<Book> GetBookByName(string BookName)
        {
            Book bk = GetUploadImage(1);
            List<Book> books = new List<Book>();
            con.Open();
            try
            {
                //SqlCommand cmd = new SqlCommand("select BookId,BookName,BookDescription from Book where BookName like ('%"+BookName+"%')", con);
                SqlCommand cmd = new SqlCommand(@"select b.BookId,b.BookName,b.BookDescription,bi.ImageData from Book b,BookImages bi where   bi.BookId=b.BookId and  b.BookName like ('%" + BookName + "%')", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Book b = new Book();
                    b.BookId = Convert.ToInt32(dr["BookId"]);
                    b.BookName = Convert.ToString(dr["BookName"]);
                    b.BookDescription = Convert.ToString(dr["BookDescription"]);

                    if (!Convert.IsDBNull(dr["ImageData"]))

                    {
                        b.BookImage = (byte[])dr["ImageData"];
                    }
                    else
                    {

                        b.BookImage = bk.BookImage;
                    }

                    //MemoryStream ms = new MemoryStream(b.BookImage);
                    //b.BookImage = Image.FromStream(ms);
                    books.Add(b);
                }
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BookName", BookName);
                //using (var reader = cmd.ExecuteReader())
                //{
                //    if (reader.Read())
                //    {
                //        Book b = new Book();
                //        b.BookId = reader.GetInt32(0);
                //        b.BookName = reader.GetString(1);
                //        b.BookDescription = reader.GetString(2);

                //        books.Add(b);
                //    }
                //}



            }

            catch (Exception ex)
            {

            }
            finally
            {

                con.Close();
            }
            return books;

        }
        public int SaveBook(Book b)
        {
            int val = 0;
            con.Open();

            SqlCommand cmd = null;
            try
            {

                if (b.BookId == 0)
                {

                    cmd = new SqlCommand("usp_SaveBook", con);
                }
                else
                {
                    cmd = new SqlCommand("usp_UpdateBook", con);
                    cmd.Parameters.AddWithValue("@BookId", b.BookId);
                }
                cmd.Parameters.AddWithValue("@BookName", b.BookName);
                cmd.Parameters.AddWithValue("@BookDescription", b.BookDescription);
                cmd.CommandType = CommandType.StoredProcedure;
                val = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {

                con.Close();
            }
            return val;

        }
        public int DeleteBook(Book b)
        {
            int val = 0;
            con.Open();

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("usp_DeleteBook", con);
                cmd.Parameters.AddWithValue("@BookId", b.BookId);
                cmd.CommandType = CommandType.StoredProcedure;
                val = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {

                con.Close();
            }
            return val;

        }

        public int FileUpload(int BookId,byte[] data)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_UploadImage",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId",BookId);
            cmd.Parameters.AddWithValue("@ImageData", data);
            int i=cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

    }
}