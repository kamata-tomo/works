use azure_db;

show tables;
#テーブル作成
create table hiscores(
                    user_id int auto_increment,
                    stage_id int,
                    time int,
                    created_at datetime default current_timestamp,
                    updated_at datetime default current_timestamp on update current_timestamp,
                    primary key(user_id, stage_id)
);

#スコア更新
insert into hiscores (stage_id,time) values (@idStage,@score);

#ランキング表示
select (select count(*)+1 from hiscores where time < hi.time and stage_id = @id) as ranking,hi.time from hiscores as hi where stage_id = @id order by hi.time limit 0,5;

