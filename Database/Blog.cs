using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using TRBlog.Database;

namespace TRBlog.Database;

public partial class Blog
{
    public long Id { get; set; }

    public string? Header { get; set; }

    public DateTime Createtimestamp { get; set; }

    public DateTime Modifytimestamp { get; set; }

    public int Ispublished { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? Body { get; set; }

    public async Task<int> DeleteBlogPermanently(long id)
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
                return 1;
            }
        }
    }


    public Task<EntityEntry> UpdateBlogPublished(bool ispublished, long id)
    {
        // const query = sqliteDb.query(`UPDATE Blog SET ispublished=?1 WHERE id = ?3;`);
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Task<EntityEntry>(() =>
        // {
        //     var blog = Blogs.First(f => f.Id == id);
        //     blog.Ispublished = ispublished;
        //     return Blogs.Update(blog);
        // });
    }


    public Task<int> UpdateBlogModifiedTimestamp(long id)
    {
        // const query = sqliteDb.query(`UPDATE Blog SET modifytimestamp=?1 WHERE id = ?2;`);
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Task<EntityEntry>(() =>
        // {
        //     var blog = Blogs.First(f => f.Id == id);
        //     blog.Modifytimestamp = DateTime.UnixEpoch;
        //     return Blogs.Update(blog);
        // });
    }


    public Task<int> UpdateBlog(string category, string header, bool ispublished, string description, string body, long id)
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }

        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`UPDATE Blog SET category=?1, header=?2, modifytimestamp=?3, ispublished=?4, description=?5, body=?6 WHERE id = ?7;`);
        //     const result = query.all(category, header, Date.now(), ispublished, description, body, id);
        //     sqliteDb.close();
        //     response(result);
        // });
    }


    public Task<int> InsertBlog(string header, string description, string name, string category, string body)
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`INSERT INTO Blog(header, createtimestamp, modifytimestamp, ispublished, description, name, category, body) VALUES (?1, ?2, NULL, false, ?3, ?4, ?5, ?6);`);
        //     const result = query.all(header, Date.now(), description, name, category, body);
        //     sqliteDb.close();
        //     response(result);
        // });
    }

    /**
     * 
     * @param {Number} id 
     * @returns {Promise<any>}
     */
    public Task<Blog> GetBlogById(long id)
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`SELECT id, header, date(createtimestamp/ 1000, 'unixepoch') as 'createtimestamp',date(modifytimestamp/ 1000, 'unixepoch') as 'modifytimestamp', ispublished, description, name, category, body FROM Blog WHERE id=?1;`);
        //     const result = query.all(id);
        //     sqliteDb.close();

        //     if (result && result.length > 0) {
        //         response(result[0]);
        //     } else {
        //         response(null);
        //     }
        // });
    }


    public Task<Blog> GetBlogByName(string name)
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`SELECT id, header,date(createtimestamp/ 1000, 'unixepoch') as 'createtimestamp',date(modifytimestamp/ 1000, 'unixepoch') as 'modifytimestamp', ispublished, description, name, category, body FROM Blog WHERE name = ?1 LIMIT 1`);
        //     const result = query.all(name);
        //     sqliteDb.close();

        //     if (result && response.length > 0) {
        //         response(result[0]);
        //     } else {
        //         response(null);
        //     }
        // });
    }


    public Task<Blog[]> GetAll()
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`SELECT id, header, date(createtimestamp/ 1000, 'unixepoch') as 'createtimestamp',date(modifytimestamp/ 1000, 'unixepoch') as 'modifytimestamp', ispublished, description, name, category, body FROM Blog ORDER BY createtimestamp DESC;`);
        //     const result = query.all();
        //     sqliteDb.close();
        //     response(result);
        // });
    }


    public Task<Blog[]> GetAllPublished()
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`SELECT id, header, date(createtimestamp/ 1000, 'unixepoch') as 'createtimestamp',date(modifytimestamp/ 1000, 'unixepoch') as 'modifytimestamp', ispublished, description, name, category FROM Blog WHERE ispublished = true ORDER BY createtimestamp DESC;`);
        //     const result = query.all();
        //     sqliteDb.close();
        //     response(result);
        // })
    }


    public Task<Blog[]> GetAllPublishedLastThirtyDays()
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const today = new Date();
        //     const query = sqliteDb.query(`SELECT * FROM Blog WHERE createtimestamp >= ?1`);
        //     const result = query.all(new Date().setDate(today.getDate() - 30));
        //     sqliteDb.close();
        //     if(response && response.length > 0){
        //         response(result);
        //     }else{
        //         response([]);
        //     }
        // });
    }


    public Task<Blog[]> GetAllArchived()
    {
        using (var connection = new SqliteConnection())
        {
            using (var cmd = new SqliteCommand(stm, connection))
            {
                return cmd.ExecuteNonQueryAsync();
            }
        }
        // return new Promise((response, reject) => {
        //     const sqliteDb = new Database("tomreeseblog.sqlite");
        //     const query = sqliteDb.query(`SELECT id, header,date(createtimestamp/ 1000, 'unixepoch') as 'createtimestamp',date(modifytimestamp/ 1000, 'unixepoch') as 'modifytimestamp', ispublished, description, name, category, body FROM Blog ORDER BY createtimestamp desc;`);
        //     const result = query.all();
        //     sqliteDb.close();

        //     if (result && result.length > 0) {

        //         const uniqueYears = result.map(item => {
        //             return new Date(item.createtimestamp).getFullYear();
        //         }).filter((item, index, arr) => {
        //             return arr.indexOf(item) === index;
        //         });

        //         const yearAndBlogs = uniqueYears.map(uy => {
        //             return {
        //                 'year': uy,
        //                 'blogs': result.filter(p => {
        //                     return new Date(p.createtimestamp).getFullYear() === uy;
        //                 })
        //             }
        //         });

        //         response(yearAndBlogs);
        //     } else {
        //         response({
        //             'year': 1970,
        //             'blogs': []
        //         });
        //     }
        // });

    }
}
