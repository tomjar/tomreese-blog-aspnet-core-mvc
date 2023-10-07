using System.Data;
using Microsoft.Data.Sqlite;

namespace TRBlog.Database;

public partial class Blog
{
    public long Id { get; set; }

    public string? Header { get; set; }

    public DateTime Createtimestamp { get; set; }

    public DateTime? Modifytimestamp { get; set; }

    public bool Ispublished { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? Body { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<int> DeleteBlogPermanently(long id)
    {
        // const string query = sqliteDb.query(`DELETE FROM Blog WHERE id = ?1;`);
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Blog WHERE id = ?1;";
                cmd.Parameters.AddWithValue("?1", id);
                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return 1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ispublished"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<int> UpdateBlogPublished(bool ispublished, long id)
    {
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Blog SET ispublished=?1 WHERE id = ?2;";
                cmd.Parameters.AddWithValue("?1", ispublished);
                cmd.Parameters.AddWithValue("?2", id);
                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return 1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<int> UpdateBlogModifiedTimestamp(long id)
    {
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Blog SET modifytimestamp=?1 WHERE id = ?2;";
                cmd.Parameters.AddWithValue("?1", DateTime.UnixEpoch.Millisecond);
                cmd.Parameters.AddWithValue("?2", id);
                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return 1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="category"></param>
    /// <param name="header"></param>
    /// <param name="ispublished"></param>
    /// <param name="description"></param>
    /// <param name="body"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<int> UpdateBlog(string category, string header, bool ispublished, string description, string body, long id)
    {
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Blog SET category=?1, header=?2, modifytimestamp=?3, ispublished=?4, description=?5, body=?6 WHERE id = ?7;";
                cmd.Parameters.AddWithValue("?1", category);
                cmd.Parameters.AddWithValue("?2", header);
                cmd.Parameters.AddWithValue("?3", DateTime.UnixEpoch.Millisecond);
                cmd.Parameters.AddWithValue("?4", ispublished);
                cmd.Parameters.AddWithValue("?5", description);
                cmd.Parameters.AddWithValue("?6", body);
                cmd.Parameters.AddWithValue("?7", id);
                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return 1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="header"></param>
    /// <param name="description"></param>
    /// <param name="name"></param>
    /// <param name="category"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static async Task<int> InsertBlog(string header, string description, string name, string category, string body)
    {
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Blog(header, createtimestamp, modifytimestamp, ispublished, description, name, category, body) VALUES (?1, ?2, NULL, false, ?3, ?4, ?5, ?6);";
                cmd.Parameters.AddWithValue("?1", header);
                cmd.Parameters.AddWithValue("?2", DateTime.UnixEpoch.Millisecond);
                cmd.Parameters.AddWithValue("?3", description);
                cmd.Parameters.AddWithValue("?4", name);
                cmd.Parameters.AddWithValue("?5", category);
                cmd.Parameters.AddWithValue("?6", body);
                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return 1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<Blog> GetBlogById(long id)
    {
        Blog blog = new Blog();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, header, createtimestamp, modifytimestamp, ispublished, description, name, category, body FROM Blog WHERE id=?1;";
                cmd.Parameters.AddWithValue("?1", id);
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        blog.Id = reader.GetInt64("id");
                        blog.Header = reader.GetString("header");
                        blog.Createtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("createtimestamp")).DateTime;
                        blog.Modifytimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("modifytimestamp")).DateTime;
                        blog.Ispublished = reader.GetBoolean("ispublished");
                        blog.Description = reader.GetString("description");
                        blog.Name = reader.GetString("name");
                        blog.Category = reader.GetString("category");
                        blog.Body = reader.GetString("body");
                    }
                }
                await con.CloseAsync();
                return blog;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static async Task<Blog> GetBlogByName(string name)
    {
        Blog blog = new Blog();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, header,createtimestamp,modifytimestamp, ispublished, description, name, category, body FROM Blog WHERE name = ?1 LIMIT 1;";
                cmd.Parameters.AddWithValue("?1", name);
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        blog.Id = reader.GetInt64("id");
                        blog.Header = reader.GetString("header");
                        blog.Createtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("createtimestamp")).DateTime;
                        blog.Modifytimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("modifytimestamp")).DateTime;
                        blog.Ispublished = reader.GetBoolean("ispublished");
                        blog.Description = reader.GetString("description");
                        blog.Name = reader.GetString("name");
                        blog.Category = reader.GetString("category");
                        blog.Body = reader.GetString("body");

                    }
                }
                await con.CloseAsync();
                return blog;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static async Task<Blog[]> GetAll()
    {
        List<Blog> blogs = new List<Blog>();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, header, createtimestamp, modifytimestamp, ispublished, description, name, category, body FROM Blog ORDER BY createtimestamp DESC;";
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        blogs.Add(new Blog()
                        {
                            Id = reader.GetInt64("id"),
                            Header = reader.GetString("header"),
                            Createtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("createtimestamp")).DateTime,
                            Modifytimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("modifytimestamp")).DateTime,
                            Ispublished = reader.GetBoolean("ispublished"),
                            Description = reader.GetString("description"),
                            Name = reader.GetString("name"),
                            Category = reader.GetString("category"),
                            Body = reader.GetString("body")
                        });

                    }
                }
                await con.CloseAsync();
                return blogs.ToArray();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static async Task<Blog[]> GetAllPublished()
    {
        List<Blog> blogs = new List<Blog>();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, header, createtimestamp, modifytimestamp, ispublished, description, name, category FROM Blog WHERE ispublished = true ORDER BY createtimestamp DESC;";
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        blogs.Add(new Blog()
                        {
                            Id = reader.GetInt64("id"),
                            Header = reader.GetString("header"),
                            Createtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("createtimestamp")).DateTime,
                            Modifytimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("modifytimestamp")).DateTime,
                            Ispublished = reader.GetBoolean("ispublished"),
                            Description = reader.GetString("description"),
                            Name = reader.GetString("name"),
                            Category = reader.GetString("category"),
                            Body = reader.GetString("body")
                        });

                    }
                }
                await con.CloseAsync();
                return blogs.ToArray();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static async Task<Blog[]> GetAllPublishedLastThirtyDays()
    {
        List<Blog> blogs = new List<Blog>();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, header, createtimestamp, modifytimestamp, ispublished, description, name, category, body FROM Blog WHERE createtimestamp >= ?1 ORDER BY createtimestamp DESC;";
                cmd.Parameters.AddWithValue("?1", DateTimeOffset.Now.AddDays(-30).ToUnixTimeMilliseconds());
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        blogs.Add(new Blog()
                        {
                            Id = reader.GetInt64("id"),
                            Header = reader.GetString("header"),
                            Createtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("createtimestamp")).DateTime,
                            Modifytimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("modifytimestamp")).DateTime,
                            Ispublished = reader.GetBoolean("ispublished"),
                            Description = reader.GetString("description"),
                            Name = reader.GetString("name"),
                            Category = reader.GetString("category"),
                            Body = reader.GetString("body")
                        });

                    }
                }
                await con.CloseAsync();
                return blogs.ToArray();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static async Task<Dictionary<int, Blog[]>> GetAllArchived()
    {
        List<Blog> blogs = new List<Blog>();
        Dictionary<int, Blog[]> yearAndBlogs = new Dictionary<int, Blog[]>();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, header, createtimestamp, modifytimestamp, ispublished, description, name, category, body FROM Blog WHERE ispublished=true ORDER BY createtimestamp DESC;";
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        blogs.Add(new Blog()
                        {
                            Id = reader.GetInt64("id"),
                            Header = reader.GetString("header"),
                            Createtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("createtimestamp")).DateTime,
                            Modifytimestamp = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64("modifytimestamp")).DateTime,
                            Ispublished = reader.GetBoolean("ispublished"),
                            Description = reader.GetString("description"),
                            Name = reader.GetString("name"),
                            Category = reader.GetString("category"),
                            Body = reader.GetString("body")
                        });

                    }
                }
                await con.CloseAsync();
            }
        }

        var years = blogs
            .GroupBy(b => b.Createtimestamp.Year)
            .Select(b => b.Key)
            .ToArray();

        foreach (var year in years)
        {
            yearAndBlogs.Add(year, blogs.Where(b => b.Createtimestamp.Year == year).ToArray());
        }
        return yearAndBlogs;
    }
}
