
CREATE TABLE sample (id bigint NOT NULL AUTO_INCREMENT, name varchar(255), subsample_id bigint, PRIMARY KEY (id)) ;

-- Samples
insert into sample (id, name,subsample_id) values (1,'Sample1', 1);
