using Microsoft.Data.Sqlite;
using System.Data;

namespace TRBlog.Database;

public partial class Setting
{
    public long Id { get; set; }

    public string? ArchiveView { get; set; }

    public string? AboutSection { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="archiveView"></param>
    /// <param name="aboutSection"></param>
    /// <returns></returns>
    public static async Task<int> UpdateSettings(string archiveView, string aboutSection)
    {

        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Setting SET archive_view=?1, about_section=?2;";
                cmd.Parameters.AddWithValue("?1", archiveView);
                cmd.Parameters.AddWithValue("?2", aboutSection);
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
    /// <returns></returns>
    public static async Task<Setting> GetSetting()
    {
        Setting setting = new Setting();
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id, archive_view, about_section FROM Setting LIMIT 1;";
                await cmd.PrepareAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        setting.AboutSection = reader.GetString("about_section");
                        setting.ArchiveView = reader.GetString("archive_view");
                        setting.Id = reader.GetInt64("id");
                    }

                    await con.CloseAsync();
                    return setting;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static async Task<int> InsertDefaultSettings()
    {
        using (var con = new SqliteConnection("FileName=tomreeseblog.sqlite"))
        {
            await con.OpenAsync();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Setting(archive_view, about_section) VALUES ( ?1, ?2);";
                cmd.Parameters.AddWithValue("?1", "date");
                cmd.Parameters.AddWithValue("?2", "fill out your about section in the admin settings page.");
                await cmd.PrepareAsync();
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return 1;
            }
        }
    }
}
