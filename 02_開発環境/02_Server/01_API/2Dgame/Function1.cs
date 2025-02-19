using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Function20241101;
using MySqlConnector;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace Function20241108
{
    public static class RankingProvider
    {
        static MySqlConnectionStringBuilder connectionBuilder = new MySqlConnectionStringBuilder
        {
#if DEBUG
            Server = "localhost",
            Database = "my_db",
            UserID = "root",
            Password = "",
            //SslMode = MySqlSslMode.Required,
#else
            Server = "db-ge-202405.mysql.database.azure.com",   //Azureのサーバー名
            Database = "azure_db",  //データベースの名前
            UserID = "student",
            Password = "Yoshidajobi2024",
            SslMode = MySqlSslMode.Required,
#endif
        };

        [FunctionName("GetRanking")]
        public static async Task<IActionResult> GetRanking(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Rankings/get")] HttpRequest req,
            ILogger log)
        {
            using var conn = new MySqlConnection(connectionBuilder.ConnectionString);
            await conn.OpenAsync();
            MySqlCommand command = conn.CreateCommand();
            string responseMessage;
            bool isSuccess = int.TryParse(req.Query["id"], out int id);
            if (!isSuccess)
            {
                return new BadRequestResult();
            }
            
            command.CommandText = $"select (select count(*)+1 from hiscores where time < hi.time and stage_id = @id) as ranking,hi.time from hiscores as hi where stage_id = @id order by hi.time limit 0,5;";
                command.Parameters.AddWithValue("@id", id);

            List<Ranking> rankingList = new List<Ranking>();
            using MySqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Ranking ranking = new Ranking((long)reader[0], (int)reader[1]);
                rankingList.Add(ranking);
            }

            responseMessage = JsonConvert.SerializeObject(rankingList);
            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = responseMessage
            };
        }

        [FunctionName("AddRanking")]
        public static async Task<IActionResult> AddRanking(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Rankings/add")] HttpRequest req,
            ILogger log)
        {

            using var conn = new MySqlConnection(connectionBuilder.ConnectionString);
            await conn.OpenAsync();
            MySqlCommand command = conn.CreateCommand();
            try
            {

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody))
                {
                    return new BadRequestResult();
                }

                Ranking ranking = JsonConvert.DeserializeObject<Ranking>(requestBody);

                //command.CommandText = $"select user_id,score from hiscores where user_id = @user_id and stage_id = @id;";
                //command.Parameters.AddWithValue("@user_id", ranking.User_id);
                //command.Parameters.AddWithValue("@id", ranking.stage_id);



                //using MySqlDataReader reader = await command.ExecuteReaderAsync();
                string sql = "";

                //await reader.ReadAsync();

                sql = $"insert into hiscores (stage_id,time) values (@idStage,@score);";

                //if (await reader.ReadAsync())
                //{
                //    if ((int)reader[1] <= ranking.Score)
                //    {
                //        sql = $"update hiscores set score = @score where user_id = @update_user_id and stage_id = @idStage;";
                //    }
                //}
                //else
                //{
                //    sql = $"insert into hiscores (user_id, stage_id,score) values (@update_user_id,@idStage,@score);";
                //}
                //reader.Close();

                //if (!string.IsNullOrEmpty(sql))
                //{
                    command.CommandText = sql;
                    //command.Parameters.AddWithValue("@update_user_id", ranking.User_id);
                    command.Parameters.AddWithValue("@idStage", ranking.stage_id);
                    command.Parameters.AddWithValue("@score", ranking.Time);
                    await command.ExecuteNonQueryAsync();

                //}
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                return new BadRequestObjectResult("正しい値を入力してください");
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult("");
        }

    }
}
